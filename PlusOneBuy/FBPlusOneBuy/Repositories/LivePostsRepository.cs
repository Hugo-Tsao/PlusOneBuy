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
                List<int> liveids = conn.Query<int>(sql, new {livePageID }).ToList();
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
        public void UpdatePost(string livePageID, int qtyOfOrders, decimal amount, DateTime endTime)
        {
            using (conn = new SqlConnection(connectionString))
            {
                var live_repo = new LivePostsRepository();
                int liveid = live_repo.Select(livePageID);
                string sql = "UPDATE LivePosts SET endTime=@endTime,QtyOfOrders=@qtyOfOrders,Amount=@amount WHERE ID=@liveid ";
                conn.Execute(sql, new { endTime, qtyOfOrders, amount, liveid });

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

        public List<LivePostViewModel> GetAllLivePosts()
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT ID, LiveName, postTime, endTime, LivePageID, Amount,  QtyOfOrders FROM LivePosts";
                List<LivePostViewModel> result = conn.Query<LivePostViewModel>(sql).ToList();
                return result;
            }
        }
    }
}