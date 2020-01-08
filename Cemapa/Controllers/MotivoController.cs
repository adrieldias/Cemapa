using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Data.Entity.Validation;
using Cemapa.Models;
using System.Web.Http;

namespace Cemapa.Controllers
{
    public class MotivoController : ApiController
    {
        private Entities db = new Entities();

        
        [HttpGet]
        public IList<TB_MOTIVO> GetPersonalizado()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = from m in db.TB_MOTIVO
                        select m;

            return query.ToList();
        }
    }
}