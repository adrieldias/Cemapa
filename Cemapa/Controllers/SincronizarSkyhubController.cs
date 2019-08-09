using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Cemapa.Models;
using System.Net.Mail;

namespace Cemapa.Controllers
{
    public class SincronizarSkyhubController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        public async Task<HttpResponseMessage> DownloadEtiqueta(int codFilial, string codMarketplace)
        {
            //Este método buscar por PLPs, que nada mais são do que agrupamentos de pedidos, para imprimir em etiquetas.
            //Problema que a forma de acessar e imprimir etiquetas pode precisar de algumas chamadas.
            
            bool wAchouPlp = false;
            string wCodigoPlp = null;
            int wMaximoIteracoes = 10;

            try
            {
                if (codFilial == 0)
                {
                    throw new Exception("Informe a filial");
                }

                if (codMarketplace == "")
                {
                    throw new Exception("Código do pedido do marketplace inválido");
                }
                
                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                if (configuracaoSkyhub != null)
                {
                    //Primeira requisição, faz uma chamada para verificar se uma PLP ja existe.

                    HttpClient Http = new HttpClient
                    {
                        BaseAddress = new Uri("https://api.skyhub.com.br")
                    };

                    Http.DefaultRequestHeaders.Accept.Clear();
                    
                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);

                    Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");
                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response;

                    while (!wAchouPlp || wMaximoIteracoes <= 0)
                    {
                        response = await Http.GetAsync($"shipments/b2w");

                        if (response.IsSuccessStatusCode)
                        {
                            //Caso encontre a PLP correspondente ao código do pedido solicitado,
                            //então copia o código da PLP para imprimir em seguida.

                            PLPs plps = await response.Content.ReadAsAsync<PLPs>();

                            foreach (Plp plp in plps.plp)
                            {
                                foreach (OrderPLP ordem in plp.orders)
                                {
                                    if (ordem.code == codMarketplace)
                                    {
                                        wAchouPlp = true;
                                        wCodigoPlp = plp.id;
                                    }
                                }
                            }

                            if (!wAchouPlp)
                            {
                                //Caso não encontre a PLP com o código do pedido solicitado, então é necessário
                                //realizar o agrupamento, para depois novamente busca-la.

                                List<string> codigos = new List<string> { codMarketplace };
                                Dictionary<string, List<string>> agrupamentos = new Dictionary<string, List<string>> { { "order_remote_codes", codigos } };
                                
                                response = await Http.PostAsJsonAsync($"shipments/b2w", agrupamentos);

                                if (!response.IsSuccessStatusCode)
                                {
                                    //Caso a skyhub já encontre alguma PLP existente para esse pedido, então ele desagrupa e
                                    //retorna um erro, porém na próxima chamada existe a certeza de que o pedido será agrupado
                                    //em uma nova PLP.

                                    response = await Http.PostAsJsonAsync($"shipments/b2w", agrupamentos);

                                    if (!response.IsSuccessStatusCode)
                                    {
                                        ErrorPLP body = new ErrorPLP();
                                        body = await response.Content.ReadAsAsync<ErrorPLP>();

                                        throw new Exception($"Não foi possível agrupar pedido em uma PLP. {codMarketplace}: {response.ReasonPhrase}. Conteúdo: {body.message}");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Error body = new Error();
                            body = await response.Content.ReadAsAsync<Error>();

                            throw new Exception($"Não foi possível buscar PLPs na skyhub. {codMarketplace}: {response.ReasonPhrase}. Conteúdo: {body.error}");
                        }

                        //Variavel controla numero máximo de iterações do loop, para não resultar em uma chamada sem fim.

                        wMaximoIteracoes--;
                    }

                    if (!wAchouPlp && wMaximoIteracoes <= 0)
                    {
                        //Caso fez varias buscas e não encontrou nada, encerra e drop o erro.
                        //Se acontecer esse caso, é provavel que exista uma possibilidade não prevista sobre gerar as etiquetas.

                        throw new Exception($"Não foi possível procurar PLP na skyhub. {codMarketplace}: Tentou procurar a PLP por muito tempo, e não encontrou nada");
                    }

                    //Finalmente recupera a etiqueta, enviando o código da PLP encontrada anteriormente.

                    Http.DefaultRequestHeaders.Clear();
                    Http.DefaultRequestHeaders.Accept.Clear();
                    
                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                    
                    Http.DefaultRequestHeaders.Add("Accept", "application/pdf;charset=UTF-8");
                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));

                    response = await Http.GetAsync($"shipments/b2w/view?plp_id={wCodigoPlp}");

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Não foi possível baixar etiqueta. {codMarketplace}: {response.ReasonPhrase}. Conteúdo: {response.Content.ReadAsStringAsync()}");
                    }

                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = response.Content
                    };
                }
                else
                {
                    throw new Exception($"Filial não encontrada: {codFilial}");
                }
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Erro: {ResolucaoExcecoes.ErroAprofundado(except)}"
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> SincronizaStatusPedido(int codFilial, string codMarketplace)
        {
            //Busca pelo pedido na skyhub, e atualiza o status para o sistema.
            //Esse método deve acontecer automatico quando ocorrem os sincronismos.
            //É uma segunda opção caso algum dia não esteja sincronizando devido a algum problema.

            try
            {
                if (codFilial == 0)
                {
                    throw new Exception("Informe a filial");
                }

                if (codMarketplace == "")
                {
                    throw new Exception("Código do pedido do marketplace inválido");
                }

                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                if (configuracaoSkyhub != null)
                {
                    HttpClient Http = new HttpClient
                    {
                        BaseAddress = new Uri("https://api.skyhub.com.br")
                    };

                    Http.DefaultRequestHeaders.Accept.Clear();
                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                    Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    HttpResponseMessage response = await Http.GetAsync($"/orders/{codMarketplace}");

                    Order ordem = new Order();
                    ordem = await response.Content.ReadAsAsync<Order>();

                    if (response.IsSuccessStatusCode)
                    {
                        if (ordem != null)
                        {
                            //Se houve uma resposta de sucesso, atualiza o campo DESC_SITUACAO_SKYHUB na tabela TB_PEDIDO_CAB

                            TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                                     where (p.COD_PEDIDO_MARKETPLACE == codMarketplace)
                                                     select p).FirstOrDefault();
                            if (wPedido != null)
                            {
                                wPedido.DESC_SITUACAO_MARKETPLACE = ordem.status.type;
                                db.Entry(wPedido).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                throw new Exception($"Erro ao atualizar pedido no sistema. Não encontrado: {codMarketplace}");
                            }
                        }
                        else
                        {
                            Error body = new Error();
                            body = await response.Content.ReadAsAsync<Error>();

                            throw new Exception($"Erro ao solicitar pedido {codMarketplace}. Conteúdo: Pedido não encontrado");
                        }
                    }
                    else
                    {
                        Error body = new Error();
                        body = await response.Content.ReadAsAsync<Error>();

                        throw new Exception($"Erro no retorno da Skyhub. {codMarketplace}: {response.ReasonPhrase}. Conteúdo: {body.error}");
                    }
                }
                else
                {
                    throw new Exception($"Filial não encontrada: {codFilial}");
                }

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    "Pedido atualizado"
                );
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Erro: {ResolucaoExcecoes.ErroAprofundado(except)}"
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> FinalizaPedido(int codFilial, string codMarketplace)
        {
            //Método não utilizado por hora.
            //Este método atualiza um pedido na B2W para o status DELIVERED, indicando que o comprador
            //recebeu o pedido. Mas esse status é atualizado pela própria B2W
            //assim que o comprador atualiza no site deles, ou o correio notifica.

            try
            {
                if (codFilial == 0)
                {
                    throw new Exception("Informe a filial");
                }

                if (codMarketplace == "")
                {
                    throw new Exception("Código do pedido do marketplace inválido");
                }

                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                if (configuracaoSkyhub != null)
                {
                    HttpClient Http = new HttpClient
                    {
                        BaseAddress = new Uri("https://api.skyhub.com.br")
                    };

                    Http.DefaultRequestHeaders.Accept.Clear();
                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                    Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    object data = null;

                    HttpResponseMessage response = await Http.PostAsJsonAsync($"/orders/{codMarketplace}/delivery", data);

                    if (response.IsSuccessStatusCode)
                    {
                        TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                                 where (p.COD_PEDIDO_MARKETPLACE == codMarketplace)
                                                 select p).FirstOrDefault();
                        if (wPedido != null)
                        {
                            wPedido.DESC_SITUACAO_MARKETPLACE = "DELIVERED";
                            wPedido.DESC_COMPLEMENTO_OBS2 = "Finalizado pelo vendedor";
                            db.Entry(wPedido).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            throw new Exception($"Erro ao atualizar pedido no sistema. Não encontrado: {codMarketplace}");
                        }
                    }
                    else
                    {
                        Error body = new Error();
                        body = await response.Content.ReadAsAsync<Error>();

                        throw new Exception($"Erro ao atualizar pedido para DELIVERED. {codMarketplace}: {response.ReasonPhrase}. Conteúdo: {body.error}");
                    }
                }
                else
                {
                    throw new Exception($"Filial não encontrada: {codFilial}");
                }

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    "Pedido cancelado"
                );
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Erro: {ResolucaoExcecoes.ErroAprofundado(except)}"
                );
            }
        }


        [HttpGet]
        public async Task<HttpResponseMessage> CancelaPedido(int codFilial, string codMarketplace)
        {
            try
            {
                if (codFilial == 0)
                {
                    throw new Exception("Informe a filial");
                }

                if (codMarketplace == "")
                {
                    throw new Exception("Código do pedido do marketplace inválido");
                }

                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                if (configuracaoSkyhub != null)
                {
                    HttpClient Http = new HttpClient
                    {
                        BaseAddress = new Uri("https://api.skyhub.com.br")
                    };

                    Http.DefaultRequestHeaders.Accept.Clear();
                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                    Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");
                    
                    object data = null;
                    HttpResponseMessage response = await Http.PostAsJsonAsync($"/orders/{codMarketplace}/cancel", data);

                    if (response.IsSuccessStatusCode)
                    {
                        //Atualiza os campos do pedido necessários para cancelado

                        TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                                 where (p.COD_PEDIDO_MARKETPLACE == codMarketplace)
                                                 select p).FirstOrDefault();
                        if (wPedido != null)
                        {
                            wPedido.IND_SITUACAO = "2";
                            wPedido.DESC_SITUACAO_MARKETPLACE = "CANCELED";
                            wPedido.DESC_COMPLEMENTO_OBS2 = "Cancelado pelo vendedor";
                            db.Entry(wPedido).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            throw new Exception($"Erro ao atualizar pedido no sistema. Não encontrado: {codMarketplace}");
                        }
                    }
                    else
                    {
                        Error body = new Error();
                        body = await response.Content.ReadAsAsync<Error>();

                        throw new Exception($"Erro ao atualizar pedido para CANCELED. {codMarketplace}: {response.ReasonPhrase}. Conteúdo: {body.error}");
                    }
                }
                else
                {
                    throw new Exception($"Filial não encontrada: {codFilial}");
                }

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    "Pedido cancelado"
                );
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Erro: {ResolucaoExcecoes.ErroAprofundado(except)}"
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> EnviaPedido(int codFilial, string codMarketplace)
        {
            //Método não utilizado por hora.
            //Este método atualiza um pedido na B2W para o status SHIPPED, indicando que o vendedor
            //entregou o pedido para o comprador. Mas esse status é atualizado pela própria B2W
            //assim que ele recebe as informações pelos correios.

            //Importante resaltar que pedidos que não utilizam os serviços de entrega da B2W
            //que no caso é feita pelos correios, o próprio vendedor deve informar a API sobre dados
            //da entrega, tais como código de rastreio, descrição da transportadora, etc...
            //O Sistema por enquanto esta implementado apenas para utilizar a B2W entregas.

            try
            {
                if (codFilial == 0)
                {
                    throw new Exception("Informe a filial");
                }

                if (codMarketplace == "")
                {
                    throw new Exception("Código do pedido do marketplace inválido");
                }

                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                if (configuracaoSkyhub != null)
                {
                    HttpClient Http = new HttpClient
                    {
                        BaseAddress = new Uri("https://api.skyhub.com.br")
                    };

                    Http.DefaultRequestHeaders.Accept.Clear();
                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                    Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");
                    
                    //Informações da transportadora optado pelo vendedor.

                    Shipment envio = new Shipment()
                    {
                        code = codMarketplace,
                        track = new Track()
                        {
                            code = "",
                            carrier = "",
                            method = "",
                            url = ""
                        }
                    };

                    //Adiciona a key shipment à requisição, padrão da API

                    Dictionary<string, Shipment> data = new Dictionary<string, Shipment> { { "shipment", envio } };
                    
                    HttpResponseMessage response = await Http.PostAsJsonAsync($"/orders/{codMarketplace}/shipments", data);

                    if (response.IsSuccessStatusCode)
                    {
                        TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                                 where (p.COD_PEDIDO_MARKETPLACE == codMarketplace)
                                                 select p).FirstOrDefault();
                        if (wPedido != null)
                        {
                            wPedido.DESC_SITUACAO_MARKETPLACE = "SHIPPED";
                            db.Entry(wPedido).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            throw new Exception($"Erro ao atualizar pedido no sistema. Não encontrado: {codMarketplace}");
                        }
                    }
                    else
                    {
                        Error body = new Error();
                        body = await response.Content.ReadAsAsync<Error>();

                        throw new Exception($"Erro no retorno da Skyhub. {codMarketplace}: {response.ReasonPhrase}. Conteúdo: {body.error}");
                    }
                }
                else
                {
                    throw new Exception($"Filial não encontrada: {codFilial}");
                }

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    "Pedido enviado"
                );
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Erro: {ResolucaoExcecoes.ErroAprofundado(except)}"
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> DetalhesPedido(int codFilial, string codMarketplace)
        {
            //Este método retorna o JSON completo do pedido como ele está na B2W

            Order ordem;

            try
            {
                if (codFilial == 0)
                {
                    throw new Exception("Informe a filial");
                }

                if (codMarketplace == null)
                {
                    throw new Exception("Código do pedido do marketplace inválido");
                }

                //Encontra a filial para buscar informações de acesso.

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                if (configuracaoSkyhub != null)
                {
                    HttpClient Http = new HttpClient
                    {
                        BaseAddress = new Uri("https://api.skyhub.com.br")
                    };

                    Http.DefaultRequestHeaders.Accept.Clear();
                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                    Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    HttpResponseMessage response = await Http.GetAsync($"/orders/{codMarketplace}");

                    if (!response.IsSuccessStatusCode)
                    {
                        Error body = new Error();
                        body = await response.Content.ReadAsAsync<Error>();

                        throw new Exception($"Erro ao buscar pedido na skyhub. {codMarketplace}: {response.ReasonPhrase}. Conteúdo: {body.error}");
                    }
                    else
                    {
                        ordem = new Order();
                        ordem = await response.Content.ReadAsAsync<Order>();
                    }
                }
                else
                {
                    throw new Exception($"Filial não encontrada: {codFilial}");
                }

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    ordem
                );
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Erro: {ResolucaoExcecoes.ErroAprofundado(except)}"
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> FaturaPedido(int codFilial, string chaveNFE, string codMarketplace)
        {
            try
            {
                if (chaveNFE.Length != 44)
                {
                    throw new Exception("Chave da NFE inválida");
                }

                if (codFilial == 0)
                {
                    throw new Exception("Informe a filial");
                }

                if (codMarketplace == "")
                {
                    throw new Exception("Código do pedido do marketplace inválido");
                }
                
                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);
                    
                if (configuracaoSkyhub != null)
                {
                    HttpClient Http = new HttpClient
                    {
                        BaseAddress = new Uri("https://api.skyhub.com.br")
                    };

                    Http.DefaultRequestHeaders.Accept.Clear();
                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                    Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    //Atribui a chave de acesso da nota fiscal à chamada

                    Invoice fatura = new Invoice()
                    {
                        key = chaveNFE
                    };

                    //Adiciona a key invoice à chamada, padrão da API

                    Dictionary<string, Invoice> data = new Dictionary<string, Invoice> { { "invoice", fatura } };

                    HttpResponseMessage response = await Http.PostAsJsonAsync($"/orders/{codMarketplace}/invoice", data);
                        
                    if (response.IsSuccessStatusCode)
                    {
                        //Se houve uma resposta de sucesso, atualiza o campo DESC_SITUACAO_SKYHUB na tabela TB_PEDIDO_CAB

                        TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                                 where (p.COD_PEDIDO_MARKETPLACE == codMarketplace)
                                                 select p).FirstOrDefault();
                        if (wPedido != null)
                        {
                            wPedido.DESC_SITUACAO_MARKETPLACE = "INVOICE";
                            db.Entry(wPedido).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            throw new Exception($"Erro ao atualizar pedido no sistema. Não encontrado: {codMarketplace}");
                        }
                    }
                    else
                    {
                        Error body = new Error();
                        body = await response.Content.ReadAsAsync<Error>();

                        throw new Exception($"Erro no retorno da Skyhub. {codMarketplace}: {response.ReasonPhrase}. Conteúdo: {body.error}");
                    }
                }
                else
                {
                    throw new Exception($"Filial não encontrada: {codFilial}");
                }
                
                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    "Pedido faturado"
                );
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Erro: {ResolucaoExcecoes.ErroAprofundado(except)}"
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> SincronizaPedidos()
        {
            try
            {
                int wTotalCancelados = 0;
                int wTotalCriados = 0;
                int wTotalAlterados = 0;

                ControlaExcecoes.Limpa();

                List<TB_CONFIGURACAO_SKYHUB> configuracoesSkyhub = GetConfiguracoes();

                //Obtém configurações para conectar com a Skyhub, disponivel na tela de parâmetros do sistema, aba Skyhub

                foreach (var configuracaoSkyhub in configuracoesSkyhub)
                {
                    try
                    {
                        HttpClient Http = new HttpClient
                        {
                            BaseAddress = new Uri("https://api.skyhub.com.br")
                        };

                        Http.DefaultRequestHeaders.Accept.Clear();
                        Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                        Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                        Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                        Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                        //Busca por um pedido na fila
                        //A fila é usada para consumir pedidos em ordem. Após consumir um pedido, é necessário em até 5 minutos,
                        //deleta-lo da fila para que a API entenda que este pedido foi salvo com sucesso no sistema.

                        bool wContinua = true;

                        while (wContinua)
                        {
                            //Irá verificar todos os pedidos da fila da API (queues)

                            HttpResponseMessage response = await Http.GetAsync("/queues/orders");

                            Order ordem = new Order();
                            ordem = await response.Content.ReadAsAsync<Order>();

                            if (response.IsSuccessStatusCode)
                            {
                                try
                                {
                                    //Verifica se ainda existem pedidos na fila, caso retorne nulo, então todos os pedidos já estão sincronizados

                                    if (ordem != null)
                                    {
                                        //Verifica se já existe esse pedido no sistema pelo campo COD_PEDIDO_MARKETPLACE
                                        //caso não exista insere um pedido novo, caso exista não faz nada.

                                        switch (ordem.status.type)
                                        {
                                            case "APPROVED":
                                                //Pedidos com status APPROVED já obtiveram o pagamento devido então precisa sincronizar

                                                if (!db.TB_PEDIDO_CAB.Any(p => p.COD_PEDIDO_MARKETPLACE == ordem.code))
                                                {
                                                    //Gera os códigos necessários para gerar um pedido no sistema

                                                    int wCadastro = GetCodCadastro(ordem);
                                                    int wSequencia = GetSequencia(ordem);
                                                    int wCodigoPedido = db.Database.SqlQuery<int>("SELECT SQPEDIDO.NEXTVAL FROM DUAL").First();

                                                    if (wCadastro == -1)
                                                    {
                                                        throw new Exception($"Cliente não encontrado e não foi possível cadastrar: {ordem.customer.name}");
                                                    }

                                                    if (wSequencia == -1)
                                                    {
                                                        throw new Exception("Não existe uma sequência para pedidos, corrija antes de sincronizar");
                                                    }

                                                    //Cria o novo pedido trazendo dados da API e algumas informações padrões no sistema

                                                    TB_PEDIDO_CAB wPedidoCab = new TB_PEDIDO_CAB()
                                                    {
                                                        COD_PEDIDO_CAB = wCodigoPedido,
                                                        COD_FILIAL = Convert.ToInt32(configuracaoSkyhub.COD_FILIAL),
                                                        COD_OPERACAO = Convert.ToInt32(configuracaoSkyhub.COD_OPERACAO),
                                                        COD_CADASTRO = wCadastro,
                                                        NUM_PEDIDO = wSequencia,
                                                        DT_EMISSAO = ordem.updated_at,
                                                        IND_SITUACAO = "1",
                                                        IND_TIPO_PAGAMENTO = "A VISTA",
                                                        IND_TIPO_FRETE = "CIF",
                                                        COD_PEDIDO_MARKETPLACE = ordem.code,
                                                        DESC_COMPLEMENTO_OBS = "Pedido do marketplace: " + ordem.code,
                                                        DESC_SITUACAO_MARKETPLACE = "APPROVED",
                                                        PERC_COMISSAO = 0,
                                                        COD_DEPARTAMENTO = 1
                                                    };

                                                    foreach (Item item in ordem.items)
                                                    {
                                                        int wCodItem = db.Database.SqlQuery<int>("SELECT SQPEDIDO_ITEM.NEXTVAL FROM DUAL").First();
                                                        int wCodTributacao = GetTributacao(item);
                                                        long wCodProduto = GetProdutosSkyhub(item);
                                                        
                                                        if (wCodTributacao == -1)
                                                        {
                                                            throw new Exception("Produto não possui nenhuma tributação configurada. Produto: " + item.id);
                                                        }

                                                        if (wCodProduto == -1)
                                                        {
                                                            throw new Exception("Produto não encontrado: " + wCodProduto);
                                                        }

                                                        wPedidoCab.TB_PEDIDO_ITEM.Add(
                                                            new TB_PEDIDO_ITEM()
                                                            {
                                                                COD_PEDIDO_ITEM = wCodItem,
                                                                COD_PEDIDO_CAB = wCodigoPedido,
                                                                COD_PRODUTO = (int)wCodProduto,
                                                                VAL_UNITARIO = Convert.ToDecimal(item.original_price),
                                                                QT_PEDIDO = item.qty,
                                                                COD_TRIBUTACAO = wCodTributacao
                                                            }
                                                        );
                                                    }

                                                    db.Entry(wPedidoCab).State = EntityState.Added;
                                                    db.SaveChanges();

                                                    //Quando um pedido novo for baixado para o sistema, envia um e-mail para os seguintes destinatários:
                                                    
                                                    try
                                                    {
                                                        List<string> eDestinatarios = new List<string>
                                                        {
                                                            "gyan0012@hotmail.com",
                                                            "marcos@cemapa.com",
                                                            "cemapa@cemapa.com"
                                                        };

                                                        EnviaEmail(eDestinatarios, "Novo pedido aprovado.", $"Um novo pedido foi aprovado pelo marketplace. Código: {ordem.code}");
                                                    }
                                                    catch (Exception)
                                                    {
                                                        //Não consegui pensar como se comportará a requisição caso o e-mail não tenha sido enviado
                                                    }

                                                    wTotalCriados++;
                                                }
                                            break;

                                            case "CANCELED":
                                                //Pedidos com status CANCELED são necessários para saber quando um cliente cancelou sua compra

                                                if (db.TB_PEDIDO_CAB.Any(p => p.COD_PEDIDO_MARKETPLACE == ordem.code))
                                                {
                                                    TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);
                                                    wPedidoCab.IND_SITUACAO = "2";
                                                    wPedidoCab.DESC_COMPLEMENTO_OBS2 = "Cancelado pelo cliente";
                                                    wPedidoCab.DESC_SITUACAO_MARKETPLACE = "CANCELED";

                                                    db.Entry(wPedidoCab).State = EntityState.Modified;
                                                    db.SaveChanges();

                                                    wTotalCancelados++;
                                                }
                                            break;

                                            case "INVOICE":
                                                //Pedidos com status INVOICE provavelmente não ocorrerão, pois esse status é acionado pelo vendedor

                                                if (db.TB_PEDIDO_CAB.Any(p => p.COD_PEDIDO_MARKETPLACE == ordem.code))
                                                {
                                                    TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);
                                                    wPedidoCab.DESC_SITUACAO_MARKETPLACE = "INVOICE";

                                                    db.Entry(wPedidoCab).State = EntityState.Modified;
                                                    db.SaveChanges();

                                                    wTotalAlterados++;
                                                }
                                            break;

                                            case "SHIPPED":
                                                //Pedido com status SHIPPED, indica que o pedido foi entrega a transportadora.
                                                //Então é necessário sincronizar o status do pedido no sistema

                                                //Importante resaltar que pedidos que não utilizam os serviços de entrega da B2W
                                                //que no caso é feita pelos correios, o próprio vendedor deve informar a API sobre dados
                                                //da entrega, tais como código de rastreio, descrição da transportadora, etc...
                                                //O Sistema por enquanto esta implementado apenas para utilizar a B2W entregas.

                                                if (db.TB_PEDIDO_CAB.Any(p => p.COD_PEDIDO_MARKETPLACE == ordem.code))
                                                {
                                                    TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);
                                                    wPedidoCab.DESC_SITUACAO_MARKETPLACE = "SHIPPED";

                                                    db.Entry(wPedidoCab).State = EntityState.Modified;
                                                    db.SaveChanges();

                                                    wTotalAlterados++;
                                                }
                                            break;

                                            case "DELIVERED":
                                                //Pedidos com status DELIVERED indicam que o comprador recebeu o produto.
                                                //Então é necessário sincronizar o status do pedido no sistema
                                                //Esse status é atualizado pelo comprador.

                                                if (db.TB_PEDIDO_CAB.Any(p => p.COD_PEDIDO_MARKETPLACE == ordem.code))
                                                {
                                                    TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);
                                                    wPedidoCab.DESC_SITUACAO_MARKETPLACE = "DELIVERED";

                                                    db.Entry(wPedidoCab).State = EntityState.Modified;
                                                    db.SaveChanges();

                                                    wTotalAlterados++;
                                                }
                                            break;

                                            case "SHIPMENT_EXCEPTION":
                                                //Pedidos com status SHIPMENT_EXCEPTION pode ocorrer quando houver algum problema com a entrega.
                                                //Então é necessário sincronizar o status do pedido no sistema

                                                if (db.TB_PEDIDO_CAB.Any(p => p.COD_PEDIDO_MARKETPLACE == ordem.code))
                                                {
                                                    TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);
                                                    wPedidoCab.DESC_SITUACAO_MARKETPLACE = "SHIPMENT_EXCEPTION";

                                                    db.Entry(wPedidoCab).State = EntityState.Modified;
                                                    db.SaveChanges();

                                                    wTotalAlterados++;
                                                }
                                            break;

                                            case "PAYMENT_OVERDUE":
                                                //Pedidos com status PAYMENT_OVERDUE ocorre quando o boleto estiver com a data de pagamento vencido.
                                                //Então é necessário sincronizar o status do pedido no sistema

                                                if (db.TB_PEDIDO_CAB.Any(p => p.COD_PEDIDO_MARKETPLACE == ordem.code))
                                                {
                                                    TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);
                                                    wPedidoCab.DESC_SITUACAO_MARKETPLACE = "PAYMENT_OVERDUE";

                                                    db.Entry(wPedidoCab).State = EntityState.Modified;
                                                    db.SaveChanges();

                                                    wTotalAlterados++;
                                                }
                                            break;
                                        }

                                        //Após consumir um pedido, é necessário confirmar com a API que ele foi consumido com sucesso.
                                        //A API aguarda 5 minutos antes de jogar o pedido pro final da fila para ser consumido novamente.

                                        response.Dispose();
                                        response = await Http.DeleteAsync("/queues/orders/" + ordem.code);

                                        if (!response.IsSuccessStatusCode)
                                        {
                                            throw new Exception("Erro ao confirmar consumo da fila com a API: " + response.ReasonPhrase);
                                        }
                                    }
                                    else
                                    {
                                        //Se não houverem mais pedidos na fila da API, então para de buscar

                                        wContinua = false;
                                    }

                                    response.Dispose();
                                }
                                catch (Exception except)
                                {
                                    ControlaExcecoes.Add($"Erro ao sincronizar. Filial: {configuracaoSkyhub.COD_FILIAL}", ResolucaoExcecoes.ErroAprofundado(except));
                                    continue;
                                }
                            }
                            else
                            {
                                ControlaExcecoes.Add($"Erro ao realizar chamada GET. Filial: {configuracaoSkyhub.COD_FILIAL}", response.ReasonPhrase);
                                continue;
                            }
                        }
                    }
                    catch (Exception except)
                    {
                        ControlaExcecoes.Add($"Erro ao sincronizar. Filial: {configuracaoSkyhub.COD_FILIAL}", ResolucaoExcecoes.ErroAprofundado(except));
                        continue;
                    }
                }

                //Biblioteca ControlaExcecoes armazenou todos os erros ocorridos, caso não ocorra nenhum erro,
                //então o método SemExcecoes retornará verdadeiro.

                if (ControlaExcecoes.SemExcecoes())
                {
                    return Request.CreateResponse(
                        HttpStatusCode.OK,
                        $"Os pedidos agora estão sincronizados. Criados: {wTotalCriados}, Cancelados: {wTotalCancelados}, Alterados: {wTotalAlterados}."
                    );
                }
                else
                {
                    return Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        $"Nem todos os pedidos foram sincronizados. Criados: {wTotalCriados}, Cancelados: {wTotalCancelados}, Alterados: {wTotalAlterados}. " +
                        $"{string.Join(", ", ControlaExcecoes.Excecoes)}"
                    );
                }
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Não foi possível começar a sincronização. {ResolucaoExcecoes.ErroAprofundado(except)}"
                );
                
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> SincronizaProdutos()
        {
            try
            {
                int totalAtualizados = 0;
                int totalCriados = 0;
                int totalDeletados = 0;

                bool saveRegistro = true;

                ControlaExcecoes.Limpa();

                //Busca sincronizações de produtos pendentes (campo IND_SINCRONIZADO esteja igual a "N") na tabela TB_SINCRONIZACAO_SKYHUB.

                List<TB_SINCRONIZACAO_SKYHUB> sincronizacoesSkyhub = GetSincronizacoes();

                foreach (var sincronizacaoSkyhub in sincronizacoesSkyhub)
                {
                    try
                    {
                        //Busca todas as configurações ativas para se conectar com a API.

                        List<TB_CONFIGURACAO_SKYHUB> configuracoesSkyhub = GetConfiguracoes();

                        foreach (var configuracaoSkyhub in configuracoesSkyhub)
                        {
                            try
                            {
                                //Seleciona as configurações do produto na tabela TB_PRODUTO_SKYHUB referente ao produto que se deseja sincronizar.

                                saveRegistro = true;

                                List<TB_PRODUTO_SKYHUB> produtosSkyhub = GetProdutosSkyhub(sincronizacaoSkyhub);

                                if (produtosSkyhub.Count > 0)
                                {
                                    foreach (TB_PRODUTO_SKYHUB produtoSkyhub in produtosSkyhub)
                                    {
                                        try
                                        {
                                            //Verifica se os dados atualmente deste produto precisam ser atualizados antes de sincronizar.

                                            TB_PRODUTO wInfosProduto = InfosProduto(configuracaoSkyhub, produtoSkyhub);
                                            decimal wTotalEstoque = TotalEstoque(configuracaoSkyhub, produtoSkyhub);
                                            
                                            //Faz algumas verificações em alguns campos antes de sincronizar.
                                            //Caso não esteja tudo ok, este produto não será sincronizado

                                            if (ProdutoEstaOK(produtoSkyhub))
                                            {
                                                HttpClient Http = new HttpClient
                                                {
                                                    BaseAddress = new Uri("https://api.skyhub.com.br")
                                                };

                                                Http.DefaultRequestHeaders.Accept.Clear();
                                                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                                Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                                                Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                                                Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                                                Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                                                //Instancia o produto da Classe ProdutoSkyhub, criada conforme a estrutura especificada no manual da API.

                                                ProdutoSkyhub ProdutoSku = new ProdutoSkyhub
                                                {
                                                    sku = produtoSkyhub.COD_PRODUTO,
                                                    name = produtoSkyhub.DESC_PRODUTO,
                                                    description = produtoSkyhub.DESC_DESCRICAO,
                                                    status = produtoSkyhub.DESC_STATUS,
                                                    qty = Convert.ToInt32(wTotalEstoque),
                                                    price = Convert.ToDouble(wInfosProduto.VAL_VAREJO),
                                                    weight = Convert.ToDouble(produtoSkyhub.VAL_PESO),
                                                    width = Convert.ToDouble(produtoSkyhub.VAL_LARGURA),
                                                    height = Convert.ToDouble(produtoSkyhub.VAL_ALTURA),
                                                    length = Convert.ToDouble(produtoSkyhub.VAL_COMPRIMENTO),
                                                    brand = produtoSkyhub.DESC_MARCA,
                                                    ean = wInfosProduto.DESC_COD_BARRA,
                                                    nbm = wInfosProduto.NUM_GENERO_NCM
                                                };
                                                
                                                ProdutoSku.categories.Add(
                                                    new Category
                                                    {
                                                        code = Convert.ToInt64(produtoSkyhub.DESC_CATEGORIA.GetHashCode()),
                                                        name = produtoSkyhub.DESC_CATEGORIA
                                                    }
                                                );

                                                ProdutoSku.AddSpecificationsCustom("Alimentação", produtoSkyhub.ESP_ALIMENTACAO);
                                                ProdutoSku.AddSpecificationsCustom("Aparelhos compatíveis", produtoSkyhub.ESP_APARELHOSCOMPATIVEIS);
                                                ProdutoSku.AddSpecificationsCustom("Conteúdo da embalagem", produtoSkyhub.ESP_CONTEUDODAEMBALAGEM);
                                                ProdutoSku.AddSpecificationsCustom("Cor", produtoSkyhub.ESP_COR);
                                                ProdutoSku.AddSpecificationsCustom("Cor - ficha técnica", produtoSkyhub.ESP_CORFICHATECNICA);
                                                ProdutoSku.AddSpecificationsCustom("Data de Lançamento no Mercado", produtoSkyhub.ESP_DATADELANCAMENTONOMERCADO);
                                                ProdutoSku.AddSpecificationsCustom("Dimensões Embalagem - cm (AxLxP)", produtoSkyhub.ESP_DIMENSOESEMBALAGEM);
                                                ProdutoSku.AddSpecificationsCustom("Dimensões Produto - cm (AxLxP)", produtoSkyhub.ESP_DIMENSOESPRODUTO);
                                                ProdutoSku.AddSpecificationsCustom("Fabricante", produtoSkyhub.ESP_FABRICANTE);
                                                ProdutoSku.AddSpecificationsCustom("Garantia do Fornecedor", produtoSkyhub.ESP_GARANTIADEFORNECEDOR);
                                                ProdutoSku.AddSpecificationsCustom("Mais informações", produtoSkyhub.ESP_MAISINFORMACOES);
                                                ProdutoSku.AddSpecificationsCustom("Manual", produtoSkyhub.ESP_MANUAL);
                                                ProdutoSku.AddSpecificationsCustom("Marca", produtoSkyhub.ESP_MARCA);
                                                ProdutoSku.AddSpecificationsCustom("Material/Composição", produtoSkyhub.ESP_MATERIALCOMPOSICAO);
                                                ProdutoSku.AddSpecificationsCustom("Modelo", produtoSkyhub.ESP_MODELO);
                                                ProdutoSku.AddSpecificationsCustom("Peso liq. da embalagem c/ produto (Kg)", produtoSkyhub.ESP_PESOLIQDAEMBALAGEMCPRODUTO);
                                                ProdutoSku.AddSpecificationsCustom("Peso liq. do produto (Kg)", produtoSkyhub.ESP_PESOLIQDOPRODUTO);
                                                ProdutoSku.AddSpecificationsCustom("Recursos/Funcionalidades", produtoSkyhub.ESP_RECURSOSFUNCIONALIDADES);
                                                ProdutoSku.AddSpecificationsCustom("Referência do Modelo", produtoSkyhub.ESP_REFERENCIADOMODELO);
                                                ProdutoSku.AddSpecificationsCustom("SAC do Fabricante", produtoSkyhub.ESP_SAC);
                                                ProdutoSku.AddSpecificationsCustom("Tamanho", produtoSkyhub.ESP_TAMANHO);
                                                ProdutoSku.AddSpecificationsCustom("Vídeo", produtoSkyhub.ESP_VIDEO);
                                                ProdutoSku.AddSpecificationsCustom("Voltagem", produtoSkyhub.ESP_VOLTAGEM);

                                                ProdutoSku.AddImagesCustom(produtoSkyhub.DESC_LINK_IMAGEM_1);
                                                ProdutoSku.AddImagesCustom(produtoSkyhub.DESC_LINK_IMAGEM_2);
                                                ProdutoSku.AddImagesCustom(produtoSkyhub.DESC_LINK_IMAGEM_3);
                                                ProdutoSku.AddImagesCustom(produtoSkyhub.DESC_LINK_IMAGEM_4);

                                                List<TB_PRODUTO_SKYHUB> variacoes = GetVariacoes(produtoSkyhub);

                                                foreach (var variacao in variacoes)
                                                {
                                                    if (ProdutoEstaOK(variacao))
                                                    {
                                                        TB_PRODUTO wInfosVariacao = InfosProduto(configuracaoSkyhub, variacao);
                                                        decimal wTotalEstoqueVar = TotalEstoque(configuracaoSkyhub, variacao);

                                                        Variation wProdutoVariacao = new Variation
                                                        {
                                                            sku = variacao.COD_PRODUTO,
                                                            price = Convert.ToDouble(wInfosVariacao.VAL_VAREJO),
                                                            qty = Convert.ToInt32(wTotalEstoqueVar),
                                                            ean = wInfosVariacao.DESC_COD_BARRA
                                                        };

                                                        wProdutoVariacao.AddSpecificationsCustom("Alimentação", variacao.ESP_ALIMENTACAO);
                                                        wProdutoVariacao.AddSpecificationsCustom("Aparelhos compatíveis", variacao.ESP_APARELHOSCOMPATIVEIS);
                                                        wProdutoVariacao.AddSpecificationsCustom("Conteúdo da embalagem", variacao.ESP_CONTEUDODAEMBALAGEM);
                                                        wProdutoVariacao.AddSpecificationsCustom("Cor", variacao.ESP_COR);
                                                        wProdutoVariacao.AddSpecificationsCustom("Cor - ficha técnica", variacao.ESP_CORFICHATECNICA);
                                                        wProdutoVariacao.AddSpecificationsCustom("Data de Lançamento no Mercado", variacao.ESP_DATADELANCAMENTONOMERCADO);
                                                        wProdutoVariacao.AddSpecificationsCustom("Dimensões Embalagem - cm (AxLxP)", variacao.ESP_DIMENSOESEMBALAGEM);
                                                        wProdutoVariacao.AddSpecificationsCustom("Dimensões Produto - cm (AxLxP)", variacao.ESP_DIMENSOESPRODUTO);
                                                        wProdutoVariacao.AddSpecificationsCustom("Fabricante", variacao.ESP_FABRICANTE);
                                                        wProdutoVariacao.AddSpecificationsCustom("Garantia do Fornecedor", variacao.ESP_GARANTIADEFORNECEDOR);
                                                        wProdutoVariacao.AddSpecificationsCustom("Mais informações", variacao.ESP_MAISINFORMACOES);
                                                        wProdutoVariacao.AddSpecificationsCustom("Manual", variacao.ESP_MANUAL);
                                                        wProdutoVariacao.AddSpecificationsCustom("Marca", variacao.ESP_MARCA);
                                                        wProdutoVariacao.AddSpecificationsCustom("Material/Composição", variacao.ESP_MATERIALCOMPOSICAO);
                                                        wProdutoVariacao.AddSpecificationsCustom("Modelo", variacao.ESP_MODELO);
                                                        wProdutoVariacao.AddSpecificationsCustom("Peso liq. da embalagem c/ produto (Kg)", variacao.ESP_PESOLIQDAEMBALAGEMCPRODUTO);
                                                        wProdutoVariacao.AddSpecificationsCustom("Peso liq. do produto (Kg)", variacao.ESP_PESOLIQDOPRODUTO);
                                                        wProdutoVariacao.AddSpecificationsCustom("Recursos/Funcionalidades", variacao.ESP_RECURSOSFUNCIONALIDADES);
                                                        wProdutoVariacao.AddSpecificationsCustom("Referência do Modelo", variacao.ESP_REFERENCIADOMODELO);
                                                        wProdutoVariacao.AddSpecificationsCustom("SAC do Fabricante", variacao.ESP_SAC);
                                                        wProdutoVariacao.AddSpecificationsCustom("Tamanho", variacao.ESP_TAMANHO);
                                                        wProdutoVariacao.AddSpecificationsCustom("Vídeo", variacao.ESP_VIDEO);
                                                        wProdutoVariacao.AddSpecificationsCustom("Voltagem", variacao.ESP_VOLTAGEM);
                                                        
                                                        wProdutoVariacao.specifications.Add(
                                                            new Specification
                                                            {
                                                                key = "price",
                                                                value = Convert.ToString(wInfosVariacao.VAL_VAREJO)
                                                            }
                                                        );

                                                        wProdutoVariacao.AddImagesCustom(variacao.DESC_LINK_IMAGEM_1);
                                                        wProdutoVariacao.AddImagesCustom(variacao.DESC_LINK_IMAGEM_2);
                                                        wProdutoVariacao.AddImagesCustom(variacao.DESC_LINK_IMAGEM_3);
                                                        wProdutoVariacao.AddImagesCustom(variacao.DESC_LINK_IMAGEM_4);
                                                        
                                                        ProdutoSku.variations.Add(wProdutoVariacao);
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Variação de produto não preenchido corretamente");
                                                    }

                                                }

                                                //Agora cria a key variation_attributes, que é gerada com base nas especificações
                                                //do produto pai e de suas variações. Caso exista alguma variação com mesma key mas
                                                //value diferente, então esse produto tem variações de atributos.

                                                foreach (Specification specification in ProdutoSku.specifications)
                                                {
                                                    bool wAchou = false;

                                                    foreach (Variation variation in ProdutoSku.variations)
                                                    {
                                                        foreach (Specification variSpecification in variation.specifications)
                                                        {
                                                            if ((variSpecification.key == specification.key) && (variSpecification.value != specification.value))
                                                            {
                                                                wAchou = true;
                                                            }
                                                        }
                                                    }

                                                    if (wAchou)
                                                    {
                                                        ProdutoSku.variation_attributes.Add(specification.key);
                                                    }
                                                }
                                                
                                                //Adiciona o produto skyhub na chave "products", padrão da API.

                                                Dictionary<string, ProdutoSkyhub> products = new Dictionary<string, ProdutoSkyhub>{{ "product", ProdutoSku }};

                                                //Por fim, executa a chamada Http conforme a requisição registrada na tabela TB_SINCRONIZACAO_SKYHUB
                                                //e caso ocorra algum erro, grava um log com informações.

                                                switch (sincronizacaoSkyhub.TIPO_ACAO)
                                                {
                                                    case "PUT":
                                                        {
                                                            HttpResponseMessage response = await Http.PutAsJsonAsync("/products/" + ProdutoSku.sku, products);

                                                            if (!response.IsSuccessStatusCode)
                                                            {
                                                                if (response.StatusCode == HttpStatusCode.NotFound)
                                                                {
                                                                    sincronizacaoSkyhub.TIPO_ACAO = "POST";
                                                                    db.SaveChanges();
                                                                    saveRegistro = false;
                                                                }
                                                                else
                                                                {
                                                                    throw new Exception("Erro na respota da API na chamada PUT: " + response.ReasonPhrase);
                                                                }
                                                            }

                                                            totalAtualizados++;
                                                        }
                                                        break;
                                                    case "DELETE":
                                                        {
                                                            HttpResponseMessage response = await Http.DeleteAsync("/products/" + ProdutoSku.sku);

                                                            if (!response.IsSuccessStatusCode)
                                                            {
                                                                if (response.StatusCode == HttpStatusCode.NotFound)
                                                                {
                                                                    //Diminui para depois aumentar, ou seja, permanecer o valor que esta.
                                                                    //Usado para uma tentativa de deletar um produto que já foi apagado não somar.

                                                                    totalDeletados--;
                                                                }
                                                                else
                                                                {
                                                                    throw new Exception("Erro na respota da API na chamada DELETE: " + response.ReasonPhrase);
                                                                }
                                                            }

                                                            totalDeletados++;
                                                        }
                                                        break;
                                                    case "POST":
                                                        {
                                                            HttpResponseMessage response = await Http.PostAsJsonAsync("/products", products);

                                                            if (!response.IsSuccessStatusCode)
                                                            {
                                                                throw new Exception("Erro na respota da API na chamada POST: " + response.ReasonPhrase);
                                                            }

                                                            totalCriados++;
                                                        }
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("Produto não preenchido corretamente");
                                            }
                                        }
                                        catch (Exception except)
                                        {
                                            ControlaExcecoes.Add($"Erro ao sincronizar. Filial: {configuracaoSkyhub.COD_FILIAL}", $"produto: {produtoSkyhub.COD_PRODUTO}", ResolucaoExcecoes.ErroAprofundado(except));
                                            saveRegistro = false;
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    //Caso não tenha sido encontrado nenhum registro da tabela TB_PRODUTO_SKYHUB referente a sincronização atual,
                                    //então o produto deve ser apagado da API também, ignorando o método pedido pela sincronização

                                    HttpClient Http = new HttpClient
                                    {
                                        BaseAddress = new Uri("https://api.skyhub.com.br")
                                    };

                                    Http.DefaultRequestHeaders.Accept.Clear();
                                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                                    Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");


                                    HttpResponseMessage response = await Http.DeleteAsync("/products/" + sincronizacaoSkyhub.COD_PRODUTO);

                                    if (!response.IsSuccessStatusCode)
                                    {
                                        if (response.StatusCode == HttpStatusCode.NotFound)
                                        {
                                            //Diminui para depois aumentar, ou seja, permanecer o valor que esta.
                                            //Usado para uma tentativa de deletar um produto que já foi apagada não somar
                                            //Serve apenas para o relatório da resposta ser preciso

                                            totalDeletados--;
                                        }
                                        else
                                        {
                                            throw new Exception("Erro na respota da API na chamada DELETE: " + response.ReasonPhrase);
                                        }
                                    }

                                    totalDeletados++;
                                }
                            }
                            catch (Exception except)
                            {
                                ControlaExcecoes.Add($"Erro ao sincronizar. Filial: {configuracaoSkyhub.COD_FILIAL}, " +
                                                     $"Sincronização: {sincronizacaoSkyhub.COD_SINCRONIZACAO_SKYHUB}",
                                                     ResolucaoExcecoes.ErroAprofundado(except));
                                saveRegistro = false;
                                continue;
                            }
                        }

                        //Altera o campo da sincronização para o produto não ficar sincronizando eternamente.
                        //Também salva este produto recentemente atualizado. Isso é uma forma preventiva para que em uma lista de sincronizações,
                        //caso haja algum item com problema de sincronização, não fique trancando essa fila, sendo necessário sincronizar tudo novamente.

                        if (saveRegistro)
                        {
                            //A variável saveRegistro, controla uma funcionalidade. Caso um produto não exista no lado do servidor da API,
                            //então não irá atualizar, verificar código anterior em que o valor da váriavel é alterado.

                            sincronizacaoSkyhub.DT_SINCRONIZACAO = DateTime.Now;
                            sincronizacaoSkyhub.IND_SINCRONIZADO = "S";
                            db.SaveChanges();
                        }
                    }
                    catch (Exception except)
                    {
                        ControlaExcecoes.Add($"Erro ao sincronizar. Sincronização: {sincronizacaoSkyhub.COD_SINCRONIZACAO_SKYHUB}", ResolucaoExcecoes.ErroAprofundado(except));
                        continue;
                    }
                }

                //Biblioteca ControlaExcecoes armazenou todos os erros ocorridos, caso não ocorra nenhum erro,
                //então o método SemExcecoes retornará verdadeiro.

                if (ControlaExcecoes.SemExcecoes())
                {
                    return Request.CreateResponse(
                        HttpStatusCode.OK,
                        $"Os produtos estão sincronizados. Criados: {totalCriados}, Atualizados: {totalAtualizados}, Removidos: {totalDeletados}"
                    );
                }
                else
                {
                    return Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        $"Nem todos os produtos foram sincronizados. Criados: {totalCriados}, Atualizados: {totalAtualizados}, Removidos: {totalDeletados}. " +
                        $"{string.Join(", ", ControlaExcecoes.Excecoes)}"
                    );
                }
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Não foi possível começar a sincronização. {ResolucaoExcecoes.ErroAprofundado(except)}"
                );
            }
        }

        private List<TB_PRODUTO_SKYHUB> GetVariacoes(TB_PRODUTO_SKYHUB produtoSky)
        {
            return (from produtoSkyhub in db.TB_PRODUTO_SKYHUB
                    where produtoSkyhub.COD_PRODUTO_SKYHUB_PAI == produtoSky.COD_PRODUTO
                    select produtoSkyhub).ToList();
        }

        private List<TB_CONFIGURACAO_SKYHUB> GetConfiguracoes()
        {
            return (from configuracaoSkyhub in db.TB_CONFIGURACAO_SKYHUB
                    where configuracaoSkyhub.IND_ATIVO == "S"
                    select configuracaoSkyhub).ToList();
        }

        private TB_CONFIGURACAO_SKYHUB GetConfiguracao(int codFilial)
        {
            return (from configuracaoSkyhub in db.TB_CONFIGURACAO_SKYHUB
                    where configuracaoSkyhub.IND_ATIVO == "S" && configuracaoSkyhub.COD_FILIAL == codFilial
                    select configuracaoSkyhub).FirstOrDefault();
        }

        private List<TB_SINCRONIZACAO_SKYHUB> GetSincronizacoes()
        {
            return (from sincronizacaoSkyhub in db.TB_SINCRONIZACAO_SKYHUB
                    where sincronizacaoSkyhub.IND_SINCRONIZADO == "N"
                    select sincronizacaoSkyhub).ToList();
        }

        private List<TB_PRODUTO_SKYHUB> GetProdutosSkyhub(TB_SINCRONIZACAO_SKYHUB sincronizacaoSkyhub)
        {
            return (from produtoSkyhub in db.TB_PRODUTO_SKYHUB
                    where produtoSkyhub.COD_PRODUTO == sincronizacaoSkyhub.COD_PRODUTO
                    select produtoSkyhub).ToList();
        }

        private TB_PRODUTO InfosProduto(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub, TB_PRODUTO_SKYHUB produtoSkyhub)
        {
            TB_PRODUTO wProduto = (from dbProduto in db.TB_PRODUTO
                                   where dbProduto.COD_PRODUTO == produtoSkyhub.COD_PRODUTO
                                   select dbProduto).First();
            return wProduto;

        }

        private decimal TotalEstoque(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub, TB_PRODUTO_SKYHUB produtoSkyhub)
        {
            List<TB_ESTOQUE> wEstoques = (from dbEstoque in db.TB_ESTOQUE
                                         where dbEstoque.COD_PRODUTO == produtoSkyhub.COD_PRODUTO
                                         && dbEstoque.COD_FILIAL == configuracaoSkyhub.COD_FILIAL
                                         select dbEstoque).ToList();

            decimal wTotalestoque = 0;
            foreach (var estoque in wEstoques)
            {
                wTotalestoque += estoque.QT_QUANTIDADE;
            }

            return wTotalestoque;
        }

        private bool ProdutoEstaOK(TB_PRODUTO_SKYHUB produtoSkyhub)
        {
            if (
                produtoSkyhub.COD_PRODUTO < 1 ||
                produtoSkyhub.VAL_ALTURA < 0 ||
                produtoSkyhub.VAL_COMPRIMENTO < 0 ||
                produtoSkyhub.VAL_LARGURA < 0 ||
                String.IsNullOrEmpty(produtoSkyhub.DESC_DESCRICAO) ||
                String.IsNullOrEmpty(produtoSkyhub.DESC_MARCA) ||
                String.IsNullOrEmpty(produtoSkyhub.DESC_PRODUTO) ||
                String.IsNullOrEmpty(produtoSkyhub.DESC_STATUS)
                )
            {
                return false;
            }

            return true;
        }

        private TB_PEDIDO_CAB GetPedidoPorMarketplace(Order ordem)
        {
            TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                     where (p.COD_PEDIDO_MARKETPLACE == ordem.code)
                                     select p).FirstOrDefault();
            return wPedido;
        }

        private int GetCodCadastro(Order ordem)
        {
            TB_CADASTRO wCadastro = (from c in db.TB_CADASTRO
                                     where (c.NUM_CGC_CPF == ordem.customer.vat_number)
                                     select c).FirstOrDefault();
            if (wCadastro == null)
            {
                string wEstado = ordem.shipping_address.region.ToUpper();
                string wCpf_cnpj = ordem.customer.vat_number;
                string wNome = ordem.customer.name.ToUpper();
                string wSexo = ordem.customer.gender == "male" ? "M" : "F";
                string wBairro = ordem.shipping_address.neighborhood;
                string wEmail = ordem.customer.email;
                DateTime wNascimento = Convert.ToDateTime(ordem.customer.date_of_birth);
                string wCelular = ordem.shipping_address.phone;
                string wTelefone = ordem.shipping_address.secondary_phone;
                string wEndereco = ordem.shipping_address.street + ", " + ordem.shipping_address.number;
                string wComplemento = ordem.shipping_address.complement;
                int wTipoCadastro;

                TB_TIPO_CADASTRO wTipoCad = (from tc in db.TB_TIPO_CADASTRO
                                             where (tc.DESC_TIPO_CADASTRO == "CLIENTE WEB")
                                             select tc).FirstOrDefault();
                if (wTipoCad == null)
                {
                    int wCodigoTipoCad = db.Database.SqlQuery<int>("SELECT SQTIPO_CADASTRO.NEXTVAL FROM DUAL").First();

                    TB_TIPO_CADASTRO wNovoTipoCadastro = new TB_TIPO_CADASTRO()
                    {
                        COD_TIPO_CADASTRO = wCodigoTipoCad,
                        DESC_TIPO_CADASTRO = "CLIENTE WEB"
                    };

                    db.Entry(wNovoTipoCadastro).State = EntityState.Added;
                    wTipoCadastro = wCodigoTipoCad;
                }
                else
                {
                    wTipoCadastro = wTipoCad.COD_TIPO_CADASTRO;
                }


                if (ordem.billing_address.number == "")
                {
                    wEndereco = ordem.billing_address.street;
                }

                if (!db.TB_ESTADO.Any(e => e.COD_ESTADO == wEstado))
                {
                    throw new Exception("Estado não encontrado: " + wEstado);
                }

                if (wCpf_cnpj.Length < 10)
                {
                    throw new Exception("CPF ou CNPJ inválido. " + wCpf_cnpj);
                }

                int wCodigoCadastro = db.Database.SqlQuery<int>("SELECT SQCADASTRO.NEXTVAL FROM DUAL").First();

                TB_CADASTRO wNovoCadastro = new TB_CADASTRO()
                {
                    COD_CADASTRO = wCodigoCadastro,
                    NOME = wNome,
                    COD_TIPO_CADASTRO = wTipoCadastro,
                    NUM_CGC_CPF = wCpf_cnpj,
                    DESC_E_MAIL = wEmail,
                    DT_NASCIMENTO = wNascimento,
                    DT_CADASTRO = DateTime.Now,
                    DESC_CELULAR = wCelular,
                    DESC_TELEFONE = wTelefone,
                    DESC_ENDERECO = wEndereco,
                    DESC_COMPLEMENTO = wComplemento,
                    DESC_BAIRRO = wBairro,
                    COD_ESTADO = wEstado,
                    IND_SEXO_CATEGORIA = wSexo,
                    IND_FISICA_JURIDICA = "F"
                };

                db.Entry(wNovoCadastro).State = EntityState.Added;

                return wCodigoCadastro;
            }
            else
            {
                return wCadastro.COD_CADASTRO;
            }

        }

        private int GetLoteTipo(Item item)
        {
            long wCodigo = Convert.ToInt64(item.id);

            TB_ESTOQUE wEstoque = (from e in db.TB_ESTOQUE
                                   where (e.COD_PRODUTO == wCodigo)
                                   select e).FirstOrDefault();

            if (wEstoque == null)
            {
                return -1;
            }
            else
            {
                return wEstoque.COD_LOTE_TIPO;
            }
        }

        private int GetTributacao(Item item)
        {
            long wCodigo = Convert.ToInt64(item.id);

            TB_PRODUTO wProduto = (from p in db.TB_PRODUTO
                                   where (p.COD_PRODUTO == wCodigo)
                                   select p).FirstOrDefault();
            if (wProduto.COD_TRIBUTACAO < 1)
            {
                return -1;
            }
            else
            {
                return wProduto.COD_TRIBUTACAO;
            }
        }

        private int GetSequencia(Order ordem)
        {
            TB_SEQUENCIA seq = (from s in db.TB_SEQUENCIA
                       where (s.NOME_SEQUENCIA == "PEDIDO")
                       select s).FirstOrDefault();
            if(seq == null)
            {
                return -1;
            }
            else
            {
                int val = seq.VAL_SEQUENCIA;
                seq.VAL_SEQUENCIA++;
                db.SaveChanges();
                return val;
            }
        }

        private long GetProdutosSkyhub(Item item)
        {
            long wCodigo = Convert.ToInt64(item.id);

            TB_PRODUTO wProduto = (from p in db.TB_PRODUTO
                                   where (p.COD_PRODUTO == wCodigo)
                                   select p).FirstOrDefault();
            if (wProduto.COD_PRODUTO < 1)
            {
                return -1;
            }
            else
            {
                return wProduto.COD_PRODUTO;
            }
        }
        
        private void EnviaEmail(List<string> eDestinos, string assunto, string corpo)
        {
            try
            {
                //TB_EMAIL wConfigEmail = (from e in db.TB_EMAIL where (e.DESC_EMAIL == "sistema@cemapa.com") select e).FirstOrDefault();

                TB_EMAIL wConfigEmail = new TB_EMAIL
                {
                    DESC_EMAIL = "sistema@cemapa.com",
                    DESC_SENHA = "C3mapaNFe",
                    NUM_PORTA_SMTP = 587,
                    DESC_SMTP = "smtp.cemapa.com",
                    IND_SSL = "N"
                };

                if (wConfigEmail != null)
                {
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = wConfigEmail.DESC_SMTP;
                        smtp.Port = wConfigEmail.NUM_PORTA_SMTP;
                        smtp.EnableSsl = wConfigEmail.IND_SSL == "S" ? true : false;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(wConfigEmail.DESC_EMAIL, wConfigEmail.DESC_SENHA);

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(wConfigEmail.DESC_EMAIL);

                            foreach (string destino in eDestinos)
                            {
                                mail.To.Add(new MailAddress(destino));
                            }

                            mail.Subject = assunto;
                            mail.Body = corpo;

                            smtp.Send(mail);
                        }
                    }
                }
            }
            catch (Exception except)
            {
                throw new Exception(except.Message);
            }
        }
    }
}
