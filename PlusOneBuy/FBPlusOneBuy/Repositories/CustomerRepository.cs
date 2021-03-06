﻿using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using FBPlusOneBuy.DBModels;

namespace FBPlusOneBuy.Repositories
{
    public class CustomerRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public bool SelectCustomer(string custId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "Select count(*) From Customers Where CustomerID = @CustomerID";
                int count_result = conn.QueryFirstOrDefault<int>(sql, new { CustomerID = custId });
                if (count_result == 0)
                {
                    return false;
                }
                return true;
            }
            
        }

        public void InsertCustomer(Customer customer)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Customers(CustomerID, CustomerName) VALUES ( @CustomerID, @CustomerName)";
                    conn.Execute(sql, new { customer.CustomerID,customer.CustomerName });
            }
        }
    }
}