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
                        where c.COD_CADASTRO < 10
                        orderby c.NOME
                        //select c //traz todos os campos
                        select new
                        {
                            Codigo = c.COD_CADASTRO,
                            Nome = c.NOME
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
