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
    [Table("mstDistricts")]
    public class Districts
    {
        public Districts()
        {
            District = "";

        }

        [Key]
        public int DistrictID { get; set; }

        public string District { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<Districts> GetList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<Districts> list = new List<Districts>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_Districts", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@DistrictID", 0));
                    command.Parameters.Add(new SqlParameter("@District", ""));
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
                                Districts obj = new Districts();
                                obj.DistrictID = Convert.ToInt32(reader["DistrictID"]);
                                obj.District = Convert.ToString(reader["District"]);
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

        public Districts GetListByID(int DistrictID)
        {
            Districts obj = new Districts();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_Districts", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@DistrictID", DistrictID));
                    command.Parameters.Add(new SqlParameter("@District", ""));
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
                                obj.DistrictID = Convert.ToInt32(reader["DistrictID"]);
                                obj.District = Convert.ToString(reader["District"]);
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
        public bool Save(string Type, int DistrictID, string District, bool IsActive, int CreatedBy, DateTime CreatedOn, int ModifiedBy, DateTime ModifiedOn, int UserID)
        {
            bool flag = false; string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("USP_Masters_Districts", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@DistrictID", DistrictID));
                    command.Parameters.Add(new SqlParameter("@District", District));
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