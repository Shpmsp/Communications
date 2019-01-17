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
    [Table("mstBureau")]
    public class Bureaus
    {
        public Bureaus()
        {

        }

        [Key]
        public int BureauId { get; set; }

        [Required(ErrorMessage = "Bureau is required.")]

        public string Bureau { get; set; }

    }
}