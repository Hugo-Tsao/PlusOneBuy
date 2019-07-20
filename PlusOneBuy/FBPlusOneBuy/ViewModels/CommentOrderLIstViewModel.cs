using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class CommentOrderLIstViewModel
    {
        [Display(Name = "訂單編號")]
        public int OrderID { get; set; }
        [Display(Name = "顧客名稱")]
        public string CustomerName { get; set; }
        [Display(Name = "商品名稱")]
        public string ProductName { get; set; }
        [Display(Name = "商品單價")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "訂購數量")]
        public int Quantity { get; set; }
        [Display(Name = "留言時間")]
        public DateTime OrderDateTime { get; set; }
        [Display(Name = "總額")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Amount => UnitPrice * Quantity;
    }
}