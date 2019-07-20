using FBPlusOneBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;

namespace FBPlusOneBuy.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            LivePostsRepository livePost_repo = new LivePostsRepository();
            ViewData.Model = livePost_repo.GetAllLivePosts();
            return View();
        }

        public ActionResult SalesOrderList(string livepageId)
        {
            var salesOrderVM = SalesOrderListService.ListSalesOrders(livepageId);
            return View(salesOrderVM);
        }
        //[HttpPost]
        //public ActionResult RealTimeTotalAndSalesOrders(string livepageId)
        //{

        //    var totalAndSalesOrders = SalesOrderListService.GetTotalAndSalesOrders(livepageId);
        //    return View(totalAndSalesOrders);
        //}
    }
}