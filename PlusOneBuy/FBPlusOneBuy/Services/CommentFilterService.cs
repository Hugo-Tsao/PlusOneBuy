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
        public static List<OrderList> KeywordFilter(string keywords,List<Datum> datas,string liveID)
        {
            var result = new List<OrderList>();
            //List<Datum> result = new List<Datum>();
            foreach (var data in datas)
            {
                //if (keywords.IndexOf(data.message + "+1") != -1)  //+1暫時先寫死
                if(data.message.Contains(keywords))
                {
                    var context = new Context();
                    var order = new OrderList();

                    var name = UTF8ConvertToString(data.from.name);
                    order.CustomerName = name;
                    order.Keyword = data.message;
                    order.ProductName = "馬桶泡泡洗"; //暫時寫死，之後用搜索
                    order.LiveID = liveID;
                    order.OrderDateTime = DateTime.Now;
                    order.Quantity = 1; //暫時寫死
                    result.Add(order);
                }
            }
            return result;
        }

        public static List<Datum> PostTimeFilter(List<Datum> comments)
        {
            Context context = new Context();
            var livePostTime = context.LivePosts.Max(x => x.postTime);
            comments = comments.Where(x => x.created_time > livePostTime).ToList();
            return comments;
        }

        public static List<OrderList> getNewOrderList(string liveID, string token, string keywords)
        {
            var orderList = new List<OrderList>();
            var allComments = FBRequestService.getAllComments(liveID, token);
            if (allComments.Count != 0)
            {
                //過濾出經由推播後的時間開始才喊關鍵字的人
                var PickPosts = CommentFilterService.PostTimeFilter(allComments);
                if (PickPosts.Count > 0)
                {
                    orderList = CommentFilterService.KeywordFilter(keywords, PickPosts, liveID);
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