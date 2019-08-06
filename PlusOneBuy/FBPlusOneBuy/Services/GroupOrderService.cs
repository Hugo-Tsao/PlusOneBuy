using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;
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
                return true;
            }
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
                groupOrder_repo.UpdateGroupOrder(GroupOrderID, NumberOfProduct, Amount, isGroup);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}