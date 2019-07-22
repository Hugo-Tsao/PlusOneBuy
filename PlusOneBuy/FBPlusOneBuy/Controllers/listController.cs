using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.Services;
using Newtonsoft.Json;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.ViewModels;

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
        public ActionResult Index(string livePageID, string liveName, string keywordPattern)
        {
            //新增直播進資料庫
            LivePostService.CreateLivePost(livePageID, liveName);
            //新增商品進資料庫
            ProductRepositories product_repo = new ProductRepositories();
            var products = ProductService.GetCurrentProducts().ProductItems;
            foreach (var item in products)
            {
                if (!product_repo.SelectProduct(item.SkuId))
                {
                    product_repo.InsertProduct(item);
                }
            }
            ViewData["products"] = products;
            ViewData["livePageID"] = livePageID;
            ViewData["keywordPattern"] = keywordPattern;            
            return View();
        }

        [HttpPost]
        public ActionResult GetPlusOneBuyOrders(string livePageID, string keywordPattern)
        {
            try
            {
                string token = Session["token"].ToString();
                //string token =
                //    "EAASxbKYYpHoBAI27CZBoK8ZBzFmJjEMIR30woKcIfDPx4mtljSUOsGxVGsKHmy1JgCay8KTilT9l3nbkSfGzBZC6wVSDUcl3ZAa7C5OyZAv8CV7K0duuyW2jHFGqZCwhIKiM6jPonrHLp7s5UEudWL5UHkT8IuZBGmBTOEHS0IjYZCsYbcQfo3j9";
                var products = ProductService.GetCurrentProducts().ProductItems;
                var OrderList = CommentFilterService.getNewOrderList(livePageID, token, products, keywordPattern);
                if (OrderList.Count > 0)
                {
                    FBSendMsgService.OrderListToSendMsg(OrderList, token);
                }

                var result = JsonConvert.SerializeObject(OrderList);
                return Json(result);
            }
            catch (Exception e)
            {
                DateTime date = DateTime.Now;
                string today = date.ToString("yyyy-MM-dd");
                string now = date.ToString("yyyy-MM-dd HH:mm:ss");
                if (!Directory.Exists("C:\\log"))
                {
                    Directory.CreateDirectory("C:\\log\\");
                }

                string nowPath = "C:\\log\\" + today + ".txt";

                System.IO.File.AppendAllText("C:\\log\\" + today + ".txt", "\r\n" + now + " : " + e);

                return Json("error");
            }
        }
        [HttpPost]
        public void SetPostEndtime(string livePageID)
        {
            OrderRepositories o_repo = new OrderRepositories();
            decimal Amount = o_repo.GetAmount(livePageID);
            int QtyOfOrders = o_repo.GetQtyOfOrders(livePageID);
            var live_repo = new LivePostsRepository();
            live_repo.UpdatePost(livePageID, QtyOfOrders, Amount, DateTime.Now);
        }

        [HttpPost]
        public void SendMsgByButton(string livePageID)
        {
            var live_repo = new LivePostsRepository();
            int liveid = live_repo.Select(livePageID);
            var order_repo = new OrderRepositories();
            //List<string> ids = new List<string> { "3032519476788720", "2762673820474754" };
            List<MsgTextViewModel> ordersinfo = order_repo.SelectAllOrdersInfo(liveid);
            string token = (string)Session["token"];
            var orders = new List<OrderList>();
            foreach (var orderinfo in ordersinfo)
            {
                var order = new OrderList();
                order.CustomerID = orderinfo.CustomerID;
                order.CustomerName = orderinfo.CustomerName;
                order.OrderID = orderinfo.OrderID;
                order.Product = new ProductViewModel { Salepage_id = orderinfo.ProductPageID, SkuId = orderinfo.ProductID, ProductName = orderinfo.ProductName };
                order.Quantity = orderinfo.Quantity;
                orders.Add(order);
            }
            FBSendMsgService.OrderListToSendMsg(orders, token);

        }
        [HttpGet]
        public ActionResult GetROIOrderInfo(string livePageId)
        {
            OrderRepositories o_repo = new OrderRepositories();
            var amount = o_repo.GetAmount(livePageId);
            var qty = o_repo.GetQtyOfOrders(livePageId);
            var Order = new { Amount = amount, Qty = qty };
            return Json(Order, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLiveVideoViews(string livePageID)
        {
            string token = Session["token"].ToString();
            int views=FBRequestService.GetLiveVideoViews(livePageID, token);

            return Json(views, JsonRequestBehavior.AllowGet);
        }
    }
}