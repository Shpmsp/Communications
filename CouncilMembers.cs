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
    [Table("mstCouncilMembers")]
    public class CouncilMembers
    {
        public CouncilMembers()
        {
            CouncilMember = "";
            Phone = "";
            EmailAddress = "";
            LiasonDistrict = "";

        }

        [Key]
        public int CouncilMemberID { get; set; }

        [Required]
        public string CouncilMember { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string LiasonDistrict { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<CouncilMembers> GetList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<CouncilMembers> list = new List<CouncilMembers>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_CouncilMembers", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CouncilMemberID", 0));
                    command.Parameters.Add(new SqlParameter("@CouncilMember", ""));
                    command.Parameters.Add(new SqlParameter("@Phone", ""));
                    command.Parameters.Add(new SqlParameter("@EmailAddress", ""));
                    command.Parameters.Add(new SqlParameter("@LiasonDistrict", ""));
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
                                CouncilMembers obj = new CouncilMembers();
                                obj.CouncilMemberID = Convert.ToInt32(reader["CouncilMemberID"]);
                                obj.CouncilMember = Convert.ToString(reader["CouncilMember"]);
                                obj.Phone = Convert.ToString(reader["Phone"]);
                                obj.EmailAddress = Convert.ToString(reader["EmailAddress"]);
                                obj.LiasonDistrict = Convert.ToString(reader["LiasonDistrict"]);
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

        public CouncilMembers GetListByID(int CouncilMemberID)
        {
            CouncilMembers obj = new CouncilMembers();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_CouncilMembers", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CouncilMemberID", CouncilMemberID));
                    command.Parameters.Add(new SqlParameter("@CouncilMember", ""));
                    command.Parameters.Add(new SqlParameter("@Phone", ""));
                    command.Parameters.Add(new SqlParameter("@EmailAddress", ""));
                    command.Parameters.Add(new SqlParameter("@LiasonDistrict", ""));
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
                                obj.CouncilMemberID = Convert.ToInt32(reader["CouncilMemberID"]);
                                obj.CouncilMember = Convert.ToString(reader["CouncilMember"]);
                                obj.Phone = Convert.ToString(reader["Phone"]);
                                obj.EmailAddress = Convert.ToString(reader["EmailAddress"]);
                                obj.LiasonDistrict = Convert.ToString(reader["LiasonDistrict"]);
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
        public bool Save(string Type, int CouncilMemberID, string CouncilMember, string Phone, string EmailAddress, string LiasonDistrict, bool IsActive, int CreatedBy, DateTime CreatedOn, int ModifiedBy, DateTime ModifiedOn, int UserID)
        {
            bool flag = false; string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_CouncilMembers", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CouncilMemberID", CouncilMemberID));
                    command.Parameters.Add(new SqlParameter("@CouncilMember", CouncilMember));
                    command.Parameters.Add(new SqlParameter("@Phone", Phone));
                    command.Parameters.Add(new SqlParameter("@EmailAddress", EmailAddress));
                    command.Parameters.Add(new SqlParameter("@LiasonDistrict", LiasonDistrict));
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