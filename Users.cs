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
     [Table("mstusers")]
    public class Users
    {
         public Users()
        {
            

        }

         [Key]
         public int UserID { get; set; }

         public string UserName { get; set; }

         public bool IsActive { get; set; }

         public int? CreatedBy { get; set; }

         public DateTime? CreatedOn { get; set; }

         public int? ModifiedBy { get; set; }

         public DateTime? ModifiedOn { get; set; }
    }

   

}