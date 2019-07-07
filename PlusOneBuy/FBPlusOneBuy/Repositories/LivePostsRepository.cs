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
                    string sql = "INSERT INTO LivePosts(LiveID,LiveName,FanPageID) VALUES ( @LiveID,@LiveName,@FanPageID)";
                    conn.Execute(sql, new { livePost.LiveID, livePost.LiveName, livePost.FanPageID });
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}