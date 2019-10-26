using Shopping_Cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Shopping_Cart.Models.ok;

namespace Shopping_Cart.Controllers
{
    public class HomeController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            var p = db.tblproes.ToList();
            ViewBag.p = p;

            var imgs = db.tblimages.ToList();
            ViewBag.imgs = imgs;
            
            return View();
        }

        public ActionResult cat(int id)
        {
            var p = db.tblproes.Where(l => l.cid == id).ToList();
            ViewBag.p = p;

            var imgs = db.tblimages.ToList();
            ViewBag.imgs = imgs;

            return View();
        }

        public ActionResult pro(int id)
        {
            var p = db.tblproes.Where(l => l.pid == id).ToList();
            ViewBag.p = p;

            var imgs = db.tblimages.Where(l => l.pid == id).ToList(); ;
            ViewBag.imgs = imgs;

            return View();
        }

        public ActionResult cart()
        {
            ViewBag.c = ok.c;
            return View();
        }

        [HttpPost]
        public ActionResult cart( string pid, string pqty)
        {
            foreach(var item in ok.c)
            {
                if(item.iid ==int.Parse(pid))
                {
                    item.iqty += int.Parse(pqty);
                    ViewBag.c = ok.c;
                    return View();
                }
                
            }

            cartitem i = new cartitem() { iid = int.Parse(pid), iqty = int.Parse(pqty) };
            ok.c.Add(i);
            ViewBag.c = ok.c;
            return View();
        }

        public ActionResult checkout(string total)
        {
            ViewBag.t = total;
            return View();
        }

        [HttpPost]
        public ActionResult doneorder(tblorder tb, string total)
        {
            tblorder obj = new tblorder();
            obj.odate = DateTime.Now;
            obj.opname = tb.opname;
            obj.opphone = tb.opphone;
            obj.opaddress = tb.opaddress;
            obj.opsaddress = tb.opsaddress;
            obj.oamount = decimal.Parse(total);
            obj.ostatus = 0;
            db.tblorders.Add(obj);  /*Order Table*/
            db.SaveChanges();

            //max order id for order details
            int moid = db.tblorders.Select(a => a.oid).DefaultIfEmpty ( 0 ).Max();

            var pro = from prod in ok.c
                      join od in db.tblproes
                      on prod.iid equals od.pid
                      select new { PID = od.pid, PPRICE = od.pprice, PQTY = prod.iqty };

            tblorderdetail orderdetails = new tblorderdetail();
            foreach(var item in pro)
            {
                orderdetails.oid = moid;
                orderdetails.pid = item.PID;
                orderdetails.pprice = item.PPRICE;
                orderdetails.pqty = item.PQTY;
                orderdetails.pamount = item.PPRICE * item.PQTY;
                db.tblorderdetails.Add(orderdetails);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}