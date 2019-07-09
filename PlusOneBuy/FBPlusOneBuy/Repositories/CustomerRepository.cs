using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FBPlusOneBuy.Models;

namespace FBPlusOneBuy.Repositories
{
    public class CustomerRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public bool SelectCustomer(string custId)
        {
            string sql = "Select * From Customers Where CustomerID = @CustomerID";
            var cust = conn.QueryFirstOrDefault<Customer>(sql,new { CustomerID=custId});
            if (cust == null)
            {
                return false;
            }
            else { return true; }
        }

        public void InsertCustomer(List<Customer> customers)
        {
            using (conn = new SqlConnection(connectionString))
            {

                foreach (var customer in customers)
                {
                    string sql = "INSERT INTO Customers(CustomerID, CustomerName) VALUES ( @CustomerID, @CustomerName)";
                    conn.Execute(sql, new { customer.CustomerID,customer.CustomerName });
                }

            }
        }
    }
}