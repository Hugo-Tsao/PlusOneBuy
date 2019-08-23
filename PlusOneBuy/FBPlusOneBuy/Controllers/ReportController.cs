using FBPlusOneBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;
using Microsoft.AspNet.Identity;

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
            ViewBag.liveId = liveId;
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
            string userId = User.Identity.GetUserId();
            int[] storeManagerIDs=CampaignService.GetStoreManagerIDs(userId);           
            ViewData.Model = CampaignService.GetAllCampaigns(storeManagerIDs);
            return View();
        }
        public ActionResult GroupOrderList(int campaignID)
        {
            ViewBag.campaignID = campaignID;            
            GroupOrderService groupOrderService = new GroupOrderService();           
            var groupOrders = groupOrderService.SelectGroupOrders(campaignID);
            CampaignService campaignService = new CampaignService();
            CampaignViewModel cvm = campaignService.GetCampaign(campaignID);
            ViewBag.title = cvm.Title;
            ViewBag.productName = cvm.ProductName;
            ViewBag.detail = cvm.Detail;
            ViewBag.productGroup = cvm.ProductGroup;           
            ViewData.Model = groupOrders;            
            
            
            return View();
        }
        public ActionResult GroupOrderDetailList(int GroupOrderID,int campaignID)
        {
            ViewBag.campaignID = campaignID;
            ViewBag.GroupOrderID = GroupOrderID;
            var GOdetailService = new GroupOrderDetailService();
            ViewData.Model=GOdetailService.GetDetailByGroupOrderID(GroupOrderID);
            return View();
        }
    }
}