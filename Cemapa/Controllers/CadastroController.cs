using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Cemapa.Models;
using System.Web.Mvc;


namespace Cemapa.Controllers
{
    public class CadastroController : ApiController
    {
        private Entities db = new Entities();

        public /*System.Linq.IOrderedQueryable<TB_CADASTRO>*/ JsonResult Get()
        {
            var query = from c in db.TB_CADASTRO
                            .Include("TB_CIDADE")                            
                            .Include("TB_TIPO_CADASTRO")
                            .Include("TB_CLASS_CADASTRO")
                        where c.COD_CADASTRO < 10
                        orderby c.NOME
                        //select c //traz todos os campos
                        select new
                        {
                            Codigo = c.COD_CADASTRO,
                            Tipo = c.TB_TIPO_CADASTRO.DESC_TIPO_CADASTRO,
                            Nome = c.NOME,                            
                            Telefone = c.DESC_TELEFONE,
                            Celular = c.DESC_CELULAR,
                            CGC_CPF = c.NUM_CGC_CPF,
                            Endereco = c.DESC_ENDERECO,
                            CodCidade = c.COD_CIDADE,
                            Cidade = c.TB_CIDADE.DESC_CIDADE,
                            Bairro = c.DESC_BAIRRO,
                            Inscricao = c.NUM_INSCRICAO,
                            Fantasia = c.DESC_FANTASIA,
                            Classificacao = c.TB_CLASS_CADASTRO.DESC_CLASSIFICACAO
                        };

            return new JsonResult()
            {
                Data = query,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            //return query;
        }
    }
}
