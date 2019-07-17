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
        public ActionResult GetSalesOrders(string  livepageId)
        {
            var salesOrderVM = SalesOrderListService.ListSalesOrders(livepageId);
            var result = JsonConvert.SerializeObject(salesOrderVM);
            return Json(result);
        }
        [HttpPost]
        public ActionResult RealTimeTotalAndSalesOrders(string livepageId)
        {
            
            var totalAndSalesOrders = SalesOrderListService.GetTotalAndSalesOrders(livepageId);
            var result = JsonConvert.SerializeObject(totalAndSalesOrders);
            return Json(result);
        }
    }
}