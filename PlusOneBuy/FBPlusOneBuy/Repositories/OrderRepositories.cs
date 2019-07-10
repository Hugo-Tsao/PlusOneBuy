using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Repositories
{
    public class OrderRepositories
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public IEnumerable<OrderList> GetAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var sql = @"SELECT o.OrderID,c.CustomerName,o.Keyword,p.ProductName,o.Quantity,o.OrderDateTime,o.LiveID FROM Orders o
                            INNER JOIN Customers c ON c.CustomerID =o.CustomerID
                            INNER JOIN Products p ON p.ProductID =o.ProductID";
            return connection.Query<OrderList>(sql);
        }
        public void InsertOrder(List<OrderList> orders)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string productId = "1";//目前寫死 還需要更改

                foreach (var order in orders)
                {
                    string sql = "INSERT INTO Orders(ProductID, CustomerID, Keyword, OrderDateTime, Quantity,LiveID) VALUES ( @ProductID, @CustomerID, @Keyword, @OrderDateTime, @Quantity,@LiveID)";
                    conn.Execute(sql, new { ProductID=productId, order.CustomerID, order.Keyword, order.OrderDateTime, order.Quantity,order.LiveID });
                }           

            }
        }
        public DateTime SelectLastOrderComment(int liveId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select top 1 * from Orders where LiveID=@liveId order by OrderDateTime desc ";
                var lastComment = conn.QueryFirstOrDefault<OrderList>(sql,new { liveId });
                if (lastComment == null)
                {
                    return DateTime.MaxValue;
                }
                else
                {
                    return lastComment.OrderDateTime;
                }
            }
        }

        public List<MsgTextViewModel> SelectAllOrdersInfo(int liveId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select o.OrderID,o.CustomerID,c.CustomerName,o.ProductID,p.ProductName,p.ProductPageID,o.Quantity,o.LiveID from Orders o inner join Products p on o.ProductID=p.ProductID inner join Customers c on o.CustomerID=c.CustomerID where o.LiveID=@liveId";
                var orders = conn.Query<MsgTextViewModel>(sql, new { liveId });
                return orders.ToList();
            }
        }
    }
}