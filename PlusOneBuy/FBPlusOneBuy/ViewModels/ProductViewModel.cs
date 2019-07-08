using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class ProductViewModel
    {
        public string Salepage_id { get; set; }
        public string SkuId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Keyword { get; set; }

        //public int Qty { get; set; }
    }
}