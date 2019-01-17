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
    [Table("mstReporters")]
    public class Reporters
    {
        public Reporters()
        {

            ReporterName = "";

        }

        [Key]
        public int ReporterID { get; set; }

        [Required(ErrorMessage = "ReporterName is required.")]
        //[StringLength(100, ErrorMessage = "ReporterName cannot be longer than 100 characters.")]
        public string ReporterName { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
        [Display(Name ="Inquiry Source")]
        public int? InquirySourceId { get; set; }
        
        public static List<Reporters> GetList(int UserID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<Reporters> list = new List<Reporters>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_Reporters", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Type", "LIST"));
                    command.Parameters.Add(new SqlParameter("@ReporterID", "0"));
                    command.Parameters.Add(new SqlParameter("@ReporterName", ""));
                    command.Parameters.Add(new SqlParameter("@IsActive", true));
                    command.Parameters.Add(new SqlParameter("@UserID", UserID));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Reporters obj = new Reporters();
                                obj.ReporterID = Convert.ToInt32(reader["ReporterID"]);
                                obj.ReporterName = Convert.ToString(reader["ReporterName"]);
                                obj.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                obj.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                                obj.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                                list.Add(obj);
                            }
                        }
                    }
                    con.Close();
                }
            }
            return list;
        }
        public Reporters GetListByID(int ReporterID, int UserID)
        {
            Reporters obj = new Reporters();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_Reporters", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Type", "GET"));
                    command.Parameters.Add(new SqlParameter("@ReporterID", ReporterID));
                    command.Parameters.Add(new SqlParameter("@ReporterName", ""));
                    command.Parameters.Add(new SqlParameter("@IsActive", true));
                    command.Parameters.Add(new SqlParameter("@UserID", UserID));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                obj.ReporterID = Convert.ToInt32(reader["ReporterID"]);
                                obj.ReporterName = Convert.ToString(reader["ReporterName"]);
                                obj.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                obj.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                                obj.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                            }
                        }
                    }
                    con.Close();
                }
            }
            return obj;
        }
        public bool Save(string Type, Reporters obj, int UserID)
        {
            bool flag = false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_Reporters", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Type", Type));
                    command.Parameters.Add(new SqlParameter("@ReporterID", obj.ReporterID));
                    command.Parameters.Add(new SqlParameter("@ReporterName", obj.ReporterName));
                    command.Parameters.Add(new SqlParameter("@IsActive", obj.IsActive));
                    command.Parameters.Add(new SqlParameter("@UserID", UserID));
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
                con.Close();
            }
            return flag;
        }
    }
}