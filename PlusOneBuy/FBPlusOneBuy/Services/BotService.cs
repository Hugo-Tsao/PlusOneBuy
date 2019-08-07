using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.VisualBasic.ApplicationServices;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Services
{
    public class BotService
    {
        internal static string channelAccessToken = ConfigurationManager.AppSettings["channelAccessToken"];
        public static void BotPushMsg(string groupid,string msg)
        {

            var pushUrl = "https://api.line.me/v2/bot/message/push";
            var client = new RestClient(pushUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "169");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api.line.me");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", string.Format("Bearer " + channelAccessToken));
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n  \"to\": \""+ groupid + "\"," +
                "\r\n    \"messages\":[{\"type\":\"text\",\"text\":\""+ msg + "\"}]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

        }
        public static StoreMeanger CheckMeanger(string groupId, string managerUserId)
        {

            var groupUrl = "https://api.line.me/v2/bot/group/" + groupId + "/member/" + managerUserId;
            var client = new RestClient(groupUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api.line.me");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", string.Format("Bearer " + channelAccessToken));
            IRestResponse response = client.Execute(request);
            StoreMeanger StoreMeanger = Newtonsoft.Json.JsonConvert.DeserializeObject<StoreMeanger>(response.Content);
            return StoreMeanger;
        }
        public static bool SearchLineCustomer(string customerId,string customerName,ref LineCustomerViewModel lineCustomer)
        {
            LineCustomerRepository LineCustomer_repo = new LineCustomerRepository();
            var checkCustomer = LineCustomer_repo.SearchLineCustomer(customerId);
            if (checkCustomer != null)
            {
                lineCustomer = checkCustomer;
                return true;
            }
            return false;
        }

        public static DateTime TimestampToDateTime(long timestamp)
        {
            double millionSecond = Convert.ToDouble(timestamp);
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0).AddHours(8).AddMilliseconds(millionSecond);
            return result;
        }
        public static LineCustomerViewModel InsertLineCustomer(string customerId, string customerName)
        {
            LineCustomerRepository LineCustomer_repo = new LineCustomerRepository();
            LineCustomer_repo.InsertCustomer(customerId,customerName);
            return new LineCustomerViewModel() {LineCustomerID = customerId, Name = customerName};
        }
    }
}