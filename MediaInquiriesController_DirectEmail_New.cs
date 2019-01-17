using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Data.Entity;
using Communications.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Security.Principal;
using System.IO;
using System.Configuration;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net.Mail;

namespace Communications.Controllers
{
    public class MediaInquiriesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: MediaInquiries
        public ActionResult Index()
        {
            return View();
        }

        // GET: MediaInquiries/Create
        public ActionResult Create()
        {

            var Reporters = db.Reporters.ToList();
            //var objReporter = new SelectList(Reporters, "ReportID", "ReporterName");
            ViewBag.Reporters = Reporters;

            var InquirySources = db.InquirySources.ToList();
            // var objInquirySources = new SelectList(InquirySources, "InquirySourceId", "InquirySourceName");
            ViewBag.InquirySources = InquirySources;


            ViewBag.InquiryType = db.TypeRequests.ToList();
            ViewBag.Bureaus = db.BureausDS.ToList();
            ViewBag.Category = db.Categories.ToList();

            MediaInquiries obj = new MediaInquiries();
            obj.Date = System.DateTime.Now;
            obj.IsEmail = 0;
            return View(obj);
        }

        public ActionResult ManageInquiry(MediaInquiries mediaInquiries)
        {
            try
            {
                db.MediaInquiries.Add(mediaInquiries);
                db.SaveChanges();

                if (!string.IsNullOrEmpty(mediaInquiries.SourceData))
                {
                    string[] details = mediaInquiries.SourceData.TrimEnd('|').Split('|');
                    foreach (string str in details)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            string[] strData = str.Trim().Split(',');

                            MediaInquiryDetail objMD = new MediaInquiryDetail()
                            {
                                MediaInquiryID = mediaInquiries.MediaInquiryID,
                                InquirySourceD = strData[0],
                                Reporter = strData[1],
                                TimeIn = strData[2],
                                TimeOut = strData[3]
                            };

                            db.MediaInquiryDetails.Add(objMD);
                        }

                    }
                    db.SaveChanges();

                }

                if (mediaInquiries.IsEmail == 1)
                {

                    bool success = DailyMediaReport(mediaInquiries.Date);
                    if (!success)
                    {
                        var resultPDF = new { Valid = false, Message = "Error" };
                        return Json(resultPDF);
                    }
                }

                var result = new { Valid = true, Message = "Media inquiries submitted successfully" };
                return Json(result);
            }
            catch (Exception ex)
            {
                string fileName = "Error.txt";
                string path = Server.MapPath("../") + "Report\\" + fileName;
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path);
                }

                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("Error-" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "-" + ex.Message);
                    tw.WriteLine(ex.InnerException);
                }

                var result = new { Valid = false, Message = ex.Message };
                return Json(result);
            }
        }

        //Renders Pdf report from SSRS, saves to file system location, and attaches report launching the Outlook window 
        public bool DailyMediaReport(DateTime todaysdate)
        {

            string dateString = todaysdate.ToString("MM-dd-yyyy");

            string userName = Convert.ToString(ConfigurationManager.AppSettings["ReportServerUsername"]);
            string password = Convert.ToString(ConfigurationManager.AppSettings["ReportServerPassword"]);
            string reportServerURL = Convert.ToString(ConfigurationManager.AppSettings["SSRSServer"]);
            string reportDomain = Convert.ToString(ConfigurationManager.AppSettings["ReportServerDomain"]);
            string reportFolder = ConfigurationManager.AppSettings["SSRSReportsFolder"];
            string reportName = ConfigurationManager.AppSettings["DailyMediaReport"];
            string format = "PDF";
            string url = @reportServerURL +
                                                                                    "/Pages/ReportViewer.aspx?" + "%2f" + reportFolder +
                                                                                    "%2f" + reportName +
                                                                                    "&rs:Command=Render&rs:format=" + format + "&Date=" + dateString; ;

            Uri myUri = new Uri(url);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Method = "GET";
            webReq.PreAuthenticate = true;
            webReq.Credentials = CredentialCache.DefaultNetworkCredentials;
            WindowsIdentity identity = System.Web.HttpContext.Current.Request.LogonUserIdentity;
            string name = identity.Name;

            try
            {
                string fileName = "DailyMediaReport -" + dateString + ".pdf";
                fileName = Server.MapPath("../") + "Report\\" + fileName;

                using (HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse())
                {
                    using (Stream report = webResp.GetResponseStream())
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            report.CopyTo(stream);
                            using (FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                            {
                                stream.WriteTo(file);
                            }
                        }
                    }
                }
                SendEmail(fileName);
                //OpenOutLook(fileName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            finally
            {

                //check stream length or if stream not close, close stream
                //Same here, use report.Close();
            }
        }

        //public void OpenOutLook(string fileName)
        //{
        //    Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
        //    Microsoft.Office.Interop.Outlook.MailItem oMsg = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
        //    //using oMsg{ 
        //    //oMsg.To = "Daily Media Report Contacts";
        //    oMsg.To = "DL_DPWIT@baltimorecity.gov";
        //    oMsg.Subject = "Department of Public Works Daily Media Log for " + System.DateTime.Now.ToShortDateString() + ".";
        //    oMsg.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
        //    oMsg.HTMLBody =
        //         "The attached log sheet reflects media contact information as of 2:30 p.m. today. Information received" +
        //        " afterwards will be listed on next day\'s log with date noted.";
        //    //oMsg.HTMLBody =
        //    //     "The attached log sheet reflects media contact information as of " + System.DateTime.Now.ToShortTimeString() + " today. Information received" +
        //    //    " afterwards will be listed on next day\'s log with date noted.";
        //    oMsg.Attachments.Add(fileName, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
        //    oMsg.Display();
        //    // }
        //}  


        private void SendEmail(string fileName)
        {
            using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["FromMail"], ConfigurationManager.AppSettings["ToMail"]))
            {
                mm.Subject = "Department of Public Works Daily Media Log for " + System.DateTime.Now.ToShortDateString() + ".";
                mm.Body = "The attached log sheet reflects media contact information as of " + System.DateTime.Now.ToShortTimeString() + " today. Information received" +
                         " afterwards will be listed on next day\'s log with date noted."; ;


                mm.Attachments.Add(new Attachment(fileName));

                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["SMTPHost"];
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["FromMail"], ConfigurationManager.AppSettings["Password"]);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mm);

            }
        }

    }
}
