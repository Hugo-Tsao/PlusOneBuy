using System;
using System.Collections.Generic;
using System.Linq;
using FBPlusOneBuy.DBModels;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Services
{
    public class GroupOrderDetailService
    {
        public void InsertGroupOrderDetail(GroupOrderDetail groupOrderDetail)
        {
            GroupOrderDetailRepository groupOrderDetail_repo = new GroupOrderDetailRepository();
            groupOrderDetail_repo.InsertGroupOrderDetail(groupOrderDetail);
        }
        public List<GroupOrderDetailViewModel> GetDetailByGroupOrderID(int GroupOrderID)
        {
            GroupOrderDetailRepository groupOrderDetail_repo = new GroupOrderDetailRepository();
            var GroupOrderDetail = groupOrderDetail_repo.GetDetailByGroupOrderID(GroupOrderID).ToList();
            return GroupOrderDetail;
        }
    }
}