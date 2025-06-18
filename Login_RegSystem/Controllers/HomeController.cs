using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_RegSystem.Models;

namespace Login_RegSystem.Controllers
{
    public class HomeController : Controller
    {
        LOG_REGSYSTEMEntities db = new LOG_REGSYSTEMEntities();

        // GET: Home
        public ActionResult Index()
        {
            var user = Session["user"] as tbluser_reg;

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(tbluser_reg user)
        {

            db.tbluser_reg.Add(user);
            db.SaveChanges();

            if(ModelState.IsValid)
            {
                ViewBag.msg = "Success";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.msg = "error";
            }
                return View(); // ✅ Return the view if model is not valid
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string mail, string pass)
        {
            var user = db.tbluser_reg.FirstOrDefault(l => l.email.Equals(mail) && l.password.Equals(pass));
            if(user!=null)
            {
                Session["user"] = user;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = "Invalid Credentials";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}