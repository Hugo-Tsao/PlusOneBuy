using Dapper;
using FBPlusOneBuy.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Repositories
{
    public class LineGroupRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public int GetMangerIdByAspNetId(string aspNetUserId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT StoreManagerID FROM StoreManager WHERE AspNetUserId=@aspNetUserId";              
                return conn.QueryFirstOrDefault<int>(sql, new { aspNetUserId });
            }
        }
        public void InsertGroupName(int managerId,string groupName)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO LineGroup(StoreManagerID, GroupName) VALUES ( @managerId, @GroupName)";
                conn.Execute(sql, new { managerId, groupName });
            }
        }
        //中台已成團
        public List<LineGroup> GetGroupList(int managerId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT GroupName,JoinDate,LineGroupID FROM LineGroup WHERE StoreManagerID=@managerId AND Status = 'TRUE'";
                return conn.Query<LineGroup>(sql, new { managerId }).ToList();
            }
        }
        //中台未成團
        public List<string> GetNullGroup(int managerId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT GroupName FROM LineGroup WHERE StoreManagerID=@managerId AND Status IS NULL";
                return conn.Query<string>(sql,new { managerId }).ToList();
            }
        }
        //LineGroupNull比對
        public List<CompareStoreManager> GroupNullCompare()
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT LineID,G.StoreManagerID FROM LineGroup AS G INNER JOIN StoreManager AS M ON M.StoreManagerID=G.StoreManagerID WHERE LineGroupID IS NULL ";
                return conn.Query<CompareStoreManager>(sql).ToList();
            }
        }
        //比對到修改資料庫
        public void CompareUpdateGroupid(string groupId,int storeManagerId,DateTime? timestampTotime)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE LineGroup SET LineGroupID=@groupId,JoinDate=@timestampTotime,Status='TRUE' WHERE StoreManagerID=@storeManagerId";
                conn.Execute(sql,new { groupId, storeManagerId, timestampTotime });
            }
        }
   
        


    }
}