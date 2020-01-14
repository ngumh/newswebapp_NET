using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public User getById(int id)
        {
            return db.User.Where(x => x.Id == id).FirstOrDefault();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Username,PasswordHash,Email,Address,City,description,imgurl,wallpaperurl")] User user, HttpPostedFileBase img, HttpPostedFileBase wallpaper)
        {
            try
            {
                var path = "";
                var filename = "";
                var path1 = "";
                var filename1 = "";
                User temp = getById(user.Id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {

                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload/img/profile/personal"), filename);
                        img.SaveAs(path);
                        temp.imgurl = filename; //Lưu ý
                    }
                    if (wallpaper != null)
                    {
                        filename1 = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + wallpaper.FileName;
                        path1 = Path.Combine(Server.MapPath("~/Content/upload/img/profile/background"), filename1);
                        wallpaper.SaveAs(path1);
                        temp.wallpaperurl = filename1; //Lưu ý
                    }
                    temp.City = user.City;
                    temp.Address = user.Address;
                    temp.description = user.description;
                    temp.Email = user.Email;
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index",new { @id = Session["UserID"] });
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
            return View("Index");
        }
    }
}