using Dapper;
using FBPlusOneBuy.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Repositories
{
    public class StoreManagerRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public void Insert(LineProfile profile)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string userid= HttpContext.Current.User.Identity.GetUserId();
                string sql = "INSERT INTO StoreManager(LineID, AspNetUserId,Name,PictureUrl,Status) VALUES ( @LineID, @AspNetUserId,@Name,@PictureUrl,@Status)";
                conn.Execute(sql, new { LineID = profile.userId, AspNetUserId = userid, Name=profile.displayName, PictureUrl=profile.pictureUrl, Status=true });
            }
        }
        public BindAcoountStoreManagerViewModel SelectBinding(string aspUserId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select StoreManagerID,LineID,Name,PictureUrl,Status from StoreManager where AspNetUserId=@aspUserId AND Status='True'";
                var profile = conn.QueryFirstOrDefault<BindAcoountStoreManagerViewModel>(sql, new { aspUserId });
                return profile;
            }

        }
        public int SearchLineID(string LineID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT StoreManagerID FROM StoreManager WHERE LineID=@LineID";
                return conn.QueryFirstOrDefault<int>(sql, new { LineID });
            }
        }
        public int GetMangerIdByAspNetId(string aspNetUserId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT StoreManagerID FROM StoreManager WHERE AspNetUserId=@aspNetUserId AND Status='True'";
                return conn.QueryFirstOrDefault<int>(sql, new { aspNetUserId });
            }
        }
        public void UpdateManagerStatus(int StoreManagerID, string status)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE StoreManager SET Status='" + status + "' WHERE StoreManagerID=@StoreManagerID";
                conn.Execute(sql, new { StoreManagerID });
            }
        }

    }
}