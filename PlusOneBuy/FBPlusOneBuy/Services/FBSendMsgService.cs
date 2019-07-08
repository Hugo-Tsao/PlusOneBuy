using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using FBPlusOneBuy.Models;
using Newtonsoft.Json;
using RestSharp;

namespace FBPlusOneBuy.Services
{
    public class FBSendMsgService
    {
        public static void SendMsg(string text, List<string> ids)
        {
            foreach (string id in ids)
            {
                var msg = new FbSendMessage.SendObject()
                {
                    message = new FbSendMessage.Message { text = text },
                    recipient = new FbSendMessage.Recipient { id = id }
                };
                var jsonMsg = JsonConvert.SerializeObject(msg);
                string token = "EAASxbKYYpHoBADjSVXOIWYxZAk2tU47ItZCAnO6gFzSpZCugIT1L8nGiZCbOFZCgCXiv2qcyANrqOmDyIFZAeYkO0TM6alFpLy928vJLpMEHHHXjcQD8xv8ZCUIVQ5ZBOrFVjNYZCvZANTIotMUQzVEZCVl1D0snswuYuu5iEahWCl3seE7zgcikk7Y";
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
    }
}