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
    [Table("mstInquirySource")] 
    public class InquirySource
    {
        public InquirySource()
        {

        }

        [Key]
        public int InquirySourceId { get; set; }
        public string InquirySourceName { get; set; }

    }
}