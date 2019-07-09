using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Models
{
    public class ProductSKUList_Data
    {
        public int Id { get; set; }
        public bool IsClosed { get; set; }
        public DateTime SellingStartDateTime { get; set; }
        public DateTime SellingEndDateTime { get; set; }
        public int SkuId { get; set; }
        public string OuterId { get; set; }
        public bool IsShow { get; set; }
    }

    public class ProductSKUList
    {
        public string ErrorId { get; set; }
        public string Status { get; set; }
        public List<ProductSKUList_Data> Data { get; set; }
        public object ErrorMessage { get; set; }
    }

    public class ProductMain_Data
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime SellingStartDateTime { get; set; }
        public DateTime SellingEndDateTime { get; set; }
        public bool IsClosed { get; set; }
        public int SuggestPrice { get; set; }
        public int Price { get; set; }
        public int Cost { get; set; }
        public int CategoryId { get; set; }
        public int ShopId { get; set; }
        public int ShopCategoryId { get; set; }
        public int ShippingTypeDef { get; set; }
        public string ShippingDate { get; set; }
        public int ShippingWaitingDays { get; set; }
        public List<string> PayTypes { get; set; }
        public List<int> ShippingTypes { get; set; }
        public string SEOTitle { get; set; }
        public string SEOKeywords { get; set; }
        public string SEODescription { get; set; }
        public string TemperatureTypeDef { get; set; }
        public object Length { get; set; }
        public object Width { get; set; }
        public object Height { get; set; }
        public object Weight { get; set; }
        public string StatusDef { get; set; }
    }

    public class ProductMain
    {
        public string ErrorId { get; set; }
        public string Status { get; set; }
        public ProductMain_Data Data { get; set; }
        public object ErrorMessage { get; set; }
    }
}