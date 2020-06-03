using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Cemapa.Models;
using Cemapa.Models.MercadoLivre.Usuario;
using Cemapa.Models.MercadoLivre.Products;
using Cemapa.Models.MercadoLivre.Orders;
using System.Data.Entity;
using Cemapa.Controllers.UtilsServidor;
using System.IO;
using Cemapa.Controllers.Http;
using System.Web;
using System.Collections.Specialized;
using Cemapa.Controllers.Email;
using System.Data.Entity.Validation;

using SearchFromProducts = Cemapa.Models.MercadoLivre.Products.Search;
using SearchFromOrders = Cemapa.Models.MercadoLivre.Orders.Search;
using AvailableFilterProducts = Cemapa.Models.MercadoLivre.Products.AvailableFilter;
using OrderFromOrders = Cemapa.Models.MercadoLivre.Orders.Order;
using ShippingFromOrders = Cemapa.Models.MercadoLivre.Orders.Shipping;
using ItemFromProducts = Cemapa.Models.MercadoLivre.Products.Item;
using ShippingFromProducts = Cemapa.Models.MercadoLivre.Products.Shipping;
using AlternativePhoneFromOrder = Cemapa.Models.MercadoLivre.Orders.AlternativePhone;
using PhoneFromOrder = Cemapa.Models.MercadoLivre.Orders.Phone;

namespace Cemapa.Controllers
{
    public class SincronizarMLivreController : ApiController
    {
        private OAuth Autenticacao = new OAuth();
        private readonly Entities db = new Entities();
        private readonly ControladorExcecoes Manipulador = new ControladorExcecoes();
        private readonly Uri MlivreUri = new Uri("https://api.mercadolibre.com");

