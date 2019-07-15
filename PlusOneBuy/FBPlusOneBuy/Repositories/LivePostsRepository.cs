using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.ViewModels;

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
                    conn.Execute(sql, new { livePost.LivePageID, livePost.LiveName,livePost.postTime, livePost.FanPageID });
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
        public List<CommentListViewModel> SelectComments(string livePageID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select o.OrderID,o.Keyword,o.Quantity,o.OrderDateTime,o.CustomerID,c.CustomerName,o.ProductID,p.ProductName,lps.LiveName,lps.postTime,lps.endTime from LivePosts lps inner join Orders o on lps.ID=o.LiveID inner join Customers c on o.CustomerID=c.CustomerID inner join Products p on o.ProductID=p.ProductID  where LivePageID=@livePageID";
                var query_result = conn.Query<CommentListViewModel>(sql, new { LivePageID = livePageID }).ToList();
                return query_result;
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
        public void UpdateEndTime(int liveid ,DateTime endTime)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE LivePosts SET endTime=@endTime WHERE ID=@liveid; ";
                conn.Execute(sql, new { endTime, liveid });

            }
        }
    }
}