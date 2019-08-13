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
using isRock.LineBot;

namespace FBPlusOneBuy.Services
{
    public class BotService
    {
        internal static string channelAccessToken = ConfigurationManager.AppSettings["channelAccessToken"];
        public static void BotPushMsg(string lineGroupid, string msg)
        {
            try
            {
                Bot bot = new Bot(channelAccessToken);
                bot.PushMessage(lineGroupid, msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

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
        public static void LeaveGroup(string groupId)
        {
            var leaveGroup = "https://api.line.me/v2/bot/group/" + groupId + "/leave";
            var client = new RestClient(leaveGroup);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer " + channelAccessToken));
            IRestResponse response = client.Execute(request);

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
            LineCustomer_repo.InsertCustomer(customerId, customerName);
            return new LineCustomerViewModel() { LineCustomerID = customerId, Name = customerName };
        }

        public static string SetMsgFormat(int groupOrderId, ref string groupId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            LineCustomerRepository lineCustomer_repo = new LineCustomerRepository();
            SendMessageViewModel messageInfo = lineGroup_repo.GetMessageInfoByGroupOrderId(groupOrderId);
            List<NameAndQtyViewModel> customers = lineCustomer_repo.GetCustomersByGroupOrderId(groupOrderId);
            string message = $"活動名稱:{messageInfo.Title}\n商品名稱:{messageInfo.ProductName}\n活動時間:{messageInfo.PostTime}到{messageInfo.EndTime}\n成團成功!\n";
            groupId = messageInfo.LineGroupId;
            foreach (var customer in customers.GroupBy(info => info.Name)
                        .Select(group => new
                        {
                            Name = group.Key,
                            Count = customers.Where(x => x.Name == group.Key).Sum(x => x.Quantity)
                        }))
            {
                message += $"@{customer.Name} : {customer.Count} 組\n";
            }

            return message;
        }
        public static string SetArrivedMsgFormat(int groupOrderId, ref string groupId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            LineCustomerRepository lineCustomer_repo = new LineCustomerRepository();
            SendMessageViewModel messageInfo = lineGroup_repo.GetMessageInfoByGroupOrderId(groupOrderId);
            List<NameAndQtyViewModel> customers = lineCustomer_repo.GetCustomersByGroupOrderId(groupOrderId);
            string message = $"活動名稱:{messageInfo.Title}\n商品名稱:{messageInfo.ProductName}\n商品已經到店囉!\n請一個禮拜內盡快來門市取貨!\n";
            groupId = messageInfo.LineGroupId;

            foreach (var customer in customers.GroupBy(info => info.Name)
                        .Select(group => new
                        {
                            Name = group.Key,
                            Count = customers.Where(x => x.Name == group.Key).Sum(x => x.Quantity)
                        }))
            {
                message += $"@{customer.Name} : {customer.Count} 組\n";
            }

            return message;
        }
    }
}