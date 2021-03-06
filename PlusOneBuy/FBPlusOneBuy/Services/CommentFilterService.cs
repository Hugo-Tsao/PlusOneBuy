﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using FBPlusOneBuy.DBModels;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;
using FBPlusOneBuy.Services;

namespace FBPlusOneBuy.Services
{
    public static class CommentFilterService
    {
        public static List<OrderList> KeywordCheck(List<ProductViewModel> products, List<Datum> datas, string livePageID, string keywordPattern)
        {
            var resultOrderList = new List<OrderList>();
            var cust_repo = new CustomerRepository();
            
            foreach (var data in datas)
            {
                if (data.from == null)
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
                    if (KeywordFilter(data.message,item.Keyword,keywordPattern) != 0)
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

        public static int KeywordFilter(string message,string keyword,string keywordPattern)
        {
            string completeKeyword = keyword + keywordPattern;
            Regex re = new Regex(keyword + keywordPattern);
            if (keywordPattern == "+1")
            {
                string pattern = "^" + keyword + "\\s?\\+[1-9][0-9]*\\s*$";
                re = new Regex(pattern);
            }

            if (re.IsMatch(message))
            {
                int number = Int32.Parse(message.Substring(message.Trim().IndexOf("+")+1));
                return number;
            }

            return 0;
        }
        public static List<Datum> PostTimeFilter(List<Datum> comments, string livePageID)
        {
            LivePostsRepository livePost_repo = new LivePostsRepository();
            var livePostTime = livePost_repo.GetMaxPostTime(livePageID);
            //去Orders Table 看有沒有留言，有的話就抓最後一個留言的時間沒有的話就過濾livePostTime 的時間
            var order_repo = new OrderRepositories();
            var live_repo = new LivePostsRepository();
            int liveid = live_repo.Select(livePageID);
            //DateTime selectResult = order_repo.SelectLastOrderComment(liveid); //SQL 要改
            if (HttpContext.Current.Session["lastPostTime"] != null)
            {
                DateTime selectResult = (DateTime)HttpContext.Current.Session["lastPostTime"];
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
                    orderList = CommentFilterService.KeywordCheck(products, PickPosts, livePageID, keywordPattern);
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