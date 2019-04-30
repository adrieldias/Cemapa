using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Cemapa.Models;

namespace Cemapa.Controllers
{
    public class SincronizarSkyhubController : ApiController
    {
        private Entities db = new Entities();
        private HttpClient Http = new HttpClient();

        [HttpGet]
        public async Task<System.Web.Mvc.JsonResult> SincronizaAsync()
        {
            try
            {
                List<TB_CONFIGURACAO_SKYHUB> configuracoesSkyhub = GetConfiguracoes();

                foreach (var configuracaoSkyhub in configuracoesSkyhub)
                {

                    List<TB_SINCRONIZACAO_SKYHUB> sincronizacoesSkyhub = GetSincronizacoes();

                    foreach (var sincronizacaoSkyhub in sincronizacoesSkyhub)
                    {
                        List<TB_PRODUTO_SKYHUB> produtosSkyhub = GetProdutosSkyhub(sincronizacaoSkyhub);

                        foreach (var produtoSkyhub in produtosSkyhub)
                        {
                            if (produtoSkyhub.IND_ATUALIZA_VALORES == "S")
                            {
                                decimal estoqueTotal = SomaEstoque(configuracaoSkyhub,produtoSkyhub);

                                produtoSkyhub.VAL_PRECO = produtoSkyhub.TB_PRODUTO.VAL_VAREJO;
                                produtoSkyhub.VAL_PESO = produtoSkyhub.TB_PRODUTO.NUM_PESO_BRUTO;
                                produtoSkyhub.QT_QUANTIDADE = estoqueTotal;
                                produtoSkyhub.VAL_CUSTO_MEDIO = 0;
                                db.SaveChanges();
                            }

                            ValidaProdutoSkyhub(produtoSkyhub);

                            Http.BaseAddress = new Uri("https://api.skyhub.com.br");
                            
                            Http.DefaultRequestHeaders.Accept.Clear();
                            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                            Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                            Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                            Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                            ProdutoSkyhub ProdutoSku = new ProdutoSkyhub
                            {
                                sku = produtoSkyhub.COD_PRODUTO,
                                name = produtoSkyhub.DESC_PRODUTO,
                                description = produtoSkyhub.DESC_DESCRICAO,
                                status = produtoSkyhub.DESC_STATUS,
                                qty = Convert.ToInt32(produtoSkyhub.QT_QUANTIDADE),
                                price = Convert.ToDouble(produtoSkyhub.VAL_PRECO),
                                promotional_price = Convert.ToDouble(produtoSkyhub.VAL_PRECO_PROMOCIONAL),
                                cost = Convert.ToDouble(produtoSkyhub.VAL_CUSTO_MEDIO),
                                weight = Convert.ToDouble(produtoSkyhub.VAL_PESO),
                                width = Convert.ToDouble(produtoSkyhub.VAL_PESO),
                                height = Convert.ToDouble(produtoSkyhub.VAL_PESO),
                                length = Convert.ToDouble(produtoSkyhub.VAL_PESO),
                                brand = produtoSkyhub.DESC_MARCA,
                                ean = produtoSkyhub.DESC_EAN,
                                nbm = produtoSkyhub.DESC_NCM
                            };

                            foreach (var categoria in produtoSkyhub.TB_PRODUTO_CATEGORIA_SKYHUB)
                            {
                                ProdutoSku.categories.Add(
                                    new CategoriaProdutoSkyhub
                                    {
                                        code = categoria.COD_PRODUTO_CATEGORIA_SKYHUB,
                                        name = categoria.DESC_CATEGORIA
                                    }
                                );
                            }

                            foreach (var espec in produtoSkyhub.TB_PRODUTO_ESP_SKYHUB)
                            {
                                ProdutoSku.specifications.Add(
                                    new EspecificacoesProdutoSkyhub
                                    {
                                        key = espec.COD_PRODUTO_ESP_SKYHUB,
                                        value = espec.DESC_ESPECIFICACAO
                                    }
                                );
                            }

                            foreach (var imagem in produtoSkyhub.TB_PRODUTO_IMAGEM_SKYHUB)
                            {
                                ProdutoSku.images.Add(imagem.DESC_IMAGEM);
                            }

                            switch (sincronizacaoSkyhub.TIPO_ACAO)
                            {
                                case "PUT":
                                    {
                                        HttpResponseMessage response = await Http.PutAsJsonAsync("/products", ProdutoSku);

                                        if (!response.IsSuccessStatusCode)
                                        {
                                            throw new Exception("Não foi possível atualizar produto: " + response.ReasonPhrase);
                                        }
                                    }
                                    break;
                                case "DELETE":
                                    {
                                        HttpResponseMessage response = await Http.DeleteAsync("/products/" + ProdutoSku.sku);

                                        if (!response.IsSuccessStatusCode)
                                        {
                                            throw new Exception("Não foi possível deletar produto: " + response.ReasonPhrase);
                                        }
                                    }
                                    break;
                                case "POST":
                                    {
                                        HttpResponseMessage response = await Http.PostAsJsonAsync("/products", ProdutoSku);

                                        if (!response.IsSuccessStatusCode)
                                        {
                                            throw new Exception("Não foi possível adicionar produto: " + response.ReasonPhrase);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }

                return new System.Web.Mvc.JsonResult()
                {
                    Data = new RetornoJson(
                        "success",
                        "Atualizado com sucesso."
                    )
                };
            }
            catch (Exception except)
            {
                return new System.Web.Mvc.JsonResult()
                {
                    Data = new RetornoJson(
                        "error",
                        except.Message,
                        except.Source,
                        except.HResult
                    )
                };
            }
        }

        private List<TB_CONFIGURACAO_SKYHUB> GetConfiguracoes()
        {
            return (from configuracaoSkyhub in db.TB_CONFIGURACAO_SKYHUB where configuracaoSkyhub.IND_ATIVO == "S" select configuracaoSkyhub).ToList();
        }

        private List<TB_SINCRONIZACAO_SKYHUB> GetSincronizacoes()
        {
            return (from sincronizacaoSkyhub in db.TB_SINCRONIZACAO_SKYHUB where sincronizacaoSkyhub.IND_SINCRONIZADO == "N" select sincronizacaoSkyhub).ToList();
        }

        private List<TB_PRODUTO_SKYHUB> GetProdutosSkyhub(TB_SINCRONIZACAO_SKYHUB sincronizacaoSkyhub)
        {
            return (from produtoSkyhub in db.TB_PRODUTO_SKYHUB where produtoSkyhub.COD_PRODUTO == sincronizacaoSkyhub.COD_PRODUTO && produtoSkyhub.IND_SINCRONIZA == "S" select produtoSkyhub).ToList();
        }

        private decimal SomaEstoque(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub, TB_PRODUTO_SKYHUB produtoSkyhub)
        {
            List<TB_ESTOQUE> estoques = (from dbEstoque in db.TB_ESTOQUE where dbEstoque.COD_PRODUTO == produtoSkyhub.COD_PRODUTO && dbEstoque.COD_FILIAL == configuracaoSkyhub.COD_FILIAL select dbEstoque).ToList();

            decimal total = 0;

            foreach (var estoque in estoques)
            {
                total += estoque.QT_QUANTIDADE;
            }
            return total;
        }

        private void ValidaProdutoSkyhub(TB_PRODUTO_SKYHUB produtoSkyhub)
        {
            if (
                produtoSkyhub.DESC_DESCRICAO.Equals("") ||
                produtoSkyhub.DESC_MARCA.Equals("") ||
                produtoSkyhub.DESC_PRODUTO.Equals("") ||
                produtoSkyhub.DESC_STATUS.Equals("")
                )
            {
                throw new Exception("Produto código: " + produtoSkyhub.COD_PRODUTO + " não esta corretamente preenchido para sincronizar");
            }
        }
    }
}
