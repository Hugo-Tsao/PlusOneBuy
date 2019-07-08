using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;
using RestSharp;

namespace FBPlusOneBuy.Services
{
    public static class ProductService
    {
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
            request.AddParameter("undefined", "{\r\n  \"id\": "+ salepage_id + "\r\n}", ParameterType.RequestBody);
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
            request.AddParameter("undefined", "{\r\n  \"id\": "+ salepage_id +"\r\n}", ParameterType.RequestBody);
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