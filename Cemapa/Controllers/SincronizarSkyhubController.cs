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
using Newtonsoft.Json;

namespace Cemapa.Controllers
{
    public class SincronizarSkyhubController : ApiController
    {
        private Entities db = new Entities();


        [HttpGet]
        public async Task<RetornoJson> SincronizaPedidos()
        {
            try
            {
                int wTotalAtualizados = 0;
                int wTotalCriados = 0;

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

                            if (response.IsSuccessStatusCode)
                            {
                                try
                                {
                                    Order ordem = new Order();
                                    ordem = await response.Content.ReadAsAsync<Order>();

                                    //Verifica se ainda existem pedidos na fila, caso retorne nulo, então todos os pedidos já estão sincronizados

                                    if (ordem != null)
                                    {
                                        //Verifica se já existe esse pedido no sistema pelo campo COD_PEDIDO_MARKETPLACE
                                        //caso não exista insere um pedido novo, caso exista não faz nada.

                                        if (ordem.status.type == "APPROVED")
                                        {
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
                                                    DT_EMISSAO = DateTime.Now,
                                                    IND_SITUACAO = "1",
                                                    IND_TIPO_PAGAMENTO = "A VISTA",
                                                    IND_TIPO_FRETE = "CIF",
                                                    COD_PEDIDO_MARKETPLACE = ordem.code,
                                                    DESC_COMPLEMENTO_OBS = "Pedido do marketplace: " + ordem.code
                                                };

                                                foreach (Item item in ordem.items)
                                                {
                                                    int wCodItem = db.Database.SqlQuery<int>("SELECT SQPEDIDO_ITEM.NEXTVAL FROM DUAL").First();
                                                    int wCodLoteTipo = GetLoteTipo(item);
                                                    int wCodTributacao = GetTributacao(item);
                                                    long wCodProduto = GetProdutosSkyhub(item);

                                                    if (wCodLoteTipo == -1)
                                                    {
                                                        throw new Exception("Produto não possui nenhum lote configurado. Produto: " + item.id);
                                                    }

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
                                                            COD_PRODUTO = wCodProduto,
                                                            VAL_UNITARIO = Convert.ToDecimal(item.original_price),
                                                            QT_PEDIDO = item.qty,
                                                            COD_LOTE_TIPO = wCodLoteTipo,
                                                            COD_TRIBUTACAO = wCodTributacao
                                                        });
                                                }

                                                db.Entry(wPedidoCab).State = EntityState.Added;
                                                db.SaveChanges();

                                                wTotalCriados++;
                                            }
                                            else
                                            {
                                                wTotalAtualizados++;
                                            }
                                        }
                                        //Pedidos com status CANCELED são necessários para saber quando um cliente cancelou sua compra
                                        else if (ordem.status.type == "CANCELED")
                                        {
                                            if (!db.TB_PEDIDO_CAB.Any(p => p.COD_PEDIDO_MARKETPLACE == ordem.code))
                                            {
                                                TB_PEDIDO_CAB wPedidoCab = GetPedidoPorMarketplace(ordem);
                                                wPedidoCab.IND_SITUACAO = "2";
                                                wPedidoCab.DESC_COMPLEMENTO_OBS = "Cancelado pelo cliente";

                                                db.Entry(wPedidoCab).State = EntityState.Modified;
                                                db.SaveChanges();

                                                wTotalAtualizados++;
                                            }
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
                                    ControlaExcecoes.Add($"Erro ao sincronizar. Filial: {configuracaoSkyhub.COD_FILIAL}", response.ReasonPhrase, except.Message);
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
                        ControlaExcecoes.Add($"Erro ao sincronizar. Filial: {configuracaoSkyhub.COD_FILIAL}", except.Message);
                        continue;
                    }
                }

