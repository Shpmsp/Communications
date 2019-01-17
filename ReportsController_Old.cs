using Communications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaInquiry.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            string ReportName = "DailyMediaReport";
            var rptInfo = new ReportInfo
            {
                ReportName = ReportName,
                ReportDescription = string.Empty,
                ReportURL = String.Format("../../Report/ReportTemplate.aspx?ReportName={0}", ReportName),
                Width = 0,
                Height = 0
            };

            return View(rptInfo);
        }
    }
}