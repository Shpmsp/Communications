using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Communications.Models
{
    [Table("mstCommunityAssociations")]
    public class CommunityAssociations
    {
        public CommunityAssociations()
        {
            Community = "";
            Salutation = "";
            FirstName = "";
            LastName = "";
            Address = "";
            ZipCode = "";
            Phone = "";
            ContactEmail = "";
            CouncilMemberID = 0;
            //IsActive = bool.Parse("0");
        }

        [Key]
        public int CommunityAssociationID { get; set; }

        [Required(ErrorMessage = "Organization is required.")]
        public string Community { get; set; }

        //[Required(ErrorMessage = "Salutation is required.")]
       // [StringLength(5, ErrorMessage = "Salutation cannot be longer than 5 characters.")]
        public string Salutation { get; set; }

        //[Required(ErrorMessage = "FirstName is required.")]
        //[StringLength(75, ErrorMessage = "FirstName cannot be longer than 75 characters.")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "LastName is required.")]
        //[StringLength(50, ErrorMessage = "LastName cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Address is required.")]
        //[StringLength(500, ErrorMessage = "Address cannot be longer than 500 characters.")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "ZipCode is required.")]
        //[StringLength(10, ErrorMessage = "ZipCode cannot be longer than 10 characters.")]
        public string ZipCode { get; set; }

        //[Required(ErrorMessage = "Phone is required.")]
        //[StringLength(50, ErrorMessage = "Phone cannot be longer than 50 characters.")]
        public string Phone { get; set; }

        //[Required(ErrorMessage = "ContactEmail is required.")]
        ////[StringLength(10, ErrorMessage = "Contact Email cannot be longer than 10 characters.")]
        //[StringLength(75, ErrorMessage = "Contact Email cannot be longer than 75 characters.")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Council Member is required.")]
        public int? CouncilMemberID { get; set; }

        [Required(ErrorMessage = "District is required.")]
        public int? DistrictID { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }


        public static List<CommunityAssociations> GetList(int UserID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<CommunityAssociations> list = new List<CommunityAssociations>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_Communications_Communities", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Type", "LIST"));
                    command.Parameters.Add(new SqlParameter("@CommunityAssociationID", "0"));
                    command.Parameters.Add(new SqlParameter("@Community", ""));
                    command.Parameters.Add(new SqlParameter("@Salutation", ""));
                    command.Parameters.Add(new SqlParameter("@FirstName", ""));
                    command.Parameters.Add(new SqlParameter("@LastName", ""));
                    command.Parameters.Add(new SqlParameter("@Address", ""));
                    command.Parameters.Add(new SqlParameter("@ZipCode", ""));
                    command.Parameters.Add(new SqlParameter("@Phone", ""));
                    command.Parameters.Add(new SqlParameter("@ContactEmail", ""));
                    command.Parameters.Add(new SqlParameter("@CouncilMemberID", "0"));
                    command.Parameters.Add(new SqlParameter("@DistrictID", "0"));
                    command.Parameters.Add(new SqlParameter("@IsActive", true));
                    command.Parameters.Add(new SqlParameter("@UserID", UserID));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CommunityAssociations obj = new CommunityAssociations();
                                obj.CommunityAssociationID = Convert.ToInt32(reader["CommunityAssociationID"]);
                                obj.Community = Convert.ToString(reader["Community"]);
                                obj.Salutation = Convert.ToString(reader["Salutation"]);
                                obj.FirstName = Convert.ToString(reader["FirstName"]);
                                obj.LastName = Convert.ToString(reader["LastName"]);
                                obj.Address = Convert.ToString(reader["Address"]);
                                obj.ZipCode = Convert.ToString(reader["ZipCode"]);
                                obj.Phone = Convert.ToString(reader["Phone"]);
                                obj.ContactEmail = Convert.ToString(reader["ContactEmail"]);
                                obj.CouncilMemberID = Convert.ToInt32(reader["CouncilMemberID"]);
                                obj.DistrictID = Convert.ToInt32(reader["DistrictID"]);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CommunityID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CommunityAssociations GetListByID(int CommunityAssociationID, int UserID)
        {
            CommunityAssociations obj = new CommunityAssociations();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("USP_GetCommunityAssociations", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Type", "GET"));
                    command.Parameters.Add(new SqlParameter("@CommunityAssociationID", CommunityAssociationID));
                    command.Parameters.Add(new SqlParameter("@Community", ""));
                    command.Parameters.Add(new SqlParameter("@Salutation", ""));
                    command.Parameters.Add(new SqlParameter("@FirstName", ""));
                    command.Parameters.Add(new SqlParameter("@LastName", ""));
                    command.Parameters.Add(new SqlParameter("@Address", ""));
                    command.Parameters.Add(new SqlParameter("@ZipCode", ""));
                    command.Parameters.Add(new SqlParameter("@Phone", ""));
                    command.Parameters.Add(new SqlParameter("@ContactEmail", ""));
                    command.Parameters.Add(new SqlParameter("@CouncilMemberID", "0"));
                    command.Parameters.Add(new SqlParameter("@DistrictID", "0"));
                    command.Parameters.Add(new SqlParameter("@IsActive", true));
                    command.Parameters.Add(new SqlParameter("@UserID", UserID));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                obj.CommunityAssociationID = Convert.ToInt32(reader["CommunityAssociationID"]);
                                obj.Community = Convert.ToString(reader["Community"]);
                                obj.Salutation = Convert.ToString(reader["Salutation"]);
                                obj.FirstName = Convert.ToString(reader["FirstName"]);
                                obj.LastName = Convert.ToString(reader["LastName"]);
                                obj.Address = Convert.ToString(reader["Address"]);
                                obj.ZipCode = Convert.ToString(reader["ZipCode"]);
                                obj.Phone = Convert.ToString(reader["Phone"]);
                                obj.ContactEmail = Convert.ToString(reader["ContactEmail"]);
                                obj.CouncilMemberID = Convert.ToInt32(reader["CouncilMemberID"]);
                                obj.DistrictID = Convert.ToInt32(reader["DistrictID"]);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="obj"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool Save(string Type, CommunityAssociations obj, int UserID)
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
                    command.Parameters.Add(new SqlParameter("@CommunityAssociationID", obj.CommunityAssociationID));
                    command.Parameters.Add(new SqlParameter("@Community", obj.Community));
                    command.Parameters.Add(new SqlParameter("@Salutation", obj.Salutation));
                    command.Parameters.Add(new SqlParameter("@FirstName", obj.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", obj.LastName));
                    command.Parameters.Add(new SqlParameter("@Address", obj.Address));
                    command.Parameters.Add(new SqlParameter("@ZipCode", obj.ZipCode));
                    command.Parameters.Add(new SqlParameter("@Phone", obj.Phone));
                    command.Parameters.Add(new SqlParameter("@ContactEmail", obj.ContactEmail));
                    command.Parameters.Add(new SqlParameter("@CouncilMemberID", obj.CouncilMemberID));
                    command.Parameters.Add(new SqlParameter("@DistrictID", obj.DistrictID));
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCommunityAssociations()
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_GetCommunityAssociationList", con))
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