using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class GroupOrderViewModel
    {
        public int GroupOrderID { get; set; }

        public int? CampaignID { get; set; }

        public DateTime? OrderDateTime { get; set; }

        public string shipDateTime { get; set; }

        public bool isGroup { get; set; }

        public int NumberOfProduct { get; set; }

        public decimal Amount { get; set; }
    }
}