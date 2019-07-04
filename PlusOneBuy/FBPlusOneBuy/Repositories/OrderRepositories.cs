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
        private string connectionString = ConfigurationManager.ConnectionStrings["ContextModel"].ConnectionString;
        public IEnumerable<OrderList> GetAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var sql = @"SELECT o.OrderID,c.CustomerName,o.Keyword,p.ProductName,o.Quantity,o.OrderDateTime FROM Orders o
                            INNER JOIN Customers c ON c.CustomerID =o.CustomerID
                            INNER JOIN Products p ON p.ProductID =o.ProductID";
            return connection.Query<OrderList>(sql);
        }
    }
}