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
        public ActionResult Index(String username, String password)
        {
            DateTime dt = DateTime.Now;
            var thismonth = dt.Month;
            var a = (from f in db.User
                     where f.Username == username && f.PasswordHash == password
                     select f);
            List<int> repa = new List<int>();
            var name = (from f in db.User
                        select f.FullName).Distinct();
            var numb = (from f in db.Post where f.CreatedDate.Value.Month == thismonth
                        select f).Count();
            
            foreach (var item in name)
            {
                repa.Add((from f in db.Post join o in db.User on f.Author_Id equals o.Id
                          where f.CreatedDate.Value.Month == thismonth && o.FullName == item && f.Author_Id == o.Id
                          select f).Count());

            }
            ViewBag.fname = numb.ToString();
            ViewBag.name = name;
            ViewBag.REP = repa.ToList();
            return View(a.ToList());
        }
    }
}
