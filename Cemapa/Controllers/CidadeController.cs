using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cemapa.Models;

namespace Cemapa.Controllers
{
    public class CidadeController : ApiController
    {
        private Entities db = new Entities();

        public IList<TB_CIDADE> Get()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = from c in db.TB_CIDADE
                        orderby c.DESC_CIDADE
                        select c;
            return query.ToList();
        }
        
        [HttpGet]
        public IList<TB_CIDADE> GetCidadesPorEstado(string id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = from c in db.TB_CIDADE
                        where (c.COD_ESTADO.Equals(id))
                        orderby c.DESC_CIDADE
                        select c;
            return query.ToList();
        }
    }
}
