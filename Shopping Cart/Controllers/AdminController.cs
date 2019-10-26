using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping_Cart;

namespace Shopping_Cart.Controllers
{
    public class AdminController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.msg = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(tbluser usr)
        {
            myshopDBEntities obj = new myshopDBEntities();
            var a = obj.tblusers.Where(l => l.uname.Equals(usr.uname) && l.upass.Equals(usr.upass)).ToList();
            if (a.Count > 0)
            {
                Session["uname"] = usr.uname.ToString();
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.msg = "Invalid Username or Password!";
            }
            return View();
        }
        public ActionResult Dashboard()
        {
            if (Session["uname"] == null)
            {
                ViewBag.msg = "Login First...!";
                return RedirectToAction("Index");
            }
            var p = db.tblorders.ToList();
            ViewBag.p = p;
            return View();
        }
    }
}