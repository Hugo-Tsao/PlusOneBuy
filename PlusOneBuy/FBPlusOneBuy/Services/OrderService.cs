using FBPlusOneBuy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.DBModels;

namespace FBPlusOneBuy.Services
{
    public class OrderService
    {
        internal OrderRepositories Order_repo;

        public OrderService()
        {
            Order_repo = new OrderRepositories();
        }
        public List<Order> GetOrders(string livePageId)
        {
            List<Order> orders = Order_repo.SelectOrdersByLivePageId(livePageId);
            return orders;
        }
    }
}