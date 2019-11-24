using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using news.Models;
using model.DAO;
namespace news.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        NewsWebAppEntities3 db = new NewsWebAppEntities3();
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginMD model)
        {
            var dao = new AccountDAO();
            var result = dao.log(model.username, model.password);

            if(result && ModelState.IsValid)
            {
                var user = dao.GetID(model.username);
                var listuser = from f in db.User
                               select f;
                var session = new LoginMD();
                Session["UserID"] = user.Username.ToString();
                Session["UserName"] = user.PasswordHash.ToString();
                session.username = user.Username;
                Session["CurrentUserName"] = user.FullName;
                Session["listUser"] = listuser.ToList();
                Session.Add(Common.User_session, session);
                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult logout()
        {
            Session[Common.User_session] = null;
            return Redirect("/");
        }
        
    }
}