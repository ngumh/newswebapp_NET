using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace news
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                 "Error",
                 url: "Error/{action}",
                 new { controller = "Error", action = "PageNotFound" },
                 namespaces: new[] { "news.Controllers" }
             );
            routes.MapRoute(
                 "Contact",
                 url: "Contact/{action}",
                 new { controller = "Contact", action = "Index" },
                 namespaces: new[] { "news.Controllers" }
             );
            routes.MapRoute(
                name: "Default",
                url: "{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "news.Controllers" }

            );
 
        }
    }
}
