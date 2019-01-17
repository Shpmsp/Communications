using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Communications.Models;

namespace Communications.Controllers
{
    public class LiaisonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Liaisons
        public ActionResult Index()
        {
            return View(db.Liaisons.ToList());
        }

        // GET: Liaisons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liaisons liaisons = db.Liaisons.Find(id);
            if (liaisons == null)
            {
                return HttpNotFound();
            }
            return View(liaisons);
        }

        // GET: Liaisons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Liaisons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LiaisonID,LiaisonName,EmailAddress,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Liaisons liaisons)
        {
            if (ModelState.IsValid)
            {
                db.Liaisons.Add(liaisons);

                liaisons.IsActive = true;
                liaisons.CreatedBy = 1;
                liaisons.CreatedOn = DateTime.Now;
                liaisons.ModifiedOn = DateTime.Now;


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(liaisons);
        }

        // GET: Liaisons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liaisons liaisons = db.Liaisons.Find(id);
            if (liaisons == null)
            {
                return HttpNotFound();
            }
            return View(liaisons);
        }

        // POST: Liaisons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LiaisonID,LiaisonName,EmailAddress,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Liaisons liaisons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liaisons).State = EntityState.Modified;

                liaisons.IsActive = true;
                liaisons.ModifiedBy = 1;
                liaisons.ModifiedOn = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(liaisons);
        }

        // GET: Liaisons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liaisons liaisons = db.Liaisons.Find(id);
            if (liaisons == null)
            {
                return HttpNotFound();
            }
            return View(liaisons);
        }

        // POST: Liaisons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Liaisons liaisons = db.Liaisons.Find(id);
            db.Liaisons.Remove(liaisons);
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
