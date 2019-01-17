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
    public class MeetingTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MeetingTypes
        public ActionResult Index()
        {
            return View(db.MeetingTypes.ToList());
        }

        // GET: MeetingTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingTypes meetingTypes = db.MeetingTypes.Find(id);
            if (meetingTypes == null)
            {
                return HttpNotFound();
            }
            return View(meetingTypes);
        }

        // GET: MeetingTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeetingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingTypeID,MeetingType,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] MeetingTypes meetingTypes)
        {
            if (ModelState.IsValid)
            {
                db.MeetingTypes.Add(meetingTypes);

                meetingTypes.IsActive = true;
                meetingTypes.CreatedBy = 1;
                meetingTypes.CreatedOn = DateTime.Now;
                meetingTypes.ModifiedOn = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetingTypes);
        }

        // GET: MeetingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingTypes meetingTypes = db.MeetingTypes.Find(id);
            if (meetingTypes == null)
            {
                return HttpNotFound();
            }
            return View(meetingTypes);
        }

        // POST: MeetingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeetingTypeID,MeetingType,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] MeetingTypes meetingTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingTypes).State = EntityState.Modified;

                meetingTypes.IsActive = true;
                meetingTypes.ModifiedBy = 1;
                meetingTypes.ModifiedOn = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingTypes);
        }

        // GET: MeetingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingTypes meetingTypes = db.MeetingTypes.Find(id);
            if (meetingTypes == null)
            {
                return HttpNotFound();
            }
            return View(meetingTypes);
        }

        // POST: MeetingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingTypes meetingTypes = db.MeetingTypes.Find(id);
            db.MeetingTypes.Remove(meetingTypes);
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
