    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using news.Models;
using news.Help;
using System.IO;

namespace news.Areas.Admin.Controllers
{
    public class PostsController : base1Controller
    {
        private NewsWebAppEntities3 db = new NewsWebAppEntities3();

        // GET: Admin/Posts
        public ActionResult Index()
        {
            var post = db.Post.Include(p => p.Catagory).Include(p => p.User).OrderByDescending(p => p.IsBreakingNews == 1);
            return View(post.ToList());
        }

        // GET: Admin/Posts/Details/5
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

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.Catagory_Id = new SelectList(db.Catagory, "Id", "Name");
            ViewBag.Author_Id = new SelectList(db.User, "Id", "FullName");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,ImageURL,Title,CreatedDate,Author_Id,Catagory_Id,Content,BoolValue,NumOfVisitors")] Post post, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                var path = "";
                var filename = "";
                if (post.BoolValue == true)
                {
                    post.IsBreakingNews = 1;
                }
                else
                {
                    post.IsBreakingNews = 0;
                }
                if (img != null)
                {
                    //filename = Guid.NewGuid().ToString() + img.FileName;
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/news"), filename);
                    img.SaveAs(path);
                    post.ImageURL = filename; //Lưu ý
                }
                else
                {
                    post.ImageURL = "logo.png";
                }
                post.CreatedDate = DateTime.Now;
                post.NumOfVisitors = 0;
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Catagory_Id = new SelectList(db.Catagory, "Id", "Name", post.Catagory_Id);
            ViewBag.Author_Id = new SelectList(db.User, "Id", "FullName", post.Author_Id);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
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

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,ImageURL,Title,Author_Id,Catagory_Id,Content,IsBreakingNews,BoolValue,NumOfVisitors")] Post post, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                Post temp = getById(post.Id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload/img/news"), filename);
                        img.SaveAs(path);
                        temp.ImageURL = filename; //Lưu ý
                    }
                    temp.Title = post.Title;
                    temp.Content = post.Content;
                    temp.CreatedDate = DateTime.Now;
                    temp.Author_Id = post.Author_Id;
                    temp.Catagory_Id = post.Catagory_Id;
                    temp.Id = post.Id;
                    if (post.BoolValue == true)
                    {
                        post.IsBreakingNews = 1;
                    }
                    else
                    {
                        post.IsBreakingNews = 0;
                    }
                    temp.IsBreakingNews = post.IsBreakingNews;
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ViewBag.Catagory_Id = new SelectList(db.Catagory, "Id", "Name", post.Catagory_Id);
            ViewBag.Author_Id = new SelectList(db.User, "Id", "FullName", post.Author_Id);
            return View(post);
        }
        public Post getById(String id)
        {
            return db.Post.Where(x => x.Id == id).FirstOrDefault();

        }

        // GET: Admin/Posts/Delete/5
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

        // POST: Admin/Posts/Delete/5
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
