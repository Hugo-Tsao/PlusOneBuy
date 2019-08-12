using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using FBPlusOneBuy.DBModels;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Repositories
{
    public class SalesOrderRepositories
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public List<SalesOrderList> Select(List<int> liveIds)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select so.SalesOrderID,so.CustomerID,so.ProductID,so.LiveID,so.Quantity,so.Total,so.CheckoutDateTime,c.CustomerName,p.ProductName from SalesOrders so inner join Products p on so.ProductID=p.ProductID inner join Customers c on so.CustomerID=c.CustomerID where LiveID IN @liveIds";
                var query_result = conn.Query<SalesOrderList>(sql, new { liveIds }).ToList();
                return query_result;
            }
        }
        public List<SalesOrder> Select(int liveId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select * from SalesOrders where LiveID =@liveId";
                var query_result = conn.Query<SalesOrder>(sql, new { liveId }).ToList();
                return query_result;
            }
        }
    }
}