using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;
using FBPlusOneBuy.Services;

namespace FBPlusOneBuy.Services
{
    public static class CommentFilterService
    {
        public static List<OrderList> KeywordFilter(List<ProductViewModel> products, List<Datum> datas, string livePageID, string keywordPattern)
        {
            var resultOrderList = new List<OrderList>();
            var cust_repo = new CustomerRepository();
            Regex re;
            
            foreach (var data in datas)
            {
                if (data.from.name == null)
                {
                    continue;
                }
                var name = UTF8ConvertToString(data.from.name);
                if (!cust_repo.SelectCustomer(data.from.id))
                {
                    var customer = new Customer { CustomerID = data.from.id, CustomerName = name };
                    cust_repo.InsertCustomer(customer);
                }

                foreach (var item in products)
                {
                    string completeKeyword = item.Keyword + keywordPattern;
                    re = new Regex(item.Keyword + keywordPattern);
                    if (keywordPattern == "+1")
                    {
                        string pattern = "^" + item.Keyword + "\\+\\d{1}$";
                        re = new Regex(pattern);
                    }
                    if (re.IsMatch(data.message))
                    {
                        var live_repo = new LivePostsRepository();
                        var order = new OrderList();
                        order.CustomerID = data.from.id;
                        order.CustomerName = name;
                        order.Keyword = data.message;
                        order.Product = new ProductViewModel
                        {
                            Salepage_id = item.Salepage_id,
                            SkuId = item.SkuId,
                            ProductName = item.ProductName,
                            UnitPrice = item.UnitPrice
                        };
                        order.LiveID = live_repo.Select(livePageID);
                        order.OrderDateTime = data.created_time.ToUniversalTime().AddHours(8);
                        order.Quantity = 1;
                        if (keywordPattern == "+1")
                        {
                            order.Quantity =
                                int.Parse(data.message.Substring(data.message.IndexOf("+", StringComparison.Ordinal)));
                        }
                        resultOrderList.Add(order);
                        break;
                    }
                }
            }
            return resultOrderList;
        }

        public static List<Datum> PostTimeFilter(List<Datum> comments, string livePageID)
        {

            LivePostsRepository livePost_repo = new LivePostsRepository();
            var livePostTime = livePost_repo.GetMaxPostTime(livePageID);
            //去Orders Table 看有沒有留言，有的話就抓最後一個留言的時間沒有的話就過濾livePostTime 的時間
            var order_repo = new OrderRepositories();
            var live_repo = new LivePostsRepository();
            int liveid = live_repo.Select(livePageID);
            DateTime selectResult = order_repo.SelectLastOrderComment(liveid); //SQL 要改
            if (selectResult != DateTime.MaxValue)
            {
                comments = comments.Where(x => DateTime.Compare(x.created_time.ToUniversalTime().AddHours(8), selectResult) > 0).ToList();
            }
            else
            {
                comments = comments.Where(x => DateTime.Compare(x.created_time.ToUniversalTime().AddHours(8), livePostTime) > 0).ToList();
            }

            return comments;
        }

        public static List<OrderList> getNewOrderList(string livePageID, string token, List<ProductViewModel> products, string keywordPattern)
        {
            var orderList = new List<OrderList>();
            var allComments = FBRequestService.getAllComments(livePageID, token);
            if (allComments.Count != 0)
            {
                //過濾出經由推播後的時間開始才喊關鍵字的人
                var PickPosts = CommentFilterService.PostTimeFilter(allComments, livePageID);
                if (PickPosts.Count > 0)
                {
                    orderList = CommentFilterService.KeywordFilter(products, PickPosts, livePageID, keywordPattern);
                }
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