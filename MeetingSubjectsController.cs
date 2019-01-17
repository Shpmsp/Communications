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
    public class MeetingSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MeetingSubjects
        public ActionResult Index()
        {
            return View(db.MeetingSubjects.ToList());
        }

        // GET: MeetingSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingSubjects meetingSubjects = db.MeetingSubjects.Find(id);
            if (meetingSubjects == null)
            {
                return HttpNotFound();
            }
            return View(meetingSubjects);
        }

        // GET: MeetingSubjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeetingSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingSubjectID,MeetingSubject,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] MeetingSubjects meetingSubjects)
        {
            if (ModelState.IsValid)
            {
                db.MeetingSubjects.Add(meetingSubjects);

                meetingSubjects.IsActive = true;
                meetingSubjects.CreatedBy = 1;
                meetingSubjects.CreatedOn = DateTime.Now;
                meetingSubjects.ModifiedOn = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetingSubjects);
        }

        // GET: MeetingSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingSubjects meetingSubjects = db.MeetingSubjects.Find(id);
            if (meetingSubjects == null)
            {
                return HttpNotFound();
            }
            return View(meetingSubjects);
        }

        // POST: MeetingSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeetingSubjectID,MeetingSubject,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] MeetingSubjects meetingSubjects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingSubjects).State = EntityState.Modified;

                meetingSubjects.IsActive = true;
                meetingSubjects.ModifiedBy = 1;
                meetingSubjects.ModifiedOn = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingSubjects);
        }

        // GET: MeetingSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingSubjects meetingSubjects = db.MeetingSubjects.Find(id);
            if (meetingSubjects == null)
            {
                return HttpNotFound();
            }
            return View(meetingSubjects);
        }

        // POST: MeetingSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingSubjects meetingSubjects = db.MeetingSubjects.Find(id);
            db.MeetingSubjects.Remove(meetingSubjects);
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
