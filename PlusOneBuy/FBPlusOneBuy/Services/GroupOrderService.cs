using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.DBModels;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Services
{
    public class GroupOrderService
    {
        internal GroupOrderRespository groupOrder_repo;

        public GroupOrderService()
        {
            groupOrder_repo = new GroupOrderRespository();
        }
        public bool GetGroupOrder(int campaignID,int remaningNumber, ref GroupOrderViewModel groupOrder)
        {
            try
            {
                groupOrder = groupOrder_repo.SearchGroupOrder(campaignID, remaningNumber);
                return groupOrder.isGroup;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                return true;
            }
        }
        public List<int> GetGroupOrderIds(int campaignID)
        {
            List<int> groupOrderids=groupOrder_repo.SelectGroupOrderIDs(campaignID);
            return groupOrderids;
        }

        public void InsertGroupOrder(int campaignID, DateTime orderDateTime, int productGroup)
        {
            GroupOrderViewModel groupOrder = new GroupOrderViewModel()
            {
                CampaignID = campaignID,
                OrderDateTime = orderDateTime,
                NumberOfProduct = 0,
                Amount = 0,
                isGroup = false
            };
            groupOrder_repo.InsertGroupOrder(groupOrder);
        }

        public bool UpdateGroupOrder(int GroupOrderID, int NumberOfProduct, decimal Amount, bool isGroup)
        {
            try
            {
                GroupOrder groupOrder = groupOrder_repo.SearchGroupOrder(GroupOrderID);
                groupOrder.NumberOfProduct = NumberOfProduct;
                groupOrder.Amount = Amount;
                groupOrder.isGroup = isGroup;
                groupOrder_repo.UpdateGroupOrder(groupOrder);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<GroupOrderListGroupOrderViewModel> SelectGroupOrders(int campaignID)
        {
            var groupOrders=groupOrder_repo.SelectGroupOrders(campaignID);
            return groupOrders;
        }

        public int GetIsGroupORder(int campaignId)
        {
            try
            {
                GroupOrderRespository groupOrder_repo = new GroupOrderRespository();
                int amount = groupOrder_repo.GetIsGroupORder(campaignId);
                return amount;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                return 0;
            }
            
        }
        public void UpdateShipDateTime(int GroupOrderID,DateTime shipDateTime)
        {
            GroupOrder groupOrder = groupOrder_repo.SearchGroupOrder(GroupOrderID);
            groupOrder.shipDateTime = shipDateTime;
            groupOrder_repo.UpdateGroupOrder(groupOrder);
        }
    }
}