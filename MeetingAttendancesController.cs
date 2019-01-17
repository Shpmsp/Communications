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
    public class MeetingAttendancesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            DataTable dt = new System.Data.DataTable();
            dt = MeetingAttendance.GetMeetingAttendanceList();
            return View(dt);
        }

       

        // GET: MeetingAttendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingAttendance meetingAttendance = db.MeetingAttendances.Find(id);
            if (meetingAttendance == null)
            {
                return HttpNotFound();
            }
            return View(meetingAttendance);
        }

        // GET: MeetingAttendances/Create
        public ActionResult Create()
        {
            List<Liaisons> list = db.Liaisons.ToList();
            ViewBag.Liaisions = list.Select(a => new SelectListItem { Text = a.LiaisonName, Value = a.LiaisonID.ToString() });

            return View();
        }

        // POST: MeetingAttendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MeetingAttendanceID,MeetingDate,LiaisonIDs,MeetingTypeID,MeetingSubjectID,CommunityID,DistrictID,CommiteeMemberID,StartTime,EndTime,Duration,Notes,IsActive")] MeetingAttendance meetingAttendance)
        //{
        public JsonResult Create(MeetingAttendance meetingAttendance)
        {
            string message = "";
            var valid = TryUpdateModel(meetingAttendance);
            if (valid == true)
            {
                db.MeetingAttendances.Add(meetingAttendance);

                meetingAttendance.IsActive = true;
                meetingAttendance.CreatedBy = 1;
                meetingAttendance.CreatedOn = DateTime.Now;
                meetingAttendance.ModifiedOn = DateTime.Now;

                try
                {
                    db.SaveChanges();
                    message = "Meetings Attended details saved successfully.";
                }
                catch (Exception ex)
                {
                    valid = false;
                    message = "Error occurred while saving Meeting Attendance.";
                }
            }

            return Json(new
            {
                Valid = valid,
                Message = message,
                Errors = GetErrorsFromModelState()
            }, JsonRequestBehavior.AllowGet);
            
        }

        // GET: MeetingAttendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingAttendance meetingAttendance = db.MeetingAttendances.Find(id);
            if (meetingAttendance == null)
            {
                return HttpNotFound();
            }
            List<Liaisons> list = db.Liaisons.ToList();

            List<Liaisons> SelectedList = new List<Liaisons>();
            foreach (Liaisons l in list)
            {
                if (meetingAttendance.LiaisonIDs.Contains(l.LiaisonID.ToString()))
                {
                    l.IsChecked = true;
                }
                else
                {
                    l.IsChecked = false;
                }

                SelectedList.Add(l);
            }

            ViewBag.Liaisions = list.Select(a => new SelectListItem { Text = a.LiaisonName, Value = a.LiaisonID.ToString() , Selected = a.IsChecked });
            return View(meetingAttendance);
        }

        // POST: MeetingAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit(MeetingAttendance meetingAttendance)
        {
            string message = "";
            var valid = TryUpdateModel(meetingAttendance);
            if (valid == true)
            {
                db.Entry(meetingAttendance).State = EntityState.Modified;

                meetingAttendance.IsActive = true;
                meetingAttendance.CreatedBy = 1;
                meetingAttendance.CreatedOn = DateTime.Now;
                meetingAttendance.ModifiedOn = DateTime.Now;

                try
                {
                    db.SaveChanges();
                    message = "Meetings Attended details updated successfully.";
                }
                catch (Exception ex)
                {
                    valid = false;
                    message = "Error occurred while updating Meeting Attendance.";
                }
            }

            return Json(new
            {
                Valid = valid,
                Message = message,
                Errors = GetErrorsFromModelState()
            }, JsonRequestBehavior.AllowGet);

        }

        // GET: MeetingAttendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingAttendance meetingAttendance = db.MeetingAttendances.Find(id);
            if (meetingAttendance == null)
            {
                return HttpNotFound();
            }
            return View(meetingAttendance);
        }

        // POST: MeetingAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingAttendance meetingAttendance = db.MeetingAttendances.Find(id);
            db.MeetingAttendances.Remove(meetingAttendance);
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
