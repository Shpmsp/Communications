using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Communications.Models;
using System.Configuration;

namespace Communications.Models
{
    public class DropdownList
    {
        public static List<SelectListItem> GetLiaisons(int liaisonID = 0)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<SelectListItem> list = (from p in context.Liaisons.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.LiaisonName,
                                                     Value = p.LiaisonID.ToString(),
                                                     Selected = p.LiaisonID == liaisonID ? true : false
                                                 }).ToList();

            return list;
        }

        public static List<SelectListItem> GetMeetingTypes(int meetingTypeID = 0)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<SelectListItem> list = (from p in context.MeetingTypes.AsEnumerable()
                                         select new SelectListItem
                                         {
                                             Text = p.MeetingType,
                                             Value = p.MeetingTypeID.ToString(),
                                             Selected = p.MeetingTypeID == meetingTypeID ? true : false
                                         }).ToList();

            return list;
        }

        public static List<SelectListItem> GetMeetingSubjects(int meetingSubjectID = 0)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<SelectListItem> list = (from p in context.MeetingSubjects.AsEnumerable()
                                         select new SelectListItem
                                         {
                                             Text = p.MeetingSubject,
                                             Value = p.MeetingSubjectID.ToString(),
                                             Selected = p.MeetingSubjectID == meetingSubjectID ? true : false
                                         }).ToList();

            return list;
        }

        public static List<SelectListItem> GetCouncilMembers(int commiteeMemberID = 0)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<SelectListItem> list = (from p in context.CouncilMembers.AsEnumerable()
                                         select new SelectListItem
                                         {
                                             Text = p.CouncilMember,
                                             Value = p.CouncilMemberID.ToString(),
                                             Selected = p.CouncilMemberID == commiteeMemberID ? true : false
                                         }).ToList();

            return list;
        }


        public static List<SelectListItem> GetCommunityAssociations(int communityAssociationID = 0)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<SelectListItem> list = (from p in context.CommunityAssociations.AsEnumerable()
                                         orderby p.Community 
                                         select new SelectListItem
                                         {
                                             Text = p.Community,
                                             Value = p.CommunityAssociationID.ToString(),
                                             Selected = p.CommunityAssociationID == communityAssociationID ? true : false
                                         }).ToList();




            return list;
        }

        //public static List<SelectListItem> GetCommunities(int communityID = 0)
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();
        //    List<SelectListItem> list = (from p in context.Communities.AsEnumerable()
        //                                 select new SelectListItem
        //                                 {
        //                                     Text = p.Community,
        //                                     Value = p.CommunityID.ToString(),
        //                                     Selected = p.CommunityID == communityID ? true : false
        //                                 }).ToList();


           

        //    return list;
        //}

        public static List<SelectListItem> GetDistricts(int districtID = 0)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<SelectListItem> list = (from p in context.Districts.AsEnumerable()
                                         select new SelectListItem
                                         {
                                             Text = p.District,
                                             Value = p.DistrictID.ToString(),
                                             Selected = p.DistrictID == districtID ? true : false
                                         }).ToList();

            return list;
        }

        public static List<SelectListItem> GetSalutation(string salutation = "")
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<SelectListItem> list = (from p in Salutation.GetSalutation().AsEnumerable()
                                         select new SelectListItem
                                         {
                                             Text = p,
                                             Value = p,
                                             Selected = p == salutation ? true : false
                                         }).ToList();

            return list;
        }

        public static List<SelectListItem> GetDistrictsByCommunityAssociationID(int communityAssociationID = 0)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<SelectListItem> list = (from d in context.Districts
                                         join c in context.CommunityAssociations
                                          on new { DistrictID = (int?)d.DistrictID, CommunityAssociationID = communityAssociationID }
                                         equals new { DistrictID = c.DistrictID, CommunityAssociationID = c.CommunityAssociationID } into ps
                                         from p in ps.DefaultIfEmpty()
                                         select new SelectListItem
                                         {
                                             Text = d.District,
                                             Value = d.DistrictID.ToString(),
                                             Selected = p.CommunityAssociationID == communityAssociationID ? true : false
                                         }).ToList();



            return list;
        }

        public static List<SelectListItem> GetCouncilMembersByCommunityAssociationID(int communityAssociationID = 0)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            List<SelectListItem> list = (from c in context.CouncilMembers
                                        join p in context.CommunityAssociations 
                                         on new { CouncilMemberID = (int?)c.CouncilMemberID , CommunityAssociationID =  communityAssociationID } 
                                         equals new {CouncilMemberID = p.CouncilMemberID, CommunityAssociationID = p.CommunityAssociationID} into ps
                                        from p in ps.DefaultIfEmpty()
                                         select new SelectListItem
                                         {
                                             Text = c.CouncilMember,
                                             Value = c.CouncilMemberID.ToString(),
                                             Selected = p.CommunityAssociationID == communityAssociationID ? true : false
                                         }).Distinct().ToList();

            return list;
        }

        public static List<string> GetTypeOfInquiryList()
        {
            string str = ConfigurationManager.AppSettings["InquiryType"].ToString();
            List<string> list = new List<string>();

            foreach (string s in str.Split(','))
            {
                list.Add(s);
            }
            return list;
        }

        public static List<string> GetBureauList()
        {
            string str = ConfigurationManager.AppSettings["Bureau"].ToString();
            List<string> list = new List<string>();

            foreach (string s in str.Split(','))
            {
                list.Add(s);
            }
            return list;
        }

        public static List<string> GetCategoryList()
        {
            string str = ConfigurationManager.AppSettings["Category"].ToString();
            List<string> list = new List<string>();

            foreach (string s in str.Split(','))
            {
                list.Add(s);
            }
            return list;
        }

        public static List<string> GetInquiryFromList()
        {
            string str = ConfigurationManager.AppSettings["InquiryFrom"].ToString();
            List<string> list = new List<string>();

            foreach (string s in str.Split(','))
            {
                list.Add(s);
            }
            return list;
        }
        
    }
}