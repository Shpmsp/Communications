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
    [Table("trnMediaInquiriesDetail")]
    public class MediaInquiryDetail
    {
        public MediaInquiryDetail()
        {

        }

        [Key]
        public int MediaInquiryDetailID { get; set; }
        public int MediaInquiryID { get; set; }
        public string InquirySourceD { get; set; }
        public string Reporter { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }

    }
}