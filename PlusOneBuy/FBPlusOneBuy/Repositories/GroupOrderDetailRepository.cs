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
    public class GroupOrderDetailRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public void InsertGroupOrderDetail(GroupOrderDetail orderDetail)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO GroupOrderDetail(GroupOrderID,LineCustomerID, ProductName,UnitPrice,Quantity,MessageDateTime) VALUES ( @GroupOrderID,@LineCustomerID, @ProductName,@UnitPrice,@Quantity,@MessageDateTime)";
                conn.Execute(sql, new
                {
                    orderDetail.GroupOrderID,
                    orderDetail.LineCustomerID,
                    orderDetail.ProductName,
                    orderDetail.UnitPrice,
                    orderDetail.Quantity,
                    orderDetail.MessageDateTime
                });
            }
        }
    }
}