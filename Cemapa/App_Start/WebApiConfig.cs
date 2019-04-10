using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Cemapa
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {   

            // Habilita o mapeamento de atributo (Data annotation nas actions)
            config.MapHttpAttributeRoutes();

            // Rotas             
            config.Routes.MapHttpRoute(
                name: "RotaComAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "Get", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
