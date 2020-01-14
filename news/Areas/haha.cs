using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using news.Models;

namespace news.Areas
{
    public class haha : Controller
    {
        private NewsWebAppEntities3 db = new NewsWebAppEntities3();

        // GET: haha
        public ActionResult Index()
        {
            var post = db.Post.Include(p => p.Catagory).Include(p => p.User);
            return View(post.ToList());
        }

        // GET: haha/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: haha/Create
        public ActionResult Create()
        {
            ViewBag.Catagory_Id = new SelectList(db.Catagory, "Id", "Name");
            ViewBag.Author_Id = new SelectList(db.User, "Id", "FullName");
            return View();
        }

        // POST: haha/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImageURL,Title,CreatedDate,Author_Id,Catagory_Id,Content,IsBreakingNews,NumOfVisitors")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Catagory_Id = new SelectList(db.Catagory, "Id", "Name", post.Catagory_Id);
            ViewBag.Author_Id = new SelectList(db.User, "Id", "FullName", post.Author_Id);
            return View(post);
        }

        // GET: haha/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Catagory_Id = new SelectList(db.Catagory, "Id", "Name", post.Catagory_Id);
            ViewBag.Author_Id = new SelectList(db.User, "Id", "FullName", post.Author_Id);
            return View(post);
        }

        // POST: haha/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImageURL,Title,CreatedDate,Author_Id,Catagory_Id,Content,IsBreakingNews,NumOfVisitors")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Catagory_Id = new SelectList(db.Catagory, "Id", "Name", post.Catagory_Id);
            ViewBag.Author_Id = new SelectList(db.User, "Id", "FullName", post.Author_Id);
            return View(post);
        }

        // GET: haha/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: haha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
