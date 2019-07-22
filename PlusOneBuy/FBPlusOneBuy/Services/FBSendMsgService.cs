using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json;
using RestSharp;

namespace FBPlusOneBuy.Services
{
    public class FBSendMsgService
    {
        public static void SendMsg(string text, List<string> ids,string token)
        {
            
            foreach (string id in ids)
            {
                var msg = new FbSendMessage.SendObject()
                {
                    message = new FbSendMessage.Message { text = text },
                    recipient = new FbSendMessage.Recipient { id = id },
                    messaging_type= "MESSAGE_TAG",
                    tag= "BUSINESS_PRODUCTIVITY"
                };
                var jsonMsg = JsonConvert.SerializeObject(msg);
                var client = new RestClient("https://graph.facebook.com/v3.3/me/messages?access_token="+ token);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-length", "92");
                request.AddHeader("accept-encoding", "gzip, deflate");
                request.AddHeader("Host", "graph.facebook.com");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", jsonMsg, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                
            }          
        }
        public static void OrderListToSendMsg(string livePageId, List<OrderList> orderList, string token)
        {
            LivePostsRepository livePost_repo = new LivePostsRepository();
            var liveId = livePost_repo.Select(livePageId);
            foreach (var order in orderList)
            {
                var link = getAddToCartLink(liveId,order.Product.Salepage_id, order.Product.SkuId, order.Quantity);
                string msgText = $"{order.CustomerName}你好，感謝您訂購我們的產品!!\r\n{order.Product.ProductName}-數量{order.Quantity}，請點擊下列連結完成接下來的購物流程!{link}";
                List<string> id = new List<string> { order.CustomerID };
                FBSendMsgService.SendMsg(msgText, id, token);
            }
        }

        public static string getAddToCartLink(int liveKey ,int salepage_id,int skuId,int qty)
        {
            string link = string.Empty;
            link = "http://64.selfshop.qa.91dev.tw/v2/ShoppingCart/BatchInsert?&data=";
            CartViewModel[] data = new CartViewModel[]
            {
                new CartViewModel()
                {
                    salepage_id = salepage_id,
                    sku_id = skuId,
                    qty = qty
                }
            };
            var JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var EncodeData = getUrlEncode(JsonData);
            link += EncodeData;
            link = ShortenLink(link);
            return link;
        }

        internal static string getUrlEncode(string JsonData)
        {
            return WebUtility.UrlEncode(JsonData);
        }
        internal static string getUrlDecode(string str)
        {
            return WebUtility.UrlDecode(str);
        }
        internal static string ShortenLink(string link)
        {
            var client = new RestClient("https://api-ssl.bitly.com/v3/shorten?login=o_3888efs55q&apiKey=R_ef7f0de73a5343d1a45acd9c93ce3d9d&longUrl="+link+"&format=txt");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            return response.Content.ToString();
        }
    }
}