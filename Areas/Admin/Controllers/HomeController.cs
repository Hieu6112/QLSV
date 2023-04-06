using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLSV.Areas.Admin.Models;

namespace QLSV.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //GET: Admin/Home
        private User userModel = new User();

        // Xử lý việc đăng nhập
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (userModel.CheckUserCredentials(username, password))
            {
                Session["username"] = username;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }
        }

        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(string username, string password)
        //{
        //    if (userModel.AddUser(username, password))
        //    {
        //        return RedirectToAction("Login", "User");
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "Tên đăng nhập đã tồn tại";
        //        return View();
        //    }
        //}
    }
}