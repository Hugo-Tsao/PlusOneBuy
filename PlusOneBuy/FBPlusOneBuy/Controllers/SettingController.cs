using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using FBPlusOneBuy.Services;
using FBPlusOneBuy.Repositories;
using Newtonsoft.Json;
using FBPlusOneBuy.Models;

namespace FBPlusOneBuy.Controllers
{
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FanPageName()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetFanPageData(string token, string fanPageName)
        {
            Session["fanPageName"] = fanPageName;
            //Session["token"] = token;
            Session["token"] = "EAASxbKYYpHoBAI27CZBoK8ZBzFmJjEMIR30woKcIfDPx4mtljSUOsGxVGsKHmy1JgCay8KTilT9l3nbkSfGzBZC6wVSDUcl3ZAa7C5OyZAv8CV7K0duuyW2jHFGqZCwhIKiM6jPonrHLp7s5UEudWL5UHkT8IuZBGmBTOEHS0IjYZCsYbcQfo3j9";
            return Json(Url.Action("video", "Setting"));

        }
        public ActionResult Video()
        {
            string fanPageName = Session["fanPageName"].ToString();
            string token = Session["token"].ToString();
            var result = FBRequestService.getLiveID(fanPageName, token);
            ViewData["liveIDList"] = result;

            string ProductName = "馬桶泡泡洗";
            ProductRepositories productRepositories = new ProductRepositories();
            List<Product> product = new List<Product>();
            product = productRepositories.FindByName(ProductName).ToList();
            ViewData["product"] = product;

            return View();
        }

        [HttpPost]
        public void SendMsg()
        {

            List<string> ids = new List<string> { "3032519476788720", "2762673820474754" };

            FBSendMsgService.SendMsg("123", ids);



        }
    }
}