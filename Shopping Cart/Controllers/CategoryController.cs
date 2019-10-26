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
    public class CategoryController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.tblcats.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcat tblcat = db.tblcats.Find(id);
            if (tblcat == null)
            {
                return HttpNotFound();
            }
            return View(tblcat);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cid,cname")] tblcat tblcat)
        {
            if (ModelState.IsValid)
            {
                db.tblcats.Add(tblcat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblcat);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcat tblcat = db.tblcats.Find(id);
            if (tblcat == null)
            {
                return HttpNotFound();
            }
            return View(tblcat);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cid,cname")] tblcat tblcat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblcat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblcat);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcat tblcat = db.tblcats.Find(id);
            if (tblcat == null)
            {
                return HttpNotFound();
            }
            return View(tblcat);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblcat tblcat = db.tblcats.Find(id);
            db.tblcats.Remove(tblcat);
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
