using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;

namespace FBPlusOneBuy.ViewModels
{

    public class SalesOrderViewModel
    {
        public List<SalesOrder> salesOrders { get; set; }
        public int salesOrderNum { get; set; }
    }
}