using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

using Cemapa.Models;


namespace Cemapa.Controllers
{
    public class CadastroEnderecoController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        public List<TB_CADASTRO_ENDERECOS> Get(int? id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = from c in db.TB_CADASTRO_ENDERECOS
                        where (id == null || c.COD_CADASTRO == id)
                        orderby c.DESC_ENDERECO
                        select c;
            return query.ToList();
        }

        [HttpGet]
        public System.Web.Mvc.JsonResult GetPersonalizado(int? id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from c in db.TB_CADASTRO_ENDERECOS
                         where c.COD_CADASTRO == id
                         orderby c.DESC_ENDERECO descending                         
                         select new
                         {
                             ENDERECO = c.DESC_ENDERECO,
                             CIDADE = c.DESC_CIDADE,
                             BAIRRO = c.DESC_BAIRRO,
                             ESTADO = c.COD_ESTADO,
                             CEP = c.DESC_CEP,
                             DISTRITO = c.DESC_DISTRITO                             
                         });

            return new System.Web.Mvc.JsonResult()
            {
                Data = query,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };
        }
    }
}
