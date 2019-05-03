using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Cemapa.Models
{
    public class ExcecoesHttp
    {
        private readonly string Conteudo;
        private readonly string Status;
        private readonly string FraseRazao;
        private readonly string Metodo;
        private readonly string Detalhes;
        private readonly bool Grava;

        public ExcecoesHttp(HttpResponseMessage resposta, string metodo, string mensagemAdicional = "", bool gravar = true)
        {
            Conteudo = resposta.Content.ReadAsStringAsync().Result;
            Status = resposta.StatusCode.ToString();
            FraseRazao = resposta.ReasonPhrase;
            Metodo = metodo;
            Grava = gravar;

            if ((Conteudo == null) || (Conteudo == ""))
            {
                switch(Status)
                {
                    case "400":
                        Conteudo = "Requisição mal formada, provavelmente a requisição JSON enviada para o servidor não esta em um formato correto";
                        break;
                    case "500":
                        Conteudo = "Erro interno do servidor, provavelmente a requisição JSON enviada para o servidor não está em um formato correto ou nos raros casos, houve um erro interno do servidor";
                        break;
                    case "404":
                        Conteudo = "Não encontrado. Não foi possível encontrar o objeto solicitado no servidor. Ele não existe ou pode ter sido apagado";
                        break;
                    case "401":
                        Conteudo = "Sem autorização. Esta requisição não foi autorizada pelo servidor. Verifique se há alguma validação não configurada corretamente";
                        break;
                    case "402":
                        Conteudo = "Pagamento requerido. Esta requisição não foi autorizada pelo servidor. Verifique pagamentos ou faturas atrasadas";
                        break;
                    case "403":
                        Conteudo = "Proibido! A requisição não tem permissão para acessar a área requisitada";
                        break;
                    case "408":
                        Conteudo = "Tempo esgotado. Não obteve resposta dentro do tempo da requisição. Verifique sua conexão com a internet ou se o destino está disponível";
                        break;
                    case "503":
                        Conteudo = "Serviço fora de operação. O servidor está rejeitando conexões ou está fora do ar. Tenta novamente mais tarde";
                        break;
                    default:
                        Conteudo = $"Erro não tratado. Código: {Status}";
                        break;
                }
            }

            switch (metodo)
            {
                case "PUT": Detalhes = $"Erro na chamada PUT"; break;
                case "POST": Detalhes = $"Erro na chamada POST"; break;
                case "DELETE": Detalhes = $"Erro na chamada DELETE"; break;
                default: Detalhes = $"Tipo de chamada Http desconhecida: ({metodo})"; break;
            }

            if (mensagemAdicional != "")
            {
                Detalhes = $"Detalhes: {Detalhes}. Informações adicionais: {mensagemAdicional}";
            }
        }

        public void Drop()
        {
            if (Grava)
            {
                GravaLog.Log($"Body: {Conteudo}. {Detalhes}");
            }
            throw new Exception($"{Conteudo}. {Detalhes}");
        }
    }
}