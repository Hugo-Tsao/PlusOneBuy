using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class ReportViewModel
    {
        public int ID { get; set; }
        public string LiveName { get; set; }
        public DateTime postTime { get; set; }
        public DateTime? endTime { get; set; }
        public string LivePageID { get; set; }
        public decimal? Amount { get; set; }
        public int? QtyOfOrders { get; set; }
    }
}