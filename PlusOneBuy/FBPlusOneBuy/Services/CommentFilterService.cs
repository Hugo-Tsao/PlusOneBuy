using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;
using FBPlusOneBuy.Services;

namespace FBPlusOneBuy.Services
{
    public static class CommentFilterService
    {
        public static List<OrderList> KeywordFilter(List<ProductViewModel> products,List<Datum> datas,string livePageID,string keywordPattern)
        {
            var resultOrderList = new List<OrderList>();
            var custsList = new List<Customer>();
            var cust_repo = new CustomerRepository();
            //List<Datum> resultDatum = new List<Datum>();
            if (keywordPattern == null)
            {
                keywordPattern = "+1";
            }
            foreach (var data in datas)
            {
                foreach (var item in products)
                {
                    string completeKeyword = item.Keyword + keywordPattern;
                    if (data.message == completeKeyword)  
                    {
                        //resultDatum.Add(data);
                        var context = new Context();
                        var order = new OrderList();
                        order.Product = new ProductViewModel();
                        var live_repo = new LivePostsRepository();

                        if (data.from.name == null)
                        {
                            break;
                        }
                        var name = UTF8ConvertToString(data.from.name);
                        if (!cust_repo.SelectCustomer(data.from.id))
                        {
                            var customer = new Customer { CustomerID = data.from.id, CustomerName = name };
                            custsList.Add(customer);
                        }
                        order.CustomerID = data.from.id;
                        order.CustomerName = name;
                        order.Keyword = data.message;
                        order.Product.Salepage_id = item.Salepage_id;
                        order.Product.SkuId = item.SkuId;
                        order.Product.ProductName = item.ProductName;
                        order.Product.UnitPrice = item.UnitPrice;
                        order.LiveID = live_repo.Select(livePageID);
                        order.OrderDateTime = data.created_time;
                        order.Quantity = 1; //因為目前只有+1，所以暫時寫死
                        resultOrderList.Add(order);
                        break;
                    }
                }
            }
            cust_repo.InsertCustomer(custsList);
            return resultOrderList;
        }

        public static List<Datum> PostTimeFilter(List<Datum> comments, string livePageID)
        {
            //Context context = new Context();
            //var livePostTime = context.LivePosts.Max(x => x.postTime);
            LivePostsRepository livePost_repo = new LivePostsRepository();
            var livePostTime = livePost_repo.GetMaxPostTime(livePageID);
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

        public static List<OrderList> getNewOrderList(string livePageID, string token, List<ProductViewModel> products,string keywordPattern)
        {
            var orderList = new List<OrderList>();            
            var allComments = FBRequestService.getAllComments(livePageID, token);
            if (allComments.Count != 0)
            {
                //過濾出經由推播後的時間開始才喊關鍵字的人
                //var PickPosts = CommentFilterService.PostTimeFilter(allComments, livePageID);
                //if (PickPosts.Count > 0)
                //{
                    orderList = CommentFilterService.KeywordFilter(products, allComments, livePageID,keywordPattern);
                    var order_repo = new OrderRepositories();
                    order_repo.InsertOrder(orderList);                   
                //}
            }
            return orderList;
        }
       


        public static string UTF8ConvertToString(string word)
        {
            if (word == null)
            {
                return null;
            }
            else
            {
                byte[] byt = System.Text.UnicodeEncoding.Unicode.GetBytes(word);
                string result = UnicodeEncoding.Unicode.GetString(byt);
                return result;
            }
        }
    }
}