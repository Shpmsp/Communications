using Communications.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Test-hp-Dec-delete
using System.Net;
using System.Data;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Security.Principal;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Web.UI;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Independentsoft.Msg;
//Test-hp-Dec-delete

namespace MediaInquiry.Controllers
{
    public class ReportsController : Controller
    {
        //Test-hp-Dec-delete
        private ApplicationDbContext db = new ApplicationDbContext();
        //private string todaysdate= "12-04-2018";//new Date();
        //End Test-Dec

        // GET: Report
        public ActionResult Index(string id)
        {
            ReportInfo rptInfo = new ReportInfo();

            if (id == "DailyMediaReport")
            {
                rptInfo.ReportName = "Daily Media Report";
                rptInfo.ReportDescription = string.Empty;
                rptInfo.ReportURL = ConfigurationManager.AppSettings["SSRSServer"] + "/Pages/ReportViewer.aspx?%2f" + ConfigurationManager.AppSettings["SSRSReportsFolder"] + "%2f" + id + "&rs:Command=Render&Date=" + System.DateTime.Now.ToString("MM-dd-yyyy");
                rptInfo.Width = 0;
                rptInfo.Height = 0;

            }
            else if (id == "InquiryByCategoryCount")
            {
                rptInfo.ReportName = "Inquiry By Category";
                rptInfo.ReportDescription = string.Empty;
                rptInfo.ReportURL = ConfigurationManager.AppSettings["SSRSServer"] + "/Pages/ReportViewer.aspx?%2f" + ConfigurationManager.AppSettings["SSRSReportsFolder"] + "%2f" + id + "&rs:Command=Render&DateFrom=" + System.DateTime.Now.ToString("MM-dd-yyyy") + "&DateTo=" + System.DateTime.Now.ToString("MM-dd-yyyy");
                rptInfo.Width = 0;
                rptInfo.Height = 0;

            }
            return View(rptInfo);
        }

