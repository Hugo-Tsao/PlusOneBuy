using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.ViewModels;
using FBPlusOneBuy.Repositories;

namespace FBPlusOneBuy.Services
{
    public class SalesOrderListService
    {
        public static SalesOrderViewModel ListSalesOrders(int liveId)
        {
            var salesorder_repo = new SalesOrderRepositories();
            var salesOrders = salesorder_repo.Select(liveId);

            var salesOrderVM = new SalesOrderViewModel()
            {
                salesOrders = salesOrders,
                salesOrderNum=salesOrders.Count
            };
            return salesOrderVM;

        }
    }
}