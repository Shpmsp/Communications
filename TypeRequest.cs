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
    [Table("mstTypeRequests")]
    public class TypeRequest
    {
        public TypeRequest()
        {

        }

        [Key]
        public int TypeId { get; set; }

        public string TypeInquiry { get; set; }

    }
}