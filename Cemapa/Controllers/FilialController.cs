using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Cemapa.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace Cemapa.Controllers
{
    public class FilialController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        public System.Web.Mvc.JsonResult GetPersonalizado()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from f in db.TB_FILIAL
                         orderby f.COD_FILIAL
                         select new
                         {
                             CODIGO = f.COD_FILIAL,
                             DESCRICAO = f.DESC_FILIAL,
                             IE = f.DESC_INSCRICAO,
                             CNPJ = f.NUM_CGC,
                             TELEFONE = f.DESC_TELEFONE
                         }
            );
            return new System.Web.Mvc.JsonResult()
            {
                Data = query,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public IList<TB_FILIAL> Get()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from f in db.TB_FILIAL
                         orderby f.COD_FILIAL
                         select f).ToList();
            return query;
        }
    }
}
