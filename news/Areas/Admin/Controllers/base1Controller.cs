using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using news.Models;

namespace news.Areas.Admin.Controllers
{
    public class base1Controller : Controller
    {
        // GET: Admin/base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            {
                var result = (LoginMD)Session[Common.User_session];
                if (result == null)
                {
                    filterContext.Result = new RedirectToRouteResult
                        (new System.Web.Routing.RouteValueDictionary(new
                        { Controller = "Login", action = "Index", Areas = "admin" }));
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}