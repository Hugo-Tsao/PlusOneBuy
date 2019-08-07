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
    public class GroupOrderRespository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public GroupOrderViewModel SearchGroupOrder(int campaignID,int remaningNumber)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM GroupOrder WHERE CampaignID=@CampaignID AND isGroup=0 AND NumberOfProduct <= @RemaningNumber";
                GroupOrderViewModel GroupOrderID = conn.QueryFirstOrDefault<GroupOrderViewModel>(sql,
                    new {CampaignID = campaignID, RemaningNumber = remaningNumber});
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

        public void InsertGroupOrder(GroupOrderViewModel groupOrder)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO GroupOrder(CampaignID, OrderDateTime,isGroup,NumberOfProduct,Amount) VALUES ( @campaignID, @orderDateTime,@isGroup,@NumberOfProduct,@amount)";
                conn.Execute(sql, new { groupOrder.CampaignID, groupOrder.OrderDateTime, groupOrder.isGroup, groupOrder.NumberOfProduct, groupOrder.Amount });
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
        public void UpdateGroupOrder(int GroupOrderID,int NumberOfProduct,decimal Amount,bool isGroup)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE GroupOrder SET isGroup = @isGroup, NumberOfProduct = @NumberOfProduct, Amount = @Amount Where GroupOrderID = @GroupOrderID";
                conn.Execute(sql, new { isGroup, NumberOfProduct, Amount, GroupOrderID });
            }
        }
        public List<GroupOrderListGroupOrderViewModel> SelectGroupOrders(int campaignID)
        {

            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select c.Title,c.Detail,c.ProductGroup,p.ProductName,[go].GroupOrderID,[go].OrderDateTime,[go].shipDateTime,[go].isGroup,[go].NumberOfProduct,[go].Amount from GroupOrder [go] inner join Campaign c on [go].CampaignID=c.CampaignID inner join Products p on c.ProductID=p.ProductID where c.CampaignID=@campaignID;";
                var groupOrders = conn.Query<GroupOrderListGroupOrderViewModel>(sql, new { campaignID }).ToList();
                return groupOrders;

            }
        }
    }
}