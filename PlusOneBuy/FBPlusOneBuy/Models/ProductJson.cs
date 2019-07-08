using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Models
{
    public class Store_Data
    {
        public int Id { get; set; }
        public bool IsClosed { get; set; }
        public DateTime SellingStartDateTime { get; set; }
        public DateTime SellingEndDateTime { get; set; }
        public bool IsShow { get; set; }
        public string Title { get; set; }
        public int SkuId { get; set; }
        public string SkuName { get; set; }
        public string OuterId { get; set; }
        public int SellingQty { get; set; }
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
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string StatusDef { get; set; }
    }

    public class Store
    {
        public string ErrorId { get; set; }
        public string Status { get; set; }
        public List<Store_Data> Data { get; set; }
        public object ErrorMessage { get; set; }
    }
}