using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using news.Models;
namespace news.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact

        NewsWebAppEntities3 _db = new NewsWebAppEntities3();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Messages([Bind(Include = "Name,Email,Phone,Subject,Message")]  Messages mess)
        {
            if (ModelState.IsValid)
            {
                mess.SentDate = System.DateTime.Now;
                _db.Messages.Add(mess);
                _db.SaveChanges();
            }
            Response.Cookies.Add(new HttpCookie("FlashMessage", "Data processed") { Path = "/" });
            return Redirect("/");
        }
    }
}