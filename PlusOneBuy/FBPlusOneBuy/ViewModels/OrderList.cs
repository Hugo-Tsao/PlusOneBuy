using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class OrderList
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Keyword { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string LiveID { get; set; }
    }
}