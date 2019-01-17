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
    [Table("mstCommunities")]
    public class Communities
    {
        public Communities()
        {
            Community = "";
        }

        [Key]
        public int CommunityID { get; set; }

        [Required(ErrorMessage = "Community is required.")]
        [StringLength(50, ErrorMessage = "Community cannot be longer than 50 characters.")]
        public string Community { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }


        public static List<Communities> GetList(int UserID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<Communities> list = new List<Communities>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_Communities", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Type", "LIST"));
                    command.Parameters.Add(new SqlParameter("@CommunityID", "0"));
                    command.Parameters.Add(new SqlParameter("@Community", ""));
                    command.Parameters.Add(new SqlParameter("@IsActive", true));
                    command.Parameters.Add(new SqlParameter("@UserID", UserID));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Communities obj = new Communities();
                                obj.CommunityID = Convert.ToInt32(reader["CommunityID"]);
                                obj.Community = Convert.ToString(reader["Community"]);
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
        public Communities GetListByID(int OrganizationID, int UserID)
        {
            Communities obj = new Communities();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_Communities", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Type", "GET"));
                    command.Parameters.Add(new SqlParameter("@CommunityID", OrganizationID));
                    command.Parameters.Add(new SqlParameter("@Community", ""));
                    command.Parameters.Add(new SqlParameter("@IsActive", true));
                    command.Parameters.Add(new SqlParameter("@UserID", UserID));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                obj.CommunityID = Convert.ToInt32(reader["CommunityID"]);
                                obj.Community = Convert.ToString(reader["Community"]);
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
        public bool Save(string Type, Communities obj, int UserID)
        {
            bool flag = false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_Communities", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Type", Type));
                    command.Parameters.Add(new SqlParameter("@CommunityID", obj.CommunityID));
                    command.Parameters.Add(new SqlParameter("@Community", obj.Community));
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