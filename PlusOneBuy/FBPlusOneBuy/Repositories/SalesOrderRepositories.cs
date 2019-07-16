using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using FBPlusOneBuy.ViewModels;
using FBPlusOneBuy.Models;

namespace FBPlusOneBuy.Repositories
{
    public class SalesOrderRepositories
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public List<SalesOrder> Select(int liveId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select * from SalesOrders where LiveID=@liveId";
                var query_result = conn.Query<SalesOrder>(sql, new { liveId }).ToList();
                return query_result;
            }
        }
    }
}