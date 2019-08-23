using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class ProductViewModel
    {
        public int Salepage_id { get; set; }
        public int SkuId { get; set; }
        public string SkuName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Keyword { get; set; }
        public int Qty { get; set; }
        public int PresetQty { get; set; }

        public int Stock { get; set; }
    }
}