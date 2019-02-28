using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Cemapa.Models;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace Cemapa.Controllers
{
    public class TipoCadastroController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        public ICollection<TB_TIPO_CADASTRO> Get()
        {
            db.Configuration.LazyLoadingEnabled = false;

            var query = from c in db.TB_TIPO_CADASTRO
                        orderby c.DESC_TIPO_CADASTRO
                        select c;
            //return new JsonResult()
            //{
            //    Data = query,
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //};

            return query.ToList();            
        }
    }
}
