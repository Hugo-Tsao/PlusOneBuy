﻿using System;
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
using Microsoft.AspNet.Identity;
using FBPlusOneBuy.DBModels;

namespace FBPlusOneBuy.Controllers
{
    [Authorize]
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
            string userid = User.Identity.GetUserId();
            //string fanpageid = (string)Session["FanPageID"];
            int fanpageid = FanPageService.GetPageId(userid);
            //新增直播進資料庫
            LivePostService.CreateLivePost(livePageID, liveName, fanpageid);
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
            //try
            //{
            //string token = Session["token"].ToString();
            string userid = User.Identity.GetUserId();
            string token = FanPageService.GetToken(userid);
            var products = ProductService.GetCurrentProducts().ProductItems;
            var OrderList = CommentFilterService.getNewOrderList(livePageID, token, products, keywordPattern);
            var Success_order = new List<OrderList>();
            if (OrderList.Count > 0)
            {
                var order_repo = new OrderRepositories();
                Session["lastPostTime"] = OrderList.Last().OrderDateTime;
                foreach (var order in OrderList)
                {
                    //留言成功，數量修改
                    if (ProductService.UpdateProductQty(order.Product.SkuId,order.Quantity))
                    {
                        Success_order.Add(order);
                        order_repo.InsertOrder(order);
                        FBSendMsgService.SuccessOrderToSendMsg(livePageID, order, token);
                    }
                    else  //留言失敗
                    {
                        FBSendMsgService.FailedOrderToSendMsg(livePageID, order, token);
                    }
                }
            }
            var result = JsonConvert.SerializeObject(Success_order);
            return Json(result);
            //}
            //catch (Exception e)
            //{
            //    DateTime date = DateTime.UtcNow.AddHours(8);
            //    string today = date.ToString("yyyy-MM-dd");
            //    string now = date.ToString("yyyy-MM-dd HH:mm:ss");
            //    if (!Directory.Exists("C:\\log"))
            //    {
            //        Directory.CreateDirectory("C:\\log\\");
            //    }

            //    string nowPath = "C:\\log\\" + today + ".txt";

            //    System.IO.File.AppendAllText("C:\\log\\" + today + ".txt", "\r\n" + now + " : " + e);

            //    return Json("error");
            //}
        }
        [HttpPost]
        public ActionResult SetPostEndtime(string livePageID)
        {
            OrderRepositories o_repo = new OrderRepositories();
            decimal Amount = o_repo.GetAmount(livePageID);
            int QtyOfOrders = o_repo.GetQtyOfOrders(livePageID);
            var live_repo = new LivePostsRepository();
            int views = 0;
            if (Session["views"] != null)
            {
                views = (int)Session["views"];

            }
            live_repo.UpdatePost(livePageID, QtyOfOrders, Amount, DateTime.UtcNow.AddHours(8), views);
            Session.Abandon();
            return RedirectToAction("Index", "Report");
        }

        [HttpPost]
        public void SendMsgByButton(string livePageID)
        {
            var live_repo = new LivePostsRepository();
            int liveid = live_repo.Select(livePageID);
            var order_repo = new OrderRepositories();
            //List<string> ids = new List<string> { "3032519476788720", "2762673820474754" };
            List<MsgTextViewModel> ordersinfo = order_repo.SelectAllOrdersInfo(liveid);
            //string token = (string)Session["token"];
            string userid = User.Identity.GetUserId();
            string token = FanPageService.GetToken(userid);
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
                FBSendMsgService.SuccessOrderToSendMsg(livePageID, order, token);
            }
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
            Session["views"] = null;
            string userid = User.Identity.GetUserId();
            try
            {
                //string token = Session["token"].ToString();

                string token = FanPageService.GetToken(userid);
                int views = FBRequestService.GetLiveVideoViews(livePageID, token);
                if (Session["views"] == null)
                {
                    Session["views"] = views;
                }
                else
                {
                    if ((int)Session["views"] < views)
                    {
                        Session["views"] = views;
                    }
                }

                return Json(views, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetOrdersCount(string livePageID)
        {
            int ordersCount = 0;
            OrderService orderService = new OrderService();
            List<Order> orders = orderService.GetOrders(livePageID);
            DateTime dtNow = DateTime.UtcNow.AddHours(8);
             
            if (Session["lastCountTime"] == null)
            {
                ordersCount = orders.Count;
                if (ordersCount!=0)
                {
                    Session["lastCountTime"] = orders.Last().OrderDateTime;
                }                
            }
            else
            {
                DateTime lastCountTime = (DateTime)Session["lastCountTime"];
                ordersCount = orders.Where(x=>DateTime.Compare(x.OrderDateTime, lastCountTime) >0).Count();
                Session["lastCountTime"] = orders.Last().OrderDateTime;
            }

            return Json(ordersCount, JsonRequestBehavior.AllowGet);
        }
    }
}