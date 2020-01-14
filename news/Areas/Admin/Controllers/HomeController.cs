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
            DateTime dt = DateTime.Now;
            var thismonth = dt.Month;
            List<int> repa = new List<int>();
            List<int> sub = new List<int>();
            var name = (from f in db.User
                        select f.FullName).Distinct();
            var numb = (from f in db.Post
                        where f.CreatedDate.Value.Month == thismonth
                        select f).Count();
            var countposts = (from f in db.Post
                              select f).Count();
            var countsubs = (from f in db.Subscriber select f).Count();
            var countViewers = (from f in db.Post select f.NumOfVisitors).Sum();
            var countMessages = (from f in db.Messages select f).Count();
            foreach (var item in name)
            {
                repa.Add((from f in db.Post
                          join o in db.User on f.Author_Id equals o.Id
                          where f.CreatedDate.Value.Month == thismonth && o.FullName == item && f.Author_Id == o.Id
                          select f).Count());
            }

            for (int i = 0; i <= 11; i++)
            {
                sub.Add((from f in db.Subscriber
                         where f.createdate.Value.Month == (i + 1) && f.createdate.Value.Year == dt.Year
                         select f).Count());
            }
            ViewBag.Messagescount = countMessages.ToString();
            ViewBag.Viewerscount = countViewers.ToString();
            ViewBag.Postscount = countposts.ToString();
            ViewBag.subscount = countsubs.ToString();
            ViewBag.sub = sub.ToList();
            ViewBag.fname = numb.ToString();
            ViewBag.name = name;
            ViewBag.REP = repa.ToList();
            return View();
        }

    }
}
