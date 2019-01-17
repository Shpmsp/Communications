using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Communications.Models;
using System.IO;

namespace Commmunications.Controllers
{
    public class ReportersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reporters
        public ActionResult Index()
        {
            return View(db.Reporters.ToList());
        }

        // GET: Reporters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reporters reporters = db.Reporters.Find(id);
            if (reporters == null)
            {
                return HttpNotFound();
            }
            return View(reporters);
        }

        // GET: Reporters/Create
        public ActionResult Create()
        {
         
            ViewBag.InquirySources = new SelectList(db.InquirySources.AsEnumerable(), "InquirySourceId", "InquirySourceName"); 
            return View();
        }

        // POST: Reporters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReporterID,ReporterName,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,InquirySourceId")] Reporters reporters)
        {
            string message = "";
            //var valid = true;
            try { //New Try-catch-HP
            ViewBag.InquirySources = new SelectList(db.InquirySources.AsEnumerable(), "InquirySourceId", "InquirySourceName");

            if (ModelState.IsValid)
            {
                db.Reporters.Add(reporters);

                reporters.IsActive = true;
                reporters.CreatedBy = 1;
                reporters.CreatedOn = DateTime.Now;
                reporters.ModifiedOn = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            }
            catch (Exception ex)
            {
                //valid = false;
                string fileName = "Error.txt";
                message = "Error occurred while adding a Reporter." + ex.Message;
                //string reportServerURL = Convert.ToString(ConfigurationManager.AppSettings["SSRSServer"]);
                string path = Server.MapPath("../") + "Report\\" + fileName;
                ////string logPath = Convert.ToString(ConfigurationManager.AppSettings["logPath"]);

                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path);
                }
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("Error-" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "-" + ex.Message);
                    tw.WriteLine(ex.InnerException);
                }
            }
            //End of new code-HP
            return View(reporters);
        }

        // GET: Reporters/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.InquirySources = new SelectList(db.InquirySources.AsEnumerable(), "InquirySourceId", "InquirySourceName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reporters reporters = db.Reporters.Find(id);
            if (reporters == null)
            {
                return HttpNotFound();
            }
            return View(reporters);
        }

        // POST: Reporters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReporterID,ReporterName,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,InquirySourceId")] Reporters reporters)
        {
            ViewBag.InquirySources = new SelectList(db.InquirySources.AsEnumerable(), "InquirySourceId", "InquirySourceName");
            if (ModelState.IsValid)
            {
                db.Entry(reporters).State = EntityState.Modified;

                reporters.IsActive = true;
                reporters.ModifiedBy = 1;
                reporters.ModifiedOn = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reporters);
        }

        // GET: Reporters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reporters reporters = db.Reporters.Find(id);
            if (reporters == null)
            {
                return HttpNotFound();
            }
            return View(reporters);
        }

        // POST: Reporters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reporters reporters = db.Reporters.Find(id);
            db.Reporters.Remove(reporters);
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

        public ActionResult GetReporter(string id)
        {
            int sourceId = (from p in db.InquirySources
                            where p.InquirySourceName == id
                            select p.InquirySourceId).FirstOrDefault();

            List<Reporters> obj = db.Reporters.Where(r => r.InquirySourceId == sourceId).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
