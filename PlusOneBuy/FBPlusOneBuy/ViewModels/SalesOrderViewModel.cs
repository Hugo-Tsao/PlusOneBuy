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
        public int SalesOrderID { get; set; }
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
        public DateTime CheckoutDateTime { get; set; }
        public int Quantity { get; set; }
        public int LiveID { get; set; }
        public decimal Total { get; set; }
    }
}