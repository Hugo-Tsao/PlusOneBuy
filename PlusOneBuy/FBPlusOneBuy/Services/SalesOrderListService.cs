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
        public static SalesOrderViewModel ListSalesOrders(string livepageId)
        {
            var salesorder_repo = new SalesOrderRepositories();
            var live_repo = new LivePostsRepository();
            List<int> liveids = live_repo.SelectIds(livepageId);
            var salesOrders = salesorder_repo.Select(liveids);
            string name = live_repo.SelectLiveName(livepageId);

            var salesOrderVM = new SalesOrderViewModel()
            {
                salesOrders = salesOrders,
                salesOrderNum=salesOrders.Count,
                total = salesOrders.Sum(x => x.Total),
                livename=name
            };
            return salesOrderVM;
        }
        public static TotalAndSalesOrders GetTotalAndSalesOrders(string livepageId)
        {
            var salesorder_repo = new SalesOrderRepositories();
            var live_repo = new LivePostsRepository();
            int liveid = live_repo.Select(livepageId);
            var salesOrders = salesorder_repo.Select(liveid);

            var totalAndSalesOrders = new TotalAndSalesOrders()
            {
                salesOrderNum = salesOrders.Count,
                total = salesOrders.Sum(x => x.Total)
            };
            return totalAndSalesOrders;

        }
    }
}