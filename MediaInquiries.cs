using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Net;
using System.Web.Mvc;
using System.Security.Principal;


namespace Communications.Models
{
    [Table("trnMediaInquiries")]
    public class MediaInquiries
    {
        public MediaInquiries()
        {
        }

        [Key]
        //public string Type { get; set; } //@Type Varchar(10),
        public int MediaInquiryID { get; set; }       // @MediaInquiryID int,

        //[Required(ErrorMessage = "Enter Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        //@Date datetime,
        //[Required(ErrorMessage = "Select Type Of Inquiry")]
        public string TypeOfInquiry { get; set; } //@TypeOfInquiry varchar(50),
        public string Bureau { get; set; }//   @Bureau varchar(50),
        //[Required(ErrorMessage = "Enter Topic of the Inquiry")]
        public string Topic { get; set; }//       @Topic varchar(500),

        public string CategoryIDs { get; set; }//@CategoryIDs varchar(500),      

        //[StringLength(50, ErrorMessage = "ReporterName cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "Enter Brief Of the Inquiry/Issue")]
        [StringLength(500, ErrorMessage = "Text limited to 500 characters.")]
        [Display(Name = "Brief of Issue")]
        public string BriefOfIssue { get; set; }//@BriefOfIssue varchar(500),

        //[StringLength(500, ErrorMessage = "Dpw Reported text limited to 500 characters.")]
        [Display(Name = "What DPW Reported")]
        public string DpwReported { get; set; }//@DpwReported varchar(500),
        [Display(Name = "What the Angle Might Be")]
        public string WhatAngle { get; set; }        //@WhatAngle varchar(500),
        public bool IsActive { get; set; }//       @IsActive bit,
        public int @UserID { get; set; } //int?

        [Display(Name = "Interviewee (For Interviews Only)")]
        public string Interviewee { get; set; }
        [Display(Name = "Misc/Note")]
        public string MiscNote { get; set; }

        [NotMapped]
        public string SourceData { get; set; }
        [NotMapped]
        public int IsEmail { get; set; }

        public List<MediaInquiryDetail> details { get; set; }

