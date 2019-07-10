using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using FBPlusOneBuy.Models;
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
                    recipient = new FbSendMessage.Recipient { id = id }
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

        public static string getAddToCartLink(int salepage_id,int skuId,int qty)
        {
            string link = string.Empty;
            link = "https://shop8.91dev.tw/v2/ShoppingCart/BatchInsert?data=";
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
    }
}