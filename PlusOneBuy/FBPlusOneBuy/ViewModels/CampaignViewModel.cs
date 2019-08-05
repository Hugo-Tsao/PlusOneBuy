using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class CampaignViewModel
    {
        public int CompaignID { get; set; }
        public int GroupID { get; set; }
        public int ProductID { get; set; }
        public int ProductSet { get; set; }
        public int ProductGroup { get; set; }
        public string Keyword { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Detail { get; set; }
    }
}