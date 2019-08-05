using FBPlusOneBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;

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
        public ActionResult FbList()
        {
            LivePostsRepository livePost_repo = new LivePostsRepository();
            ViewData.Model = livePost_repo.GetAllLivePosts();
            return View();
        }
        public ActionResult SalesOrderList(string livepageId)
        {
            if (livepageId == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["livepageId"] = livepageId;
            var salesOrderVM = SalesOrderListService.ListSalesOrders(livepageId);
            return View(salesOrderVM);
        }
        public ActionResult CommentsOrderList(int liveId)
        {
            LivePostsRepository livePost_repo = new LivePostsRepository();
             ViewData["LivePost"] = livePost_repo.GetLivePost(liveId);

             OrderRepositories order_repo = new OrderRepositories();
             List<CommentOrderLIstViewModel> orders = order_repo.GetOrders(liveId);
             ViewData.Model = orders;
            return View();
        }
        public ActionResult ROIOrderList(int liveId)
        {
            LivePostsRepository livePost_repo = new LivePostsRepository();
            ViewData.Model = livePost_repo.GetLivePost(liveId);

            var SaleOrder = livePost_repo.SaleOrder(liveId);
            ViewData["SaleOrder"] = SaleOrder;
            return View();
        }
        public ActionResult LineList()
        {
            return View();
        }
        public ActionResult GroupOrderList()
        {
            return View();
        }
    }
}