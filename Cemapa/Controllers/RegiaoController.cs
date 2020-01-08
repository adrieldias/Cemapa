using Cemapa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cemapa.Controllers
{
    public class RegiaoController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        public IList<TB_REGIAO> Get(int? id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from r in db.TB_REGIAO
                         where (id == null || r.COD_REGIAO == id)
                         orderby r.DESC_REGIAO
                         select r).ToList();
            return query;                        
        }

        [HttpGet]
        public IList<TB_REGIAO> Get()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from r in db.TB_REGIAO                         
                         orderby r.DESC_REGIAO
                         select r).ToList();
            return query;
        }
    }
}
