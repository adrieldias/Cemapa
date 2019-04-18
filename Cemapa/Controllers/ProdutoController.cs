using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Cemapa.Models;

namespace Cemapa.Controllers
{
    public class ProdutoController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        public TB_PRODUTO Get(int? id)
        {            
            db.Configuration.LazyLoadingEnabled = false;

            var query = (from c in db.TB_PRODUTO 
                         .Include("TB_CLASSE")
                         where (id == null || c.COD_PRODUTO == id)
                         orderby c.COD_PRODUTO
                         select c).FirstOrDefault();
            return query;
        }

        [HttpPost]
        public void Create([FromBody] TB_PRODUTO produto)
        {
            db.Configuration.LazyLoadingEnabled = false;

            db.TB_PRODUTO.Add(produto);
            db.SaveChanges();

        }


    }
}
