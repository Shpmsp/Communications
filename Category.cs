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
    [Table("mstCategories")]
    public class Category
    {
        public Category()
        {

        }

        [Key]
        public int CategoryId { get; set; }


        public string CategoryName { get; set; }

    }
}