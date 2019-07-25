using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Models
{
    public class ProductCategory_Data
    {
        public int Id { get; set; }
    }

    public class ProductMain_Data
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
    }

    public class ProductMain
    {
        public string ErrorId { get; set; }
        public string Status { get; set; }
        public ProductMain_Data Data { get; set; }
        public object ErrorMessage { get; set; }
    }

    public class ProductCategory
    {
        public string ErrorId { get; set; }
        public string Status { get; set; }
        public List<ProductCategory_Data> Data { get; set; }
        public object ErrorMessage { get; set; }
    }
    public class ProductStock
    {
        public string ErrorId { get; set; }
        public string Status { get; set; }
        public List<ProductStock_Data> Data { get; set; }
        public object ErrorMessage { get; set; }
    }
    public class ProductStock_Data
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SkuId { get; set; }
        public string SkuName { get; set; }
        public int SellingQty { get; set; }

    }
}