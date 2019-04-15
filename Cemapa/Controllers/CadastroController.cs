using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Cemapa.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace Cemapa.Controllers
{
    public class CadastroController : ApiController
    {
        private Entities db = new Entities();
        
        //[Route("API/Cadastro/GetPersonalizado")]
        [HttpGet]
        public System.Web.Mvc.JsonResult GetPersonalizado()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from c in db.TB_CADASTRO
                            //.Include("TB_CIDADE")                            
                        orderby c.DT_CADASTRO descending
                        //select c //traz todos os campos
                        select new
                        {
                            CODIGO = c.COD_CADASTRO,
                            TIPO = c.TB_TIPO_CADASTRO.DESC_TIPO_CADASTRO,
                            NOME = c.NOME,                            
                            TELEFONE = c.DESC_TELEFONE,
                            CELULAR = c.DESC_CELULAR,
                            CNPJ_CPF = c.NUM_CGC_CPF,
                            ENDERECO = c.DESC_ENDERECO,
                            //CodCidade = c.COD_CIDADE,
                            CIDADE = c.TB_CIDADE.DESC_CIDADE,
                            BAIRRO = c.DESC_BAIRRO,
                            INSCRICAO = c.NUM_INSCRICAO,
                            FANTASIA = c.DESC_FANTASIA,
                            CLASSIFICACAO = c.TB_CLASS_CADASTRO.DESC_CLASSIFICACAO                            
                        }).Take(100);

            return new System.Web.Mvc.JsonResult()
            {
                Data = query,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };
        }

        //[Route("api/Cadastro/Get/{id}")]
        public TB_CADASTRO Get(int? id)
        {
            db.Configuration.LazyLoadingEnabled = false;           
            var query = (from c in db.TB_CADASTRO                                 
                                //.Include("TB_USUARIO")
                            where (id == null || c.COD_CADASTRO == id)
                            orderby c.DT_CADASTRO
                            select c).FirstOrDefault();
            return query;            
        }

        [System.Web.Http.HttpPost]
        public void Create([FromBody] TB_CADASTRO cadastro)
        {
            db.Configuration.LazyLoadingEnabled = false;

            db.TB_CADASTRO.Add(cadastro);
            db.SaveChanges();
            
        }       
    }

   
}
