using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using news.Models;
namespace news.Areas.Admin.Controllers
{
    public class ProfileController : base1Controller
    {
        private NewsWebAppEntities3 db = new NewsWebAppEntities3();
        // GET: Admin/Profile
        public ActionResult Index(string id)
        {
            var user = (from f in db.User
                        where f.Username == id
                        select f).ToList();
            ViewBag.user = user;
            return View(user);
        }
    }
}