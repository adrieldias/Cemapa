using Cemapa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cemapa.Controllers
{
    public class OperacaoController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        public IList<TB_OPERACAO> Get()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from o in db.TB_OPERACAO
                         orderby o.DESC_OPERACAO
                         select o).ToList();
            return query;
        }

        [HttpGet]
        public IList<TB_OPERACAO> Get(string tipoOperacao)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = (from o in db.TB_OPERACAO
                         where o.IND_TIPO_OPERACAO == tipoOperacao
                         orderby o.DESC_OPERACAO
                         select o).ToList();
            return query;
        }
    }
}