                if (ControlaExcecoes.SemExcecoes())
                {
                    return new RetornoJson()
                    {
                        Id = 0,
                        Status = "success",
                        Mensagem = "Os pedidos agora estão sincronizados.",
                        Complemento = $"Criados: {wTotalCriados}, Atualizados: {wTotalAtualizados}"
                    };
                }
                else
                {
                    return new RetornoJson()
                    {
                        Id = 0,
                        Status = "erro",
                        Mensagem = "Nem todos os pedidos foram sincronizados.",
                        Complemento = $"Criados: {wTotalCriados}, Atualizados: {wTotalAtualizados}",
                        Corpo = ControlaExcecoes.Excecoes
                    };
                }
            }
            catch (Exception except)
            {
                return new RetornoJson()
                {
                    Id = 0,
                    Status = "erro",
                    Mensagem = "Não foi possível começar a sincronização.",
                    Complemento = except.Message
                };
            }
        }

        [HttpGet]
        public async Task<RetornoJson> SincronizaProdutos()
        {
            try
            {
                int totalAtualizados = 0;
                int totalCriados = 0;
                int totalDeletados = 0;

                bool saveRegistro = true;

                ControlaExcecoes.Limpa();

                //Busca sincronizações de produtos pendentes (campo IND_SINCRONIZADO esteja igual a "N").

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

                                            if (wTotalEstoque < 1)
                                            {
                                                throw new Exception("Produto sem estoque");
                                            }

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
                                                //Por enquanto, o campo custo será sempre zero, solicitado por Marcos, até descobrirmos o real
                                                //propósito de existir tal campo em uma loja online.

                                                ProdutoSkyhub ProdutoSku = new ProdutoSkyhub
                                                {
                                                    sku = produtoSkyhub.COD_PRODUTO,
                                                    name = produtoSkyhub.DESC_PRODUTO,
                                                    description = produtoSkyhub.DESC_DESCRICAO,
                                                    status = produtoSkyhub.DESC_STATUS,
                                                    qty = Convert.ToInt32(wTotalEstoque),
                                                    price = Convert.ToDouble(wInfosProduto.VAL_VAREJO),
                                                    weight = Convert.ToDouble(wInfosProduto.NUM_PESO_BRUTO),
                                                    width = Convert.ToDouble(produtoSkyhub.VAL_LARGURA),
                                                    height = Convert.ToDouble(produtoSkyhub.VAL_ALTURA),
                                                    length = Convert.ToDouble(produtoSkyhub.VAL_COMPRIMENTO),
                                                    brand = produtoSkyhub.DESC_MARCA,
                                                    ean = wInfosProduto.DESC_COD_BARRA,
                                                    nbm = wInfosProduto.NUM_GENERO_NCM
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
                                                saveRegistro = false;
                                            }
                                        }
                                        catch (Exception except)
                                        {
                                            ControlaExcecoes.Add($"Erro ao sincronizar. Filial: {configuracaoSkyhub.COD_FILIAL}", $"produto: {produtoSkyhub.COD_PRODUTO}", except.Message);
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
                                            //Usado para uma tentativa de deletar um produto que já foi apagada não somar.

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
                                ControlaExcecoes.Add($"Erro ao sincronizar. Filial: {configuracaoSkyhub.COD_FILIAL}", except.Message);
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
                        ControlaExcecoes.Add($"Erro ao sincronizar. Sincronização: {sincronizacaoSkyhub.COD_SINCRONIZACAO_SKYHUB}", except.Message);
                        continue;
                    }
                }

                if (ControlaExcecoes.SemExcecoes())
                {
                    return new RetornoJson()
                    {
                        Id = 0,
                        Status = "success",
                        Mensagem = "Os produtos agora estão sincronizados em todas as filiais.",
                        Complemento = $"Criados: {totalCriados}, Atualizados: {totalAtualizados}, Removidos: {totalDeletados}"

                    };
                }
                else
                {
                    return new RetornoJson()
                    {
                        Id = 0,
                        Status = "erro",
                        Mensagem = "Nem todos os produtos foram sincronizados.",
                        Complemento = $"Criados: {totalCriados}, Atualizados: {totalAtualizados}, Removidos: {totalDeletados}",
                        Corpo = ControlaExcecoes.Excecoes

                    };
                }
            }
            catch (Exception except)
            {
                return new RetornoJson()
                {
                    Id = 0,
                    Status = "erro",
                    Mensagem = "Não foi possível começar a sincronização.",
                    Complemento = except.Message
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
                produtoSkyhub.VAL_ALTURA < 1 ||
                produtoSkyhub.VAL_COMPRIMENTO < 1 ||
                produtoSkyhub.VAL_LARGURA < 1 ||
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
                int wCodigoCadastro = db.Database.SqlQuery<int>("SELECT SQCADASTRO.NEXTVAL FROM DUAL").First();
                string wSexo;

                if (ordem.customer.gender == "male")
                {
                    wSexo = "M";
                }
                else
                {
                    wSexo = "F";
                }

                TB_CADASTRO wNovo = new TB_CADASTRO()
                {
                    COD_CADASTRO = wCodigoCadastro,
                    NOME = ordem.customer.name,
                    NUM_CGC_CPF = ordem.customer.vat_number,
                    DESC_E_MAIL = ordem.customer.email,
                    DT_NASCIMENTO = Convert.ToDateTime(ordem.customer.date_of_birth),
                    DESC_CELULAR = ordem.billing_address.phone,
                    DESC_TELEFONE = ordem.billing_address.secondary_phone,
                    DESC_ENDERECO = ordem.billing_address.street + ", " + ordem.billing_address.number,
                    DESC_COMPLEMENTO = ordem.billing_address.complement,
                    DESC_BAIRRO = ordem.billing_address.neighborhood,
                    COD_ESTADO = ordem.billing_address.region.ToUpper(),
                    IND_SEXO_CATEGORIA = wSexo,
                    COD_TIPO_CADASTRO = 1
                };

                db.Entry(wNovo).State = EntityState.Added;
                db.SaveChanges();

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
    }
}