        //Test-hp-Send Email
        // Saves User inputted Media Inquiries data to db, calls to export the data to DailyMediaReport pdf
        ////public ActionResult ManageInquirySendEmail(MediaInquiries mediaInquiries)
        public ActionResult ManageInquirySendEmail(string todaysdate)
              {
            ////    ////FileStream fs = null;
            try
            {
                ////        db.MediaInquiries.Add(mediaInquiries);
                ////        db.SaveChanges();

                ////        if (!string.IsNullOrEmpty(mediaInquiries.SourceData))
                ////        {
                ////            string[] details = mediaInquiries.SourceData.TrimEnd('|').Split('|');
                ////            foreach (string str in details)
                ////            {
                ////                if (!string.IsNullOrEmpty(str))
                ////                {
                ////                    string[] strData = str.Trim().Split(',');

                ////                    MediaInquiryDetail objMD = new MediaInquiryDetail()
                ////                    {
                ////                        MediaInquiryID = mediaInquiries.MediaInquiryID,
                ////                        InquirySourceD = strData[0],
                ////                        Reporter = strData[1],
                ////                        TimeIn = strData[2],
                ////                        TimeOut = strData[3]
                ////                    };

                ////                    db.MediaInquiryDetails.Add(objMD);
                ////                }

                ////            }
                ////            db.SaveChanges();

                ////        }

                ////        if (mediaInquiries.IsEmail == 1)
                ////        {
                ////            //bool success = DailyMediaReport(System.DateTime.Now.Date);
                ////            ////bool success = DailyMediaReport(mediaInquiries.Date);
                ////            //string res = DailyMediaReport(mediaInquiries.Date);
                ////            string res = DailyMediaReportSMTP(mediaInquiries.Date);
                ////            if (res != "true")
                ////            {
                ////                var resultPDF = new { Valid = false, Message = res };
                ////                return Json(resultPDF);
                ////            }
                ////        }
                ////        //Andy-New
                ////        ////var result = new { Valid = true, Message = "Media inquiries submitted successfully" };
                ////        ////return Json(result);

                ////        //string tempFolder = Server.MapPath("../") + "Temp"; ;
                ////        //var filePath = Directory.GetFiles(tempFolder).Single();

                ////        // stream out the contents - don't need to dispose because File() does it for you
                ////        //fs = new FileStream(filePath, FileMode.Open);
                ////        //return File(fs, "message/rfc822", "mymessage.eml");
                ////        //return File(fs, "application/vnd.ms-outlook", "mymessage.eml");
                //var todaysdate1 = mediaInquiries.Date();
                DateTime enteredDate = System.DateTime.Now.Date;
                if (todaysdate != "")
                {
                    enteredDate = DateTime.Parse(todaysdate);
                }
                string res = DailyMediaReport(enteredDate);
                if (res != "true")
                {
                    var resultPDF = new { Valid = false, Message = res };
                    return Json(resultPDF);
                }

                string handle = Guid.NewGuid().ToString();
                string emlFileName = Server.MapPath("../") + "Temp\\mymessage.eml";
                return Json(new { FileGuid = handle, FileName = emlFileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Valid = false, Message = ex.InnerException };
                return Json(result);
            }
        }
        ////}
        //End of test

        //Test-hp-Dec-delete
        // Saves User inputted Media Inquiries data to db, calls to export the data to DailyMediaReport pdf
        ////public ActionResult ManageInquiry(MediaInquiries mediaInquiries)
        // public ActionResult ManageInquiry(DateTime todaysdate)
        //{
        //    //FileStream fs = null;
        //    ////try
        //    ////{
        //    ////    db.MediaInquiries.Add(mediaInquiries);
        //    ////    db.SaveChanges();

        //    ////    if (!string.IsNullOrEmpty(mediaInquiries.SourceData))
        //    ////    {
        //    ////        string[] details = mediaInquiries.SourceData.TrimEnd('|').Split('|');
        //    ////        foreach (string str in details)
        //    ////        {
        //    ////            if (!string.IsNullOrEmpty(str))
        //    ////            {
        //    ////                string[] strData = str.Trim().Split(',');

        //    ////                MediaInquiryDetail objMD = new MediaInquiryDetail()
        //    ////                {
        //    ////                    MediaInquiryID = mediaInquiries.MediaInquiryID,
        //    ////                    InquirySourceD = strData[0],
        //    ////                    Reporter = strData[1],
        //    ////                    TimeIn = strData[2],
        //    ////                    TimeOut = strData[3]
        //    ////                };

        //    ////                db.MediaInquiryDetails.Add(objMD);
        //    ////            }

        //    ////        }
        //    ////        db.SaveChanges();

        //    ////    }

        //        ////if (mediaInquiries.IsEmail == 1)
        //        ////{
        //        //bool success = DailyMediaReport(System.DateTime.Now.Date);
        //        ////bool success = DailyMediaReport(mediaInquiries.Date);
        //        //string res = DailyMediaReport(mediaInquiries.Date);
        //        string dateString = todaysdate.ToString("MM-dd-yyyy");
        //        ////string res = DailyMediaReport(mediaInquiries.Date);
        //        string res = TestDailyMediaReportSMTP(dateString);
        //            if (res != "true")
        //            {
        //                var resultPDF = new { Valid = false, Message = res };
        //                return Json(resultPDF);
        //            }
        //        ////}
        //        //Andy-New
        //        //var result = new { Valid = true, Message = "Media inquiries submitted successfully" };
        //        //return Json(result);

        //        //string tempFolder = Server.MapPath("../") + "Temp"; ;
        //        //var filePath = Directory.GetFiles(tempFolder).Single();

        //        // stream out the contents - don't need to dispose because File() does it for you
        //        //fs = new FileStream(filePath, FileMode.Open);
        //        //return File(fs, "message/rfc822", "mymessage.eml");
        //        //return File(fs, "application/vnd.ms-outlook", "mymessage.eml");

        //        string handle = Guid.NewGuid().ToString();
        //        string emlFileName = Server.MapPath("../") + "Temp\\mymessage.eml";
        //        return Json(new { FileGuid = handle, FileName = emlFileName }, JsonRequestBehavior.AllowGet);
        //    }
        //    ////catch (Exception ex)
        //    ////{
        //    ////    var result = new { Valid = false, Message = ex.InnerException };
        //    ////    return Json(result);
        //    ////}

        //}

        //Test for Action
        // public ActionResult ManageInquiry(DateTime todaysdate)
        //{

        //Renders Pdf report from SSRS, saves to file system, and attaches report launching the Outlook client window (Not the SMTP approach)
        ////public ActionResult DailyMediaReport(DateTime todaysdate)
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
                
                file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                stream.WriteTo(file);
                file.Close();
                stream.Close();
                report.Close();
                webResp.Close();
                OpenOutLookToEmail(fileName);//Eml file approach
                                          ////OpenOutLook(fileName);//Old code
                                          //To send DIRECT EMAIL-New-Nov
                                          ////SendEmail(fileName);
                return "true";

                ////return Json(new { success= true }, JsonRequestBehavior.AllowGet);
                ////return ContentResult("true");

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
                //return false;
                return exeception;
                ////return Json(exeception);
            }
            finally
            {
                //check stream length or if stream not close, close stream
                //Same here, use report.Close();         
                ////file.Close();
                ////stream.Close();
                ////report.Close();
                ////webResp.Close();
            }
        }
        public void OpenOutLookToEmail(string fileName)
        {
            var mailMessage = new MailMessage();
            // string dateString = todaysdate.ToString("MM-dd-yyyy");

            WindowsIdentity identity = System.Web.HttpContext.Current.Request.LogonUserIdentity;
            string name = identity.Name;
            String[] breakApart = name.Split('\\');
            string fromEmail = breakApart[1] + "@baltimorecity.gov";
            ////mailMessage.From = new MailAddress("haritha.ponduri@baltimorecity.gov");
            mailMessage.From = new MailAddress(fromEmail);//Is Mandatory
            mailMessage.To.Add(new MailAddress("DL_DPWDailyMediaContacts@baltimorecity.gov"));//DL_DPWIT@baltimorecity.gov
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
 
        }
        //Returns modified Eml file
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
        //Convert file to Eml format 
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
  
    }
}