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
    public class LineCustomerRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public bool GetCustomerById(string customerId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "Select count(*) FROM LineCustomer WHERE LineCustomerID=@customerId";
                int result = conn.QueryFirstOrDefault<int>(sql, new {  customerId });
                if (result == 0) { return false; }
                else { return true; }
            }

        }
        
        public void InsertCustomer(string customerId, string customerName)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO LineCustomer(LineCustomerID, Name) VALUES ( @customerId, @customerName)";
                conn.Execute(sql, new { customerId, customerName });
            }
        }
    }
}