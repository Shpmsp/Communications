using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Communications.Models
{
    public class Lookup
    {
        public static string GetUserName(int userID = 0)
        {
            string userName = "";
            ApplicationDbContext context = new ApplicationDbContext();

            List<string> userList = (from p in context.Users.AsEnumerable()
                               where p.UserID == userID
                               select p.UserName).ToList();

            if (userList.Count() > 0)
            {
                userName = userList[0];
            }

            return userName;
        }


        public static string GetCommunity(int communityID = 0)
        {
            string community = "";

            ApplicationDbContext context = new ApplicationDbContext();

            List<string> list = (from p in context.Communities.AsEnumerable()
                                     where p.CommunityID == communityID
                                     select p.Community).ToList();

            if (list.Count() > 0)
            {
                community = list[0];
            }

            return community;
        }


        public static string GetCouncilMember(int councilMemberID = 0)
        {
            string councilMemmber = "";

            ApplicationDbContext context = new ApplicationDbContext();

            List<string> list = (from p in context.CouncilMembers.AsEnumerable()
                                 where p.CouncilMemberID == councilMemberID
                                 select p.CouncilMember).ToList();

            if (list.Count() > 0)
            {
                councilMemmber = list[0];
            }

            return councilMemmber;
        }


        public static string GetDistrict(int districtID = 0)
        {
            string district = "";

            ApplicationDbContext context = new ApplicationDbContext();

            List<string> list = (from p in context.Districts.AsEnumerable()
                                 where p.DistrictID == districtID
                                 select p.District).ToList();

            if (list.Count() > 0)
            {
                district = list[0];
            }

            return district;
        }


        
    }
}