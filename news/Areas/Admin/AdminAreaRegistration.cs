using System.Web.Mvc;
using System.Web.Routing;
namespace news.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("login", "{type}",
          new { controller = "login", action = "logout" },
          new RouteValueDictionary
          {
                { "type", "logout" }
          },
          namespaces: new[] { "news.Areas.Admin.Controllers" });
            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index",Controller = "Home", id = UrlParameter.Optional }
            );
        }
    }
}