using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.ViewModels;
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
        public static void OrderListToSendMsg(List<OrderList> orderList, string token)
        {
            foreach (var order in orderList)
            {
                string msgText = $"{order.CustomerName}你好，感謝您訂購我們的產品!!\r\n{order.ProductName}-數量{order.Quantity}，請點擊下列連結完成接下來的購物流程!";
                List<string> id = new List<string> { order.CustomerID };
                FBSendMsgService.SendMsg(msgText, id, token);
            }
        }
    }
}