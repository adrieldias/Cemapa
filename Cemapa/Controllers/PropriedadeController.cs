using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cemapa.Models;

namespace Cemapa.Controllers
{
    public class PropriedadeController : ApiController
    {
        private Entities db = new Entities();

        [HttpGet]
        public TB_PROPRIEDADE Get(int? id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var query = from p in db.TB_PROPRIEDADE
                            //.Include("TB_CIDADE")
                        where (id == null || p.COD_PROPRIEDADE == id)
                        select p;
            return query.FirstOrDefault();
        }

        [HttpGet]
        public System.Web.Mvc.JsonResult GetPersonalizado([FromUri] TB_PROPRIEDADE id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if (ModelState.IsValid)
            {
                try
                {
                    var query = (from p in db.TB_PROPRIEDADE
                            .Include("TB_CIDADE")
                            .Include("TB_TIPO_PROPRIEDADE")
                                where (id.COD_PROPRIEDADE == 0 || p.COD_PROPRIEDADE == id.COD_PROPRIEDADE)
                                where (id.COD_CADASTRO == null || p.COD_CADASTRO == id.COD_CADASTRO)
                                where (id.DESC_PROPRIEDADE == string.Empty || p.DESC_PROPRIEDADE == id.DESC_PROPRIEDADE)
                                orderby p.DESC_PROPRIEDADE
                                select new
                                {
                                    CODIGO = p.COD_PROPRIEDADE,
                                    NOME = p.DESC_PROPRIEDADE,
                                    ENDERECO = p.DESC_LOCALIDADE,
                                    CIDADE = p.TB_CIDADE.DESC_CIDADE,
                                    BAIRRO = p.DESC_BAIRRO,
                                    CEP = p.DESC_CEP,
                                    AREA = p.NUM_AREA,
                                    VALOR = p.VAL_PROPRIEDADE,
                                    MATRICULA = p.NUM_MATRICULA,
                                    CRI = p.DESC_CRI,
                                    TIPO = p.TB_TIPO_PROPRIEDADE.DESC_TIPO_PROPRIEDADE,
                                    PROPRIO = p.IND_TIPO_IMOVEL                                    
                                });
                    return new System.Web.Mvc.JsonResult()
                    {
                        Data = query,
                        JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
                    };                     
                }catch(Exception ex)
                {
                    return new System.Web.Mvc.JsonResult()
                    {
                        Data = ex.Message,
                        JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
                    };
                }
            }

            return new System.Web.Mvc.JsonResult()
            {
                Data = "Modelo Invalido",
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public System.Web.Mvc.JsonResult Save([FromBody] TB_PROPRIEDADE propriedade)
        {

            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            if (ModelState.IsValid)
            {
                return new System.Web.Mvc.JsonResult()
                {
                    Data = propriedade.DESC_PROPRIEDADE,
                    JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
                };
            }

            return new System.Web.Mvc.JsonResult()
            {
                Data = "ERRO",
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };
        }
    }
}
