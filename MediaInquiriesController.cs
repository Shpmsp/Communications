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
using System.Web.UI;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Independentsoft.Msg;

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
        //MediaInquiries
        //public ActionResult MediaInquiriesList()
        public ActionResult MediaInquiriesList(string todaysdate)
        {
            //string todaysdate= ;
            DataTable dt = new System.Data.DataTable();
            DateTime enteredDate = System.DateTime.Now.Date;
            if (todaysdate != "" && todaysdate != null)
            {
                enteredDate = DateTime.Parse(todaysdate);
            }
            dt = MediaInquiries.GetMediaInquiriesByDate(enteredDate);
            return View("MediaInquiriesList", dt);
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

        public Dictionary<string, object> GetErrorsFromModelState()
        {
            var errors = new Dictionary<string, object>();
            foreach (var key in ModelState.Keys)
            {
                // Only send the errors to the client.
                if (ModelState[key].Errors.Count > 0)
                {
                    errors[key] = ModelState[key].Errors;
                }
            }

            return errors;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var Reporters = db.Reporters.ToList();
            //var objReporter = new SelectList(Reporters, "ReportID", "ReporterName");
            ViewBag.Reporters = Reporters;

            var InquirySources = db.InquirySources.ToList();
            // var objInquirySources = new SelectList(InquirySources, "InquirySourceId", "InquirySourceName");
            ViewBag.InquirySources = InquirySources;


            ViewBag.InquiryType = db.TypeRequests.ToList();
            ViewBag.Bureaus = db.BureausDS.ToList();
            ViewBag.Category = db.Categories.ToList();

            //MediaInquiries obj = new MediaInquiries();
            //obj.Date = System.DateTime.Now;
            //obj.IsEmail = 0;
            //return View(obj);

            DataTable dt = new System.Data.DataTable();          
            dt = MediaInquiries.GetMediaInquiriesById(id);

            //Get rest of the Inquiry details-selected inquiries source, GetReporter
            DataTable DT_mediaInquiryDetails = new System.Data.DataTable();
            if (id != null)
            {
                DT_mediaInquiryDetails = MediaInquiries.GetMediaInquiriesByInqId(id);
            }

            List<MediaInquiryDetail> mediaInquiryDetails = ConvertDT2InqDetails(DT_mediaInquiryDetails);
            MediaInquiries mediaInquiries = ConvertDT2MediaInq(dt);
            ViewBag.mediaInquiryDetails = mediaInquiryDetails;
            return View(mediaInquiries);
        }

        private MediaInquiries ConvertDT2MediaInq(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new MediaInquiries()
                                 {
                                     MediaInquiryID = Convert.ToInt32(rw["MediaInquiryID"]),
                                     Date = Convert.ToDateTime(rw["Date"]),
                                     TypeOfInquiry = Convert.ToString(rw["TypeOfInquiry"]),
                                     Bureau = Convert.ToString(rw["Bureau"]),
                                     Topic = Convert.ToString(rw["Topic"]),
                                     CategoryIDs = Convert.ToString(rw["CategoryIDs"]),
                                     BriefOfIssue = Convert.ToString(rw["BriefOfIssue"]),
                                     DpwReported = Convert.ToString(rw["DpwReported"]),
                                     WhatAngle = Convert.ToString(rw["WhatAngle"]),
                                     IsActive = Convert.ToBoolean(rw["IsActive"]),
                                     UserID = Convert.ToInt32(rw["UserID"]),
                                     Interviewee = Convert.ToString(rw["Interviewee"]),
                                     MiscNote = Convert.ToString(rw["MiscNote"])
                                 }).ToList();

            if(convertedList != null && convertedList.Count > 0)
            {
                return convertedList.ElementAt(0);
            }
            return null;
        }

        private List<MediaInquiryDetail> ConvertDT2InqDetails(DataTable dT_mediaInquiryDetails)
        {
            var convertedList = (from rw in dT_mediaInquiryDetails.AsEnumerable()
                                 select new MediaInquiryDetail()
                                 {
                                     MediaInquiryDetailID = Convert.ToInt32(rw["MediaInquiryDetailID"]),
                                     MediaInquiryID = Convert.ToInt32(rw["MediaInquiryID"]),
                                     InquirySourceD = Convert.ToString(rw["InquirySourceD"]),
                                     Reporter = Convert.ToString(rw["Reporter"]),
                                     TimeIn = Convert.ToString(rw["TimeIn"]),
                                     TimeOut = Convert.ToString(rw["TimeOut"])
                                 }).ToList();

            return convertedList;
        }

        // POST: MeetingAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit(MediaInquiries mediaInquiries)
        {
            string message = "";
            var valid = true;
            //test
            bool result;
            try
            {
                result=MediaInquiries.UpdateMediaInquiries(mediaInquiries);
                if (result ==false) {
                    message = "Media Inquiries not saved ";
                    valid = false;
                }
                else {
                    message = "Media Inquiries details updated successfully.";
                }
            }
            catch (Exception ex)
            {
                valid = false;
                message = "Error occurred while updating Media Inquiries." + ex.Message;
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
            DataTable dt = new System.Data.DataTable();
            dt = MediaInquiries.GetMediaInquiriesById(id);
            MediaInquiries mediaInquiries = ConvertDT2MediaInq(dt);

            return View(mediaInquiries);
        }

        // POST: MeetingAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //MediaInquiries mediaInquiries = db.MediaInquiries.Find(id);
            //db.MediaInquiries.Remove(mediaInquiries);
            //db.SaveChanges();
            bool res = MediaInquiries.DeleteMediaInquiries(id);
            return RedirectToAction("MediaInquiriesList");
        }

        //Downloads the Pdf file as .eml file(outlook supported file) to Temp folder
        [HttpGet]
        public virtual ActionResult DownloadFile(string fileguid, string filename)
        {
            string tempFolder = Server.MapPath("../") + "Temp";
            // var filePath = Directory.GetFiles(tempFolder).Single();
            var filePath = tempFolder + "\\mymessage.eml";

            //stream out the contents -don't need to dispose because File() does it for you
            string pathMod = ReturnModifyEML(filePath);
            FileStream  fs = new FileStream(pathMod, FileMode.Open);
            return File(fs, "application/vnd.ms-outlook", "mymessage.eml");
          
        }

        // Saves User inputted Media Inquiries data to db, calls to export the data to DailyMediaReport pdf
        public ActionResult ManageInquiry(MediaInquiries mediaInquiries)
        {
            ////FileStream fs = null;
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
                    //bool success = DailyMediaReport(System.DateTime.Now.Date);
                    ////bool success = DailyMediaReport(mediaInquiries.Date);
                    //string res = DailyMediaReportSMTP(mediaInquiries.Date);
                    string res = DailyMediaReport(mediaInquiries.Date);
                    if (res != "true")
                    {
                        var resultPDF = new { Valid = false, Message = res };
                        return Json(resultPDF);
                    }
                }
                //hp-Test 
                var result = new { Valid = true, Message = "Media inquiries submitted successfully" };
                return Json(result);
                ////return Json(new
                ////{
                ////    //Valid = valid,
                ////     result = result;
                ////     Message = result.Message,
                ////     Errors = GetErrorsFromModelState()
                ////  }, JsonRequestBehavior.AllowGet);

                //string tempFolder = Server.MapPath("../") + "Temp"; ;
                //var filePath = Directory.GetFiles(tempFolder).Single();

                // stream out the contents - don't need to dispose because File() does it for you
                //fs = new FileStream(filePath, FileMode.Open);
                //return File(fs, "message/rfc822", "mymessage.eml");
                //return File(fs, "application/vnd.ms-outlook", "mymessage.eml");

                //Test Approach 2-hp
                ////string handle = Guid.NewGuid().ToString();
                ////string emlFileName = Server.MapPath("../") + "Temp\\mymessage.eml";
                ////return Json(new { FileGuid = handle, FileName = emlFileName }, JsonRequestBehavior.AllowGet);
                //Uncomment until here-hp
            }
            catch (Exception ex)
            {
                ////string fileName = "Error.txt";
                //string reportServerURL = Convert.ToString(ConfigurationManager.AppSettings["SSRSServer"]);
                //string path = Server.MapPath("../") + "Report\\" + fileName;
                ////string logPath = Convert.ToString(ConfigurationManager.AppSettings["logPath"]);
                string path=System.Web.Hosting.HostingEnvironment.MapPath("~/Report/Error.txt");//which is static

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
        //Original method-HP- New
        //public ActionResult ManageInquiry(MediaInquiries mediaInquiries)
        //{
        //    try
        //    {
        //        db.MediaInquiries.Add(mediaInquiries);
        //        db.SaveChanges();

        //        if (!string.IsNullOrEmpty(mediaInquiries.SourceData))
        //        {
        //            string[] details = mediaInquiries.SourceData.TrimEnd('|').Split('|');
        //            foreach (string str in details)
        //            {
        //                if (!string.IsNullOrEmpty(str))
        //                {
        //                    string[] strData = str.Trim().Split(',');

        //                    MediaInquiryDetail objMD = new MediaInquiryDetail()
        //                    {
        //                        MediaInquiryID = mediaInquiries.MediaInquiryID,
        //                        InquirySourceD = strData[0],
        //                        Reporter = strData[1],
        //                        TimeIn = strData[2],
        //                        TimeOut = strData[3]
        //                    };

        //                    db.MediaInquiryDetails.Add(objMD);
        //                }

        //            }
        //            db.SaveChanges();

        //        }

        //        if (mediaInquiries.IsEmail == 1)
        //        {
        //            //bool success = DailyMediaReport(System.DateTime.Now.Date);
        //            ////bool success = DailyMediaReport(mediaInquiries.Date);
        //            //string res = DailyMediaReport(mediaInquiries.Date);
        //            string res = DailyMediaReportSMTP(mediaInquiries.Date);
        //            if (res != "true")
        //            {
        //                var resultPDF = new { Valid = false, Message = res };
        //                return Json(resultPDF);
        //            }
        //        }
        //        var result = new { Valid = true, Message = "Media inquiries submitted successfully" };
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = new { Valid = false, Message = ex.InnerException };
        //        return Json(result);
        //    }
        //}
        ////Renders Pdf report from SSRS, saves to file system location, and attaches report launching the Outlook window 
        //public string DailyMediaReport(DateTime todaysdate)
        //{

        //   string dateString = todaysdate.ToString("MM-dd-yyyy");

        //    string userName = Convert.ToString(ConfigurationManager.AppSettings["ReportServerUsername"]);
        //    string password = Convert.ToString(ConfigurationManager.AppSettings["ReportServerPassword"]);
        //    string reportServerURL = Convert.ToString(ConfigurationManager.AppSettings["SSRSServer"]);
        //    string reportDomain = Convert.ToString(ConfigurationManager.AppSettings["ReportServerDomain"]);
        //    string reportFolder = ConfigurationManager.AppSettings["SSRSReportsFolder"];
        //    string reportName = ConfigurationManager.AppSettings["DailyMediaReport"];
        //    string format = "PDF";
        //    string url = @reportServerURL +
        //                                                                            "/Pages/ReportViewer.aspx?" + "%2f" + reportFolder +
        //                                                                            "%2f" + reportName +
        //                                                                            "&rs:Command=Render&rs:format=" + format + "&Date="+ dateString;                                                                                   ;
        //    //"&ReportId=" + todaysdate;

        //    Uri myUri = new Uri(url);
        //    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
        //    webReq.Method = "GET";
        //    webReq.PreAuthenticate = true;
        //    webReq.Credentials = CredentialCache.DefaultNetworkCredentials;
        //    WindowsIdentity identity = System.Web.HttpContext.Current.Request.LogonUserIdentity;
        //    string name = identity.Name;
        //    MemoryStream stream;
        //    FileStream file;
        //    // andy 
        //    ////string fileNNNN = "DailyMediaReport - " + dateString + ".pdf";
        //    try
        //    {
        //        HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
        //        Stream report = webResp.GetResponseStream();
        //         stream = new MemoryStream();
        //        report.CopyTo(stream);


        //        string handle = Guid.NewGuid().ToString();
        //       // var dateVal= todaysdate.Value.ToShortDateString();
        //        string fileName = "DailyMediaReport -" + dateString + ".pdf";//& " (" & Format(Date, "mm-dd-yy") & ")" &
        //                                                                     //TempData[handle] = stream.ToArray();

        //        fileName = Server.MapPath("../") + "Report\\" + fileName;
        //        //fileName = "\\dpw-cisweb-tst\\E$\\Inetpub\\WWWRoot\\Communications\\" + "Report\\" + fileName;
        //        // andy
        //       // fileNNNN = fileName;
        //        file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        //        stream.WriteTo(file);
        //        file.Close();
        //        stream.Close();
        //        report.Close();
        //        webResp.Close();
        //        OpenOutLookSMTP(fileName);
        //        ////OpenOutLook(fileName);
        //        return "true";

        //        //New TEst -11/21/18
        //        //// handle = Guid.NewGuid().ToString();
        //        //string fileName = reportName + ".pdf";
        //        TempData[handle] = stream.ToArray();
        //        // sendEmail(stream.ToArray());

        //        return Json(new { FileGuid = handle, FileName = fileName }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        var resultPdfReport = new { Valid = false, Message = ex.InnerException };

        //        var exeception = ex.Message;// + "  Path: " + fileNNNN;
        //        // andy
        //        //return false;
        //        return exeception;
        //    }
        //    finally
        //    {
        //        //check stream length or if stream not close, close stream
        //        //Same here, use report.Close();             
        //    }
        //}


        //Renders Pdf report from SSRS, saves to file system, and attaches report launching the Outlook client window (Not the SMTP approach)
        public string DailyMediaReport(DateTime todaysdate)
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
            //"&ReportId=" + todaysdate;

            Uri myUri = new Uri(url);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Method = "GET";
            webReq.PreAuthenticate = true;
            webReq.Credentials = CredentialCache.DefaultNetworkCredentials;
            WindowsIdentity identity = System.Web.HttpContext.Current.Request.LogonUserIdentity;
            string name = identity.Name;
            MemoryStream stream;
            FileStream file;
            // andy 
            ////string fileNNNN = "DailyMediaReport - " + dateString + ".pdf";
            try
            {
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                Stream report = webResp.GetResponseStream();
                stream = new MemoryStream();
                report.CopyTo(stream);


                string handle = Guid.NewGuid().ToString();
                // var dateVal= todaysdate.Value.ToShortDateString();
                string fileName = "DailyMediaReport -" + dateString + ".pdf";//& " (" & Format(Date, "mm-dd-yy") & ")" &
                                                                             //TempData[handle] = stream.ToArray();

                fileName = Server.MapPath("../") + "Report\\" + fileName;
                //fileName = "\\dpw-cisweb-tst\\E$\\Inetpub\\WWWRoot\\Communications\\" + "Report\\" + fileName;
                // andy
                // fileNNNN = fileName;
                file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                stream.WriteTo(file);
                file.Close();
                stream.Close();
                report.Close();
                webResp.Close();
                OpenOutLookSMTP(fileName);//Eml file approach
                ////OpenOutLook(fileName);//Old code
                
                //To send DIRECT EMAIL-New
                ////SendEmail(fileName);
                return "true";

                //New TEst -11/21/18
                //// handle = Guid.NewGuid().ToString();
                //string fileName = reportName + ".pdf";
                //TempData[handle] = stream.ToArray();
                // sendEmail(stream.ToArray());

               // return Json(new { FileGuid = handle, FileName = fileName }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var resultPdfReport = new { Valid = false, Message = ex.InnerException };

                var exeception = ex.Message;// + "  Path: " + fileNNNN;
                // andy
                //return false;
                return exeception;
            }
            finally
            {
                //check stream length or if stream not close, close stream
                //Same here, use report.Close();             
            }
        }
        //public string DailyMediaReportSMTP(DateTime todaysdate)
        //{
        //    try
        //    {
                
        //        string fileName = Server.MapPath("../") + "Report\\" + "DailyMediaReport -11-20-2018.pdf";
        //        //fileName = "\\dpw-cisweb-tst\\E$\\Inetpub\\WWWRoot\\Communications\\" + "Report\\" + fileName;
        //        OpenOutLookSMTP(fileName);//DailyMediaReport -11-20-2018
        //        return "true";

        //    }
        //    catch (Exception ex)
        //    {
        //        var resultPdfReport = new { Valid = false, Message = ex.InnerException };

        //        var exeception = ex.Message;
        //        // andy
        //        //return false;
        //        return exeception;
        //    }
        //    finally
        //    {
        //        //check stream length or if stream not close, close stream
        //        //Same here, use report.Close();             
        //    }
        //}

        //Old method-HP
        public void OpenOutLook(string fileName)
        {
            Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.MailItem oMsg = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            //Verify it haritha
            //Globals.ThisAddIn.Application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olAppointmentItem)
            //using oMsg{ 
            //oMsg.To = "Daily Media Report Contacts";
            oMsg.To = "DL_DPWIT@baltimorecity.gov";
            oMsg.Subject = "Department of Public Works Daily Media Log for " + System.DateTime.Now.ToShortDateString() + ".";
                oMsg.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                oMsg.HTMLBody =
                     "The attached log sheet reflects media contact information as of 2:30 p.m. today. Information received" +
                    " afterwards will be listed on next day\'s log with date noted.";
                //oMsg.HTMLBody =
                //     "The attached log sheet reflects media contact information as of " + System.DateTime.Now.ToShortTimeString() + " today. Information received" +
                //    " afterwards will be listed on next day\'s log with date noted.";
                oMsg.Attachments.Add(fileName, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                oMsg.Display(oMsg);
           // }
        }

        public void OpenOutLookSMTP(string fileName)
        {
            var mailMessage = new MailMessage();
           // string dateString = todaysdate.ToString("MM-dd-yyyy");

            WindowsIdentity identity = System.Web.HttpContext.Current.Request.LogonUserIdentity;
            string name = identity.Name;
            String[] breakApart = name.Split('\\');
            string fromEmail = breakApart[1] + "@baltimorecity.gov";
            ////mailMessage.From = new MailAddress("haritha.ponduri@baltimorecity.gov");
            mailMessage.From = new MailAddress(fromEmail);
            ////mailMessage.To.Add(new MailAddress("DL_DPWIT@baltimorecity.gov"));
            mailMessage.Subject = "Department of Public Works Daily Media Log for " + System.DateTime.Now.ToShortDateString() + ".";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "The attached log sheet reflects media contact information as of 2:30 p.m. today. Information received" +
                " afterwards will be listed on next day\'s log with date noted.";

            //string fileName = "DailyMediaReport -" + dateString + ".pdf";
            ////string[] pdfFileName = fileName.Split('\\');
            ////fileName = pdfFileName[7]; //On server it should be pdfFileName[6]-HP

            MemoryStream ms = new MemoryStream(); //store the mail into this ms 'memory stream'
            ms.Position = 0;
            ////mailMessage.Attachments.Add(new Attachment(ms, fileName));
            mailMessage.Attachments.Add(new System.Net.Mail.Attachment(fileName));
            var filename = Server.MapPath("../") + "Temp\\mymessage.eml";
            //save the MailMessage to the filesystem
            //oMsg.Save(filename);
            //oMsg.Display(true);
            // }
            //string eml = ToEml(mailMessage);
            //Save(mailMessage, filename);
            mailMessage.Save(filename);
            //// Andy Wu 11/26/2018
            //toMSG(filename);
            ////Process.Start(filename);


            //Test-New-hp- SMTP: For sending direct email rather than initiating the Email client, which is currently being used
            ////SmtpClient client = new SmtpClient();
            ////client.Port = 25;
            ////client.EnableSsl = false;
            ////client.DeliveryMethod = SmtpDeliveryMethod.Network;
            ////client.UseDefaultCredentials = true;
            ////client.Host = mailrelay.baltimore.city;       //  Enter your company's email server here!

            ////mailMessage.Priority = MailPriority.Normal;
            ////client.Send(mailMessage);
            ////mailMessage.Dispose();
            ////client.Dispose();
            //Verify if needed- System.Net.Mail.SmtpClient
            ////s = new SmtpClient(smtpServer);
            ////if (s != null)
            ////{
            ////    s.Send(oMail);
            ////}
            ////oMail.Dispose();
            ////s = null;


            //Test-Old-hp
            ////Outlook.Application POfficeApp = (Outlook.Application)Marshal.GetActiveObject("Outlook.Application");  // note that it returns an exception if Outlook is not running
            ////POfficeApp=null;
            ////Type acType = Type.GetTypeFromProgID("POfficeApp");
            ////POfficeApp = (POfficeApp.Application)Activator.CreateInstance(acType, true);
            ////Outlook.MailItem POfficeItem = (Outlook.MailItem)POfficeApp.ActiveInspector().CurrentItem; // now pOfficeItem is the COM object that represents your .eml file
            ////POfficeItem.Display(true);
        }

        public string ReturnModifyEML(string emlPath)
        {
            var stream = new MemoryStream();
            var folderPath = Server.MapPath("../") + "Temp";
            string newFile = Path.Combine(folderPath, "modified.eml");
            using (var sr = new StreamReader(emlPath))
            {
                using (var sw = new StreamWriter(newFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!line.StartsWith("X-Sender:") &&
                            !line.StartsWith("From:")) 
                            // dummy email which is used if receiver address is empty
                      
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }

            // stream out the contents
            using (var fs = new FileStream(newFile, FileMode.Open))
            {
                fs.CopyTo(stream);
            }
            return newFile;


        }
        public void toMSG(string emlPath)
        {
            Independentsoft.Email.Mime.Message mimeMessage = new Independentsoft.Email.Mime.Message(emlPath);

            Independentsoft.Msg.Message msgMessage = new Independentsoft.Msg.Message(mimeMessage);
            
            var filename = Server.MapPath("../") + "Temp\\mymessage.msg";
            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }
            msgMessage.Save(filename);
        }
        public static string ToEml(MailMessage message)
        {
            var assembly = typeof(SmtpClient).Assembly;
            var mailWriterType = assembly.GetType("System.Net.Mail.MailWriter");

            using (var memoryStream = new MemoryStream())
            {
                // Get reflection info for MailWriter contructor
                var mailWriterContructor = mailWriterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Stream) }, null);

                // Construct MailWriter object with our FileStream
                var mailWriter = mailWriterContructor.Invoke(new object[] { memoryStream });

                // Get reflection info for Send() method on MailMessage
                var sendMethod = typeof(MailMessage).GetMethod("Send", BindingFlags.Instance | BindingFlags.NonPublic);

                // Call method passing in MailWriter
                sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { mailWriter, true, true }, null);

                // Finally get reflection info for Close() method on our MailWriter
                var closeMethod = mailWriter.GetType().GetMethod("Close", BindingFlags.Instance | BindingFlags.NonPublic);

                // Call close method
                closeMethod.Invoke(mailWriter, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null);

                return Encoding.ASCII.GetString(memoryStream.ToArray());
            }
        }

        //public static void Save(MailMessage Message, string FileName)
        //{
        //    Assembly assembly = typeof(SmtpClient).Assembly;
        //    Type _mailWriterType =
        //      assembly.GetType("System.Net.Mail.MailWriter");

        //    using (FileStream _fileStream =
        //           new FileStream(FileName, FileMode.Create))
        //    {
        //        // Get reflection info for MailWriter contructor
        //        ConstructorInfo _mailWriterContructor =
        //            _mailWriterType.GetConstructor(
        //                BindingFlags.Instance | BindingFlags.NonPublic,
        //                null,
        //                new Type[] { typeof(Stream) },
        //                null);

        //        // Construct MailWriter object with our FileStream
        //        object _mailWriter =
        //          _mailWriterContructor.Invoke(new object[] { _fileStream });

        //        // Get reflection info for Send() method on MailMessage
        //        MethodInfo _sendMethod =
        //            typeof(MailMessage).GetMethod(
        //                "Send",
        //                BindingFlags.Instance | BindingFlags.NonPublic);

        //        // Call method passing in MailWriter
        //        _sendMethod.Invoke(
        //            Message,
        //            BindingFlags.Instance | BindingFlags.NonPublic,
        //            null,
        //            new object[] { _mailWriter, true },
        //            null);

        //        // Finally get reflection info for Close() method on our MailWriter
        //        MethodInfo _closeMethod =
        //            _mailWriter.GetType().GetMethod(
        //                "Close",
        //                BindingFlags.Instance | BindingFlags.NonPublic);

        //        // Call close method
        //        _closeMethod.Invoke(
        //            _mailWriter,
        //            BindingFlags.Instance | BindingFlags.NonPublic,
        //            null,
        //            new object[] { },
        //            null);
        //    }
        //}

        // Code: To send Direct email to Contacts using SMTP-New-Nov
        //private void SendEmail(string fileName)
        //{
        //    using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["FromMail"], ConfigurationManager.AppSettings["ToMail"]))
        //    {
        //        mm.Subject = "Department of Public Works Daily Media Log for " + System.DateTime.Now.ToShortDateString() + ".";
        //        mm.Body = "The attached log sheet reflects media contact information as of " + System.DateTime.Now.ToShortTimeString() + " today. Information received" +
        //                 " afterwards will be listed on next day\'s log with date noted."; ;


        //        mm.Attachments.Add(new Attachment(fileName));

        //        mm.IsBodyHtml = false;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = ConfigurationManager.AppSettings["SMTPHost"];
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["FromMail"], ConfigurationManager.AppSettings["Password"]);
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        //        smtp.Send(mm);

        //    }
        //}
    }
}
