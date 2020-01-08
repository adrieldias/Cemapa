using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Cemapa.Models;
using Cemapa.Models.MercadoLivre;
using Newtonsoft.Json;
using HttpParamsUtility;

using Attribute = Cemapa.Models.MercadoLivre.Attribute;

namespace Cemapa.Controllers
{
    public class SincronizarMLivreController : ApiController
    {
        private OAuth Autenticacao = new OAuth();
        private readonly Entities db = new Entities();
        private readonly HttpClient Http = new HttpClient();
        private readonly Uri MlivreUri = new Uri("https://api.mercadolibre.com");
        
        [HttpGet]
        public async Task<HttpResponseMessage> SincronizaProdutos()
        {
            try
            {

                int totalAtualizados = 0;
                int totalCriados = 0;
                int totalDeletados = 0;
                bool saveRegistro = true;

                HttpResponseMessage response;
                
                Http.BaseAddress = MlivreUri;

                ControladorExcecoes.Limpa();

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
                                
                                saveRegistro = true;

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

                                            //Inicia a busca pelas credenciais.
                                            //O mercadolivre utiliza um access token que é obtido atravéz da chamada.
                                            
                                            Http.DefaultRequestHeaders.Accept.Clear();
                                            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                            Http.Timeout = TimeSpan.FromSeconds(30);

                                            switch (sincronizacaoSkyhub.TIPO_ACAO)
                                            {
                                                case "PUT":
                                                    {
                                                        //Começa o ProdutoSkyhub, criado conforme a estrutura especificada no manual da API.

                                                        Item produtoML = new Item
                                                        {
                                                            title = produtoSkyhub.DESC_PRODUTO,
                                                            seller_custom_field = Convert.ToString(produtoSkyhub.COD_PRODUTO),
                                                            price = Convert.ToDouble(wInfosProduto.VAL_VAREJO),
                                                            available_quantity = Convert.ToInt32(wTotalEstoque),
                                                            warranty = produtoSkyhub.ESP_GARANTIADEFORNECEDOR,
                                                            category_id = produtoSkyhub.COD_CATEGORIA_ML
                                                        };

                                                        produtoML.status = produtoSkyhub.DESC_STATUS == "disabled" ? "paused" : "active";

                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_1));
                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_2));
                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_3));
                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_4));

                                                        produtoML.AddAttributesCustom(new Attribute("BRAND", produtoSkyhub.ESP_MARCA));
                                                        produtoML.AddAttributesCustom(new Attribute("COLOR", produtoSkyhub.ESP_COR));
                                                        produtoML.AddAttributesCustom(new Attribute("MODEL", produtoSkyhub.ESP_MODELO));
                                                        produtoML.AddAttributesCustom(new Attribute("VOLTAGE", produtoSkyhub.ESP_VOLTAGEM));
                                                        produtoML.AddAttributesCustom(new Attribute("GTIN", wInfosProduto.DESC_COD_BARRA));

                                                         //Busca pelo código do produto do mercado livre para poder realizar as próximas chamadas.
                                                         //Realiza a busca pelo código sku, que vem do campo seller_custom_field.

                                                         response = await Http.GetAsync($"/users/{Autenticacao.user_id}/items/search?sku={produtoML.seller_custom_field}&access_token={Autenticacao.access_token}");

                                                        if (response.IsSuccessStatusCode)
                                                        {
                                                            //Se o produto não for localizado no Mercado livre, então atualiza a sincronização
                                                            //para POST, para então criar o produto.

                                                            try
                                                            {
                                                                Search search = await response.Content.ReadAsAsync<Search>();

                                                                if (search.results.Count == 0)
                                                                {
                                                                    sincronizacaoSkyhub.TIPO_ACAO = "POST";
                                                                    db.SaveChanges();
                                                                    saveRegistro = false;
                                                                }
                                                                else
                                                                {
                                                                    response = await Http.PutAsJsonAsync($"/items/{search.results.First()}?access_token={Autenticacao.access_token}", ObjetoJsonFormatado(produtoML));

                                                                    if (!response.IsSuccessStatusCode)
                                                                    {
                                                                        throw new HttpListenerException((int)response.StatusCode, $"PUT: {response.ReasonPhrase}. {response.Content.ReadAsStringAsync().Result}");
                                                                    }

                                                                    totalAtualizados++;
                                                                }
                                                            }
                                                            catch (Exception except)
                                                            {
                                                                throw new HttpListenerException((int)response.StatusCode, $"Problema ao buscar produto no mercadolivre: {except.Message}");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new HttpListenerException((int)response.StatusCode, $"Problema ao buscar id do produto no mercadolivre: {response.ReasonPhrase}. {response.Content.ReadAsStringAsync().Result}");
                                                        }
                                                    }
                                                    break;
                                                case "DELETE":
                                                    {
                                                        //Se o compilador chegou neste trecho do código, indica que temos uma solicitação para
                                                        //deletar o produto da API, porém, o produto ainda permanece no sistema, então desta forma
                                                        //apenas pausamos o anuncio para não ocorrer eventuais problemas.

                                                        Item produtoML = new Item
                                                        {
                                                            title = produtoSkyhub.DESC_PRODUTO,
                                                            seller_custom_field = Convert.ToString(produtoSkyhub.COD_PRODUTO),
                                                            price = Convert.ToDouble(wInfosProduto.VAL_VAREJO),
                                                            available_quantity = Convert.ToInt32(wTotalEstoque),
                                                            warranty = produtoSkyhub.ESP_GARANTIADEFORNECEDOR,
                                                            category_id = produtoSkyhub.COD_CATEGORIA_ML,
                                                            status = "paused"
                                                        };

                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_1));
                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_2));
                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_3));
                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_4));

                                                        produtoML.AddAttributesCustom(new Attribute("BRAND", produtoSkyhub.ESP_MARCA));
                                                        produtoML.AddAttributesCustom(new Attribute("COLOR", produtoSkyhub.ESP_COR));
                                                        produtoML.AddAttributesCustom(new Attribute("MODEL", produtoSkyhub.ESP_MODELO));
                                                        produtoML.AddAttributesCustom(new Attribute("VOLTAGE", produtoSkyhub.ESP_VOLTAGEM));
                                                        produtoML.AddAttributesCustom(new Attribute("GTIN", wInfosProduto.DESC_COD_BARRA));

                                                        //Busca pelo código do produto do mercado livre para poder realizar as próximas chamadas.
                                                        //Realiza a busca pelo código sku, que vem do campo seller_custom_field.

                                                        response = await Http.GetAsync($"/users/{Autenticacao.user_id}/items/search?sku={produtoML.seller_custom_field}&access_token={Autenticacao.access_token}");

                                                        if (response.IsSuccessStatusCode)
                                                        {
                                                            //Se o produto não for localizado no Mercado livre, então atualiza a sincronização
                                                            //para POST, para então criar o produto.

                                                            Search search = await response.Content.ReadAsAsync<Search>();

                                                            if (search.results.Count > 0)
                                                            {
                                                                response = await Http.PutAsJsonAsync($"/items/{search.results.First()}?access_token={Autenticacao.access_token}", ObjetoJsonFormatado(produtoML));

                                                                if (!response.IsSuccessStatusCode)
                                                                {
                                                                    throw new HttpListenerException((int)response.StatusCode, $"PUT: {response.ReasonPhrase}. {response.Content.ReadAsStringAsync().Result}");
                                                                }

                                                                totalDeletados++;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new HttpListenerException((int)response.StatusCode, $"Problema ao buscar id do produto no mercadolivre: {response.ReasonPhrase}. {response.Content.ReadAsStringAsync().Result}");
                                                        }
                                                    }
                                                    break;
                                                case "POST":
                                                    {
                                                        //Instancia o produto da Classe ProdutoSkyhub, criada conforme a estrutura especificada no manual da API.

                                                        Item produtoML = new Item
                                                        {
                                                            title = produtoSkyhub.DESC_PRODUTO,
                                                            description = new Description(produtoSkyhub.DESC_DESCRICAO),
                                                            seller_custom_field = Convert.ToString(produtoSkyhub.COD_PRODUTO),
                                                            price = Convert.ToDouble(wInfosProduto.VAL_VAREJO),
                                                            available_quantity = Convert.ToInt32(wTotalEstoque),
                                                            warranty = produtoSkyhub.ESP_GARANTIADEFORNECEDOR,
                                                            category_id = produtoSkyhub.COD_CATEGORIA_ML,
                                                            listing_type_id = "gold_special"
                                                        };

                                                        produtoML.status = produtoSkyhub.DESC_STATUS == "disabled" ? "paused" : "active";

                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_1));
                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_2));
                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_3));
                                                        produtoML.AddImagesCustom(new Picture(produtoSkyhub.DESC_LINK_IMAGEM_4));

                                                        produtoML.AddAttributesCustom(new Attribute("BRAND", produtoSkyhub.ESP_MARCA));
                                                        produtoML.AddAttributesCustom(new Attribute("COLOR", produtoSkyhub.ESP_COR));
                                                        produtoML.AddAttributesCustom(new Attribute("MODEL", produtoSkyhub.ESP_MODELO));
                                                        produtoML.AddAttributesCustom(new Attribute("VOLTAGE", produtoSkyhub.ESP_VOLTAGEM));
                                                        produtoML.AddAttributesCustom(new Attribute("GTIN", wInfosProduto.DESC_COD_BARRA));

                                                        //Anuncios com estoque zero não são criados

                                                        if (produtoML.available_quantity > 0)
                                                        {
                                                            response = await Http.PostAsJsonAsync($"/items?access_token={Autenticacao.access_token}", ObjetoJsonFormatado(produtoML));

                                                            if (!response.IsSuccessStatusCode)
                                                            {
                                                                throw new HttpListenerException((int)response.StatusCode, $"POST: {response.ReasonPhrase}. {response.Content.ReadAsStringAsync().Result}");
                                                            }

                                                            totalCriados++;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        catch (Exception except)
                                        {
                                            ControladorExcecoes.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}", $"Produto: {produtoSkyhub.COD_PRODUTO}" });
                                            saveRegistro = false;
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
                                    Http.Timeout = TimeSpan.FromSeconds(30);

                                    //Busca pelo código do produto do mercado livre para poder realizar as próximas chamadas.
                                    //Realiza a busca pelo código sku, que vem do campo seller_custom_field.
                                    //Publicações com status closed seram apagadas pelo ML depois de um tempo.
                                    
                                    response = await Http.GetAsync($"/users/{Autenticacao.user_id}/items/search?sku={sincronizacaoSkyhub.COD_PRODUTO}&access_token={Autenticacao.access_token}");

                                    if (response.IsSuccessStatusCode)
                                    {
                                        Search search = await response.Content.ReadAsAsync<Search>();

                                        if (search.results.Count > 0)
                                        {
                                            response = await Http.PutAsJsonAsync($"/items/{search.results.First()}?access_token={Autenticacao.access_token}", ObjetoJsonFormatado(new { status = "closed" }));

                                            if (!response.IsSuccessStatusCode)
                                            {
                                                throw new HttpListenerException((int)response.StatusCode, $"PUT: {response.ReasonPhrase}. {response.Content.ReadAsStringAsync().Result}");
                                            }

                                            totalDeletados++;
                                        }
                                    }
                                    else
                                    {
                                        //Se não encontrou nenhum produto com aquele Sku, então já está apagado.

                                        if (response.StatusCode != HttpStatusCode.NotFound)
                                        {
                                            throw new HttpListenerException((int)response.StatusCode, $"Problema ao buscar id do produto no mercadolivre: {response.ReasonPhrase}. {response.Content.ReadAsStringAsync().Result}");
                                        }
                                    }
                                }
                            }
                            catch (Exception except)
                            {
                                ControladorExcecoes.Adiciona(except, new List<string> { $"Filial: {configuracaoSkyhub.COD_FILIAL}", $"Sincronização: {sincronizacaoSkyhub.COD_SINCRONIZACAO_SKYHUB}" });
                                continue;
                            }

                            //Altera o campo da sincronização para o produto não ficar sincronizando eternamente.
                            //Também salva este produto recentemente atualizado. Isso é uma forma preventiva para que em uma lista de sincronizações,
                            //caso haja algum item com problema de sincronização, não fique trancando essa fila, sendo necessário sincronizar tudo novamente.

                            if (saveRegistro)
                            {
                                //A variável saveRegistro, controla uma funcionalidade. Caso um produto não exista no lado do servidor da API,
                                //então não irá atualizar, verificar código anterior em que o valor da váriavel é alterado.

                                sincronizacaoSkyhub.DT_SINCRONIZACAO_ML = DateTime.Now;
                                sincronizacaoSkyhub.IND_SINCRONIZADO_ML = "S";
                                db.SaveChanges();
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
                        $"Nem todos os produtos foram sincronizados. Criados: {totalCriados}, Atualizados: {totalAtualizados}, Removidos: {totalDeletados}. " +
                        $"{ControladorExcecoes.Printa()}"
                    );
                }
            }
            catch (Exception except)
            {
                ControladorExcecoes.Adiciona(except);

                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    $"Não foi possível começar a sincronização. {ControladorExcecoes.Printa()}"
                );
            }
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
                throw new ArgumentException($"Produto skyhub: altura inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (produtoSkyhub.VAL_COMPRIMENTO < 0)
                throw new ArgumentException($"Produto skyhub: comprimento inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (produtoSkyhub.VAL_LARGURA < 0)
                throw new ArgumentException($"Produto skyhub: Altura inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.DESC_DESCRICAO))
                throw new ArgumentException($"Produto skyhub: ficha técnica inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.DESC_MARCA))
                throw new ArgumentException($"Produto skyhub: marca inválida, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.DESC_PRODUTO))
                throw new ArgumentException($"Produto skyhub: descrição do produto inválido, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.DESC_STATUS))
                throw new ArgumentException($"Produto skyhub: status inválido, produto: {produtoSkyhub.COD_PRODUTO}");
            if (String.IsNullOrEmpty(produtoSkyhub.COD_CATEGORIA_ML))
                throw new ArgumentException($"Produto skyhub: categoria inválida, produto: {produtoSkyhub.COD_PRODUTO}");
        }

        private async Task ValidaAutenticacao(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub)
        {
            if (!TokenEstaOK(configuracaoSkyhub))
            {
                HttpParams parans = new HttpParams().Add("client_id", configuracaoSkyhub.DESC_ML_CLIENT_ID)
                                                    .Add("client_secret", configuracaoSkyhub.DESC_ML_CLIENT_SECRET)
                                                    .Add("grant_type", "client_credentials");

                HttpResponseMessage response = await Http.PostAsync($"/oauth/token?{parans}", null);

                if (response.IsSuccessStatusCode)
                {
                    Autenticacao = await response.Content.ReadAsAsync<OAuth>();

                    configuracaoSkyhub.DESC_ACCESS_TOKEN_ML = Autenticacao.access_token;
                    configuracaoSkyhub.DESC_USER_ID_ML = Autenticacao.user_id;
                    configuracaoSkyhub.DT_ACCESS_TOKEN_ML = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    throw new HttpListenerException(401, $"Não foi possível obter token de autenticação: {response.StatusCode}, Mensagem: {response.Content.ReadAsStringAsync()}");
                }
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

        private object ObjetoJsonFormatado(object objeto)
        {
            JsonSerializerSettings jsonConfigs = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(objeto, jsonConfigs));
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
    }
}
