using FBPlusOneBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBPlusOneBuy.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SalesOrderList(string livepageId)
        {
            var salesOrderVM = SalesOrderListService.ListSalesOrders(livepageId);
            return View(salesOrderVM);
        }
    }
}