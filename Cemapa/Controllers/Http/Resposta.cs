using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cemapa.Controllers.Http
{
    public static class Resposta
    {
        static List<HttpStatusCode> StatusPrevistos = new List<HttpStatusCode>();

        public static void Status404Previsto()
        {
            StatusPrevistos.Add(HttpStatusCode.NotFound);
        }

        public static async Task<HttpResponseMessage> Status200OrDie(this HttpResponseMessage response)
        {
            if ((!response.IsSuccessStatusCode) && (!StatusPrevistos.Contains(response.StatusCode)))
            {
                HttpStatusCode code = response.StatusCode;
                string reason = response.ReasonPhrase;
                string url = response.RequestMessage.RequestUri.AbsoluteUri;
                string method = response.RequestMessage.Method.Method;
                string body = await response.Content.ReadAsStringAsync();

                body = String.IsNullOrEmpty(body) ? "Corpo da resposta está vazio." : body.Replace("\""," ");

                switch (code)
                {
                    case HttpStatusCode.BadRequest: reason = "Requisição mal formada(400)"; break;
                    case HttpStatusCode.Forbidden: reason = "Acesso não permitido(403)"; break;
                    case HttpStatusCode.NotFound: reason = "Não encontrado(404)"; break;
                    case HttpStatusCode.InternalServerError: reason = "Erro interno no servidor(500)"; break;
                    case HttpStatusCode.BadGateway: reason = "Servidor obteve uma resposta inválida(502)"; break;
                }

                StatusPrevistos.Clear();

                throw new HttpRequestException($"{method}:{url}, {reason}, {body}");
            }
            else
            {
                StatusPrevistos.Clear();
                return response;
            }
        }
    }
}