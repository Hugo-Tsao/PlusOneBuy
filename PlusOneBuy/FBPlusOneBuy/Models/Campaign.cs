using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Models
{
    public class Campaign
    {
        public int CompaignID { get; set; }
        public string GroupID { get; set; }
        public int ProductID { get; set; }
        public int ProductSet { get; set; }
        public int PeopleGroup { get; set; }
        public string Keyword { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Detail { get; set; }
    }
}