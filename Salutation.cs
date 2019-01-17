using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Communications.Models
{
    public class Salutation
    {
        public static List<string> GetSalutation()
        {
            string str = ConfigurationManager.AppSettings["Salutation"].ToString();
            List<string> list = new List<string>();

            foreach (string s in str.Split(','))
            {
                list.Add(s);
            }
            return list;
        }
    }
}