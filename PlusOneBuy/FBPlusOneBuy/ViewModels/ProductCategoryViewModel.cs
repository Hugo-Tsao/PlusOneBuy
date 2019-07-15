using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class ProductCategoryViewModel
    {
        public DateTime CreatedDateTimeStart { get; set; }

        public DateTime CreatedDateTimeEnd { get; set; }

        public int Position { get; set; }
        public int Count { get; set; }
        public int ShopCategoryId { get; set; }
        public bool IsClosed { get; set; }

        public ProductCategoryViewModel()
        {
            CreatedDateTimeStart = new DateTime(2000,1,1);
            CreatedDateTimeEnd = new DateTime(9999,1,1);
            Position = 0;
            Count = 500;
            IsClosed = true;
        }
    }
}