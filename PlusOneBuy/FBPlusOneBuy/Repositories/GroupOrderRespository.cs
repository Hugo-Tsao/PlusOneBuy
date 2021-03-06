﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using FBPlusOneBuy.DBModels;
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
        public GroupOrder SearchGroupOrder(int groupOrderId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM GroupOrder WHERE GroupOrderID = @GroupOrderID";
                GroupOrder GroupOrder = conn.QueryFirstOrDefault<GroupOrder>(sql,
                    new { GroupOrderID = groupOrderId });
                return GroupOrder;
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

        public void UpdateGroupOrder(GroupOrder groupOrder)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE GroupOrder  SET isGroup = @isGroup, NumberOfProduct = @NumberOfProduct, Amount = @Amount, shipDateTime = @shipDateTime, BtnOrderClickDateTime = @BtnOrderClickDateTime, BtnGroupClickDateTime = @BtnGroupClickDateTime Where GroupOrderID = @GroupOrderID";
                conn.Execute(sql, new { groupOrder.isGroup, groupOrder.NumberOfProduct, groupOrder.Amount, groupOrder.shipDateTime,groupOrder.BtnOrderClickDateTime, groupOrder.BtnGroupClickDateTime, groupOrder.GroupOrderID });
            }
        }

        public List<GroupOrderListGroupOrderViewModel> SelectGroupOrders(int campaignID)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select c.Title,c.Detail,c.ProductGroup,c.EndTime,p.ProductName,[go].GroupOrderID,[go].OrderDateTime,[go].shipDateTime,[go].isGroup,[go].NumberOfProduct,[go].Amount,[go].BtnOrderClickDateTime,[go].BtnGroupClickDateTime from GroupOrder [go] inner join Campaign c on [go].CampaignID=c.CampaignID inner join Products p on c.ProductID=p.ProductID where c.CampaignID=@campaignID;";
                var groupOrders = conn.Query<GroupOrderListGroupOrderViewModel>(sql, new { campaignID }).ToList();
                return groupOrders;

            }
        }

        public int GetIsGroupORder(int campaignId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT SUM(OD.Quantity) FROM GroupOrder O INNER JOIN GroupOrderDetail OD ON O.GroupOrderID = OD.GroupOrderID INNER JOIN Campaign C ON C.CampaignID = O.CampaignID INNER JOIN Products P ON P.ProductID = C.ProductID WHERE C.CampaignID = @CampaignID AND O.isGroup = 1 AND O.BtnOrderClickDateTime is null";
                int amount = conn.QueryFirstOrDefault<int>(sql, new {CampaignID = campaignId});
                return amount;
            }
        }

        public List<int> SelectGroupOrderIDsByShipIsNull(int campaignId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select GroupOrderID from GroupOrder where CampaignID=@campaignId and isGroup=1 and shipDateTime is null;";
                List<int> groupOrderids = conn.Query<int>(sql, new { campaignId }).ToList();
                return groupOrderids;
            }
        }
        public List<int> SelectGroupOrderIDsByOrderIsNull(int campaignId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select GroupOrderID from GroupOrder where CampaignID=@campaignId and isGroup=1 and BtnOrderClickDateTime is null;";
                List<int> groupOrderids = conn.Query<int>(sql, new { campaignId }).ToList();
                return groupOrderids;
            }
        }
    }
}