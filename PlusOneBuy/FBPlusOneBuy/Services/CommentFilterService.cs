using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Services
{
    public static class CommentFilterService
    {
        public static List<OrderList> KeywordFilter(List<string> keywords,List<Datum> datas,string liveID)
        {
            var result = new List<OrderList>();
            //List<Datum> result = new List<Datum>();
            foreach (var data in datas)
            {
                if (keywords.IndexOf(data.message + "+1") != -1)  //+1暫時先寫死
                {
                    var context = new Context();
                    var order = new OrderList();
                    var name = FBRequestService.UTF8ConvertToString(data.from.name);
                    order.CustomerName = name;
                    order.Keyword = data.message;
                    //order.ProductName = ProductName; //暫時寫死，之後用搜索
                    order.LiveID = liveID;
                    order.OrderDateTime = DateTime.Now;
                    order.Quantity = 1; //暫時寫死
                    result.Add(order);
                }
            }
            return result;
        }
    }
}