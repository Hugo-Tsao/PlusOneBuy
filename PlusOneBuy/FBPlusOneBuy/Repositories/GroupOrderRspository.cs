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
    public class GroupOrderRspository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public int GetGroupOrderById(int campaignID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT GroupOrderID FROM GroupOrder WHERE CampaignID=@campaignID AND isGroup=0";
                int GroupOrderID = conn.QueryFirstOrDefault<int>(sql, new { campaignID });
                return GroupOrderID; 
            }

        }
        public int CheckisGroup(int campaignID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT SUM(c.PeopleGroup-o.NumberOfPeople) AS isGroup
                                FROM GroupOrder as o
                                INNER JOIN Campaign as c on c.CampaignID = o.CampaignID
                                WHERE c.CampaignID = @campaignID";
                int isGroup = conn.QueryFirstOrDefault<int>(sql, new { campaignID });
                return isGroup;
            }
        }
        public void UpdateisGroup(int campaignID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE GroupOrder SET isGroup=1 WHERE CampaignID=@CampaignID";
                conn.Execute(sql, new { campaignID });
            }
        }
        public Product GetProductById(int productID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT ProductName,UnitPrice FROM Products WHERE ProductID=@productID";
                Product product = conn.QueryFirstOrDefault<Product>(sql, new { productID });
                return product;
            }
        }

        public void InsertGroupOrder(int campaignID,DateTime orderDateTime,decimal? amount)
        {
            using (conn = new SqlConnection(connectionString))
            {
                int isGroup = 0;
                int NumberOfPeople = 1;
                string sql = "INSERT INTO GroupOrder(CampaignID, OrderDateTime,isGroup,NumberOfPeople,Amount) VALUES ( @campaignID, @orderDateTime,@isGroup,@NumberOfPeople,@amount)";
                conn.Execute(sql, new { campaignID, orderDateTime, isGroup, NumberOfPeople, amount });
            }
        }

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

        public bool CheckOrderDetailByCustomer(int GroupOrderID, string LineCustomerID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT count(*) FROM GroupOrderDetail WHERE GroupOrderID=@GroupOrderID AND LineCustomerID='" + LineCustomerID + "'";
                int result = conn.QueryFirstOrDefault<int>(sql, new { GroupOrderID });
                if (result == 0) { return false; }
                else { return true; }
            }
        }

        public void UpdateOrderDetailByCustomer(int GroupOrderID, string LineCustomerID, int qty)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE GroupOrderDetail SET Quantity=@qty WHERE GroupOrderID=@GroupOrderID AND LineCustomerID='" + LineCustomerID + "'";
                conn.Execute(sql, new{ GroupOrderID , qty });
            }
        }

        public GroupOrder GETGroupOrderAmountQty(int GroupOrderID)
        {

            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT SUM(UnitPrice*Quantity) AS Amount,COUNT(*) AS NumberOfPeople FROM GroupOrderDetail WHERE GroupOrderID=@GroupOrderID";
                var result = conn.QueryFirstOrDefault<GroupOrder>(sql,new { GroupOrderID });
                return result;

            }
        }
        public void UpdateGroupOrder(int GroupOrderID,int? NumberOfPeople,decimal? Amount)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE GroupOrder SET NumberOfPeople=@NumberOfPeople,Amount=@Amount WHERE GroupOrderID=@GroupOrderID";
                conn.Execute(sql, new { GroupOrderID,NumberOfPeople, Amount });
            }
        }

    }
}