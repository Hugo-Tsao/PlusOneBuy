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
                
                foreach (var order in orders)
                {
                    string sql = "INSERT INTO Orders(ProductID, CustomerID, Keyword, OrderDateTime, Quantity,LiveID) VALUES ( @ProductID, @CustomerID, @Keyword, @OrderDateTime, @Quantity,@LiveID)";
                    conn.Execute(sql, new { ProductID=order.Product.SkuId, order.CustomerID, order.Keyword, order.OrderDateTime, order.Quantity,order.LiveID });
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

        public decimal GetAmount(string livePageId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                LivePostsRepository live_repo = new LivePostsRepository();
                var liveId = live_repo.Select(livePageId);
                string sql =
                    "select SUM(p.UnitPrice) as Amount from Products p inner join Orders o on o.ProductID = p.ProductID where o.LiveID = @liveId";
                 decimal? amount = conn.QueryFirstOrDefault<decimal?>(sql, new {liveId});
                 decimal result = 0;
                if (amount != null)
                {
                    result = (decimal)amount;
                }
                return result;
            }
        }

        public int GetQtyOfOrders(string livePageId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                LivePostsRepository live_repo = new LivePostsRepository();
                var liveId = live_repo.Select(livePageId);
                string sql =
                    "select COUNT(OrderID) as Count from Orders o WHERE o.LiveID = @liveId";
                int result = conn.QueryFirstOrDefault<int>(sql, new { liveId });
                return result;
            }
        }
    }
}