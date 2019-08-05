using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class LineListCampaignViewModel
    {
        [Display(Name = "活動編號")]
        public int CompaignID { get; set; }

        [Display(Name = "群組名稱")]
        public string GroupName { get; set; }

        [Display(Name = "活動標題")]
        public string Title { get; set; }

        [Display(Name = "商品名稱")]
        public string ProductName { get; set; }

        [Display(Name = "成組數")]
        public int ProductSet { get; set; }

        [Display(Name = "成團數")]
        public int ProductGroup { get; set; }

        [Display(Name = "關鍵字")]
        public string Keyword { get; set; }
        [Display(Name = "開始時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime PostTime { get; set; }
        [Display(Name = "結束時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime EndTime { get; set; }
    }
}