using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                int totalAtualizados = 0;
                int totalCriados = 0;
                int totalDeletados = 0;

                bool saveRegistro = true;

                //Busca sincronizações de produtos pendentes (campo IND_SINCRONIDO esteja igual a "N").

                List<TB_SINCRONIZACAO_SKYHUB> sincronizacoesSkyhub = GetSincronizacoes();
                
                foreach (var sincronizacaoSkyhub in sincronizacoesSkyhub)
                {
                    //Busca todas as configurações ativas para se conectar com a API.

                    List<TB_CONFIGURACAO_SKYHUB> configuracoesSkyhub = GetConfiguracoes();

                    foreach (var configuracaoSkyhub in configuracoesSkyhub)
                    {
                        //Seleciona as configurações do produto na tabela TB_PRODUTO_SKYHUB referente ao produto que se deseja sincronizar.

                        List<TB_PRODUTO_SKYHUB> produtosSkyhub = GetProdutosSkyhub(sincronizacaoSkyhub);

                        foreach (var produtoSkyhub in produtosSkyhub)
                        {
                            //Verifica se os dados atualmente deste produto precisam ser atualizados antes de sincronizar.

                            //Caso o campo IND_ATUALIZA_VALORES seja igual a "N", os valores do produto (Apenas alguns campos) skyhub ficará fixo,
                            //não sendo atualizado antes de enviar.

                            //Caso o campo IND_ATUALIZA_VALORES seja igual a "S", todos os campos do produto skyhub serão atualizados antes
                            //de sincronizar.

                            if (produtoSkyhub.IND_ATUALIZA_VALORES == "S")
                            {
                                //Procura pela quantidade e custo na tabela estoque (Caso o registro não seja encontrada, ficará zerado).
                                //Aqui são atualizados: preço, peso bruto, quantidade e custo.

                                decimal estoqueTotal = SomaEstoque(configuracaoSkyhub,produtoSkyhub);

                                produtoSkyhub.VAL_PRECO = produtoSkyhub.TB_PRODUTO.VAL_VAREJO;
                                produtoSkyhub.VAL_PESO = produtoSkyhub.TB_PRODUTO.NUM_PESO_BRUTO;
                                produtoSkyhub.QT_QUANTIDADE = estoqueTotal;
                                produtoSkyhub.VAL_CUSTO_MEDIO = 0;
                                db.SaveChanges();
                            }

                            //Faz algumas verificações em alguns campos antes de sincronizar.

                            ValidaProdutoSkyhub(produtoSkyhub);

                            Http.BaseAddress = new Uri("https://api.skyhub.com.br");
                            
                            Http.DefaultRequestHeaders.Accept.Clear();
                            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            Http.DefaultRequestHeaders.Add("X-User-Email", configuracaoSkyhub.DESC_USUARIO_EMAIL);
                            Http.DefaultRequestHeaders.Add("X-Api-Key", configuracaoSkyhub.DESC_TOKEN_INTEGRACAO);
                            Http.DefaultRequestHeaders.Add("X-Accountmanager-Key", configuracaoSkyhub.DESC_TOKEN_ACCOUNT);
                            Http.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                            //Instancia o produto da Classe ProdutoSkyhub, criada conforme a estrutura especificada no manual da API.
                            //Por enquanto, o campo custo será sempre zero, solicitado por Marcos, até descobrirmos o real
                            //propósito de existir tal campo em uma loja online.

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
                                        key = espec.DESC_ESPECIFICACAO,
                                        value = espec.VAL_ESPECIFICACAO
                                    }
                                );
                            }

                            foreach (var imagem in produtoSkyhub.TB_PRODUTO_IMAGEM_SKYHUB)
                            {
                                ProdutoSku.images.Add(imagem.DESC_IMAGEM);
                            }

                            //Adiciona o produto skyhub na chave "products", padrão da API.

                            Dictionary<string, ProdutoSkyhub> products = new Dictionary<string, ProdutoSkyhub>
                            {
                                { "product", ProdutoSku }
                            };

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

                                                ExcecoesHttp excecaoHttp = new ExcecoesHttp(response, "PUT", $"Sku: {ProdutoSku.sku}. Alterado para POST", false);
                                                excecaoHttp.Drop();
                                            }
                                            else
                                            {
                                                ExcecoesHttp excecaoHttp = new ExcecoesHttp(response, sincronizacaoSkyhub.TIPO_ACAO, $"Sku: {ProdutoSku.sku}");
                                                excecaoHttp.Drop();
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
                                            ExcecoesHttp excecaoHttp = new ExcecoesHttp(response, sincronizacaoSkyhub.TIPO_ACAO, $"Sku: {ProdutoSku.sku}");
                                            excecaoHttp.Drop();
                                        }

                                        totalDeletados++;
                                    }
                                    break;
                                case "POST":
                                    {
                                        HttpResponseMessage response = await Http.PostAsJsonAsync("/products", products);

                                        if (!response.IsSuccessStatusCode)
                                        {
                                            ExcecoesHttp excecaoHttp = new ExcecoesHttp(response, sincronizacaoSkyhub.TIPO_ACAO, $"Sku: {ProdutoSku.sku}");
                                            excecaoHttp.Drop();
                                        }

                                        totalCriados++;
                                    }
                                    break;
                            }
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


                return new System.Web.Mvc.JsonResult()
                {
                    Data = new RetornoJson()
                    {
                        Id = 0,
                        Status = "success",
                        Mensagem = "Os produtos agora estão sincronizados em todas as filiais.",
                        Complemento = $"Criados: {totalCriados}, Atualizados: {totalAtualizados}, Removidos: {totalDeletados}"

                    }
                };
            }
            catch (Exception except)
            {
                return new System.Web.Mvc.JsonResult()
                {
                    Data = new RetornoJson()
                    {
                        Id = except.HResult,
                        Status = "error",
                        Mensagem = except.Message,
                        Complemento = $"{except.Source}.{except.GetType().Name}.{except.TargetSite.Name}"
                    }
                };
            }
        }

        private List<TB_CONFIGURACAO_SKYHUB> GetConfiguracoes()
        {
            return (from configuracaoSkyhub in db.TB_CONFIGURACAO_SKYHUB
                    where configuracaoSkyhub.IND_ATIVO == "S"
                    select configuracaoSkyhub).ToList();
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
                    && produtoSkyhub.IND_SINCRONIZA == "S"
                    select produtoSkyhub).ToList();
        }

        private decimal SomaEstoque(TB_CONFIGURACAO_SKYHUB configuracaoSkyhub, TB_PRODUTO_SKYHUB produtoSkyhub)
        {
            List<TB_ESTOQUE> estoques = (from dbEstoque in db.TB_ESTOQUE
                                         where dbEstoque.COD_PRODUTO == produtoSkyhub.COD_PRODUTO
                                         && dbEstoque.COD_FILIAL == configuracaoSkyhub.COD_FILIAL
                                         select dbEstoque).ToList();

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
