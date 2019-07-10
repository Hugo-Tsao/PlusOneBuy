using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.Services;
using Newtonsoft.Json;
using FBPlusOneBuy.Models;

namespace FBPlusOneBuy.Controllers
{
    public class listController : Controller
    {
        [HttpGet]
        // GET: list
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string keyWord, string ProductName, string livePageID)
        {
            //新增直播進資料庫
            LivePostService.CreateLivePost(livePageID);

            ViewData["keyWord"] = keyWord;
            ViewData["ProductName"] = ProductName;
            ViewData["livePageID"] = livePageID;
            ProductRepositories productRepositories = new ProductRepositories();
            var product = new List<Product>();
            product = productRepositories.FindByName(ProductName).ToList();
            ViewData["product"] = product;
            return View();
        }
        //[HttpPost]
        //public ActionResult GetAll(string liveID)
        //{
        //    ViewData["liveID"] = liveID;
        //    OrderRepositories orderRepositories = new OrderRepositories();
        //    var order = orderRepositories.GetAll();
        //    order = order.Where((x) => x.LiveID == liveID);
        //    var result = JsonConvert.SerializeObject(order);
        //    return Json(result);
        //}

        [HttpPost]
        public ActionResult GetPlusOneBuyOrders(string livePageID)
        {
            string token = Session["token"].ToString();
            //string token =
            //    "EAASxbKYYpHoBAI27CZBoK8ZBzFmJjEMIR30woKcIfDPx4mtljSUOsGxVGsKHmy1JgCay8KTilT9l3nbkSfGzBZC6wVSDUcl3ZAa7C5OyZAv8CV7K0duuyW2jHFGqZCwhIKiM6jPonrHLp7s5UEudWL5UHkT8IuZBGmBTOEHS0IjYZCsYbcQfo3j9";
            var products = ProductService.GetCurrentProducts().ProductItems;
            var OrderList = CommentFilterService.getNewOrderList(livePageID, token, products);
            if (OrderList.Count > 0)
            {
                FBSendMsgService.OrderListToSendMsg(OrderList, token);
            }
            var result = JsonConvert.SerializeObject(OrderList);
            return Json(result);
        }
        [HttpPost]
        public void SetPostEndtime(string livePageID)
        {
            var live_repo = new LivePostsRepository();
            int liveid = live_repo.Select(livePageID);
            live_repo.UpdateEndTime(liveid,DateTime.Now);
        }
    }
}