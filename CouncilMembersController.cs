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
    public class CouncilMembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CouncilMembers
        public ActionResult Index()
        {
            return View(db.CouncilMembers.ToList());
        }

        // GET: CouncilMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouncilMembers councilMembers = db.CouncilMembers.Find(id);
            if (councilMembers == null)
            {
                return HttpNotFound();
            }
            return View(councilMembers);
        }

        // GET: CouncilMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CouncilMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CouncilMemberID,CouncilMember,Phone,EmailAddress,LiasonDistrict,IsActive")] CouncilMembers councilMembers)
        {
            if (ModelState.IsValid)
            {
                db.CouncilMembers.Add(councilMembers);
                try
                {
                    councilMembers.IsActive = true;
                    councilMembers.CreatedBy = 1;
                    councilMembers.CreatedOn = DateTime.Now;
                    councilMembers.ModifiedOn = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                { 
                
                }
                return RedirectToAction("Index");
            }

            return View(councilMembers);
        }

        // GET: CouncilMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouncilMembers councilMembers = db.CouncilMembers.Find(id);
            if (councilMembers == null)
            {
                return HttpNotFound();
            }
            return View(councilMembers);
        }

        // POST: CouncilMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CouncilMemberID,CouncilMember,Phone,EmailAddress,LiasonDistrict,IsActive,CreatedOn")] CouncilMembers councilMembers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(councilMembers).State = EntityState.Modified;

                councilMembers.IsActive = true;
                councilMembers.ModifiedBy = 1;
                councilMembers.ModifiedOn = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(councilMembers);
        }

        // GET: CouncilMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouncilMembers councilMembers = db.CouncilMembers.Find(id);
            if (councilMembers == null)
            {
                return HttpNotFound();
            }
            return View(councilMembers);
        }

        // POST: CouncilMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CouncilMembers councilMembers = db.CouncilMembers.Find(id);
            db.CouncilMembers.Remove(councilMembers);
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
