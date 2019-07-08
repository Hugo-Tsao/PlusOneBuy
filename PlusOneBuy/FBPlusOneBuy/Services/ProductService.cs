using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using RestSharp;

namespace FBPlusOneBuy.Services
{
    public static class ProductService
    {
        //儲存所有商品
        [WebMethod(EnableSession = true)] //啟用Session
        public static ProductList GetCurrentCart()
        {
            //確認HttpContext.Current是否為空
            if (HttpContext.Current != null)
            {
                //如果Session["Cart"]不存在，就建一個空的Cart物件
                if (HttpContext.Current.Session["Cart"] == null)
                {
                    var order = new ProductList();
                    HttpContext.Current.Session["Cart"] = order;
                }

                return (ProductList)HttpContext.Current.Session["Cart"];
            }
            else
            {
                throw new InvalidOperationException("System.web.HttpContext.Current為空，請檢查");
            }
        }

        public static void ClearCart()
        {
            HttpContext.Current.Session["Cart"] = new ProductList();
        }
        internal static string keyValue = ConfigurationManager.AppSettings["X-API-KEY"];
        public static Store GetSKUListByMain(int salepage_id)
        {
            var client = new RestClient("https://api.91app.com/ec/V1/SalePage/GetSKUListByMain");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "21");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api.91app.com");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-api-key", keyValue);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n  \"id\": " + salepage_id + "\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Store store = Newtonsoft.Json.JsonConvert.DeserializeObject<Store>(response.Content);

            return store;
        }

        public static Store GetMain(int salepage_id)
        {
            var client = new RestClient("https://api.91app.com/ec/V1/SalePage/GetMain");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "21");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api.91app.com");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-api-key", keyValue);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n  \"id\": " + salepage_id + "\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Store store = Newtonsoft.Json.JsonConvert.DeserializeObject<Store>(response.Content);

            return store;
        }

        public static Store GetStock(int salepage_id, int skuId)
        {
            var client = new RestClient("https://api.91app.com/ec/V1/SalePage/GetStock");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "43");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api.91app.com");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-api-key", keyValue);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n  \"id\": " + salepage_id + ",\r\n  \"skuId\": " + skuId + "\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Store store = Newtonsoft.Json.JsonConvert.DeserializeObject<Store>(response.Content);

            return store;
        }
    }
}