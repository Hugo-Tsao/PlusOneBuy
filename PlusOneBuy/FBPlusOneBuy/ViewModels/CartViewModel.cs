using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class CartViewModel
    {
        public int salepage_id { get; set; }
        public int sku_id { get; set; }
        public int qty { get; set; }
        public string act { get; set; }
    }
}