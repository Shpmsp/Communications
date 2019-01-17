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
    public class CommunityAssociationsController : BaseController
    {
        private int UserID = 1;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            DataTable dt = new System.Data.DataTable();
            dt = CommunityAssociations.GetCommunityAssociations();
            return View(dt);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Communications.Models.CommunityAssociations obj = new Communications.Models.CommunityAssociations();
            return View(obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult Create(Communications.Models.CommunityAssociations obj)
        {
            string message = "";
            var valid = TryUpdateModel(obj);

            if (valid == true)
            {
                db.CommunityAssociations.Add(obj);

                obj.IsActive = true;
                obj.CreatedBy = 1;
                obj.CreatedOn = DateTime.Now;


                try
                {
                    db.SaveChanges();
                    message = "Community Association Saved Successfully";
                }
                catch (Exception ex)
                {
                    valid = false;
                    message = "Error occurred while saving Community Association";
                }
            }
            else
            {
                message = "Invalid Inputs";
            }

            return Json(new
            {
                Valid = valid,
                Message = message,
                Errors = GetErrorsFromModelState()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Communications.Models.CommunityAssociations obj = new Communications.Models.CommunityAssociations();
            obj = obj.GetListByID(id, UserID);
            return View(obj);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(Communications.Models.CommunityAssociations obj)
        {
            string message = "";
            var valid = TryUpdateModel(obj);

            if (valid == true)
            {
                db.Entry(obj).State = EntityState.Modified;

                obj.IsActive = true;
                obj.ModifiedBy = 1;
                obj.ModifiedOn = DateTime.Now;


                try
                {
                    db.SaveChanges();

                    message = "Community Association Saved Successfully";
                }
                catch (Exception ex)
                {
                    valid = false;
                    message = "Error occurred while saving the Community Association";
                }
            }
            else
            {
                message = "Invalid Inputs";
            }

            return Json(new
            {
                Valid = valid,
                Message = message,
                Errors = GetErrorsFromModelState()
            }, JsonRequestBehavior.AllowGet);
        }


        // GET: Organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunityAssociations ca = db.CommunityAssociations.Find(id);
            if (ca == null)
            {
                return HttpNotFound();
            }
            return View(ca);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommunityAssociations ca = db.CommunityAssociations.Find(id);
            db.CommunityAssociations.Remove(ca);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public JsonResult GetDistrictsByCommunityAssocationID(int communityAssociationID) // its a GET, not a POST
        {
            List<SelectListItem> list = DropdownList.GetDistrictsByCommunityAssociationID(communityAssociationID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCouncilsByCommunityAssociationID(int communityAssociationID) // its a GET, not a POST
        {
            List<SelectListItem> list = DropdownList.GetCouncilMembersByCommunityAssociationID(communityAssociationID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}