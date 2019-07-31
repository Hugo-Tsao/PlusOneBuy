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
    public class CompaignRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public IEnumerable<Compaign> GetALL()
        {
            using (conn=new SqlConnection(connectionString))
            {
                string url = "SELECT * FROM Compaign";
                return conn.Query<Compaign>(url);
            }
        }
        public bool InsertGroupBuy(CompaignViewModel cvm)
        {
            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    string sql =
                        "INSERT INTO Compaign(CompaignID,GroupID,ProductID,ProductSet,PeopleGroup,Keyword,PostTime,EndTime,Detail) VALUES(@CompaignID, @GroupID, @ProductID, @ProductSet, @PeopleGroup, @Keyword, @PostTime, @EndTime, @Detail)";
                    conn.Execute(sql, new { cvm.CompaignID, cvm.GroupID, cvm.ProductID, cvm.ProductSet, cvm.PeopleGroup, cvm.Keyword, cvm.PostTime , cvm.EndTime, cvm.Detail });
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