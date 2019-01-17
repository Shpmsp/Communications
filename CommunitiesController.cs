using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Communications.Models;

namespace Commmunications.Controllers
{
    public class CommunitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Organizations
        public ActionResult Index()
        {
            return View(db.Communities.ToList());
        }

        // GET: Organizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communities organizations = db.Communities.Find(id);
            if (organizations == null)
            {
                return HttpNotFound();
            }
            return View(organizations);
        }

        // GET: Organizations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommunityID,Community,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Communities communities)
        {
            if (ModelState.IsValid)
            {
                db.Communities.Add(communities);

               

                try
                {
                    communities.IsActive = true;
                    communities.CreatedBy = 1;
                    communities.CreatedOn = DateTime.Now;
                    communities.ModifiedOn = DateTime.Now;
                    db.SaveChanges();
                    
                }
                catch (Exception ex)
                {
                   
                }
                return RedirectToAction("Index");
            }

            return View(communities);
        }

        // GET: Organizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communities organizations = db.Communities.Find(id);
            if (organizations == null)
            {
                return HttpNotFound();
            }
            return View(organizations);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommunityID,Community,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Communities communities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(communities).State = EntityState.Modified;

                communities.IsActive = true;
                communities.ModifiedBy = 1;
                communities.ModifiedOn = DateTime.Now;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(communities);
        }

        // GET: Organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communities communities = db.Communities.Find(id);
            if (communities == null)
            {
                return HttpNotFound();
            }
            return View(communities);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Communities communities = db.Communities.Find(id);
            db.Communities.Remove(communities);
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
