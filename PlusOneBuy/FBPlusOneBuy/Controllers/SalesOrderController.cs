using FBPlusOneBuy.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBPlusOneBuy.Controllers
{
    public class SalesOrderController : Controller
    {       // GET: SalesOrder
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSalesOrders(int liveId)
        {
            var salesOrderVM = SalesOrderListService.ListSalesOrders(liveId);
            var result = JsonConvert.SerializeObject(salesOrderVM);
            return Json(result);
        }
    }
}