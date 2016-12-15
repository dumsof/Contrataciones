using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Contrataciones
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                        name: "AsignarUsuario",
                        url: "Usuarios/Index/{id}/{nombreRol}",
                        defaults: new
                        {
                            controller = "Usuarios",
                            action = "Index",
                            id = UrlParameter.Optional,
                            nombreRol = UrlParameter.Optional
                        });
        }
    }
}
