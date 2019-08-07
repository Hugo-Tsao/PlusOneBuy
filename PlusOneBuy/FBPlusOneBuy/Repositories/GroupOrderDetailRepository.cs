using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.ViewModels;

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
        public IEnumerable<GroupOrderDetailViewModel> GetDetailByGroupOrderID(int GroupOrderID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                //string sql = "SELECT GroupOrderDetailID,Name,ProductName,UnitPrice,Quantity,MessageDateTime FROM GroupOrderDetail AS Od INNER JOIN LineCustomer AS Lc ON lc.LineCustomerID=Od.LineCustomerID WHERE GroupOrderID=@GroupOrderID";
                string sql = @"SELECT g.Amount,g.NumberOfProduct,GroupOrderDetailID,Name,ProductName,UnitPrice,Quantity,MessageDateTime
                                FROM GroupOrderDetail AS Od 
                                INNER JOIN LineCustomer AS Lc ON lc.LineCustomerID=Od.LineCustomerID 
                                INNER JOIN GroupOrder AS G ON od.GroupOrderID=g.GroupOrderID
                                WHERE od.GroupOrderID=@GroupOrderID";
                return conn.Query<GroupOrderDetailViewModel>(sql,new { GroupOrderID });
            }
        }
    }
}