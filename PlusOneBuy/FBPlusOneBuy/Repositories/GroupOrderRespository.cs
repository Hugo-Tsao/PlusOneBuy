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
        public void InsertGroupOrder(GroupOrderViewModel groupOrder)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO GroupOrder(CampaignID, OrderDateTime,isGroup,NumberOfProduct,Amount) VALUES ( @campaignID, @orderDateTime,@isGroup,@NumberOfProduct,@amount)";
                conn.Execute(sql, new { groupOrder.CampaignID, groupOrder.OrderDateTime, groupOrder.isGroup, groupOrder.NumberOfProduct, groupOrder.Amount });
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
    }
}