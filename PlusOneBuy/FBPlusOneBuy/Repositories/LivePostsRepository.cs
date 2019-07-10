using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FBPlusOneBuy.Models;

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