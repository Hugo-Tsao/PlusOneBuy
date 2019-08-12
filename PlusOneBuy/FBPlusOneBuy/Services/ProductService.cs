using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using FBPlusOneBuy.DBModels;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;
using RestSharp;

namespace FBPlusOneBuy.Services
{
    public static class ProductService
    {
        //儲存所有商品
        [WebMethod(EnableSession = true)] //啟用Session
        public static ProductList GetCurrentProducts()
        {
            //確認HttpContext.Current是否為空
            if (HttpContext.Current != null)
            {
                //如果Session["Product"]不存在，就建一個空的Products物件
                if (HttpContext.Current.Session["Products"] == null)
                {
                    var order = new ProductList();
                    HttpContext.Current.Session["Products"] = order;
                }

                return (ProductList)HttpContext.Current.Session["Products"];
            }
            else
            {
                throw new InvalidOperationException("System.web.HttpContext.Current為空，請檢查");
            }
        }

        public static void ClearProducts()
        {
            HttpContext.Current.Session.Abandon();
        }

        public static bool DeleteProduct(int skuId)
        {
            var productList = GetCurrentProducts();
            if (productList.ProductItems.Any(x => x.SkuId == skuId))
            {
                productList.DeleteProduct(skuId);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateProductInfo(int skuId,string keyword,int presetQty)
        {
            var productList = GetCurrentProducts();
            int index = productList.ProductItems.FindIndex(x=>x.SkuId == skuId);
            if (index != -1)
            {
                productList.ProductItems[index].PresetQty = presetQty;
                productList.ProductItems[index].Keyword = keyword;
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool UpdateProductQty(int skuid, int BoughtQty)
        {
            var products = GetCurrentProducts();
            var index = products.ProductItems.FindIndex(x=>x.SkuId == skuid);
            var ProductQty = products.ProductItems[index].PresetQty;
            if (ProductQty > 0 && ProductQty >= BoughtQty)
            {
                products.ProductItems[index].PresetQty -= BoughtQty;
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static string keyValue = ConfigurationManager.AppSettings["X-API-KEY"];

        public static ProductMain GetMain(int salepage_id)
        {
            var shopUrl = "https://apigw.qa.91dev.tw/ec/V1/SalePage/GetMain";
            var client = new RestClient(shopUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "21");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "apigw.qa.91dev.tw");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-api-key", keyValue);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n  \"id\": " + salepage_id + "\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ProductMain store = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductMain>(response.Content);

            return store;
        }

        public static ProductCategory GetSKUList(ProductCategoryViewModel pcvm)
        {
            var JsonPCVM = Newtonsoft.Json.JsonConvert.SerializeObject(pcvm);
            var shopUrl = "https://apigw.qa.91dev.tw/ec/V1/SalePage/GetSKUList";
            var client = new RestClient(shopUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "193");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "apigw.qa.91dev.tw");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-api-key", keyValue);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", JsonPCVM, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ProductCategory store = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCategory>(response.Content);

            return store;
        }

        public static ProductStock GetStock(int salepage_id)
        {
            //等到有登入功能後，就可綁ShopID
            var shopUrl = "https://apigw.qa.91dev.tw/ec/V1/SalePage/GetStock";
            var client = new RestClient(shopUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "193");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "apigw.qa.91dev.tw");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-api-key", keyValue);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n  \"id\": " + salepage_id + "\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ProductStock store = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductStock>(response.Content);

            return store;
        }

        public static Product GetProductById(int productId)
        {
            ProductRepositories product_repo = new ProductRepositories();
            var product = product_repo.GetProductById(productId);
            return product;
        }
    }
}