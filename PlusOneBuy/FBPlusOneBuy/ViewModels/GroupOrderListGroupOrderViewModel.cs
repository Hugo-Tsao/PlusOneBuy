using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class GroupOrderListGroupOrderViewModel
    {
        [Display(Name = "活動標題")]
        public string Title { get; set; }

        [Display(Name = "團購商品")]
        public string ProductName { get; set; }

        [Display(Name = "活動內容")]
        public string Detail { get; set; }

        [Display(Name = "團單編號")]
        public string GroupOrderID { get; set; }

        [Display(Name = "成單時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime OrderDateTime { get; set; }

        [Display(Name = "到店時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime? shipDateTime { get; set; }

        [Display(Name = "是否成團")]
        public bool isGroup { get; set; }

        [Display(Name = "商品組數")]
        public int NumberOfProduct { get; set; }

        [Display(Name = "總額")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Amount { get; set; }

        [Display(Name = "成團數量")]
        public int ProductGroup { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime? BtnOrderClickDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime? BtnGroupClickDateTime { get; set; }
    }
}