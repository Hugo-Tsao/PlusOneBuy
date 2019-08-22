using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.Services;
using FBPlusOneBuy.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBPlusOneBuy.Controllers
{
    public class ChartController : Controller
    {
        [HttpPost]
        public ActionResult GroupOrderDetailChart(int GroupOrderID)
        {
            var name = new List<string>();
            var qty = new List<int>();
            var dateTime = new List<string>();
            var price = new List<decimal>(); 
            var arrayData = new { name,dateTime, qty, price };
            
            var GOdetailService = new GroupOrderDetailService();
            var data = GOdetailService.GetDetailByGroupOrderID(GroupOrderID);
            foreach (var item in data)
            {
                name.Add(item.Name);
                qty.Add(item.Quantity);
                dateTime.Add(item.MessageDateTime.ToString());
                var amount = item.Quantity * item.UnitPrice;
                price.Add(amount);
            }           
            return Json(arrayData);
        }

        [HttpPost]
        public ActionResult GroupOrderChart(int CampaignID)
        {
            var name = new List<string>();
            var qty = new List<int>();
            var dateTime = new List<string>();
            var price = new List<decimal>();
            var arrayData = new { name, dateTime, qty, price };

            var GOdetailService = new GroupOrderDetailService();
            var data = GOdetailService.GetDetailByCampaignID(CampaignID);
            foreach (var item in data)
            {
                name.Add(item.Name);
                qty.Add(item.Quantity);
                dateTime.Add(item.MessageDateTime.ToString());
                var amount = item.Quantity * item.UnitPrice;
                price.Add(amount);
            }
            return Json(arrayData);
        }
        [HttpPost]
        public ActionResult FbOrderChart(int liveId)
        {
            LivePostsRepository livePost_repo = new LivePostsRepository();
            var time = livePost_repo.GetLivePost(liveId);

            OrderRepositories order_repo = new OrderRepositories();
            var orders = order_repo.GetOrders(liveId);

            var post = time.postTime.Minute;
            TimeSpan interval = time.endTime - time.postTime;
            var minutes = Math.Round(Convert.ToDouble(interval.TotalMinutes));
            var intervalOrders = new List<CommentOrderLIstViewModel>();

            var sum = new List<int>();
            var datetime = new List<string>();
            var chartData = new { datetime, sum };
            for (var i = 1; i <= minutes; i++)
            {
                intervalOrders = orders.Where((x) => x.OrderDateTime.Minute >= post + i && x.OrderDateTime.Minute < post + i + 1).ToList();
                var qty = 0;
                foreach (var item in intervalOrders)
                {
                    qty += item.Quantity;
                }
                datetime.Add(time.postTime.AddMinutes(i).ToString("HH:mm"));
                sum.Add(qty);
            }

            return Json(chartData);
        }

        [HttpPost]
        public ActionResult ReCordViewers(string livePageID, string numberOfViewers)
        {
            try
            {
                int liveID = LivePostService.Select(livePageID);
                ViewerService viewerService = new ViewerService();
                viewerService.Create(liveID, numberOfViewers);
                return Json("OK",JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("error:" + e, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}