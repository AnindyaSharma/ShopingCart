using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shopping_Cart;

namespace Shopping_Cart.Controllers
{
    public class ProductController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();

        // GET: Product
        public ActionResult Index()
        {
            var tblproes = db.tblproes.Include(t => t.tblcat);
            return View(tblproes.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblpro tblpro = db.tblproes.Find(id);
            if (tblpro == null)
            {
                return HttpNotFound();
            }
            return View(tblpro);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.cid = new SelectList(db.tblcats, "cid", "cname");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pid,pname,pprice,pdetails,cid")] tblpro tblpro)
        {
            if (ModelState.IsValid)
            {
                db.tblproes.Add(tblpro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cid = new SelectList(db.tblcats, "cid", "cname", tblpro.cid);
            return View(tblpro);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblpro tblpro = db.tblproes.Find(id);
            if (tblpro == null)
            {
                return HttpNotFound();
            }
            ViewBag.cid = new SelectList(db.tblcats, "cid", "cname", tblpro.cid);
            return View(tblpro);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pid,pname,pprice,pdetails,cid")] tblpro tblpro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblpro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cid = new SelectList(db.tblcats, "cid", "cname", tblpro.cid);
            return View(tblpro);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblpro tblpro = db.tblproes.Find(id);
            if (tblpro == null)
            {
                return HttpNotFound();
            }
            return View(tblpro);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblpro tblpro = db.tblproes.Find(id);
            db.tblproes.Remove(tblpro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Images (int id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pro = db.tblproes.Where(l => l.pid == id).ToList();
            if (pro == null)
            {
                return HttpNotFound();
            }
            var imgs = db.tblimages.Where(l => l.pid == id).ToList();
            ViewBag.imgs = imgs;
            ViewBag.pro = pro;
            return View();
        }

        [HttpPost]
        public ActionResult Images(int id,HttpPostedFileBase file)
        {
            string path = System.IO.Path.Combine("~/Content/Images/" + file.FileName);
            file.SaveAs(Server.MapPath(path));
            tblimage obj = new tblimage();
            obj.iname = file.FileName.ToString();
            obj.pid = id;

            db.tblimages.Add(obj);
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