        [HttpGet]
        public async Task<HttpResponseMessage> AtualizarURLs(int codFilial, int codProduto = 0)
        {
            try
            {
                //Começa a busca pela URL do produto.
                //A URL do produto é o link completo onde o produto esta sendo exibido na loja online.
                
                if (codFilial == 0)
                {
                    throw new ArgumentNullException("codFilial", "Filial não informada");
                }

                if (codProduto == 0)
                {
                    throw new ArgumentNullException("codProduto", "Produto não informado");
                }

                //Busca pela configuração informada para se conectar com a API.

                TB_CONFIGURACAO_SKYHUB configuracao = GetConfiguracao(codFilial);

                ConfiguracaoEstaOK(configuracao);

                HttpClient Http = new HttpClient
                {
                    BaseAddress = MlivreUri
                };

                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;
                NameValueCollection parametros = HttpUtility.ParseQueryString(string.Empty);

                await ValidaAutenticacao(configuracao);

                //Busca pelas URLS em todos os canais da skyhub.
                //Irá buscar pelo produto específicado para atualizar a URL.

                TB_PRODUTO_SKYHUB produto = GetProdutoSkyhub(codProduto);

                if (produto != null)
                {
                    parametros.Clear();
                    parametros["sku"] = Convert.ToString(produto.COD_PRODUTO);
                    parametros["access_token"] = Autenticacao.access_token;

                    response = await Http.GetAsync($"/users/{Autenticacao.user_id}/items/search?{parametros.ToString()}").Result.Status200OrDie();

                    SearchFromProducts search = await response.Content.ReadAsAsync<SearchFromProducts>();

                    if (search.results.Count > 0)
                    {
                        response = await Http.GetAsync($"/items/{search.results.First()}").Result.Status200OrDie();

                        ItemFromProducts produtoSkyhub = await response.Content.ReadAsAsync<ItemFromProducts>();

                        produto.URL_MLIVRE = produtoSkyhub.permalink;

                        db.Entry(produto).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new HttpListenerException(404, $"Produto não encontrado no marketplace({codProduto})");
                    }
                }
                else
                {
                    throw new HttpListenerException(404, $"Produto não encontrado no sistema({codProduto})");
                }

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    $"As URLs foram atualizadas"
                );
            }
            catch (Exception except)
            {
                Manipulador.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    Manipulador.FormatoJson($"Urls não atualizadas")
                );
            }
        }

        [HttpGet]
        public HttpResponseMessage CancelaPedido(int codFilial, string codMarketplace)
        {
            return Request.CreateResponse(
                HttpStatusCode.Forbidden,
                $"Por enquanto essa opção deve ser realizada pelo site do Mercado Livre."
            );
        }

        [HttpGet]
        public HttpResponseMessage FaturaPedido(int codFilial, string chaveNFE, string codMarketplace)
        {
            return Request.CreateResponse(
                HttpStatusCode.Forbidden,
                $"Por enquanto a opção de enviar nota fiscal para o mercado livre não esta disponível"
            );
        }

        [HttpGet]
        public async Task<HttpResponseMessage> DownloadEtiqueta(int codFilial, string codMarketplace)
        {
            try
            {
                //Este método busca código do envio do pedido informado e então baixa um pdf da etiqueta.
                
                if (codFilial == 0)
                {
                    throw new ArgumentNullException("codFilial", "Filial não informada");
                }

                if (String.IsNullOrEmpty(codMarketplace))
                {
                    throw new ArgumentNullException("codMarketplace", "Código do marketplace não informado");
                }

                if (codMarketplace.Contains("Mercado Livre-"))
                {
                    codMarketplace = codMarketplace.Replace("Mercado Livre-", "");
                }

                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                NameValueCollection parametros = HttpUtility.ParseQueryString(string.Empty);

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                ConfiguracaoEstaOK(configuracaoSkyhub);

                await ValidaAutenticacao(configuracaoSkyhub);

                HttpClient Http = new HttpClient
                {
                    BaseAddress = MlivreUri
                };

                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                parametros.Clear();
                parametros["access_token"] = Autenticacao.access_token;

                HttpResponseMessage response = await Http.GetAsync($"/orders/{codMarketplace}?{parametros.ToString()}").Result.Status200OrDie();

                OrderFromOrders ordem = await response.Content.ReadAsAsync<OrderFromOrders>();

                if (ordem == null)
                {
                    throw new HttpListenerException((int)response.StatusCode, $"Erro ao buscar pedido no mercado livre({codMarketplace}). {response.Content.ReadAsStringAsync().Result}");
                }

                response = await Http.GetAsync($"/shipments/{ordem.shipping.id}?{parametros.ToString()}").Result.Status200OrDie();

                ordem.shipping = await response.Content.ReadAsAsync<ShippingFromOrders>();
                
                if (ordem.shipping.status != "ready_to_ship")
                {
                    throw new InvalidOperationException($"Status atual do pedido não permite gerar etiquetas({ordem.shipping.status})");
                }

                Http.DefaultRequestHeaders.Accept.Clear();
                
                response = await Http.GetAsync($"/shipment_labels?shipment_ids={ordem.shipping.id}&response_type=pdf&access_token={Autenticacao.access_token}").Result.Status200OrDie();

                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = response.Content
                };
            }
            catch (Exception except)
            {
                Manipulador.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    Manipulador.FormatoJson($"Não foi possível baixar a etiqueta")
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> SincronizaStatusPedido(int codFilial, string codMarketplace)
        {
            //Busca pelo pedido no mercadolivre, e atualiza o status para o sistema.
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

                if (codMarketplace.Contains("Mercado Livre-"))
                {
                    codMarketplace = codMarketplace.Replace("Mercado Livre-", "");
                }
                
                NameValueCollection parametros = HttpUtility.ParseQueryString(string.Empty);

                //Encontra a filial para buscar informações de acesso.
                //Também garante que um pedido não irá se misturar com o pedido de outra filial
                //Podendo confundir as contas às quais os pedidos são sincronizados

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);
                
                ConfiguracaoEstaOK(configuracaoSkyhub);

                await ValidaAutenticacao(configuracaoSkyhub);

                HttpClient Http = new HttpClient
                {
                    BaseAddress = MlivreUri
                };
                
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                parametros.Clear();
                parametros["access_token"] = Autenticacao.access_token;

                HttpResponseMessage response = await Http.GetAsync($"/orders/{codMarketplace}?{parametros.ToString()}").Result.Status200OrDie();

                Result ordem = await response.Content.ReadAsAsync<Result>();

                if (ordem == null)
                {
                    throw new HttpListenerException((int)response.StatusCode, $"Erro ao buscar pedido no mercado livre({codMarketplace}). {response.Content.ReadAsStringAsync().Result}");
                }

                response = await Http.GetAsync($"/shipments/{ordem.shipping.id}?{parametros.ToString()}").Result.Status200OrDie();

                ordem.shipping = await response.Content.ReadAsAsync<ShippingFromOrders>();
                
                codMarketplace = $"Mercado Livre-{codMarketplace}";

                TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                            where (p.COD_PEDIDO_MARKETPLACE == codMarketplace)
                                            select p).FirstOrDefault();
                if (wPedido == null)
                {
                    throw new InvalidOperationException($"Erro ao atualizar pedido no sistema. Não encontrado: { codMarketplace }");
                }

                if ((wPedido.DESC_SITUACAO_MARKETPLACE != "FINALIZADO") && (db.TB_PEDIDO_CAB.Any(p => p.COD_PEDIDO_MARKETPLACE == codMarketplace)))
                {
                    //Pedidos com o status FINALIZADO não devem mais ser alterados.

                    TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);

                    switch (ordem.shipping.status)
                    {
                        case "ready_to_ship": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "APPROVED"; break;
                        case "shipped": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "SHIPPED"; break;
                        case "canceled": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "CANCELED"; break;
                        case "delivered": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "DELIVERED"; break;
                    }

                    db.Entry(wPedidoCab).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    "Pedido atualizado"
                );
            }
            catch (Exception except)
            {
                Manipulador.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    Manipulador.FormatoJson($"Não foi possível começar a atualização")
                );
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> DetalhesPedido(int codFilial, string codMarketplace)
        {
            try
            {
                //Este método retorna o JSON completo do pedido como ele está na B2W

                if (codFilial == 0)
                {
                    throw new FormatException("Filial não informada");
                }

                if (String.IsNullOrEmpty(codMarketplace))
                {
                    throw new FormatException("Código do marketplace não informado");
                }

                if(codMarketplace.Contains("Mercado Livre-"))
                {
                    codMarketplace = codMarketplace.Replace("Mercado Livre-", "");
                }

                OrderFromOrders ordem;
                NameValueCollection parametros = HttpUtility.ParseQueryString(string.Empty);

                //Encontra a filial para buscar informações de acesso.

                TB_CONFIGURACAO_SKYHUB configuracaoSkyhub = GetConfiguracao(codFilial);

                ConfiguracaoEstaOK(configuracaoSkyhub);

                await ValidaAutenticacao(configuracaoSkyhub);

                HttpClient Http = new HttpClient
                {
                    BaseAddress = MlivreUri
                };
                
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                parametros.Clear();
                parametros["access_token"] = Autenticacao.access_token;

                HttpResponseMessage response = await Http.GetAsync($"/orders/{codMarketplace}?{parametros.ToString()}").Result.Status200OrDie();

                ordem = await response.Content.ReadAsAsync<OrderFromOrders>();

                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    ordem
                );
            }
            catch (Exception except)
            {
                Manipulador.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    Manipulador.FormatoJson($"Não foi possível começar a atualização")
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

                Manipulador.ErrosPersonalizados(true);
                HttpResponseMessage response;
                NameValueCollection parametros = HttpUtility.ParseQueryString(string.Empty);

                List<TB_CONFIGURACAO_SKYHUB> configuracoesSkyhub = GetConfiguracoes();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //Obtém configurações para conectar com a API, disponivel na tela de parâmetros do sistema.

                HttpClient Http = new HttpClient
                {
                    BaseAddress = MlivreUri
                };
                
                foreach (var configuracaoSkyhub in configuracoesSkyhub)
                {
                    try
                    {
                        ConfiguracaoEstaOK(configuracaoSkyhub);

                        if (configuracaoSkyhub.DT_ULTIMA_ATUALIZACAO_ML == null)
                        {
                            configuracaoSkyhub.DT_ULTIMA_ATUALIZACAO_ML = DateTime.Now.AddDays(-1);
                        }

                        await ValidaAutenticacao(configuracaoSkyhub);

                        Http.DefaultRequestHeaders.Accept.Clear();
                        Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        //Para buscar por atualizações de pedidos buscamos por um intervalo de data especifico.
                        //Assim que todos os pedidos nesse período estiver sincronizados, atualiza-se o campo DT_ULTIMA_ATUALIZACAO_ML.
                        //O mercadolivre considera a data e hora ao buscar pelos pedidos.

                        parametros.Clear();
                        parametros["seller"] = Autenticacao.user_id;
                        parametros["access_token"] = Autenticacao.access_token;
                        parametros["order.date_last_updated.from"] = Convert.ToDateTime(configuracaoSkyhub.DT_ULTIMA_ATUALIZACAO_ML).ToString("yyyy-MM-ddTHH:00:00.000-00:00");

                        response = await Http.GetAsync($"/orders/search?{parametros.ToString()}").Result.Status200OrDie();

                        SearchFromOrders search = await response.Content.ReadAsAsync<SearchFromOrders>();

                        //Esta linha vai filtrar por ordens que tenha produtos com sku cadastrado, que é o que nos interessa.

                        List<Result> results = search.results.FindAll(r => r.order_items.Any(oi => oi.item.seller_custom_field != null));

                        foreach (Result ordem in results)
                        {
                            try
                            {
                                //Os status que nos interessam são aqueles que já obteveram algum pagamento.
                                //Outros status antes disso são simplesmente ignorados.

                                if ((ordem.status == "paid") || (ordem.status == "confirmed") || (ordem.status == "canceled"))
                                {
                                    //Verifica se já existe esse pedido no sistema pelo campo COD_PEDIDO_MARKETPLACE.
                                    //Caso não exista então será criado, caso exista será atualizado.

                                    string wCodPedido = $"Mercado Livre-{ordem.id}";

                                    if (db.TB_PEDIDO_CAB.FirstOrDefault(p => p.COD_PEDIDO_MARKETPLACE == wCodPedido) == null)
                                    {
                                        //Caso a API não nos retornar as informações de envio, então busca por eles com uma nova requisição.

                                        if (ordem.shipping.receiver_address == null)
                                        {
                                            parametros.Clear();
                                            parametros["access_token"] = Autenticacao.access_token;

                                            response = await Http.GetAsync($"shipments/{ordem.shipping.id}?{parametros.ToString()}").Result.Status200OrDie();

                                            ordem.shipping = await response.Content.ReadAsAsync<ShippingFromOrders>();
                                        }

                                        //Gera os códigos necessários para gerar um pedido no sistema

                                        TB_CADASTRO wCadastro = SelecionaComprador(
                                            ordem,
                                            configuracaoSkyhub
                                        );

                                        TB_SEQUENCIA wSequencia = SelecionaSequencia(configuracaoSkyhub.COD_FILIAL);

                                        int wCodigoPedido = db.Database.SqlQuery<int>("SELECT SQPEDIDO.NEXTVAL FROM DUAL").First();

                                        //Cria o novo pedido trazendo dados da API e algumas informações padrões no sistema.

                                        TB_PEDIDO_CAB wPedidoCab = new TB_PEDIDO_CAB()
                                        {
                                            COD_PEDIDO_CAB = wCodigoPedido,
                                            COD_FILIAL = Convert.ToInt32(configuracaoSkyhub.COD_FILIAL),
                                            COD_OPERACAO = Convert.ToInt32(configuracaoSkyhub.COD_OPERACAO),
                                            COD_CADASTRO = wCadastro.COD_CADASTRO,
                                            NUM_PEDIDO = wSequencia.VAL_SEQUENCIA,
                                            DT_EMISSAO = ordem.date_created,
                                            IND_SITUACAO = "1",
                                            COD_DEPARTAMENTO = configuracaoSkyhub.COD_DEPARTAMENTO,
                                            IND_TIPO_PAGAMENTO = configuracaoSkyhub.IND_TIPO_PAGAMENTO,
                                            COD_VENDEDOR = configuracaoSkyhub.COD_VENDEDOR,
                                            IND_TIPO_FRETE = "CIF",
                                            COD_PEDIDO_MARKETPLACE = "Mercado Livre-" + ordem.id,
                                            DESC_COMPLEMENTO_OBS = "Pedido do marketplace: Mercado Livre-" + ordem.id,
                                            PERC_COMISSAO = 0,
                                            VAL_FRETE_MARKETPLACE = Convert.ToDecimal(ordem.shipping.shipping_option.list_cost)
                                        };

                                        switch (ordem.shipping.status)
                                        {
                                            case "ready_to_ship": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "APPROVED"; break;
                                            case "shipped": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "SHIPPED"; break;
                                            case "cancelled": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "CANCELED"; break;
                                            case "delivered": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "DELIVERED"; break;
                                        }

                                        foreach (OrderItem item in ordem.order_items)
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
                                                    VAL_UNITARIO = Convert.ToDecimal(item.unit_price),
                                                    QT_PEDIDO = item.quantity
                                                }
                                            );
                                        }

                                        //O seguinte if foi adicionado pois pela manhã, boa parte das vezes, os pedidos eram criados duas vezes
                                        //no sistema. Minha teoria é que, como o servidor é lento ao iniciar, talvez 2 requisições estivessem
                                        //sendo ocorridas no determinado instante, adicionando 2 vezes o mesmo pedido.
                                        //Para garantir, também foi adicionado uma chave UNIQUE no banco de dados.

                                        if (db.TB_PEDIDO_CAB.FirstOrDefault(p => p.COD_PEDIDO_MARKETPLACE == wCodPedido) == null)
                                        {
                                            db.Entry(wPedidoCab).State = EntityState.Added;
                                            db.SaveChanges();
                                            wTotalCriados++;

                                            NotificaResponsaveis(configuracaoSkyhub, wPedidoCab);
                                        }
                                    }
                                    else
                                    {
                                        //Se o pedido já foi localizado no sistema, então atualiza seus dados.

                                        TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);

                                        switch (ordem.shipping.status)
                                        {
                                            case "ready_to_ship": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "APPROVED"; break;
                                            case "shipped": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "SHIPPED"; break;
                                            case "cancelled": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "CANCELED"; break;
                                            case "delivered": wPedidoCab.DESC_SITUACAO_MARKETPLACE = "DELIVERED"; break;
                                        }

                                        db.Entry(wPedidoCab).State = EntityState.Modified;
                                        db.SaveChanges();

                                        wTotalAlterados++;
                                    }                                 
                                }
                            }
                            catch (DbEntityValidationException except)
                            {
                                Manipulador.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}, Pedido: Mercado Livre-{ordem.id}" });
                            }
                            catch (Exception except)
                            {
                                Manipulador.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}, Pedido: Mercado Livre-{ordem.id}" });
                            }
                        }

                        //Caso não haja exceção alguma ao sincronizar todos os pedidos no período,
                        //então atualiza o campo que armazena o período da ultima sincronização.

                        if (Manipulador.SemExcecoes() && ((wTotalCriados) > 0 || (wTotalAlterados > 0) || (wTotalCancelados > 0)))
                        {
                            configuracaoSkyhub.DT_ULTIMA_ATUALIZACAO_ML = DateTime.Now;
                            db.Entry(configuracaoSkyhub).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    catch (Exception except)
                    {
                        Manipulador.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}" });
                    }
                }

                //Biblioteca ControlaExcecoes armazenou todos os erros ocorridos, caso não ocorra nenhum erro,
                //então o método SemExcecoes retornará verdadeiro.

                if (Manipulador.SemExcecoes())
                {
                    return Request.CreateResponse(
                        HttpStatusCode.OK,
                        $"Os pedidos agora estão sincronizados. Criados: {wTotalCriados}, Cancelados: {wTotalCancelados}, Alterados: {wTotalAlterados}"
                    );
                }
                else
                {
                    return Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        Manipulador.FormatoJson($"Nem todos os pedidos foram sincronizados. Criados: {wTotalCriados}, Cancelados: {wTotalCancelados}, Alterados: {wTotalAlterados}")
                    );
                }
            }
            catch (Exception except)
            {
                Manipulador.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    Manipulador.FormatoJson($"Não foi possível começar a sincronização")
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

                HttpClient Http = new HttpClient
                {
                    BaseAddress = MlivreUri
                };
                
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Manipulador.ErrosPersonalizados(true);
                HttpResponseMessage response;
                NameValueCollection parametros = HttpUtility.ParseQueryString(String.Empty);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //Busca sincronizações de produtos pendentes (campo IND_SINCRONIZADO_ML esteja igual a "N") na tabela TB_SINCRONIZACAO_SKYHUB.

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
                                
                                ConfiguracaoEstaOK(configuracaoSkyhub);
                                
                                List<TB_PRODUTO_SKYHUB> produtosSkyhub = GetProdutosSkyhub(sincronizacaoSkyhub);

                                //Verifica se já temos um access_token do mercado livre dentro do prazo.
                                //Caso ele já tenha expirado, realizamos uma nova chamada pelo novo token, e o gravamos no banco.
                                //Se ainda não expirou então busca no banco.

                                await ValidaAutenticacao(configuracaoSkyhub);

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

                                            //Começa o ProdutoSkyhub, criado conforme a estrutura especificada no manual da API.

                                            ItemFromProducts produtoML = new ItemFromProducts
                                            {
                                                seller_custom_field = Convert.ToString(produtoSkyhub.COD_PRODUTO),
                                                available_quantity = Convert.ToInt32(wTotalEstoque)
                                            };

                                            //À pedido do Cesar 19/02/2020, usar o valor de atacado.

                                            produtoML.price = wInfosProduto.VAL_ATACADO > 0 ? Convert.ToDouble(wInfosProduto.VAL_ATACADO) : Convert.ToDouble(wInfosProduto.VAL_VAREJO);
                                            
                                            produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_1));
                                            produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_2));
                                            produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_3));
                                            produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_4));

                                            produtoML.AddAttributesCustom(new Models.MercadoLivre.Products.Attribute("BRAND", produtoSkyhub.ESP_MARCA));
                                            produtoML.AddAttributesCustom(new Models.MercadoLivre.Products.Attribute("COLOR", produtoSkyhub.ESP_COR));
                                            produtoML.AddAttributesCustom(new Models.MercadoLivre.Products.Attribute("MODEL", produtoSkyhub.ESP_MODELO));
                                            produtoML.AddAttributesCustom(new Models.MercadoLivre.Products.Attribute("VOLTAGE", produtoSkyhub.ESP_VOLTAGEM));
                                            produtoML.AddAttributesCustom(new Models.MercadoLivre.Products.Attribute("GTIN", wInfosProduto.DESC_COD_BARRA));

                                            produtoML.status = produtoSkyhub.DESC_STATUS == "disabled" ? "paused" : "active";
                                            
                                            if (produtoML.available_quantity < 1)
                                            {
                                                produtoML.status = "paused";
                                            }

                                            //Realiza a busca pelo código sku que vem do campo seller_custom_field, em busca do código do mercadolivre.

                                            parametros.Clear();
                                            parametros["sku"] = produtoML.seller_custom_field;
                                            parametros["access_token"] = Autenticacao.access_token;

                                            response = await Http.GetAsync($"/users/{Autenticacao.user_id}/items/search?{parametros.ToString()}").Result.Status200OrDie();
                                            
                                            SearchFromProducts search = await response.Content.ReadAsAsync<SearchFromProducts>();

                                            switch (sincronizacaoSkyhub.TIPO_ACAO)
                                            {
                                                case "PUT":
                                                    {
                                                        if (search.results.Count > 0)
                                                        {
                                                            //O mercadolivre não permite atualizar alguns campos da publicação que já tiver uma venda.
                                                            //Nesse caso, procuramos pelo campo na pesquisa que contém o valor de vendas
                                                            //e caso não exista venda, atualizaremos estes campos.

                                                            AvailableFilterProducts filtro = search.available_filters.Find(x => x.id == "labels");
                                                            Value valor = filtro.values.Find(x => x.id == "with_bids");

                                                            //Mais de 20 vendas não pode atualizar a descrição do produto.

                                                            if (valor.results <= 20)
                                                            {
                                                                produtoML.title = produtoSkyhub.DESC_PRODUTO;
                                                            }

                                                            //Mais de 1 venda não pode alterar garantia e categoria.

                                                            if (valor.results == 0)
                                                            {
                                                                produtoML.category_id = produtoSkyhub.COD_CATEGORIA_ML;
                                                                produtoML.warranty = produtoSkyhub.ESP_GARANTIADEFORNECEDOR;
                                                            }
                                                            
                                                            parametros.Clear();
                                                            parametros["access_token"] = Autenticacao.access_token;

                                                            await Http.PutAsJsonAsync($"/items/{search.results.First()}?{parametros.ToString()}", StringUtils.ObjetoJsonFormatado(produtoML)).Result.Status200OrDie();

                                                            totalAtualizados++;
                                                        }
                                                        else
                                                        {
                                                            //Se não existir tal produto no mercadolivre com o sku buscado anteriormente, então ele deve ser criado.

                                                            produtoML.title = produtoSkyhub.DESC_PRODUTO;
                                                            produtoML.description = new Description(produtoSkyhub.DESC_DESCRICAO);
                                                            produtoML.warranty = produtoSkyhub.ESP_GARANTIADEFORNECEDOR;
                                                            produtoML.category_id = produtoSkyhub.COD_CATEGORIA_ML;
                                                            produtoML.buying_mode = "buy_it_now";
                                                            produtoML.condition = "new";
                                                            produtoML.listing_type_id = "gold_special";
                                                            produtoML.shipping = new ShippingFromProducts
                                                            {
                                                                mode = "me2",
                                                                free_shipping = false
                                                            };

                                                            parametros.Clear();
                                                            parametros["access_token"] = Autenticacao.access_token;

                                                            if (produtoML.available_quantity > 0)
                                                            {
                                                                await Http.PostAsJsonAsync($"/items?{parametros.ToString()}", StringUtils.ObjetoJsonFormatado(produtoML)).Result.Status200OrDie();
                                                                totalCriados++;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "DELETE":
                                                    {
                                                        //Se o compilador chegou neste trecho do código, indica que temos uma solicitação para
                                                        //deletar o produto da API, porém, o produto ainda permanece no sistema, então desta forma
                                                        //apenas pausamos o anuncio para não ocorrer eventuais problemas.

                                                        produtoML.status = "paused";

                                                        if (search.results.Count > 0)
                                                        {
                                                            parametros.Clear();
                                                            parametros["access_token"] = Autenticacao.access_token;

                                                            await Http.PutAsJsonAsync($"/items/{search.results.First()}{parametros.ToString()}", StringUtils.ObjetoJsonFormatado(produtoML)).Result.Status200OrDie();

                                                            totalDeletados++;
                                                        }
                                                    }
                                                    break;
                                                case "POST":
                                                    {
                                                        if (search.results.Count == 0)
                                                        {
                                                            produtoML.title = produtoSkyhub.DESC_PRODUTO;
                                                            produtoML.description = new Description(produtoSkyhub.DESC_DESCRICAO);
                                                            produtoML.warranty = produtoSkyhub.ESP_GARANTIADEFORNECEDOR;
                                                            produtoML.category_id = produtoSkyhub.COD_CATEGORIA_ML;
                                                            produtoML.buying_mode = "buy_it_now";
                                                            produtoML.condition = "new";
                                                            produtoML.listing_type_id = "gold_special";
                                                            produtoML.shipping = new ShippingFromProducts
                                                            {
                                                                mode = "me2",
                                                                free_shipping = false
                                                            };
                                                            
                                                            parametros.Clear();
                                                            parametros["access_token"] = Autenticacao.access_token;

                                                            if (produtoML.available_quantity > 0)
                                                            {
                                                                await Http.PostAsJsonAsync($"/items?{parametros.ToString()}", StringUtils.ObjetoJsonFormatado(produtoML)).Result.Status200OrDie();
                                                                totalCriados++;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //Caso tenha sido encontrado algum produto com mesmo sku, então ele ja existe e não deve ser criado novamente.
                                                            
                                                            AvailableFilterProducts filtro = search.available_filters.Find(x => x.id == "labels");
                                                            Value valor = filtro.values.Find(x => x.id == "with_bids");

                                                            //O mercadolivre não permite atualizar alguns campos da publicação que já tiver uma venda.
                                                            //Nesse caso, procuramos pelo campo na pesquisa que contém o valor de vendas
                                                            //e caso não exista venda, atualizaremos estes campos.

                                                            //Mais de 20 vendas não pode atualizar a descrição do produto.

                                                            if (valor.results <= 20)
                                                            {
                                                                produtoML.title = produtoSkyhub.DESC_PRODUTO;
                                                            }

                                                            //Mais de 1 venda não pode alterar garantia e categoria.

                                                            if (valor.results == 0)
                                                            {
                                                                produtoML.category_id = produtoSkyhub.COD_CATEGORIA_ML;
                                                                produtoML.warranty = produtoSkyhub.ESP_GARANTIADEFORNECEDOR;
                                                            }

                                                            parametros.Clear();
                                                            parametros["access_token"] = Autenticacao.access_token;

                                                            await Http.PutAsJsonAsync($"/items/{search.results.First()}?{parametros.ToString()}", StringUtils.ObjetoJsonFormatado(produtoML)).Result.Status200OrDie();

                                                            totalAtualizados++;
                                                        }
                                                    }
                                                    break;
                                            }
                                            
                                            //Altera o campo da sincronização para o produto não ficar sincronizando eternamente.
                                            //Também salva este produto recentemente atualizado. Isso é uma forma preventiva para que em uma lista de sincronizações,
                                            //caso haja algum item com problema de sincronização, não fique trancando essa fila, sendo necessário sincronizar tudo novamente.

                                            sincronizacaoSkyhub.DT_SINCRONIZACAO_ML = DateTime.Now;
                                            sincronizacaoSkyhub.IND_SINCRONIZADO_ML = "S";
                                            db.SaveChanges();
                                        }
                                        catch (Exception except)
                                        {
                                            Manipulador.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}", $"Sincronização: {sincronizacaoSkyhub.COD_SINCRONIZACAO_SKYHUB}" });
                                        }
                                    }
                                }
                                else
                                {
                                    //Caso não tenha sido encontrado nenhum registro da tabela TB_PRODUTO_SKYHUB referente a sincronização atual,
                                    //então o produto deve ser apagado da API também, ignorando o método pedido pela sincronização

                                    Http.BaseAddress = MlivreUri;

                                    Http.DefaultRequestHeaders.Accept.Clear();
                                    Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                    //Busca pelo código do produto do mercado livre para poder realizar as próximas chamadas.
                                    //Realiza a busca pelo código sku, que vem do campo seller_custom_field.
                                    //Publicações com status closed seram apagadas pelo ML depois de um tempo.

                                    parametros.Clear();
                                    parametros["access_token"] = Autenticacao.access_token;
                                    parametros["sku"] = Convert.ToString(sincronizacaoSkyhub.COD_PRODUTO);

                                    response = await Http.GetAsync($"/users/{Autenticacao.user_id}/items/search?{parametros.ToString()}");

                                    if (response.StatusCode != HttpStatusCode.NotFound)
                                    {
                                        await response.Status200OrDie();

                                        SearchFromProducts search = await response.Content.ReadAsAsync<SearchFromProducts>();

                                        if (search.results.Count > 0)
                                        {
                                            parametros.Clear();
                                            parametros["access_token"] = Autenticacao.access_token;

                                            await Http.PutAsJsonAsync($"/items/{search.results.First()}?{parametros.ToString()}", StringUtils.ObjetoJsonFormatado(new { status = "closed" })).Result.Status200OrDie();

                                            totalDeletados++;
                                        }
                                    }

                                    //Altera o campo da sincronização para o produto não ficar sincronizando eternamente.
                                    //Também salva este produto recentemente atualizado. Isso é uma forma preventiva para que em uma lista de sincronizações,
                                    //caso haja algum item com problema de sincronização, não fique trancando essa fila, sendo necessário sincronizar tudo novamente.

                                    sincronizacaoSkyhub.DT_SINCRONIZACAO_ML = DateTime.Now;
                                    sincronizacaoSkyhub.IND_SINCRONIZADO_ML = "S";
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception except)
                            {
                                Manipulador.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}", $"Sincronização: {sincronizacaoSkyhub.COD_SINCRONIZACAO_SKYHUB}" });
                            }
                        }
                    }
                    catch (Exception except)
                    {
                        Manipulador.Adiciona(except, new List<string> { $"Sincronização: {sincronizacaoSkyhub.COD_SINCRONIZACAO_SKYHUB}" });
                    }
                }

                //Biblioteca ControlaExcecoes armazenou todos os erros ocorridos, caso não ocorra nenhum erro,
                //então o método SemExcecoes retornará verdadeiro.

                if (Manipulador.SemExcecoes())
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
                        Manipulador.FormatoJson($"Nem todos os produtos foram sincronizados. Criados: {totalCriados}, Atualizados: {totalAtualizados}, Removidos: {totalDeletados}")
                    );
                }
            }
            catch (Exception except)
            {
                Manipulador.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    Manipulador.FormatoJson($"Não foi possível começar a sincronização")
                );
            }
        }

        private TB_CADASTRO SelecionaComprador(Result ordem, TB_CONFIGURACAO_SKYHUB configuracao)
        {
            //Pré-processa os dados, formatando-os para nosso sistema.
            
            ordem.shipping.receiver_address.state.id = ordem.shipping.receiver_address.state.id.ToUpper().Replace("BR-", "");
            ordem.shipping.receiver_address.street_name = ordem.shipping.receiver_address.street_name.ToUpper();
            ordem.shipping.receiver_address.comment = ordem.shipping.receiver_address.comment?.ToUpper();
            ordem.shipping.receiver_address.zip_code = String.Format(@"{0:00000\-000}", Convert.ToInt64(ordem.shipping.receiver_address.zip_code));
            ordem.shipping.receiver_address.city.name = ordem.shipping.receiver_address.city.name.ToUpper();

            ordem.buyer.email = ordem.buyer.email.ToUpper();

            if (ordem.buyer.phone != null)
            {
                ordem.buyer.phone.number = (ordem.buyer.phone.area_code + ordem.buyer.phone.number).Truncate(12);
            }
            else
            {
                ordem.buyer.phone = new PhoneFromOrder
                {
                    number = ""
                };
            }

            if (ordem.buyer.alternative_phone != null)
            {
                ordem.buyer.alternative_phone.number = (ordem.buyer.alternative_phone.area_code + ordem.buyer.alternative_phone.number).Truncate(12);
            }
            else
            {
                ordem.buyer.alternative_phone = new AlternativePhoneFromOrder
                {
                    number = ""
                };
            }

            if (String.IsNullOrEmpty(ordem.shipping.receiver_address.neighborhood.name))
            {
                ordem.shipping.receiver_address.neighborhood = new Neighborhood
                {
                    id = null,
                    name = "NÃO INFORMADO"
                };
            }
            else
            {
                ordem.shipping.receiver_address.neighborhood.name = ordem.shipping.receiver_address.neighborhood.name.ToUpper();
            }

            string indCgc;
            if (ordem.buyer.billing_info.doc_type == "CPF")
            {
                ordem.buyer.billing_info.doc_number = String.Format(@"{0:000\.000\.000\-00}", Convert.ToInt64(ordem.buyer.billing_info.doc_number));
                indCgc = "F";
            }
            else
            {
                ordem.buyer.billing_info.doc_number = String.Format(@"{0:00\.000\.000/0000\-00}", Convert.ToInt64(ordem.buyer.billing_info.doc_number));
                indCgc = "J";
            }


            //Busca o comprador no banco de dados. Caso não encontre, começa o processo de cadastro.
            //Caso encontre, atualiza alguns dados.

            TB_CADASTRO wCadastro = (from c in db.TB_CADASTRO
                                     where c.NUM_CGC_CPF == ordem.buyer.billing_info.doc_number
                                     select c).FirstOrDefault();

            //Busca a cidade para esse comprador, Caso não encontre, também cadastra.

            TB_CIDADE wCidade = (from c in db.TB_CIDADE
                                 where c.DESC_CIDADE == ordem.shipping.receiver_address.city.name
                                 select c).FirstOrDefault();
            if (wCidade == null)
            {
                int wCodigoCidade = db.Database.SqlQuery<int>("SELECT SQCIDADE.NEXTVAL FROM DUAL").First();

                wCidade = new TB_CIDADE()
                {
                    COD_CIDADE = wCodigoCidade,
                    DESC_CIDADE = ordem.shipping.receiver_address.city.name,
                    COD_ESTADO = ordem.shipping.receiver_address.state.id
                };

                db.Entry(wCidade).State = EntityState.Added;
            }

            if (wCadastro == null)
            {
                int wCodigoCadastro = db.Database.SqlQuery<int>("SELECT SQCADASTRO.NEXTVAL FROM DUAL").First();

                wCadastro = new TB_CADASTRO()
                {
                    COD_CADASTRO = wCodigoCadastro,
                    NOME = (ordem.buyer.first_name + " " + ordem.buyer.last_name).ToUpper(),
                    COD_TIPO_CADASTRO = Convert.ToInt32(configuracao.COD_TIPO_CADASTRO),
                    NUM_CGC_CPF = ordem.buyer.billing_info.doc_number,
                    DESC_E_MAIL = ordem.buyer.email,
                    DT_CADASTRO = DateTime.Now,
                    DESC_CELULAR = ordem.buyer.alternative_phone.number,
                    DESC_TELEFONE = ordem.buyer.phone.number,
                    DESC_ENDERECO = $"{ordem.shipping.receiver_address.street_name}, {ordem.shipping.receiver_address.street_number}",
                    DESC_ENDERECO_COBRANCA = ($"{ordem.shipping.receiver_address.street_name}, {ordem.shipping.receiver_address.street_number}").Truncate(40),
                    DESC_COMPLEMENTO = ordem.shipping.receiver_address.comment.Truncate(40),
                    DESC_BAIRRO = ordem.shipping.receiver_address.neighborhood.name,
                    DESC_BAIRRO_COBRANCA = (ordem.shipping.receiver_address.neighborhood.name).Truncate(12),
                    COD_ESTADO = ordem.shipping.receiver_address.state.id,
                    DESC_ESTADO_COBRANCA = ordem.shipping.receiver_address.state.id,
                    IND_SEXO_CATEGORIA = "M",
                    IND_FISICA_JURIDICA = indCgc,
                    COD_CIDADE = wCidade.COD_CIDADE,
                    DESC_CIDADE = ordem.shipping.receiver_address.city.name,
                    DESC_CEP = ordem.shipping.receiver_address.zip_code,
                    DESC_CEP_COBRANCA = ordem.shipping.receiver_address.zip_code,
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
                wCadastro.NOME = (ordem.buyer.first_name + " " + ordem.buyer.last_name).ToUpper();
                wCadastro.DESC_E_MAIL = ordem.buyer.email;
                wCadastro.DESC_CELULAR = ordem.buyer.alternative_phone.number;
                wCadastro.DESC_TELEFONE = ordem.buyer.phone.number;
                wCadastro.DESC_ENDERECO = $"{ordem.shipping.receiver_address.street_name}, {ordem.shipping.receiver_address.street_number}";
                wCadastro.DESC_COMPLEMENTO = ordem.shipping.receiver_address.comment.Truncate(12);
                wCadastro.DESC_BAIRRO = ordem.shipping.receiver_address.neighborhood.name;
                wCadastro.COD_ESTADO = ordem.shipping.receiver_address.state.id;
                wCadastro.COD_CIDADE = wCidade.COD_CIDADE;
                wCadastro.DESC_CIDADE = ordem.shipping.receiver_address.city.name;
                wCadastro.DESC_CEP = ordem.shipping.receiver_address.zip_code;

                db.Entry(wCadastro).State = EntityState.Modified;
            }

            db.SaveChanges();

            return wCadastro;
        }

        private TB_PRODUTO SelecionaProduto(OrderItem item)
        {
            //Busca pelo produto no sistema a partir do item de uma compra.
            //Tais valores serão utilizados para adicionar o item ao pedido.
            //Caso o produto não seja encontrado no sistema, então cadastra conforme informações do ML.

            long id;

            if(String.IsNullOrEmpty(item.item.seller_custom_field))
            {
                id = db.Database.SqlQuery<int>("SELECT SQPRODUTO.NEXTVAL FROM DUAL").First();
            }
            else
            {
                id = Convert.ToInt64(item.item.seller_custom_field);
            }
            
            TB_PRODUTO wProduto = (from p in db.TB_PRODUTO
                                   where (p.COD_PRODUTO == id)
                                   select p).FirstOrDefault();
            if (wProduto == null)
            {
                //Com as informações do item, pré-processa os dados para salvar um novo produto no sistema.
                //Este produto será trazido apenas para cadastro do pedido, ele necessita de atenção.
                //Esse caso ocorreu devido a um produto que existe na skyhub mas foi apagado do sistema, dessa forma,
                //quando baixar um pedido, este produto precisa ser criado.

                item.item.title = $"(PRODUTO EXISTENTE APENAS NO MERCADOLIVRE!){item.item.title.ToUpper()}";

                TB_CLASSE wClasse = (from c in db.TB_CLASSE select c).FirstOrDefault();
                TB_TRIBUTACAO wTributacao = (from t in db.TB_TRIBUTACAO select t).FirstOrDefault();

                wProduto = new TB_PRODUTO
                {
                    COD_PRODUTO = id,
                    DESC_PRODUTO = item.item.title,
                    VAL_VAREJO = Convert.ToDecimal(item.unit_price),
                    VAL_ATACADO = Convert.ToDecimal(item.unit_price),
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

        private TB_PEDIDO_CAB GetPedidoPorMarketplace(Result ordem)
        {
            string wCodPedido = $"Mercado Livre-{ordem.id}";

            TB_PEDIDO_CAB wPedido = (from p in db.TB_PEDIDO_CAB
                                     where (p.COD_PEDIDO_MARKETPLACE == wCodPedido)
                                     select p).FirstOrDefault();
            return wPedido;
        }

        private List<TB_SINCRONIZACAO_SKYHUB> GetSincronizacoes()
        {
            return (from sincronizacaoSkyhub in db.TB_SINCRONIZACAO_SKYHUB
                    where sincronizacaoSkyhub.IND_SINCRONIZADO_ML == "N"
                    select sincronizacaoSkyhub).ToList();
        }

        private List<TB_CONFIGURACAO_SKYHUB> GetConfiguracoes()
        {
            return (from configuracaoSkyhub in db.TB_CONFIGURACAO_SKYHUB
                    where configuracaoSkyhub.IND_ML_ATIVO == "S"
                    select configuracaoSkyhub).ToList();
        }

        private List<TB_PRODUTO_SKYHUB> GetProdutosSkyhub(TB_SINCRONIZACAO_SKYHUB sincronizacaoSkyhub)
        {
            return (from produtoSkyhub in db.TB_PRODUTO_SKYHUB
                    where produtoSkyhub.COD_PRODUTO == sincronizacaoSkyhub.COD_PRODUTO
                    select produtoSkyhub).ToList();
        }

        public TB_PRODUTO_SKYHUB GetProdutoSkyhub(int codProduto)
        {
            return (from produtoSkyhub in db.TB_PRODUTO_SKYHUB
                    where (produtoSkyhub.COD_PRODUTO == codProduto)
                    select produtoSkyhub).FirstOrDefault();
        }

        private TB_PRODUTO InfosProduto(TB_PRODUTO_SKYHUB produtoSkyhub)
        {
            TB_PRODUTO wProduto = (from dbProduto in db.TB_PRODUTO
                                   where dbProduto.COD_PRODUTO == produtoSkyhub.COD_PRODUTO
                                   select dbProduto).First();
            return wProduto;
        }
        
        private TB_CONFIGURACAO_SKYHUB GetConfiguracao(int codFilial)
        {
            return (from configuracaoSkyhub in db.TB_CONFIGURACAO_SKYHUB
                    where configuracaoSkyhub.IND_ATIVO == "S" && configuracaoSkyhub.COD_FILIAL == codFilial
                    select configuracaoSkyhub).FirstOrDefault();
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
            if (String.IsNullOrEmpty(produtoSkyhub.COD_CATEGORIA_ML))
                throw new ArgumentException($"Produto marketplace: categoria inválida, produto: {produtoSkyhub.COD_PRODUTO}");
        }

        private async Task ValidaAutenticacao(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub)
        {
            if (!TokenEstaOK(configuracaoSkyhub))
            {
                NameValueCollection parametros = HttpUtility.ParseQueryString(string.Empty);

                parametros.Clear();
                parametros["client_id"] = configuracaoSkyhub.DESC_ML_CLIENT_ID;
                parametros["client_secret"] = configuracaoSkyhub.DESC_ML_CLIENT_SECRET;
                parametros["grant_type"] = "client_credentials";
                
                HttpClient Http = new HttpClient
                {
                    BaseAddress = MlivreUri
                };

                HttpResponseMessage response = await Http.PostAsync($"/oauth/token?{parametros.ToString()}", null).Result.Status200OrDie();

                Autenticacao = await response.Content.ReadAsAsync<OAuth>();

                configuracaoSkyhub.DESC_ACCESS_TOKEN_ML = Autenticacao.access_token;
                configuracaoSkyhub.DESC_USER_ID_ML = Autenticacao.user_id;
                configuracaoSkyhub.DT_ACCESS_TOKEN_ML = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                Autenticacao = new OAuth
                {
                    access_token = configuracaoSkyhub.DESC_ACCESS_TOKEN_ML,
                    user_id = configuracaoSkyhub.DESC_USER_ID_ML
                };
            }
        }

        private bool TokenEstaOK(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub)
        {
            if ((configuracaoSkyhub.DT_ACCESS_TOKEN_ML == null) ||
                (configuracaoSkyhub.DESC_USER_ID_ML == null) ||
                (String.IsNullOrEmpty(configuracaoSkyhub.DESC_ACCESS_TOKEN_ML)))
            {
                return false;
            }

            DateTime dataExpiracao = Convert.ToDateTime(configuracaoSkyhub.DT_ACCESS_TOKEN_ML).AddSeconds(21600);

            if (DateTime.Now < dataExpiracao)
            {
                return true;
            }

            return false;
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
            if (String.IsNullOrEmpty(configuracaoSkyhub.DESC_ML_CLIENT_ID))
                throw new ArgumentException($"Configuração skyhub: token inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
            if (String.IsNullOrEmpty(configuracaoSkyhub.DESC_ML_CLIENT_SECRET))
                throw new ArgumentException($"Configuração skyhub: token inválido, filial: {configuracaoSkyhub.COD_FILIAL}");
        }

        private TB_SEQUENCIA SelecionaSequencia(int codFilial)
        {
            //Busca valor da sequência para pedidos. Caso não encontre, cadastra uma nova.

            TB_SEQUENCIA wSequencia = (from s in db.TB_SEQUENCIA
                                       where (s.NOME_SEQUENCIA == "PEDIDO") && (s.COD_FILIAL == codFilial)
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
                
        private void NotificaResponsaveis(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub, TB_PEDIDO_CAB ordem)
        {
            try
            {
                //Quando um pedido novo for baixado para o sistema, envia um e-mail para os destinatários cadastrados.

                List<string> eDestinatarios = new List<string>();

                foreach (TB_EMAIL_NOTIFICACAO email in configuracaoSkyhub.TB_EMAIL_NOTIFICACAO)
                {
                    eDestinatarios.Add(email.DESC_EMAIL);
                }

                Envio.EnviaEmail(eDestinatarios, "Novo pedido aprovado.", $"Um novo pedido foi aprovado pelo marketplace. Código: {ordem.COD_PEDIDO_MARKETPLACE}");
            }
            catch
            {
                Console.WriteLine("Erro ao enviar e-mail?");
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> DownloadArquivoCategorias()
        {
            try
            {
                //Este método busca todas as categorias atualizadas no Mercadolivre e às processa
                //para gerar um arquivo txt de categorias no nosso formato.

                HttpClient Http = new HttpClient
                {
                    BaseAddress = MlivreUri
                };
                
                Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await Http.GetAsync($"/sites/MLB/categories/all?withAttributes=false").Result.Status200OrDie();

                List<Categoria> catFinais = new List<Categoria>();
                List<Dictionary<string, string>> idsCategoria = new List<Dictionary<string, string>>();

                string jsonText = StringUtils.Unzip(await response.Content.ReadAsByteArrayAsync());

                Dictionary<string, Category> categorysResponse = JsonConvert.DeserializeObject<Dictionary<string, Category>>(jsonText);

                foreach (KeyValuePair<string, Category> category in categorysResponse)
                {
                    Dictionary<string, string> idCategoria = new Dictionary<string, string>();

                    List<string> names = new List<string>();

                    foreach (PathFromRoot item in category.Value.path_from_root)
                    {
                        names.Add(item.name);
                    }

                    idCategoria.Add(category.Key, String.Join(" > ", names));

                    idsCategoria.Add(idCategoria);

                    if (category.Value.path_from_root.Count == 1)
                    {
                        catFinais.Add(
                            new Categoria
                            {
                                id = category.Value.id,
                                descricao = category.Value.name
                            }
                        );
                    }
                }

                foreach (Categoria catFinal in catFinais)
                {
                    foreach (KeyValuePair<string, Category> category in categorysResponse)
                    {
                        if (category.Key == catFinal.id)
                        {
                            foreach (ChildrenCategory filho1 in category.Value.children_categories)
                            {
                                catFinal.filhos.Add(
                                    new Categoria
                                    {
                                        id = filho1.id,
                                        descricao = filho1.name
                                    }
                                );
                            }
                        }
                    }

                    foreach (Categoria catFinal2 in catFinal.filhos)
                    {
                        foreach (KeyValuePair<string, Category> category in categorysResponse)
                        {
                            if (category.Key == catFinal2.id)
                            {
                                foreach (ChildrenCategory filho2 in category.Value.children_categories)
                                {
                                    catFinal2.filhos.Add(
                                        new Categoria
                                        {
                                            id = filho2.id,
                                            descricao = filho2.name
                                        }
                                    );
                                }
                            }
                        }

                        foreach (Categoria catFinal3 in catFinal2.filhos)
                        {
                            foreach (KeyValuePair<string, Category> category in categorysResponse)
                            {
                                if (category.Key == catFinal3.id)
                                {
                                    foreach (ChildrenCategory filho3 in category.Value.children_categories)
                                    {
                                        catFinal3.filhos.Add(
                                            new Categoria
                                            {
                                                id = filho3.id,
                                                descricao = filho3.name
                                            }
                                        );
                                    }
                                }
                            }
                        }
                    }
                }

                MemoryStream stream = new MemoryStream();

                StreamWriter sw = new StreamWriter(stream);

                foreach (Categoria cat1 in catFinais)
                {
                    sw.WriteLine($">>{cat1.descricao}");
                    foreach (Categoria cat2 in cat1.filhos)
                    {
                        sw.WriteLine($">>>>{cat2.descricao}");
                        foreach (Categoria cat3 in cat2.filhos)
                        {
                            sw.WriteLine($">>>>>>{cat3.descricao}");
                            foreach (Categoria cat4 in cat3.filhos)
                            {
                                sw.WriteLine($">>>>>>>>{cat4.descricao}");
                                sw.Flush();
                            }
                        }
                    }
                }

                sw.WriteLine($"#===#");

                foreach (Dictionary<string, string> ids in idsCategoria)
                {
                    foreach (KeyValuePair<string, string> id in ids)
                    {
                        sw.WriteLine($"{id.Key}###{id.Value}");
                        sw.Flush();
                    }
                }

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray())
                };

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "CategoriasSkyhub.txt" };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                sw.Close();
                stream.Close();

                return result;
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Não foi possível atualizar arquivo de configurações. {exception.Message}"
                );
            }
        }
    }
}
