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
using Cemapa.Models.Skyhub;
using System.Text.RegularExpressions;
using Cemapa.Controllers.UtilsServidor;
using Cemapa.Controllers.Http;
using Cemapa.Controllers.Email;

namespace Cemapa.Controllers
{
    public class SincronizarSkyhubController : ApiController
    {
        private readonly Entities db = new Entities();
        private readonly Uri SkyhubUri = new Uri("https://api.skyhub.com.br");

        [HttpGet]
        public async Task<HttpResponseMessage> AtualizarStatusConexao(int codFilial, int codProduto = 0)
        {
            try
            {
                //Começa a busca pela sincronização do status de conexão.
                //O status de conexão é o status de um produto que esta na skyhub e mostra se ele está ou não conectado com a b2w.
                //Esse status é importante para quem usa o sistema ter uma idéia dos produtos que estão ok e os que precisam de atenção.

                int wTotalAlterados = 0;

                ControladorExcecoes.Limpa();

                //Busca todas as configurações ativas para se conectar com a API.

                List<TB_CONFIGURACAO_SKYHUB> configuracoesSkyhub = GetConfiguracoes();

                HttpClient Http = new HttpClient
                {
                    BaseAddress = SkyhubUri
                };

                foreach (var configuracaoSkyhub in configuracoesSkyhub)
                {
                    Http.DefaultRequestHeaders.Clear();
                    Http.DefaultRequestHeaders.Accept.Clear();
                    Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                    Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                    Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Se for específicado um produto para atualizar, então irá atualizar somente ele. Se não for específicado nenhum
                    //irá atualizar todos os status de todos os produtos.

                    if (codProduto > 0)
                    {
                        //Irá buscar pelo produto específicado para atualizar o status da conexão.
                        
                        try
                        {
                            TB_PRODUTO_SKYHUB wProdutoAtualizar = GetProdutosSkyhubPorCodigoProduto(codProduto);

                            HttpResponseMessage response = await Http.GetAsync($"/products/{wProdutoAtualizar.COD_PRODUTO}").Result.Status200OrDie();

                            ProdutoSkyhub produtoSkyhub = await response.Content.ReadAsAsync<ProdutoSkyhub>();

                            //Busca pela chave platform, que contenha o valor B2W, para buscar seu status e atualizar o campo no banco

                            Association associacao = produtoSkyhub.associations.Find(x => x.platform.Contains("B2W"));

                            if (associacao == null)
                            {
                                wProdutoAtualizar.DESC_CONEXAO_MARKETPLACE = "unknow";

                                db.Entry(wProdutoAtualizar).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                wProdutoAtualizar.DESC_CONEXAO_MARKETPLACE = associacao.status;

                                db.Entry(wProdutoAtualizar).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                            wTotalAlterados++;
                        }
                        catch (Exception except)
                        {
                            ControladorExcecoes.Adiciona(except);
                        }
                    }
                    else
                    {
                        //Irá passar por todos os produtos para sincronizar o status da conexão.

                        List<TB_PRODUTO_SKYHUB> todosProdutoSkyhub = GetProdutosSkyhub();

                        foreach (TB_PRODUTO_SKYHUB wProdutoAtualizar in todosProdutoSkyhub)
                        {
                            try
                            {
                                HttpResponseMessage response = await Http.GetAsync($"/products/{wProdutoAtualizar.COD_PRODUTO}").Result.Status200OrDie();

                                ProdutoSkyhub produtoSkyhub = await response.Content.ReadAsAsync<ProdutoSkyhub>();

                                //Busca pela chave platform, que contenha o valor B2W, para buscar seu status e atualizar o campo no banco

                                Association associacao = produtoSkyhub.associations.Find(x => x.platform.Contains("B2W"));

                                if (associacao == null)
                                {
                                    wProdutoAtualizar.DESC_CONEXAO_MARKETPLACE = "unknow";

                                    db.Entry(wProdutoAtualizar).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    wProdutoAtualizar.DESC_CONEXAO_MARKETPLACE = associacao.status;

                                    db.Entry(wProdutoAtualizar).State = EntityState.Modified;
                                    db.SaveChanges();
                                }

                                wTotalAlterados++;
                            }
                            catch (Exception except)
                            {
                                ControladorExcecoes.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}", $"Produto: {wProdutoAtualizar.COD_PRODUTO}" });
                            }
                        }
                    }
                }

                //Biblioteca ControlaExcecoes armazenou todos os erros ocorridos, caso não ocorra nenhum erro,
                //então o método SemExcecoes retornará verdadeiro.

                if (ControladorExcecoes.SemExcecoes())
                {
                    return Request.CreateResponse(
                        HttpStatusCode.OK,
                        $"Os status de conexão foram atualizados. Atualizados: {wTotalAlterados}. "
                    );
                }
                else
                {
                    return Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        ControladorExcecoes.FormatoJson($"Nem todos os status de conexão foram atualizados. Atualizados: {wTotalAlterados}")
                    );
                }
            }
            catch (Exception except)
            {
                ControladorExcecoes.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    ControladorExcecoes.FormatoJson($"Não foi possível começar a atualização")
                );
            }
        }
        
        [HttpGet]
        public async Task<HttpResponseMessage> DownloadEtiqueta(int codFilial, string codMarketplace)
        {
            //Este método busca por PLPs, que nada mais são do que agrupamentos de pedidos para imprimir em etiquetas.
            //Problema que a forma de acessar e imprimir etiquetas pode precisar de algumas chamadas.
            
            try
            {
                bool wAchouPlp = false;
                string wCodigoPlp = null;
                int wMaximoIteracoes = 10;

                if (codFilial == 0)
                {
                    throw new ArgumentNullException("codFilial", "Filial não informada");
                }

                if (String.IsNullOrEmpty(codMarketplace))
                {
                    throw new ArgumentNullException("codMarketplace", "Código do marketplace não informado");
                }

                //Antes de tudo verifica o status do pedido, pedidos que já foram enviados não podem mais gerar etiqueta.

                TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                         where (p.COD_PEDIDO_MARKETPLACE.EndsWith(codMarketplace))
                                         select p).FirstOrDefault();
                if (wPedido != null)
                {
                    if (new List<string>{ "SHIPPED", "DELIVERED", "CANCELED", "FINALIZADO" }.Contains(wPedido.DESC_SITUACAO_MARKETPLACE))
                    {
                        throw new InvalidOperationException($"Status atual do pedido não permite gerar etiquetas");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Erro ao atualizar pedido no sistema. Não encontrado: {codMarketplace}");
                }
                
                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                ConfiguracaoEstaOK(configuracaoSkyhub);

                //Primeira requisição, faz uma chamada para verificar se uma PLP ja existe.

                HttpClient Http = new HttpClient
                {
                    BaseAddress = SkyhubUri
                };

                Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;

                while (!wAchouPlp || wMaximoIteracoes <= 0)
                {
                    response = await Http.GetAsync($"shipments/b2w").Result.Status200OrDie();
                    
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
                        //Caso não encontre a PLP com o código do pedido solicitado, então é ne  cessário
                        //realizar o agrupamento, para depois novamente busca-la.

                        List<string> codigos = new List<string> { codMarketplace };
                        Dictionary<string, List<string>> agrupamentos = new Dictionary<string, List<string>> { { "order_remote_codes", codigos } };
                                
                        response = await Http.PostAsJsonAsync($"shipments/b2w", agrupamentos);

                        if (!response.IsSuccessStatusCode)
                        {
                            //Caso a skyhub já encontre alguma PLP existente para esse pedido, então ele desagrupa e
                            //retorna um erro, porém na próxima chamada existe a certeza de que o pedido será agrupado
                            //em uma nova PLP.

                            await Http.PostAsJsonAsync($"shipments/b2w", agrupamentos).Result.Status200OrDie(); 
                        }
                    }

                    //Variavel controla numero máximo de iterações do loop, para não resultar em uma chamada sem fim.

                    wMaximoIteracoes--;
                }

                //Finalmente recupera a etiqueta, enviando o código da PLP encontrada anteriormente.

                response = await Http.GetAsync($"shipments/b2w/view?plp_id={wCodigoPlp}").Result.Status200OrDie();

                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = response.Content
                };
            }
            catch (Exception except)
            {
                ControladorExcecoes.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    ControladorExcecoes.FormatoJson($"Erro ao buscar etiquetas")
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
                    throw new ArgumentNullException("codFilial", "Filial não informada");
                }

                if (String.IsNullOrEmpty(codMarketplace))
                {
                    throw new ArgumentNullException("codMarketplace", "Código do marketplace não informado");
                }

                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                ConfiguracaoEstaOK(configuracaoSkyhub);

                HttpClient Http = new HttpClient
                {
                    BaseAddress = SkyhubUri
                };

                Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await Http.GetAsync($"/orders/{codMarketplace}").Result.Status200OrDie();
                
                Order ordem = await response.Content.ReadAsAsync<Order>();

                if (ordem != null)
                {
                    //Se houve uma resposta de sucesso, atualiza o campo DESC_SITUACAO_SKYHUB na tabela TB_PEDIDO_CAB

                    TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                                where (p.COD_PEDIDO_MARKETPLACE == codMarketplace)
                                                select p).FirstOrDefault();
                    if (wPedido != null)
                    {
                        //Pedidos com o status FINALIZADO não devem mais ser alterados.

                        if(wPedido.DESC_SITUACAO_MARKETPLACE != "FINALIZADO")
                        {
                            wPedido.DESC_SITUACAO_MARKETPLACE = ordem.status.type;
                            db.Entry(wPedido).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"Erro ao atualizar pedido no sistema. Não encontrado: {codMarketplace}");
                    }
                }
                else
                {
                    Error body = await response.Content.ReadAsAsync<Error>();

                    throw new HttpListenerException((int)response.StatusCode, $"Erro ao solicitar pedido {codMarketplace}. Conteúdo: {body.error}");
                }

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    "Pedido atualizado"
                );
            }
            catch (Exception except)
            {
                ControladorExcecoes.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    ControladorExcecoes.FormatoJson($"Não foi possível começar a atualização")
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
                    throw new ArgumentNullException("codFilial", "Filial não informada");
                }

                if (String.IsNullOrEmpty(codMarketplace))
                {
                    throw new ArgumentNullException("codMarketplace", "Código do marketplace não informado");
                }

                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                ConfiguracaoEstaOK(configuracaoSkyhub);

                HttpClient Http = new HttpClient
                {
                    BaseAddress = SkyhubUri
                };

                Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                ConfiguracaoEstaOK(configuracaoSkyhub);

                object data = null;
                await Http.PostAsJsonAsync($"/orders/{codMarketplace}/cancel", data).Result.Status200OrDie();
                
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
                    throw new InvalidOperationException($"Erro ao atualizar pedido no sistema. Não encontrado: {codMarketplace}");
                }

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    "Pedido cancelado"
                );
            }
            catch (Exception except)
            {
                ControladorExcecoes.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    ControladorExcecoes.FormatoJson($"Não foi possível começar a atualização")
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> DetalhesPedido(int codFilial, string codMarketplace)
        {
            //Este método retorna o JSON completo do pedido como ele está na B2W
            
            try
            {
                if (codFilial == 0)
                {
                    throw new FormatException("Filial não informada");
                }

                if (String.IsNullOrEmpty(codMarketplace))
                {
                    throw new FormatException("Código do marketplace não informado");
                }

                //Encontra a filial para buscar informações de acesso.

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                ConfiguracaoEstaOK(configuracaoSkyhub);

                HttpClient Http = new HttpClient
                {
                    BaseAddress = SkyhubUri
                };

                Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                ConfiguracaoEstaOK(configuracaoSkyhub);

                HttpResponseMessage response = await Http.GetAsync($"/orders/{codMarketplace}").Result.Status200OrDie();
                
                Order ordem = await response.Content.ReadAsAsync<Order>();

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    ordem
                );
            }
            catch (Exception except)
            {
                ControladorExcecoes.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    ControladorExcecoes.FormatoJson($"Não foi possível começar a atualização")
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> FaturaPedido(int codFilial, string chaveNFE, string codMarketplace)
        {
            //Este método atualiza o status do pedido para INVOICED, enviando a chave de acesso da nota fiscal para a skyhub.

            try
            {
                if (chaveNFE.Length != 44)
                {
                    throw new ArgumentNullException("chaveNFE", "Chave da NFE inválida");
                }

                if (codFilial == 0)
                {
                    throw new ArgumentNullException("codFilial", "Filial não informada");
                }

                if (String.IsNullOrEmpty(codMarketplace))
                {
                    throw new ArgumentNullException("codMarketplace", "Código do marketplace não informado");
                }

                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                ConfiguracaoEstaOK(configuracaoSkyhub);

                HttpClient Http = new HttpClient
                {
                    BaseAddress = SkyhubUri
                };

                Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                ConfiguracaoEstaOK(configuracaoSkyhub);

                //Atribui a chave de acesso da nota fiscal à chamada

                Invoice fatura = new Invoice()
                {
                    key = chaveNFE
                };

                //Adiciona a key invoice à chamada, padrão da API

                Dictionary<string, Invoice> data = new Dictionary<string, Invoice> { { "invoice", fatura } };

                await Http.PostAsJsonAsync($"/orders/{codMarketplace}/invoice", data).Result.Status200OrDie();
                   
                //Se houve uma resposta de sucesso, atualiza o campo DESC_SITUACAO_SKYHUB na tabela TB_PEDIDO_CAB

                TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                            where (p.COD_PEDIDO_MARKETPLACE == codMarketplace)
                                            select p).FirstOrDefault();
                if (wPedido != null)
                {
                    wPedido.DESC_SITUACAO_MARKETPLACE = "INVOICED";
                    db.Entry(wPedido).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Erro ao atualizar pedido no sistema. Não encontrado: {codMarketplace}");
                }
                
                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    "Pedido faturado"
                );
            }
            catch (Exception except)
            {
                ControladorExcecoes.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    ControladorExcecoes.FormatoJson($"Não foi possível começar a atualização")
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

                ControladorExcecoes.Limpa();
                ControladorExcecoes.ErrosPersonalizados(true);
                HttpResponseMessage response;

                List<TB_CONFIGURACAO_SKYHUB> configuracoesSkyhub = GetConfiguracoes();

                //Obtém configurações para conectar com a Skyhub, disponivel na tela de parâmetros do sistema, aba Skyhub

                HttpClient Http = new HttpClient
                {
                    BaseAddress = SkyhubUri
                };

                foreach (var configuracaoSkyhub in configuracoesSkyhub)
                {
                    try
                    {
                        ConfiguracaoEstaOK(configuracaoSkyhub);

                        Http.DefaultRequestHeaders.Clear();
                        Http.DefaultRequestHeaders.Accept.Clear();
                        Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                        Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                        Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                        Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        //Busca por um pedido na fila
                        //A fila é usada para consumir pedidos em ordem. Após consumir um pedido, é necessário em até 5 minutos,
                        //deleta-lo da fila para que a API entenda que este pedido foi salvo com sucesso no sistema.

                        response = await Http.GetAsync("/queues/orders").Result.Status200OrDie();
                        
                        Order ordem = await response.Content.ReadAsAsync<Order>();

                        if (ordem != null)
                        {
                            //Verifica se já existe esse pedido no sistema pelo campo COD_PEDIDO_MARKETPLACE
                            //caso não exista insere um pedido novo, caso exista não faz nada.

                            switch (ordem.status.type)
                            {
                                case "APPROVED":
                                    //Pedidos com status APPROVED já obtiveram o pagamento devido então precisa sincronizar
                                                
                                    if (db.TB_PEDIDO_CAB.FirstOrDefault(p => p.COD_PEDIDO_MARKETPLACE == ordem.code) == null)
                                    {
                                        //Gera os códigos necessários para gerar um pedido no sistema
                                                    
                                        TB_CADASTRO wCadastro = SelecionaComprador(
                                            ordem,
                                            configuracaoSkyhub
                                        );

                                        TB_SEQUENCIA wSequencia = SelecionaSequencia(configuracaoSkyhub.COD_FILIAL);

                                        int wCodigoPedido = db.Database.SqlQuery<int>("SELECT SQPEDIDO.NEXTVAL FROM DUAL").First();
                                                    
                                        //Cria o novo pedido trazendo dados da API e algumas informações padrões no sistema

                                        TB_PEDIDO_CAB wPedidoCab = new TB_PEDIDO_CAB()
                                        {
                                            COD_PEDIDO_CAB = wCodigoPedido,
                                            COD_FILIAL = Convert.ToInt32(configuracaoSkyhub.COD_FILIAL),
                                            COD_OPERACAO = Convert.ToInt32(configuracaoSkyhub.COD_OPERACAO),
                                            COD_CADASTRO = wCadastro.COD_CADASTRO,
                                            NUM_PEDIDO = wSequencia.VAL_SEQUENCIA,
                                            DT_EMISSAO = ordem.updated_at,
                                            IND_SITUACAO = "1",
                                            COD_DEPARTAMENTO = configuracaoSkyhub.COD_DEPARTAMENTO,
                                            IND_TIPO_PAGAMENTO = configuracaoSkyhub.IND_TIPO_PAGAMENTO,
                                            COD_VENDEDOR = configuracaoSkyhub.COD_VENDEDOR,
                                            IND_TIPO_FRETE = "CIF",
                                            COD_PEDIDO_MARKETPLACE = ordem.code,
                                            DESC_COMPLEMENTO_OBS = "Pedido do marketplace: " + ordem.code,
                                            DESC_SITUACAO_MARKETPLACE = "APPROVED",
                                            PERC_COMISSAO = 0,
                                            VAL_FRETE_MARKETPLACE = Convert.ToDecimal(ordem.shipping_cost)
                                        };

                                        foreach (Item item in ordem.items)
                                        {
                                            int wCodItem = db.Database.SqlQuery<int>("SELECT SQPEDIDO_ITEM.NEXTVAL FROM DUAL").First();
                                            TB_PRODUTO wProdutoSistema = SelecionaProduto(item);

                                            wPedidoCab.TB_PEDIDO_ITEM.Add(
                                                new TB_PEDIDO_ITEM()
                                                {
                                                    COD_PEDIDO_ITEM = wCodItem,
                                                    COD_PEDIDO_CAB = wCodigoPedido,
                                                    COD_PRODUTO = wProdutoSistema.COD_PRODUTO,
                                                    COD_TRIBUTACAO = wProdutoSistema.COD_TRIBUTACAO,
                                                    COD_LOTE_TIPO = configuracaoSkyhub.COD_LOTE_TIPO,
                                                    VAL_UNITARIO = Convert.ToDecimal(item.original_price),
                                                    QT_PEDIDO = item.qty
                                                }
                                            );
                                        }

                                        //O seguinte if foi adicionado pois pela manhã, boa parte das vezes, os pedidos eram criados duas vezes
                                        //no sistema. Minha teoria é que, como o servidor é lento ao iniciar, talvez 2 requisições estivessem
                                        //sendo ocorridas no determinado instante, adicionando 2 vezes o mesmo pedido.

                                        if (db.TB_PEDIDO_CAB.FirstOrDefault(p => p.COD_PEDIDO_MARKETPLACE == ordem.code) == null)
                                        {
                                            db.Entry(wPedidoCab).State = EntityState.Added;
                                            db.SaveChanges();
                                            wTotalCriados++;

                                            NotificaResponsaveis(configuracaoSkyhub, ordem);
                                        }
                                    }
                                break;

                                case "CANCELED":

                                    //Pedidos com status CANCELED são necessários para saber quando um cliente cancelou sua compra

                                    if (db.TB_PEDIDO_CAB.FirstOrDefault(p => p.COD_PEDIDO_MARKETPLACE == ordem.code) != null)
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

                                case "INVOICED":
                                    //Pedidos com status INVOICE provavelmente não ocorrerão, pois esse status é acionado pelo vendedor

                                    if (db.TB_PEDIDO_CAB.FirstOrDefault(p => p.COD_PEDIDO_MARKETPLACE == ordem.code) != null)
                                    {
                                        TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);
                                        wPedidoCab.DESC_SITUACAO_MARKETPLACE = "INVOICED";

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

                                    if (db.TB_PEDIDO_CAB.FirstOrDefault(p => p.COD_PEDIDO_MARKETPLACE == ordem.code) != null)
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

                                    if (db.TB_PEDIDO_CAB.FirstOrDefault(p => p.COD_PEDIDO_MARKETPLACE == ordem.code) != null)
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

                                    if (db.TB_PEDIDO_CAB.FirstOrDefault(p => p.COD_PEDIDO_MARKETPLACE == ordem.code) != null)
                                    {
                                        TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);
                                        wPedidoCab.DESC_SITUACAO_MARKETPLACE = "SHIPMENT_EXCEPTION";

                                        db.Entry(wPedidoCab).State = EntityState.Modified;
                                        db.SaveChanges();

                                        wTotalAlterados++;
                                    }
                                break;
                            }

                            //Após consumir um pedido, é necessário confirmar com a API que ele foi consumido com sucesso.
                            //A API aguarda 5 minutos antes de jogar o pedido pro final da fila para ser consumido novamente.
                                    
                            await Http.DeleteAsync($"/queues/orders/{ordem.code}").Result.Status200OrDie();
                        }
                    }
                    catch (Exception except)
                    {
                        ControladorExcecoes.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}" });
                    }
                }

                //Biblioteca ControlaExcecoes armazenou todos os erros ocorridos, caso não ocorra nenhum erro,
                //então o método SemExcecoes retornará verdadeiro.

                if (ControladorExcecoes.SemExcecoes())
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
                        ControladorExcecoes.FormatoJson($"Nem todos os pedidos foram sincronizados. Criados: {wTotalCriados}, Cancelados: {wTotalCancelados}, Alterados: {wTotalAlterados}")
                    );
                }
            }
            catch (Exception except)
            {
                ControladorExcecoes.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    ControladorExcecoes.FormatoJson($"Não foi possível começar a sincronização")
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

                ControladorExcecoes.Limpa();
                ControladorExcecoes.ErrosPersonalizados(true);
                HttpResponseMessage response;

                //Busca sincronizações de produtos pendentes (campo IND_SINCRONIZADO esteja igual a "N") na tabela TB_SINCRONIZACAO_SKYHUB.

                List<TB_SINCRONIZACAO_SKYHUB> sincronizacoesSkyhub = GetSincronizacoes();
                
                HttpClient Http = new HttpClient
                {
                    BaseAddress = SkyhubUri
                };

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

                                ConfiguracaoEstaOK(configuracaoSkyhub);
                                
                                Http.DefaultRequestHeaders.Clear();
                                Http.DefaultRequestHeaders.Accept.Clear();
                                Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                                Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                                Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                List<TB_PRODUTO_SKYHUB> produtosSkyhub = GetProdutosSkyhub(sincronizacaoSkyhub);

                                if (produtosSkyhub.Count > 0)
                                {
                                    foreach (TB_PRODUTO_SKYHUB produtoSkyhub in produtosSkyhub)
                                    {
                                        try
                                        {
                                            //Verifica se os dados atuais deste produto precisam ser atualizados antes de sincronizar.

                                            TB_PRODUTO wInfosProduto = InfosProduto(produtoSkyhub);
                                            decimal wTotalEstoque = TotalEstoque(configuracaoSkyhub, produtoSkyhub);

                                            //Faz algumas verificações em alguns campos antes de sincronizar.
                                            //Caso não esteja tudo ok, este produto não será sincronizado

                                            ProdutoEstaOK(produtoSkyhub);

                                            //Instancia o produto da Classe ProdutoSkyhub, criada conforme a estrutura especificada no manual da API.

                                            ProdutoSkyhub ProdutoSku = new ProdutoSkyhub
                                            {
                                                sku = produtoSkyhub.COD_PRODUTO,
                                                name = produtoSkyhub.DESC_PRODUTO,
                                                description = produtoSkyhub.DESC_DESCRICAO,
                                                status = produtoSkyhub.DESC_STATUS,
                                                qty = Convert.ToInt32(wTotalEstoque),
                                                weight = Convert.ToDouble(produtoSkyhub.VAL_PESO),
                                                width = Convert.ToDouble(produtoSkyhub.VAL_LARGURA),
                                                height = Convert.ToDouble(produtoSkyhub.VAL_ALTURA),
                                                length = Convert.ToDouble(produtoSkyhub.VAL_COMPRIMENTO),
                                                brand = produtoSkyhub.DESC_MARCA,
                                                ean = wInfosProduto.DESC_COD_BARRA,
                                                nbm = wInfosProduto.NUM_GENERO_NCM
                                            };
                                            
                                            //À pedido do Cesar 19/02/2020, usar o valor de atacado.

                                            if (wInfosProduto.VAL_ATACADO > 0)
                                            {
                                                ProdutoSku.price = Convert.ToDouble(wInfosProduto.VAL_ATACADO);
                                            }
                                            else
                                            {
                                                ProdutoSku.price = Convert.ToDouble(wInfosProduto.VAL_VAREJO);
                                            }

                                            ProdutoSku.categories.Add(
                                                new Models.Skyhub.Category
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
                                                TB_PRODUTO wInfosVariacao = InfosProduto(variacao);
                                                decimal wTotalEstoqueVar = TotalEstoque(configuracaoSkyhub, variacao);

                                                Variation wProdutoVariacao = new Variation
                                                {
                                                    sku = variacao.COD_PRODUTO,
                                                    qty = Convert.ToInt32(wTotalEstoqueVar),
                                                    ean = wInfosVariacao.DESC_COD_BARRA
                                                };
                                                
                                                //À pedido do Cesar 19/02/2020, usar o valor de atacado.

                                                if (wInfosProduto.VAL_ATACADO > 0)
                                                {
                                                    ProdutoSku.price = Convert.ToDouble(wInfosProduto.VAL_ATACADO);
                                                }
                                                else
                                                {
                                                    ProdutoSku.price = Convert.ToDouble(wInfosProduto.VAL_VAREJO);
                                                }

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
                                                
                                                //À pedido do Cesar 19/02/2020, usar o valor de atacado.

                                                if (wInfosVariacao.VAL_ATACADO > 0)
                                                {
                                                    wProdutoVariacao.specifications.Add(
                                                        new Specification
                                                        {
                                                            key = "price",
                                                            value = Convert.ToString(wInfosVariacao.VAL_ATACADO)
                                                        }
                                                    );
                                                }
                                                else
                                                {
                                                    wProdutoVariacao.specifications.Add(
                                                        new Specification
                                                        {
                                                            key = "price",
                                                            value = Convert.ToString(wInfosVariacao.VAL_VAREJO)
                                                        }
                                                    );
                                                }


                                                wProdutoVariacao.AddImagesCustom(variacao.DESC_LINK_IMAGEM_1);
                                                wProdutoVariacao.AddImagesCustom(variacao.DESC_LINK_IMAGEM_2);
                                                wProdutoVariacao.AddImagesCustom(variacao.DESC_LINK_IMAGEM_3);
                                                wProdutoVariacao.AddImagesCustom(variacao.DESC_LINK_IMAGEM_4);
                                                        
                                                ProdutoSku.variations.Add(wProdutoVariacao);
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
                                                        //Caso não encontre um produto para atualizar, então adiciona.

                                                        Resposta.Status404Previsto();
                                                        response = await Http.PutAsJsonAsync("/products/" + ProdutoSku.sku, products).Result.Status200OrDie();

                                                        if (response.StatusCode == HttpStatusCode.NotFound)
                                                        {
                                                            await Http.PostAsJsonAsync("/products", products).Result.Status200OrDie();
                                                            totalCriados++;
                                                        }
                                                        else
                                                        {
                                                            totalAtualizados++;
                                                        }
                                                    }
                                                    break;
                                                case "DELETE":
                                                    {
                                                        Resposta.Status404Previsto();
                                                        await Http.DeleteAsync("/products/" + ProdutoSku.sku).Result.Status200OrDie();
                                                        totalDeletados++;

                                                    }
                                                    break;
                                                case "POST":
                                                    {
                                                        await Http.PostAsJsonAsync("/products", products).Result.Status200OrDie();
                                                        totalCriados++;
                                                    }
                                                    break;
                                            }

                                            //Altera o campo da sincronização para o produto não ficar sincronizando eternamente.
                                            //Também salva este produto recentemente atualizado. Isso é uma forma preventiva para que em uma lista de sincronizações,
                                            //caso haja algum item com problema de sincronização, não fique trancando essa fila, sendo necessário sincronizar tudo novamente.

                                            sincronizacaoSkyhub.DT_SINCRONIZACAO = DateTime.Now;
                                            sincronizacaoSkyhub.IND_SINCRONIZADO = "S";
                                            db.SaveChanges();

                                        }
                                        catch (Exception except)
                                        {
                                            ControladorExcecoes.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}", $"Produto: {produtoSkyhub.COD_PRODUTO}" });
                                        }
                                    }
                                }
                                else
                                {
                                    //Caso não tenha sido encontrado nenhum registro da tabela TB_PRODUTO_SKYHUB referente a sincronização atual,
                                    //então o produto deve ser apagado da API também, ignorando o método pedido pela sincronização.
                                    
                                    Resposta.Status404Previsto();
                                    await Http.DeleteAsync("/products/" + sincronizacaoSkyhub.COD_PRODUTO).Result.Status200OrDie();
                                    totalDeletados++;
                                }
                            }
                            catch (Exception except)
                            {
                                ControladorExcecoes.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}", $"Sincronização: {sincronizacaoSkyhub.COD_SINCRONIZACAO_SKYHUB}" });
                            }
                        }
                    }
                    catch (Exception except)
                    {
                        ControladorExcecoes.Adiciona(except, new List<string> { $"Sincronização: {sincronizacaoSkyhub.COD_SINCRONIZACAO_SKYHUB}" });
                    }
                }

                //Biblioteca ControlaExcecoes armazenou todos os erros ocorridos, caso não ocorra nenhum erro,
                //então o método SemExcecoes retornará verdadeiro.

                if (ControladorExcecoes.SemExcecoes())
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
                        ControladorExcecoes.FormatoJson($"Nem todos os pedidos foram sincronizados. Criados: {totalCriados}, Atualizados: {totalAtualizados}, Removidos: {totalDeletados}")
                    );
                }
            }
            catch (Exception except)
            {
                ControladorExcecoes.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    ControladorExcecoes.FormatoJson($"Não foi possível começar a sincronização")
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

        private List<TB_PRODUTO_SKYHUB> GetProdutosSkyhub(TB_SINCRONIZACAO_SKYHUB sincronizacaoSkyhub)
        {
            return (from produtoSkyhub in db.TB_PRODUTO_SKYHUB
                    where produtoSkyhub.COD_PRODUTO == sincronizacaoSkyhub.COD_PRODUTO
                    select produtoSkyhub).ToList();
        }

        private List<TB_PRODUTO_SKYHUB> GetProdutosSkyhub()
        {
            return (from produtoSkyhub in db.TB_PRODUTO_SKYHUB
                    select produtoSkyhub).ToList();
        }

        private TB_PRODUTO_SKYHUB GetProdutosSkyhubPorCodigoProduto(int codProduto)
        {
            return (from produtoSkyhub in db.TB_PRODUTO_SKYHUB
                    where (produtoSkyhub.COD_PRODUTO == codProduto)
                    select produtoSkyhub).FirstOrDefault();
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

        private TB_PRODUTO InfosProduto(TB_PRODUTO_SKYHUB produtoSkyhub)
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

        private void ProdutoEstaOK(TB_PRODUTO_SKYHUB produtoSkyhub)
        {
            if (produtoSkyhub.VAL_ALTURA < 0)
                throw new ArgumentException($"Produto marketplace: altura inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (produtoSkyhub.VAL_COMPRIMENTO < 0)
                throw new ArgumentException($"Produto marketplace: comprimento inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (produtoSkyhub.VAL_LARGURA < 0)
                throw new ArgumentException($"Produto marketplace: Altura inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.DESC_DESCRICAO))
                throw new ArgumentException($"Produto marketplace: ficha técnica inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.DESC_MARCA))
                throw new ArgumentException($"Produto marketplace: marca inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.DESC_PRODUTO))
                throw new ArgumentException($"Produto marketplace: descrição do produto inválido, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.DESC_STATUS))
                throw new ArgumentException($"Produto marketplace: status inválido, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.DESC_CATEGORIA))
                throw new ArgumentException($"Produto marketplace: categoria inválida, produto: {produtoSkyhub.COD_PRODUTO}");
        }

        private void ConfiguracaoEstaOK(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub)
        {
            if (configuracaoSkyhub == null)
                throw new ArgumentException($"Configuração skyhub não encontrada para filial informada");
            if (configuracaoSkyhub.COD_CLASS_CADASTRO < 0)
                throw new ArgumentException($"Configuração skyhub: classe de cadastro inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (configuracaoSkyhub.COD_DEPARTAMENTO < 0)
                throw new ArgumentException($"Configuração skyhub: departamento inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (configuracaoSkyhub.COD_LOTE_TIPO < 0)
                throw new ArgumentException($"Configuração skyhub: lote inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (configuracaoSkyhub.COD_RAMO_FISICA < 0)
                throw new ArgumentException($"Configuração skyhub: ramo para pessoa física inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (configuracaoSkyhub.COD_RAMO_JURIDICA < 0)
                throw new ArgumentException($"Configuração skyhub:  ramo para pessoa jurídica inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (configuracaoSkyhub.COD_VENDEDOR < 0)
                throw new ArgumentException($"Configuração skyhub: vendedor inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (String.IsNullOrEmpty(configuracaoSkyhub.IND_TIPO_PAGAMENTO))
                throw new ArgumentException($"Configuração skyhub: tipo de pagamento inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (String.IsNullOrEmpty(configuracaoSkyhub.DESC_TOKEN_ACCOUNT))
                throw new ArgumentException($"Configuração skyhub: token inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (String.IsNullOrEmpty(configuracaoSkyhub.DESC_TOKEN_INTEGRACAO))
                throw new ArgumentException($"Configuração skyhub: token inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (String.IsNullOrEmpty(configuracaoSkyhub.DESC_USUARIO_EMAIL))
                throw new ArgumentException($"Configuração skyhub: e-mail inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
        }
        
        private TB_PEDIDO_CAB GetPedidoPorMarketplace(Order ordem)
        {
            TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                     where (p.COD_PEDIDO_MARKETPLACE == ordem.code)
                                     select p).FirstOrDefault();
            return wPedido;
        }

        private TB_CADASTRO SelecionaComprador(Order ordem, TB_CONFIGURACAO_SKYHUB configuracao)
        {
            //Pré-processa os dados, formatando-os para nosso sistema.

            ordem.shipping_address.region = ordem.shipping_address.region.ToUpper();
            ordem.customer.name = ordem.customer.name.ToUpper();
            ordem.customer.gender = ordem.customer.gender == "male" ? "M" : "F";
            ordem.shipping_address.neighborhood = ordem.shipping_address.neighborhood.ToUpper();
            ordem.shipping_address.street = ordem.shipping_address.street.ToUpper();
            ordem.shipping_address.complement = ordem.shipping_address.complement?.ToUpper();
            ordem.shipping_address.postcode = String.Format(@"{0:00000\-000}", Convert.ToInt64(ordem.shipping_address.postcode));
            ordem.shipping_address.city = ordem.shipping_address.city.ToUpper();

            int qtCpfCnpj = Regex.Replace(ordem.customer.vat_number, "[^0-9]", "").Length;
            string indCgc = "F";

            if (qtCpfCnpj == 11)
            {
                ordem.customer.vat_number = String.Format(@"{0:000\.000\.000\-00}", Convert.ToInt64(ordem.customer.vat_number));
                ordem.customer.state_registration = "";
                indCgc = "F";
            }
            else if (qtCpfCnpj == 14)
            {
                ordem.customer.vat_number = String.Format(@"{0:00\.000\.000/0000\-00}", Convert.ToInt64(ordem.customer.vat_number));
                ordem.customer.state_registration = ordem.customer.state_registration.ToUpper();
                indCgc = "J";
            }

            //Busca o comprador no banco de dados. Caso não encontre, começa o processo de cadastro.
            //Caso encontre, atualiza alguns dados.

            TB_CADASTRO wCadastro = (from c in db.TB_CADASTRO
                                     where (c.NUM_CGC_CPF == ordem.customer.vat_number)
                                     select c).FirstOrDefault();

            //Busca a cidade para esse comprador, Caso não encontre, também cadastra.

            TB_CIDADE wCidade = (from c in db.TB_CIDADE
                                 where (c.DESC_CIDADE == ordem.shipping_address.city)
                                 select c).FirstOrDefault();
            if (wCidade == null)
            {
                int wCodigoCidade = db.Database.SqlQuery<int>("SELECT SQCIDADE.NEXTVAL FROM DUAL").First();

                wCidade = new TB_CIDADE()
                {
                    COD_CIDADE = wCodigoCidade,
                    DESC_CIDADE = ordem.shipping_address.city,
                    COD_ESTADO = ordem.shipping_address.region
                };

                db.Entry(wCidade).State = EntityState.Added;
            }

            if (wCadastro == null)
            {
                int wCodigoCadastro = db.Database.SqlQuery<int>("SELECT SQCADASTRO.NEXTVAL FROM DUAL").First();

                wCadastro = new TB_CADASTRO()
                {
                    COD_CADASTRO = wCodigoCadastro,
                    NOME = ordem.customer.name,
                    COD_TIPO_CADASTRO = Convert.ToInt32(configuracao.COD_TIPO_CADASTRO),
                    NUM_CGC_CPF = ordem.customer.vat_number,
                    DESC_E_MAIL = ordem.customer.email,
                    DT_NASCIMENTO = Convert.ToDateTime(ordem.customer.date_of_birth),
                    DT_CADASTRO = DateTime.Now,
                    DESC_CELULAR = ordem.shipping_address.secondary_phone,
                    DESC_TELEFONE = ordem.shipping_address.phone,
                    DESC_ENDERECO = $"{ordem.shipping_address.street.ToUpper()}, {ordem.shipping_address.number}",
                    DESC_ENDERECO_COBRANCA = $"{ordem.shipping_address.street.ToUpper()}, {ordem.shipping_address.number}",
                    DESC_COMPLEMENTO = ordem.shipping_address.complement,
                    DESC_BAIRRO = ordem.shipping_address.neighborhood,
                    DESC_BAIRRO_COBRANCA = ordem.shipping_address.neighborhood.Truncate(20),
                    COD_ESTADO = ordem.shipping_address.region,
                    DESC_ESTADO_COBRANCA = ordem.shipping_address.region,
                    IND_SEXO_CATEGORIA = ordem.customer.gender,
                    IND_FISICA_JURIDICA = indCgc,
                    COD_CIDADE = wCidade.COD_CIDADE,
                    DESC_CIDADE = ordem.shipping_address.city,
                    DESC_CEP = ordem.shipping_address.postcode,
                    DESC_CEP_COBRANCA = ordem.shipping_address.postcode,
                    COD_RAMO = indCgc == "J" ? configuracao.COD_RAMO_JURIDICA : configuracao.COD_RAMO_FISICA,
                    COD_CLASS_CADASTRO = configuracao.COD_CLASS_CADASTRO,
                    IND_LIBERA_VENDA = "NAO",
                    IND_LIBERA_BLOQUETO = "NAO",
                    IND_PIS_COFINS_RETIDO = "NAO",
                    IND_ATACADO_VAREJO = "V",
                    IND_CONSUMIDOR_FINAL = "N",
                    NUM_INSCRICAO = "",
                    COD_FILIAL = configuracao.COD_FILIAL,
                    COD_REGIAO = configuracao.COD_REGIAO,
                    COD_USUARIO = 1,
                    COD_PAIS = 1,
                    IND_REGIME_TRIBUTARIO = 3,
                    IND_ISS_RETIDO = 2,
                    PERC_CAP_TOT = 0,
                    PERC_CAP_VOT = 0
                };

                db.Entry(wCadastro).State = EntityState.Added;
            }
            else
            {
                wCadastro.NOME = ordem.customer.name;
                wCadastro.DESC_E_MAIL = ordem.customer.email;
                wCadastro.DESC_CELULAR = ordem.shipping_address.secondary_phone;
                wCadastro.DESC_TELEFONE = ordem.shipping_address.phone;
                wCadastro.DESC_ENDERECO = $"{ordem.shipping_address.street.ToUpper()}, {ordem.shipping_address.number}";
                wCadastro.DESC_COMPLEMENTO = ordem.shipping_address.complement;
                wCadastro.DESC_BAIRRO = ordem.shipping_address.neighborhood;
                wCadastro.COD_ESTADO = ordem.shipping_address.region;
                wCadastro.COD_CIDADE = wCidade.COD_CIDADE;
                wCadastro.DESC_CIDADE = ordem.shipping_address.city;
                wCadastro.DESC_CEP = ordem.shipping_address.postcode;

                db.Entry(wCadastro).State = EntityState.Modified;
            }

            db.SaveChanges();

            return wCadastro;
        }

        private TB_PRODUTO SelecionaProduto(Item item)
        {
            //Busca pelo produto no sistema a partir do item de uma compra.
            //Tais valores serão utilizados para adicionar o item ao pedido.
            //Caso o produto não seja encontrado no sistema, então cadastra conforme informações da skyhub.
            long id = Convert.ToInt64(item.id);

            TB_PRODUTO wProduto = (from p in db.TB_PRODUTO
                                    where (p.COD_PRODUTO == id)
                                    select p).FirstOrDefault();
            if(wProduto == null)
            {
                //Com as informações do item, pré-processa os dados para salvar um novo produto no sistema.
                //Este produto será trazido apenas para cadastro do pedido, ele necessita de atenção.
                //Esse caso ocorreu devido a um produto que existe na skyhub mas foi apagado do sistema, dessa forma,
                //quando baixar um pedido, este produto precisa ser criado.

                item.name = $"(PRODUTO EXISTENTE APENAS NA SKYHUB!){item.name.ToUpper()}";

                TB_CLASSE wClasse = (from c in db.TB_CLASSE select c).FirstOrDefault();
                TB_TRIBUTACAO wTributacao = (from t in db.TB_TRIBUTACAO select t).FirstOrDefault();

                wProduto = new TB_PRODUTO
                {
                    COD_PRODUTO = Convert.ToInt64(item.id),
                    DESC_PRODUTO = item.name,
                    VAL_VAREJO = Convert.ToDecimal(item.special_price),
                    COD_TRIBUTACAO = wTributacao.COD_TRIBUTACAO,
                    COD_CLASSE = wClasse.COD_CLASSE,
                    IND_TIPO_PRODUTO = "Comercial",
                    PERC_ISS = 0,
                    PERC_INSS = 0
                };

                db.Entry(wProduto).State = EntityState.Added;
                db.SaveChanges();
            }

            return wProduto;
        }

        private TB_SEQUENCIA SelecionaSequencia(int codFilial)
        {
            //Busca valor da sequência para pedidos. Caso não encontre, cadastra uma nova.

            TB_SEQUENCIA wSequencia = (from s in db.TB_SEQUENCIA
                                where ((s.NOME_SEQUENCIA == "PEDIDO") && (s.COD_FILIAL == codFilial))
                                select s).FirstOrDefault();
            if (wSequencia == null)
            {
                wSequencia = new TB_SEQUENCIA
                {
                    VAL_SEQUENCIA = 1,
                    COD_FILIAL = codFilial,
                    NOME_SEQUENCIA = "PEDIDO"
                };

                db.Entry(wSequencia).State = EntityState.Added;
            }
            else
            {
                wSequencia.VAL_SEQUENCIA++;
                db.Entry(wSequencia).State = EntityState.Modified;
            }

            db.SaveChanges();

            return wSequencia;
        }

        private void NotificaResponsaveis(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub, Order ordem)
        {
            try
            {
                //Quando um pedido novo for baixado para o sistema, envia um e-mail para os destinatários cadastrados.

                List<string> eDestinatarios = new List<string>();

                foreach (TB_EMAIL_NOTIFICACAO email in configuracaoSkyhub.TB_EMAIL_NOTIFICACAO)
                {
                    eDestinatarios.Add(email.DESC_EMAIL);
                }

                Envio.EnviaEmail(eDestinatarios, "Novo pedido aprovado.", $"Um novo pedido foi aprovado pelo marketplace. Código: {ordem.code}");
            }
            catch
            {
                Console.WriteLine("Erro ao enviar e-mail mas e daí?");
            }
        }
    }
}
