using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;

namespace FBPlusOneBuy.Services
{
    public class GroupOrderDetailService
    {
        public void InsertGroupOrderDetail(GroupOrderDetail groupOrderDetail)
        {
            GroupOrderDetailRepository groupOrderDetail_repo = new GroupOrderDetailRepository();
            groupOrderDetail_repo.InsertGroupOrderDetail(groupOrderDetail);
        }
    }
}