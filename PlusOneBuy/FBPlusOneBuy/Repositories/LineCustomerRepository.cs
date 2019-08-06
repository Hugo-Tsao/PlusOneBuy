using Dapper;
using FBPlusOneBuy.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Repositories
{
    public class LineCustomerRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public LineCustomerViewModel SearchLineCustomer(string customerId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "Select LineCustomerID,Name FROM LineCustomer WHERE LineCustomerID=@customerId";
                LineCustomerViewModel result = conn.QueryFirstOrDefault<LineCustomerViewModel>(sql, new {  customerId });
                return result;
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