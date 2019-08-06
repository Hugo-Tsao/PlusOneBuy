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
        //public static StoreMeanger CheckMeanger(string groupId, string managerUserId)
        //{

        //    var groupUrl = "https://api.line.me/v2/bot/group/" + groupId + "/member/" + managerUserId;
        //    var client = new RestClient(groupUrl);
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("Connection", "keep-alive");
        //    request.AddHeader("Accept-Encoding", "gzip, deflate");
        //    request.AddHeader("Host", "api.line.me");
        //    request.AddHeader("Cache-Control", "no-cache");
        //    request.AddHeader("Accept", "*/*");
        //    request.AddHeader("Authorization", string.Format("Bearer " + channelAccessToken));
        //    IRestResponse response = client.Execute(request);
        //    StoreMeanger StoreMeanger = Newtonsoft.Json.JsonConvert.DeserializeObject<StoreMeanger>(response.Content);
        //    return StoreMeanger;
        //}
        public static bool SearchLineCustomer(string customerId,string customerName,ref LineCustomerViewModel lineCustomer)
        {
            LineCustomerRepository LineCustomer_repo = new LineCustomerRepository();
            var checkCustomer = LineCustomer_repo.SearchLineCustomer(customerId);
            if (checkCustomer != default)
            {
                lineCustomer = checkCustomer;
                return true;
            }
            return false;
        }

        public static DateTime TimestampToDateTime(long timestamp)
        {
            //double millionSecond = Convert.ToDouble(timestamp);
            //DateTime timestampTotime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //DateTime result = timestampTotime.AddMilliseconds(millionSecond).AddHours(8).ToLocalTime();
            DateTime result = (new DateTime(1970, 1, 1, 0, 0, 0)).AddHours(8).AddSeconds(timestamp);
            return result;
        }
        public static LineCustomerViewModel InsertLineCustomer(string customerId, string customerName)
        {
            LineCustomerRepository LineCustomer_repo = new LineCustomerRepository();
            LineCustomer_repo.InsertCustomer(customerId,customerName);
            return new LineCustomerViewModel() {LineCustomerID = customerId, Name = customerName};
        }
        public static void CheckGroupOrder(int campaignID, DateTime orderDateTime, string userId, int productId, int qty)
        {
            //GroupOrderRespository GroupOrder_repo = new GroupOrderRespository();
            //Product product = GroupOrder_repo.GetProductById(productId);

            //判斷GroupOrder是否有
            //int checkGroupOrder =GroupOrder_repo.GetGroupOrderById(campaignID);


            //if (checkGroupOrder == 0 )
            //{
            //    //新增GroupOrder
            //    decimal? amount = product.UnitPrice * qty;
            //    GroupOrder_repo.InsertGroupOrder(campaignID, orderDateTime, amount);
            //}

            ////新增GroupOrderDetail並判斷顧客是否留言過
            //int GroupOrderID = GroupOrder_repo.GetGroupOrderById(campaignID);
            //var CheckDetailCustomer = GroupOrder_repo.CheckOrderDetailByCustomer(GroupOrderID, userId);

            //if (CheckDetailCustomer == true)
            //{   //修改顧客GroupOrderDetail數量        
            //    GroupOrder_repo.UpdateOrderDetailByCustomer(GroupOrderID, userId, qty);

            //}
            //else
            //{
            //    //新增GroupOrderDetail
            //    var OrderDetail = new GroupOrderDetail()
            //    {
            //        GroupOrderID = GroupOrderID,
            //        LineCustomerID = userId,
            //        ProductName = product.ProductName,
            //        UnitPrice = product.UnitPrice,
            //        Quantity = qty,
            //        MessageDateTime = orderDateTime
            //    };
            //GroupOrder_repo.InsertGroupOrderDetail(OrderDetail);
            //var checkisGroup = GroupOrder_repo.CheckisGroup(campaignID);
            //if (checkisGroup == 0)
            //{
            //    //修改grouporder isgroup 狀態
            //    GroupOrder_repo.UpdateisGroup(campaignID);
            //}

            //}

            ////修改GroupOrder(AmountQty) 
            //var AmountQty = GroupOrder_repo.GETGroupOrderAmountQty(GroupOrderID);
            //GroupOrder_repo.UpdateGroupOrder(GroupOrderID, AmountQty.NumberOfPeople, AmountQty.Amount);

        }
    }
}