        internal static DataTable GetMediaInquiriesByDate(DateTime Date)
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_MediaInquiriesByDate", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Date", Date));
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        internal static DataTable GetMediaInquiriesById(int? id)
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_MediaInquiriesById", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        internal static DataTable GetMediaInquiriesByInqId(int? id)
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_GetMediaInquiriesByInqId", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        internal static bool UpdateMediaInquiries(MediaInquiries mediaInquiries)
        {
            bool flag = false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlTransaction sqlTrans = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                sqlTrans = con.BeginTransaction();
                try
                {
                    using (SqlCommand command1 = new SqlCommand("USP_UpdateMediaInquiries", con, sqlTrans))
                    {
                        command1.CommandType = System.Data.CommandType.StoredProcedure;
                        command1.Parameters.Add(new SqlParameter("@MediaInquiryID", mediaInquiries.MediaInquiryID));
                        command1.Parameters.Add(new SqlParameter("@Date", mediaInquiries.Date));
                        command1.Parameters.Add(new SqlParameter("@TypeOfInquiry", mediaInquiries.TypeOfInquiry));
                        command1.Parameters.Add(new SqlParameter("@Bureau", mediaInquiries.Bureau));
                        command1.Parameters.Add(new SqlParameter("@Topic", mediaInquiries.Topic));
                       
                        ////command1.Parameters.Add(new SqlParameter("@CategoryIDs", mediaInquiries.CategoryIDs));
                        command1.Parameters.AddWithValue("@CategoryIDs", ((object)mediaInquiries.CategoryIDs) ?? DBNull.Value);//Can be optional at times
                        command1.Parameters.Add(new SqlParameter("@BriefOfIssue", mediaInquiries.BriefOfIssue));
                        
                        command1.Parameters.AddWithValue("@DpwReported", ((object)mediaInquiries.DpwReported) ?? DBNull.Value);//optional
                        command1.Parameters.AddWithValue("@WhatAngle", ((object)mediaInquiries.WhatAngle) ?? DBNull.Value);//optional
                        command1.Parameters.Add(new SqlParameter("@IsActive", mediaInquiries.IsActive));
                        command1.Parameters.Add(new SqlParameter("@UserID", mediaInquiries.UserID));
                        command1.Parameters.AddWithValue("@Interviewee", ((object)mediaInquiries.Interviewee) ?? DBNull.Value);//optional
                        command1.Parameters.AddWithValue("@MiscNote", ((object)mediaInquiries.MiscNote) ?? DBNull.Value);//optional

                        //command1.Parameters.Add(new SqlParameter("@WhatAngle", mediaInquiries.WhatAngle));//Can be null
                        //@IsActive bit, @UserID int,
                        //command1.Parameters.Add(new SqlParameter("@Interviewee", mediaInquiries.Interviewee));//optional, can be null
                        //command1.Parameters.Add(new SqlParameter("@MiscNote", mediaInquiries.MiscNote));//optional, can be null

                        //command1.Transaction = objTrans                
                        command1.ExecuteNonQuery();                   
                    }

                    if (mediaInquiries.details != null && mediaInquiries.details.Count > 0)
                    {
                        for (int i = 0; i < mediaInquiries.details.Count; i++)
                        {
                            using (SqlCommand command2 = new SqlCommand("USP_UpdateMediaInqDetails", con, sqlTrans))
                            {
                                //Test
                                //SqlParameter parammeters = new SqlParameter("@TimeIn", SqlDbType.Time);
                                //SqlParameter[] paramTime1 = new SqlParameter[1];
                                command2.CommandType = System.Data.CommandType.StoredProcedure;
                                command2.Parameters.Add(new SqlParameter("@MediaInquiryID", mediaInquiries.details[i].MediaInquiryID));
                                //command2.Parameters.Add(new SqlParameter("@InquirySourceD", mediaInquiries.details[i].InquirySourceD));
                                command2.Parameters.AddWithValue("@InquirySourceD", ((object)mediaInquiries.details[i].InquirySourceD) ?? DBNull.Value);
                                command2.Parameters.AddWithValue("@Reporter", ((object)mediaInquiries.details[i].Reporter) ?? DBNull.Value);
                                command2.Parameters.AddWithValue("@TimeIn", ((object)mediaInquiries.details[i].TimeIn) ?? DBNull.Value);
                                command2.Parameters.AddWithValue("@TimeOut", ((object)mediaInquiries.details[i].TimeOut) ?? DBNull.Value);
                              
                                command2.ExecuteNonQuery();
                            }
                        }                         
                    }
                    //Test-New Dec-hp
                    //using (SqlCommand command1 = new SqlCommand("USP_UpdateMediaInquiries", con, sqlTrans))
                    //{
                    //    command1.CommandType = System.Data.CommandType.StoredProcedure;
                    //    command1.Parameters.Add(new SqlParameter("@MediaInquiryID", mediaInquiries.MediaInquiryID));
                    //    command1.Parameters.Add(new SqlParameter("@Date", mediaInquiries.Date));
                    //    command1.Parameters.Add(new SqlParameter("@TypeOfInquiry", mediaInquiries.TypeOfInquiry));
                    //    command1.Parameters.Add(new SqlParameter("@Bureau", mediaInquiries.Bureau));
                    //    command1.Parameters.Add(new SqlParameter("@Topic", mediaInquiries.Topic));
                    //    //command1.Transaction = objTrans                
                    //    command1.ExecuteNonQuery();
                    //}

                    sqlTrans.Commit();
                    flag = true;
                }
                catch (Exception ex)
                {
                    ////string logPath = Convert.ToString(ConfigurationManager.AppSettings["logPath"]);
                    ////string fileName = "Error.txt";
                    ////string logPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Report\\" + fileName;
                    string logPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Report/Error.txt");//which is static

                    if (!System.IO.File.Exists(logPath))
                    {
                        System.IO.File.Create(logPath);
                    }
                    using (var tw = new StreamWriter(logPath, true))
                    {
                        tw.WriteLine("Error-" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "-" + ex.Message);
                        tw.WriteLine(ex.InnerException);
                    }

                    sqlTrans.Rollback();
                    flag = false;
                }
                finally
                {
                    con.Close();
                }            
            }

            return flag;
        }

        internal static bool DeleteMediaInquiries(int id)
        {
            bool flag = false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_DeleteMediaInquiriesByInqId", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        command.ExecuteNonQuery();
                        flag = true;
                    }
                }
            }
            return flag;
        }
    }
}