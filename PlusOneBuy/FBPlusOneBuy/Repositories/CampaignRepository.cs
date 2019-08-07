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
    public class CampaignRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public IEnumerable<Campaign> GetALL(string GroupID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string url =
                    "SELECT c.CampaignID,c.GroupID,c.ProductID,c.ProductSet,c.ProductGroup,c.Keyword,c.PostTime,c.EndTime,c.Detail FROM Campaign c INNER JOIN LineGroup lg ON c.GroupID = lg.GroupID WHERE lg.LineGroupID = @GroupID";
                return conn.Query<Campaign>(url, new { GroupID });
            }
        }

        public void InsertGroupBuy(CampaignViewModel cvm)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql =
                    "INSERT INTO Campaign(GroupID,ProductID,ProductSet,ProductGroup,Keyword,PostTime,EndTime,Detail,Title) VALUES(@GroupID, @ProductID, @ProductSet, @ProductGroup, @Keyword, @PostTime, @EndTime, @Detail,@Title)";
                conn.Execute(sql,
                    new
                    {
                        cvm.GroupID,
                        cvm.ProductID,
                        cvm.ProductSet,
                        cvm.ProductGroup,
                        cvm.Keyword,
                        cvm.PostTime,
                        cvm.EndTime,
                        cvm.Detail,
                        cvm.Title
                    });
            }
        }

        public int SelectStoreManagerID(string userId)
        {
            using (conn=new SqlConnection(connectionString))
            {
                string sql = "SELECT StoreManagerID FROM StoreManager WHERE AspNetUserId=@AspNetUserId and Status='True';";
                int storeManagerID = conn.QueryFirstOrDefault<int>(sql, new { AspNetUserId = userId });
                return storeManagerID;
            }
        }

        public List<LineListCampaignViewModel> SelectCampaigns(int storeManagerID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT c.CampaignID,l.GroupName,p.ProductName,c.Keyword,c.ProductGroup,c.ProductSet,c.PostTime,c.EndTime,c.Title FROM AspNetUsers u INNER JOIN StoreManager s ON u.Id = s.AspNetUserId INNER JOIN LineGroup l ON s.StoreManagerID = l.StoreManagerID INNER JOIN Campaign c ON l.GroupID = c.GroupID INNER JOIN Products p ON c.ProductID = p.ProductID Where s.StoreManagerID=@storeManagerID;";
                var campaigns = conn.Query<LineListCampaignViewModel>(sql, new { storeManagerID }).ToList();
                return campaigns;
            }
        }


    }
}