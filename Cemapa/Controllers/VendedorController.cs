using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cemapa.Models;
using System.Web.Mvc;

namespace Cemapa.Controllers
{
    public class VendedorController : ApiController
    {
        private Entities db = new Entities();

        public IList<TB_VENDEDOR> Get()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = from c in db.TB_VENDEDOR
                        select c;
            return query.ToList();
        }
    }
}
