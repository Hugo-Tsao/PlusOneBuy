using Dapper;
using FBPlusOneBuy.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using FBPlusOneBuy.DBModels;
using FBPlusOneBuy.ViewModels;


namespace FBPlusOneBuy.Repositories
{
    public class LineGroupRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public string SelectLine(string LineID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT StoreManagerID FROM StoreManager WHERE AspNetUserId=@aspNetUserId AND Status='True'";
                return conn.QueryFirstOrDefault<string>(sql, new { LineID });
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
                string sql = "SELECT GroupID,GroupName,JoinDate,LineGroupID FROM LineGroup WHERE StoreManagerID=@managerId AND Status = 'TRUE'";
                return conn.Query<LineGroup>(sql, new { managerId }).ToList();
            }
        }
        //中台未成團
        public List<LineGroup> GetNullGroup(int managerId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT GroupID,GroupName FROM LineGroup WHERE StoreManagerID=@managerId AND Status IS NULL";
                return conn.Query<LineGroup>(sql,new { managerId }).ToList();
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
                string sql = "UPDATE LineGroup SET LineGroupID=@groupId,JoinDate=@timestampTotime,Status='TRUE' WHERE StoreManagerID=@storeManagerId AND LineGroupID IS NULL";
                conn.Execute(sql,new { groupId, storeManagerId, timestampTotime });
            }
        }
   
        public LineGroup SearchLineGroup(string groupId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM LineGroup WHERE LineGroupID = @groupId";
                var LineGroup = conn.QueryFirstOrDefault<LineGroup>(sql, new {groupId});
                return LineGroup;
            }
        }

        public int GetIdByGroupId(string groupId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT GroupID FROM LineGroup WHERE LineGroupID='"+ groupId + "'";
                return conn.QueryFirstOrDefault<int>(sql);
            }
        }

        public SendMessageViewModel GetMessageInfoByGroupOrderId(int groupOrderId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT lg.LineGroupID,p.ProductName,c.Title,c.PostTime,c.EndTime FROM LineGroup lg INNER JOIN Campaign c ON c.GroupID = lg.GroupID INNER JOIN GroupOrder g ON g.CampaignID = c.CampaignID INNER JOIN Products p ON p.ProductID = c.ProductID WHERE g.GroupOrderID = @GroupOrderId";
                SendMessageViewModel result = conn.QueryFirstOrDefault<SendMessageViewModel>(sql,new { GroupOrderId = groupOrderId });
                return result;
            }
        }
        public void DelNullGroup(int groupId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM LineGroup WHERE GroupID=@GroupID";
                conn.Execute(sql, new { groupId});
            }
        }
        public void UpdateGroupStatus(int groupId,string status)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE LineGroup SET Status='"+ status + "' WHERE GroupID=@GroupID";
                conn.Execute(sql, new { groupId });
            }
        }
        public void removeManager(int StoreManagerID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE StoreManager SET Status='FALSE' WHERE StoreManagerID=@StoreManagerID";
                conn.Execute(sql, new { StoreManagerID });
            }
        }
        public void removeManagerGroup(int StoreManagerID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE LineGroup SET Status='FALSE' WHERE StoreManagerID=@StoreManagerID";
                conn.Execute(sql, new { StoreManagerID });
            }
        }

    }
}