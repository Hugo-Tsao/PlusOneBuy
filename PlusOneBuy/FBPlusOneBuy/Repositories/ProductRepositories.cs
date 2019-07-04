using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FBPlusOneBuy.Repositories
{
    public class ProductRepositories
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ContextModel"].ConnectionString;
        public IEnumerable<Product> GetAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection.Query<Product>("SELECT * FROM Products");
        }
    }
}