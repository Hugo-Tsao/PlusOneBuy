using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Services
{
    public static class CommentFilterService
    {
        public static List<OrderList> KeywordFilter(string keywords,List<Datum> datas,string livePageID)
        {
            var resultOrderList = new List<OrderList>();
            var custsList = new List<Customer>();
            var cust_repo = new CustomerRepository();
            //List<Datum> resultDatum = new List<Datum>();
            foreach (var data in datas)
            {
                //if (keywords.IndexOf(data.message + "+1") != -1)  //+1暫時先寫死
                if(data.message.Contains(keywords))
                {
                    //resultDatum.Add(data);
                    
                    
                    var context = new Context();
                    var order = new OrderList();
                    var live_repo = new LivePostsRepository();

                    var name = UTF8ConvertToString(data.from.name);
                    if (!cust_repo.SelectCustomer(data.from.id))
                    {
                        var customer = new Customer { CustomerID = data.from.id, CustomerName = name };
                        custsList.Add(customer);
                    }
                    order.CustomerID =data.from.id;
                    order.CustomerName = name;
                    order.Keyword = data.message;
                    order.ProductName = "馬桶泡泡洗"; //暫時寫死，之後用搜索
                    order.LiveID = live_repo.Select(livePageID); //需要把end_time 判斷補上
                    order.OrderDateTime = Convert.ToDateTime(data.created_time);
                    order.Quantity = 1; //暫時寫死
                    resultOrderList.Add(order);

                }
            }
            cust_repo.InsertCustomer(custsList);
            return resultOrderList;
        }

        public static List<Datum> PostTimeFilter(List<Datum> comments, string livePageID)
        {
            Context context = new Context();
            var livePostTime = context.LivePosts.Max(x => x.postTime);
            //comments = comments.Where(x => x.created_time > livePostTime).ToList();
            //去Orders Table 看有沒有留言，有的話就抓最後一個留言的時間沒有的話就過濾livePostTime 的時間
            var order_repo = new OrderRepositories();
            var live_repo = new LivePostsRepository();
            int liveid = live_repo.Select(livePageID);
            DateTime selectResult = order_repo.SelectLastOrderComment(liveid); //SQL 要改
            if (selectResult != DateTime.MaxValue)
            {
                comments = comments.Where(x => x.created_time > selectResult).ToList();
            }
            else
            {
                comments = comments.Where(x => x.created_time > livePostTime).ToList();
            }

            return comments;
        }

        public static List<OrderList> getNewOrderList(string livePageID, string token, string keywords)
        {
            var orderList = new List<OrderList>();            
            var allComments = FBRequestService.getAllComments(livePageID, token);
            if (allComments.Count != 0)
            {
                //過濾出經由推播後的時間開始才喊關鍵字的人
                var PickPosts = CommentFilterService.PostTimeFilter(allComments, livePageID);
                if (PickPosts.Count > 0)
                {
                    orderList = CommentFilterService.KeywordFilter(keywords, PickPosts, livePageID);
                    var order_repo = new OrderRepositories();
                    order_repo.InsertOrder(orderList);
                }
            }
            return orderList;
        }

        public static string UTF8ConvertToString(string word)
        {
            byte[] byt = System.Text.UnicodeEncoding.Unicode.GetBytes(word);
            string result = UnicodeEncoding.Unicode.GetString(byt);
            return result;
        }
    }
}