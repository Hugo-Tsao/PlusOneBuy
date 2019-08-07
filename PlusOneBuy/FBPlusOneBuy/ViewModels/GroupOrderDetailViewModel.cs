using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class GroupOrderDetailViewModel
    {
        [Display(Name = "明細編號")]
        public int GroupOrderDetailID { get; set; }
        [Display(Name = "顧客名稱")]
        public string Name { get; set; }
        [Display(Name = "產品名稱")]
        public string ProductName { get; set; }
        [Display(Name = "價格")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "數量")]
        public int Quantity { get; set; }
        [Display(Name = "留言時間")]
        public DateTime MessageDateTime { get; set; }
        [Display(Name = "總額")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Amount { get; set; }
        [Display(Name = "單量")]
        public int NumberOfProduct { get; set; }
    }
}