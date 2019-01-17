using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Communications.Models
{
    public class ApplicationDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Communications.Models.CouncilMembers> CouncilMembers { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.Liaisons> Liaisons { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.MeetingSubjects> MeetingSubjects { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.MeetingTypes> MeetingTypes { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.MeetingAttendance> MeetingAttendances { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.CommunityAssociations> CommunityAssociations { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.Districts> Districts { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.Users> Users { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.Communities> Communities { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.MediaInquiries> MediaInquiries { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.Reporters> Reporters { get; set; }
        //New Added
        public System.Data.Entity.DbSet<Communications.Models.Bureaus> BureausDS { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.TypeRequest> TypeRequests { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.InquirySource> InquirySources { get; set; }

        public System.Data.Entity.DbSet<Communications.Models.MediaInquiryDetail> MediaInquiryDetails { get; set; }
    }
}
