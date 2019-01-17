using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace Communications.Models
{
    [Table("trnMeetingAttendance")]
    public class MeetingAttendance
    {
        public MeetingAttendance()
        {

        }

        [Key]
        public int MeetingAttendanceID { get; set; }

        [Required(ErrorMessage = "Enter Meeting Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MeetingDate { get; set; }

        [Required(ErrorMessage = "Select Liaison(s)")]
        public string LiaisonIDs { get; set; }

        [Required(ErrorMessage = "Select Meeting Type")]
        public int MeetingTypeID { get; set; }

        public int? MeetingSubjectID { get; set; }

        [Required(ErrorMessage = "Select Community")]
        public int CommunityID { get; set; }

        [Required(ErrorMessage = "Select District")]
        public int DistrictID { get; set; }

        [Required(ErrorMessage = "Select Council Member")]
        public int CouncilMemberID { get; set; }

       
        public string StartTime { get; set; }
      
        public string EndTime { get; set; }
       
        public int Duration { get; set; }

        [Required(ErrorMessage = "Enter Meeting Notes")]
        public string Notes { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MeetingAttendance> GetList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<MeetingAttendance> list = new List<MeetingAttendance>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_MeetingAttendance", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MeetingAttendanceID", 0));
                    command.Parameters.Add(new SqlParameter("@MeetingDate", 0));
                    command.Parameters.Add(new SqlParameter("@LiaisonID", 0));
                    command.Parameters.Add(new SqlParameter("@MeetingTypeID", 0));
                    command.Parameters.Add(new SqlParameter("@MeetingSubjectID", 0));
                    command.Parameters.Add(new SqlParameter("@CommunityID", 0));
                    command.Parameters.Add(new SqlParameter("@DistrictID", 0));
                    command.Parameters.Add(new SqlParameter("@CommiteeMemberID", 0));
                    command.Parameters.Add(new SqlParameter("@StartTime", 0));
                    command.Parameters.Add(new SqlParameter("@EndTime", 0));
                    command.Parameters.Add(new SqlParameter("@Duration", 0));
                    command.Parameters.Add(new SqlParameter("@IsActive", 0));
                    command.Parameters.Add(new SqlParameter("@CreatedBy", 0));
                    command.Parameters.Add(new SqlParameter("@CreatedOn", 0));
                    command.Parameters.Add(new SqlParameter("@ModifiedBy", 0));
                    command.Parameters.Add(new SqlParameter("@ModifiedOn", 0));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MeetingAttendance obj = new MeetingAttendance();
                                obj.MeetingAttendanceID = Convert.ToInt32(reader["MeetingAttendanceID"]);
                                obj.MeetingDate = Convert.ToDateTime(reader["MeetingDate"]);
                                obj.LiaisonIDs = Convert.ToString(reader["LiaisonID"]);
                                obj.MeetingTypeID = Convert.ToInt32(reader["MeetingTypeID"]);
                                obj.MeetingSubjectID = Convert.ToInt32(reader["MeetingSubjectID"]);
                                obj.CommunityID = Convert.ToInt32(reader["CommunityID"]);
                                obj.DistrictID = Convert.ToInt32(reader["DistrictID"]);
                                obj.CouncilMemberID = Convert.ToInt32(reader["CommiteeMemberID"]);
                                obj.StartTime = Convert.ToString(reader["StartTime"]);
                                obj.EndTime = Convert.ToString(reader["EndTime"]);
                                obj.Duration = Convert.ToInt32(reader["Duration"]);
                                obj.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                obj.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                                obj.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                                obj.ModifiedBy = Convert.ToInt32(reader["ModifiedBy"]);
                                obj.ModifiedOn = Convert.ToDateTime(reader["ModifiedOn"]);
                                list.Add(obj);
                            }

                        }

                    }

                }

            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeetingAttendanceID"></param>
        /// <returns></returns>
        public MeetingAttendance GetListByID(int MeetingAttendanceID)
        {
            MeetingAttendance obj = new MeetingAttendance();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_MeetingAttendance", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MeetingAttendanceID", MeetingAttendanceID));
                    command.Parameters.Add(new SqlParameter("@MeetingDate", 0));
                    command.Parameters.Add(new SqlParameter("@LiaisonID", 0));
                    command.Parameters.Add(new SqlParameter("@MeetingTypeID", 0));
                    command.Parameters.Add(new SqlParameter("@MeetingSubjectID", 0));
                    command.Parameters.Add(new SqlParameter("@CommunityID", 0));
                    command.Parameters.Add(new SqlParameter("@DistrictID", 0));
                    command.Parameters.Add(new SqlParameter("@CommiteeMemberID", 0));
                    command.Parameters.Add(new SqlParameter("@StartTime", 0));
                    command.Parameters.Add(new SqlParameter("@EndTime", 0));
                    command.Parameters.Add(new SqlParameter("@Duration", 0));
                    command.Parameters.Add(new SqlParameter("@IsActive", 0));
                    command.Parameters.Add(new SqlParameter("@CreatedBy", 0));
                    command.Parameters.Add(new SqlParameter("@CreatedOn", 0));
                    command.Parameters.Add(new SqlParameter("@ModifiedBy", 0));
                    command.Parameters.Add(new SqlParameter("@ModifiedOn", 0));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                obj.MeetingAttendanceID = Convert.ToInt32(reader["MeetingAttendanceID"]);
                                obj.MeetingDate = Convert.ToDateTime(reader["MeetingDate"]);
                                obj.LiaisonIDs = Convert.ToString(reader["LiaisonID"]);
                                obj.MeetingTypeID = Convert.ToInt32(reader["MeetingTypeID"]);
                                obj.MeetingSubjectID = Convert.ToInt32(reader["MeetingSubjectID"]);
                                obj.CommunityID = Convert.ToInt32(reader["CommunityID"]);
                                obj.DistrictID = Convert.ToInt32(reader["DistrictID"]);
                                obj.CouncilMemberID = Convert.ToInt32(reader["CommiteeMemberID"]);
                                obj.StartTime = Convert.ToString(reader["StartTime"]);
                                obj.EndTime = Convert.ToString(reader["EndTime"]);
                                obj.Duration = Convert.ToInt32(reader["Duration"]);
                                obj.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                obj.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                                obj.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                                obj.ModifiedBy = Convert.ToInt32(reader["ModifiedBy"]);
                                obj.ModifiedOn = Convert.ToDateTime(reader["ModifiedOn"]);


                            }

                        }

                    }

                }
            }
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="MeetingAttendanceID"></param>
        /// <param name="MeetingDate"></param>
        /// <param name="LiaisonID"></param>
        /// <param name="MeetingTypeID"></param>
        /// <param name="MeetingSubjectID"></param>
        /// <param name="CommunityID"></param>
        /// <param name="DistrictID"></param>
        /// <param name="CommiteeMemberID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="Duration"></param>
        /// <param name="IsActive"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CreatedOn"></param>
        /// <param name="ModifiedBy"></param>
        /// <param name="ModifiedOn"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>

        public bool Save(string Type, int MeetingAttendanceID, DateTime MeetingDate, int LiaisonID, int MeetingTypeID, int MeetingSubjectID, int CommunityID, int DistrictID, int CommiteeMemberID, DateTime StartTime, DateTime EndTime, int Duration, bool IsActive, int CreatedBy, DateTime CreatedOn, int ModifiedBy, DateTime ModifiedOn, int UserID)
        {
            bool flag = false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_MeetingAttendance", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MeetingAttendanceID", MeetingAttendanceID));
                    command.Parameters.Add(new SqlParameter("@MeetingDate", MeetingDate));
                    command.Parameters.Add(new SqlParameter("@LiaisonID", LiaisonID));
                    command.Parameters.Add(new SqlParameter("@MeetingTypeID", MeetingTypeID));
                    command.Parameters.Add(new SqlParameter("@MeetingSubjectID", MeetingSubjectID));
                    command.Parameters.Add(new SqlParameter("@CommunityID", CommunityID));
                    command.Parameters.Add(new SqlParameter("@DistrictID", DistrictID));
                    command.Parameters.Add(new SqlParameter("@CommiteeMemberID", CommiteeMemberID));
                    command.Parameters.Add(new SqlParameter("@StartTime", StartTime));
                    command.Parameters.Add(new SqlParameter("@EndTime", EndTime));
                    command.Parameters.Add(new SqlParameter("@Duration", Duration));
                    command.Parameters.Add(new SqlParameter("@IsActive", IsActive));
                    command.Parameters.Add(new SqlParameter("@CreatedBy", CreatedBy));
                    command.Parameters.Add(new SqlParameter("@CreatedOn", CreatedOn));
                    command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
                    command.Parameters.Add(new SqlParameter("@ModifiedOn", ModifiedOn));
                    try
                    {
                        int i = command.ExecuteNonQuery();
                        flag = true;
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                    }



                }

            }

            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMeetingAttendanceList()
        {
            DataTable dt = new DataTable();
             string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
             using (SqlConnection con = new SqlConnection(connectionString))
             {
                 using (SqlCommand command = new SqlCommand("USP_GetMeetingAttendanceList", con))
                 {
                     using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                     {
                         adapter.Fill(dt);
                     }
                 }
             }
             return dt;
        }

    }
}
