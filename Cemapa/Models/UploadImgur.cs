using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cemapa.Models.Imgur
{
    public class UploadImgur
    {
        private readonly List<Carregamento> Carregamentos = new List<Carregamento>();
        private Cliente Conta = new Cliente();
        private Credencial Autenticacao = new Credencial();
        private readonly int AcessTokenValidade = 2419200;

        public TokenExpiravel AccessToken()
        {
            return Autenticacao.AccessToken;
        }

        public void Carrega(byte[] imagem)
        {
            if(imagem != null && imagem.Length > 0)
            {
                Carregamentos.Add(
                    new Carregamento
                    {
                        Imagem = imagem
                    }
                );
            }
        }
        
        public async Task Configura(string clientId, string clientSecret, string refreshToken, string accessToken, DateTime dtAccessToken)
        {
            if (String.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("Conta Imgur não configurada");
            }

            Conta = new Cliente
            {
                ClientID = clientId,
                ClientSecret = clientSecret
            };

            if (!String.IsNullOrEmpty(refreshToken))
            {
                Autenticacao = new Credencial
                {
                    RefreshToken = refreshToken,
                    AccessToken = new TokenExpiravel(dtAccessToken, AcessTokenValidade)
                };
                
                if (Autenticacao.AccessToken.Expirou())
                {
                    ImgurClient client = new ImgurClient(Conta.ClientID, Conta.ClientSecret);
                    OAuth2Endpoint endpoint = new OAuth2Endpoint(client);
                    IOAuth2Token token = await endpoint.GetTokenByRefreshTokenAsync(Autenticacao.RefreshToken);

                    Autenticacao.AccessToken.OAuth = token;
                    Autenticacao.AccessToken.DataRegistro = DateTime.Now;
                }
                else
                {
                    Autenticacao.AccessToken.OAuth = new OAuth2Token(accessToken, refreshToken, "", "", "", 0);
                }
            }
        }

        public async Task Sobe()
        {
            foreach (Carregamento up in Carregamentos)
            {
                ImgurClient client = new ImgurClient(Conta.ClientID, Conta.ClientSecret);

                if (Autenticacao.AccessToken != null)
                {
                    client.SetOAuth2Token(Autenticacao.AccessToken.OAuth);
                }

                ImageEndpoint endpoint = new ImageEndpoint(client);

                IImage image = await endpoint.UploadImageStreamAsync(new MemoryStream(up.Imagem));
                    
                up.Enviado = DateTime.Now;
                up.Link = image.Link;
            }
        }

        public string Link(byte[] imagem)
        {
            if (Carregamentos.Exists(x => x.Imagem == imagem))
            {
                return Carregamentos.Find(x => x.Imagem == imagem).Link;
            }
            else
            {
                return null;
            }
        }

        public bool FoiEnviado(byte[] imagem)
        {
            return Carregamentos.Exists(x => x.Imagem == imagem);
        }
    }

    public class Carregamento
    {
        public byte[] Imagem { get; set; }
        public DateTime? Enviado { get; set; }
        public string Link { get; set; }
    }

    public class Cliente
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
    }

    public class Credencial
    {
        public string RefreshToken { get; set; }
        public TokenExpiravel AccessToken { get; set; }
    }

    public class TokenExpiravel
    {
        public IOAuth2Token OAuth { get; set; }
        public DateTime DataRegistro { get; set; }
        public int Validade { get; }
        
        public TokenExpiravel(DateTime dtRegistro, int segundos)
        {
            DataRegistro = dtRegistro;
            Validade = segundos;
        }

        public bool Expirou()
        {
            DateTime dataExpiracao = Convert.ToDateTime(DataRegistro).AddSeconds(Validade);

            if (DateTime.Now > dataExpiracao)
            {
                return true;
            }

            return false;
        }
    }
}