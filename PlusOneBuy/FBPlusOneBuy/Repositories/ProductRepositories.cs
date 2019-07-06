using Dapper;
using FBPlusOneBuy.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Repositories
{
    public class ProductRepositories
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        public IEnumerable<Product> FindByName(string productName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var sql = "SELECT * FROM Products WHERE ProductName = @productName";
            var result = connection.QueryMultiple(sql, new { productName });
            var products = result.Read<Product>().ToList();
            return products;
        }
    }
}