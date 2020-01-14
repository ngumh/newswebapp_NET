using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using news.Models;

namespace news.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        NewsWebAppEntities3 _db = new NewsWebAppEntities3();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Subcribe([Bind(Include = "Name,Email")] Subscriber subcribe)
        {
            if (ModelState.IsValid)
            {
                subcribe.createdate = System.DateTime.Now;
                _db.Subscriber.Add(subcribe);
                _db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Search(string key)
        {
            @ViewBag.id = key;
            return View();
        }
        public ActionResult getSearch(String key)
        {
            @ViewBag.id = key;
            var news = from m in _db.Post
                         select m;

            if (!String.IsNullOrEmpty(key))
            {
                news = news.Where(s => s.Title.Contains(key));
            }

            return PartialView(news.Take(5).ToList());
        }
        public ActionResult lst3Newest()
        {
            var v = (from t in _db.Post
                     orderby t.CreatedDate descending
                     select t).Take(3);
            return PartialView(v.ToArray());
        }

        public ActionResult getCatagories()
        {
            var v = from t in _db.Catagory
                    where t.Name != null
                    orderby t.Id ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getBreakingNews()
        {
            var v = (from t in _db.Post
                    where t.IsBreakingNews == 1
                    orderby t.CreatedDate 
                    select t).Take(5);
            return PartialView(v.ToList());
        }
        public ActionResult lst3InterNews()
        {
            var v = (from t in _db.Post
                     where t.Catagory_Id == 1
                     orderby t.CreatedDate
                     select t).Take(5);
            return PartialView(v.ToList());
        }
        public ActionResult lstNewestPostOfCategories()
        {
            var res = (from t in _db.Post
                      where t.Catagory_Id == t.Catagory.Id
                      orderby t.CreatedDate descending
                       select t).Take(5).ToList();
            return PartialView(res);

        }
        public ActionResult lst4PopularNews()
        {
            var v = (from t in _db.Post
                    orderby t.NumOfVisitors descending
                    select t).Take(4);
            return PartialView(v.ToList());

        }
        public ActionResult lst4PopularestNews()
        {
            var v = (from t in _db.Post
                     orderby t.NumOfVisitors descending
                     select t).Take(4);
            return PartialView(v.ToList());

        }
        public ActionResult no1News()
        {
            var v = (from t in _db.Post
                     orderby t.CreatedDate descending
                     select t).Take(1);
            return PartialView(v.ToList());
        }
        public ActionResult li2News()
        {
            var sortedResults = (from r in _db.Post orderby Guid.NewGuid() ascending select r).Take(2);
            return PartialView(sortedResults.ToList());
        }

        public ActionResult liNewsByCategory(string id)
        {
            ViewBag.id = id;
            if(id == "Breakingnews")
            {
                var v = from t in _db.Post
                        where t.IsBreakingNews == 1 
                        orderby t.CreatedDate descending
                        select t;
                return PartialView(v.ToList());
            }
            else
            {
                var v = from t in _db.Post
                        where t.Catagory.Name == id
                        orderby t.CreatedDate descending
                        select t;
                return PartialView(v.ToList());
            }
        }
        public ActionResult getDetail(string id)
        {
            ViewBag.postId = id;
            var v = from t in _db.Post
                    where t.Id == id
                    orderby t.CreatedDate descending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult Category()
        {
            return View();
        }

        public Post getById(string id)
        {
            return _db.Post.Where(x => x.Id == id).FirstOrDefault();

        }

        public ActionResult Detail(string id)
        {
            var v = (from t in _db.Post
                     where t.Id == id
                     select t.NumOfVisitors).First();
                Post temp = getById(id);
                temp.NumOfVisitors = v + 1;
                _db.Entry(temp).State = EntityState.Modified;
                _db.SaveChanges();
            ViewBag.name = id;
            return View(v);
        }
    }
}