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
        public IEnumerable<Campaign> GetALL()
        {
            using (conn=new SqlConnection(connectionString))
            {
                string url = "SELECT CampaignID,GroupID,ProductID,ProductSet,PeopleGroup,Keyword,PostTime,EndTime,Detail FROM Campaign";
                return conn.Query<Campaign>(url);
            }
        }
        public bool InsertGroupBuy(CampaignViewModel cvm)
        {
            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    cvm.GroupID = "Cdce46b42293efcd6ff973d08be1e0642"; //群組ID暫時只有一個所以先寫死來DEMO
                    string sql =
                        "INSERT INTO Campaign(GroupID,ProductID,ProductSet,PeopleGroup,Keyword,PostTime,EndTime,Detail) VALUES(@GroupID, @ProductID, @ProductSet, @PeopleGroup, @Keyword, @PostTime, @EndTime, @Detail)";
                    conn.Execute(sql, new { cvm.GroupID, cvm.ProductID, cvm.ProductSet, cvm.PeopleGroup, cvm.Keyword, cvm.PostTime , cvm.EndTime, cvm.Detail });
                    return true;
                }
            }
            catch (Exception e)
            {

                return false;
            }
            
        }
    }
}