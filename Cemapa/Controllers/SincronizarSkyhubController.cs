using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Cemapa.Models;

namespace Cemapa.Controllers
{
    public class SincronizarSkyhubController : ApiController
    {
        private Entities db = new Entities();
        //Api/SincronizarSkyhub/Sincroniza
        [HttpGet]
        public System.Web.Mvc.JsonResult Sincroniza()
        {
            try
            {

                List<TB_PRODUTO> produtos = (from produto in db.TB_PRODUTO where produto.IND_SYNC_SKYHUB == "N" select produto).ToList();
                
                foreach(var produto in produtos)
                {
                    produto.IND_SYNC_SKYHUB = "S";
                }
                db.SaveChanges();

                return new System.Web.Mvc.JsonResult()
                {
                    Data = new RetornoJson(
                        "success",
                        "Atualizado com sucesso."
                    )
                };
            }
            catch (Exception except)
            {
                return new System.Web.Mvc.JsonResult()
                {
                    Data = new RetornoJson(
                        "error",
                        except.Message,
                        except.Source,
                        except.HResult
                    )
                };
            }
        }
    }
}
