using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HuiYin.Models;

namespace HuiYin.Controllers
{
    public class LhUserController : Controller
    {
        AppDbContext db = new AppDbContext();

        [Authorize]
        public ActionResult AdminIndex()
        {
            //暂时跳转到订单页面
            return RedirectToAction("AllOrder", "Order");
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(string txtName, string txtPwd)
        {
            if (db.LhUsers.Any(x => x.Name == txtName && x.Pwd == txtPwd && x.UserType == UserType.Admin))
            {
                FormsAuthentication.SetAuthCookie(txtName, false);//验证通过

                return RedirectToAction("AdminIndex");
            }
            return Content("用户名或密码错误");
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult Index()
        {
            return Content("");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PersonInfo(LhUser user)
        {
            var u = db.LhUsers.Find(user.Id);
            db.LhUsers.Attach(u);
            u.XingMing = user.XingMing;
            u.Address = user.Address;
            u.Sex = user.Sex;
            u.Email = user.Email;
            u.UserType = UserType.Customer;
            db.SaveChanges();

            return RedirectToAction("PersonInfo");
        }


        [Authorize]
        public ActionResult PersonInfo(string formindex="")
        {
            ViewBag.FomIndex = false;
            if (!string.IsNullOrEmpty(formindex))
            {
                ViewBag.FomIndex = true;
            }
            var u = db.LhUsers.Find(BLContext.LhUserId);
            return View(u);
        }


        // GET: LhUser
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string txtName, string txtPwd)
        {
            if (db.LhUsers.Any(x => x.Name == txtName && x.Pwd == txtPwd))
            {
                FormsAuthentication.SetAuthCookie(txtName, false);//验证通过
                var returnurl = FormsAuthentication.GetRedirectUrl(txtName, false);

                return Redirect(returnurl);
            }
            else
            {
                return Content("用户名或密码错误");
                
            }

        }
        public ActionResult Regist()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Regist(string txtName, string txtPwd)
        {

            var user = new LhUser();
            user.Name = txtName;
            user.Pwd = txtPwd;
            db.LhUsers.Add(user);
            db.SaveChanges();

            return Redirect(FormsAuthentication.GetRedirectUrl(txtName, false));
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
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