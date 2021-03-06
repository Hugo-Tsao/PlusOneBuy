﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FBPlusOneBuy.DBModels;
using FBPlusOneBuy.ViewModels;
using Microsoft.AspNet.Identity;

namespace FBPlusOneBuy.Repositories
{
    public class LivePostsRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public bool Create(LivePost livePost)
        {
            using (conn = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = "INSERT INTO LivePosts(LivePageID,LiveName,postTime,FanPageID) VALUES ( @LivePageID,@LiveName,@postTime,@FanPageID)";
                    conn.Execute(sql, new { livePost.LivePageID, livePost.LiveName, livePost.postTime, livePost.FanPageID });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public int Select(string livePageID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select ID from LivePosts where LivePageID=@LivePageID and endTime is null";
                int liveid = conn.QueryFirstOrDefault<int>(sql, new { LivePageID = livePageID });
                return liveid;
            }
        }
        public List<int> SelectIds(string livePageID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select ID from LivePosts where LivePageID=@livePageID";
                List<int> liveids = conn.Query<int>(sql, new { livePageID }).ToList();
                return liveids;
            }
        }
        public List<CommentListViewModel> SelectComments(string livePageID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select o.OrderID,o.Keyword,o.Quantity,o.OrderDateTime,o.CustomerID,c.CustomerName,o.ProductID,p.ProductName,lps.LiveName,lps.postTime,lps.endTime from LivePosts lps inner join Orders o on lps.ID=o.LiveID inner join Customers c on o.CustomerID=c.CustomerID inner join Products p on o.ProductID=p.ProductID  where LivePageID=@livePageID";
                var query_result = conn.Query<CommentListViewModel>(sql, new { livePageID }).ToList();
                return query_result;
            }
        }
        public string SelectLiveName(string livePageID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select top 1 LiveName from LivePosts where LivePageID=@livePageID";
                string livename = conn.QueryFirstOrDefault<string>(sql, new { livePageID });
                return livename;
            }
        }
        public List<LivePostViewModel> Select()
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "Select distinct LiveName,LivePageID From LivePosts";
                var liveposts = conn.Query<LivePostViewModel>(sql).ToList();
                return liveposts;
            }
        }
        public void UpdatePost(string livePageID, int qtyOfOrders, decimal amount, DateTime endTime, int maxViews)
        {
            using (conn = new SqlConnection(connectionString))
            {
                var live_repo = new LivePostsRepository();
                int liveid = live_repo.Select(livePageID);
                string sql = "UPDATE LivePosts SET endTime=@endTime,QtyOfOrders=@qtyOfOrders,Amount=@amount,MaxViews=@maxViews WHERE ID=@liveid ";
                conn.Execute(sql, new { endTime, qtyOfOrders, amount, maxViews, liveid });

            }
        }
        public DateTime GetMaxPostTime(string livePageId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select MAX(postTime) as postTime from LivePosts where LivePageID = @livePageId";
                DateTime postTime = conn.Query<DateTime>(sql, new { livePageId }).FirstOrDefault();
                return postTime;
            }
        }

        public List<ReportViewModel> GetAllLivePosts()
        {
            using (conn = new SqlConnection(connectionString))
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                string sql =
                    "SELECT l.ID, l.LiveName, l.postTime, l.endTime, l.LivePageID, l.Amount, l.QtyOfOrders,f.FanPageName FROM AspNetUsers a INNER JOIN FanPages f ON a.Id = f.AspNetUserId INNER JOIN LivePosts l ON f.ID = l.FanPageID WHERE a.Id = '" + userId + "' ORDER BY l.ID DESC";
                var result = conn.Query<ReportViewModel>(sql).ToList();
                return result;
            }
        }
        public ReportViewModel GetLivePost(int liveId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT ID, LiveName, postTime, endTime, LivePageID, Amount,  QtyOfOrders,MaxViews FROM LivePosts WHERE ID = @liveId";
                var result = conn.QueryFirstOrDefault<ReportViewModel>(sql, new { liveId });
                return result;
            }
        }

        public SalesOrderList SaleOrder(int liveId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT SUM(Quantity) as Quantity,  SUM(Total) as Total FROM SalesOrders WHERE LiveID = @liveId";
                var result = conn.QueryFirstOrDefault<SalesOrderList>(sql, new { liveId });
                return result;
            }
        }
    }
}