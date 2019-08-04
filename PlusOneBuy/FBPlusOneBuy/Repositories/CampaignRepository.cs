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
                    "SELECT CampaignID,GroupID,ProductID,ProductSet,PeopleGroup,Keyword,PostTime,EndTime,Detail FROM Campaign c INNER JOIN LineGroup lg ON c.GroupID = lg.ID WHERE lg.LineGroupID = @GroupID";
                return conn.Query<Campaign>(url, new { GroupID });
            }
        }

        public void InsertGroupBuy(CampaignViewModel cvm)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql =
                    "INSERT INTO Campaign(GroupID,ProductID,ProductSet,PeopleGroup,Keyword,PostTime,EndTime,Detail) VALUES(@GroupID, @ProductID, @ProductSet, @PeopleGroup, @Keyword, @PostTime, @EndTime, @Detail)";
                conn.Execute(sql,
                    new
                    {
                        cvm.GroupID,
                        cvm.ProductID,
                        cvm.ProductSet,
                        cvm.PeopleGroup,
                        cvm.Keyword,
                        cvm.PostTime,
                        cvm.EndTime,
                        cvm.Detail
                    });
            }
        }
    }
}