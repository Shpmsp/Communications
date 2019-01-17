using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Communications.Models
{
    [Table("mstMeetingTypes")]
    public class MeetingTypes
    {
        public MeetingTypes()
        {
            MeetingType = "";

        }

        [Key]
        public int MeetingTypeID { get; set; }

        [Required]
        public string MeetingType { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<MeetingTypes> GetList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<MeetingTypes> list = new List<MeetingTypes>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_MeetingTypes", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MeetingTypeID", 0));
                    command.Parameters.Add(new SqlParameter("@MeetingType", ""));
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
                                MeetingTypes obj = new MeetingTypes();
                                obj.MeetingTypeID = Convert.ToInt32(reader["MeetingTypeID"]);
                                obj.MeetingType = Convert.ToString(reader["MeetingType"]);
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

        public MeetingTypes GetListByID(int MeetingTypeID)
        {
            MeetingTypes obj = new MeetingTypes();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_MeetingTypes", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MeetingTypeID", MeetingTypeID));
                    command.Parameters.Add(new SqlParameter("@MeetingType", ""));
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
                                obj.MeetingTypeID = Convert.ToInt32(reader["MeetingTypeID"]);
                                obj.MeetingType = Convert.ToString(reader["MeetingType"]);
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
        public bool Save(string Type, int MeetingTypeID, string MeetingType, bool IsActive, int CreatedBy, DateTime CreatedOn, int ModifiedBy, DateTime ModifiedOn, int UserID)
        {
            bool flag = false; string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_MeetingTypes", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MeetingTypeID", MeetingTypeID));
                    command.Parameters.Add(new SqlParameter("@MeetingType", MeetingType));
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

    }
}