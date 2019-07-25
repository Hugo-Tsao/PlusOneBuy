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
    public class FanPagesRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public bool isExist(string fanpageid)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select * from FanPages where FanPageID=@fanpageid";
                var fanpage = conn.QueryFirstOrDefault<FanPage>(sql, new { fanpageid });
                if (fanpage!=null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public void Insert(string fanpageid, string fanpagename)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO FanPages(FanPageID, FanPageName) VALUES ( @fpID, @fpName)";
                conn.Execute(sql, new { fpID= fanpageid, fpName= fanpagename });
            }
        }
    }
}