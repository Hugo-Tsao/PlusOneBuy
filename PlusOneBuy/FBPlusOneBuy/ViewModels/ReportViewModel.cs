using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class ReportViewModel
    {
        [Display(Name = "編號")]
        public int ID { get; set; }
        [Display(Name = "直播名稱")]
        public string LiveName { get; set; }
        [Display(Name = "開始時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime postTime { get; set; }
        [Display(Name = "結束時間")]
        public DateTime? endTime { get; set; }
        [Display(Name = "直播編號")]
        public string LivePageID { get; set; }
        [Display(Name = "收單金額")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal? Amount { get; set; }
        [Display(Name = "收單數量")]
        public int? QtyOfOrders { get; set; }
    }
}