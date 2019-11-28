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
    public class HomeController : base1Controller
    {
        
        private NewsWebAppEntities3 db = new NewsWebAppEntities3();
        public ActionResult Index()
        {
            return View();
        }

    }
}
