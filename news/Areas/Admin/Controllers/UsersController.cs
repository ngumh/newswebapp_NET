using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using news.Models;

namespace news.Areas.Admin.Controllers
{
    public class UsersController : base1Controller
    {
        private NewsWebAppEntities3 db = new NewsWebAppEntities3();

        // GET: Admin/User
        public ActionResult Index()
        {

            return View(db.User.ToList());
        }
    }
}