using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;

namespace FBPlusOneBuy.ViewModels
{
    public class TotalAndSalesOrders
    {
        public int salesOrderNum { get; set; }
        public decimal total { get; set; }
    }
    public class SalesOrderViewModel
    {
        public List<SalesOrderList> salesOrders { get; set; }
        public int salesOrderNum { get; set; }
        public decimal total { get; set; }
        public string livename { get; set; }
    }
    public class SalesOrderList
    {
        [Display(Name = "銷售單編號")]
        public int SalesOrderID { get; set; }
        [Display(Name = "商品名稱")]
        public string ProductName { get; set; }
        [Display(Name = "商品編號")]
        public int ProductID { get; set; }
        [Display(Name = "顧客名稱")]
        public string CustomerName { get; set; }
        [Display(Name = "顧客編號")]
        public string CustomerID { get; set; }
        [Display(Name = "成立時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime CheckoutDateTime { get; set; }
        [Display(Name = "數量")]
        public int Quantity { get; set; }
        [Display(Name = "直播編號")]
        public int LiveID { get; set; }
        [Display(Name = "金額")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Total { get; set; }
    }
}