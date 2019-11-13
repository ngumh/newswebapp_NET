using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using news.Models;

namespace news.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private NewsWebAppEntities db = new NewsWebAppEntities();

        public ActionResult Index()
        {
            return View();
        }
    }
}
