using Dapper;
using FBPlusOneBuy.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FBPlusOneBuy.ViewModels;
using Microsoft.Owin.Security.Provider;

namespace FBPlusOneBuy.Repositories
{
    public class ProductRepositories
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        public IEnumerable<Product> FindByName(string productName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var sql = "SELECT * FROM Products WHERE ProductName = @ProductName";
            var result = connection.QueryMultiple(sql, new { ProductName = productName });
            var products = result.Read<Product>().ToList();
            return products;
        }

        public bool SelectProduct(int productId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string sql = "Select count(*) From Products Where ProductID = @ProductID";
                int count_result = conn.QueryFirstOrDefault<int>(sql, new {ProductID = productId });
                if (count_result == 0)
                {
                    return false;
                }
                else { return true; }
            }
        }

        public void InsertProduct(ProductViewModel pvm)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Products(ProductID, ProductPageID, UnitPrice, ProductName) VALUES ( @SkuId, @Salepage_id, @UnitPrice,@ProductName)";
                conn.Execute(sql, new { pvm.SkuId, pvm.Salepage_id, pvm.UnitPrice,pvm.ProductName });
            }
        }
        public Product GetProductById(int productID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT ProductName,UnitPrice FROM Products WHERE ProductID=@productID";
                Product product = conn.QueryFirstOrDefault<Product>(sql, new { productID });
                return product;
            }
        }
    }
}