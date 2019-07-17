using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;

namespace FBPlusOneBuy.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        public ActionResult GetROIOrderInfo(string livePageId)
        {
            OrderRepositories o_repo = new OrderRepositories();
            var Amount = o_repo.GetAmount(livePageId);
            var Qty = o_repo.GetQtyOfOrders(livePageId);
            return Json("");
        }
    }
}