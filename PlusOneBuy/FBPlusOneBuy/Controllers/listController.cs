using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;
using Newtonsoft.Json;

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
        public ActionResult Index(string keyWord, string ProductName, string liveID)
        {
            ViewData["keyWord"] = keyWord;
            ViewData["ProductName"] = ProductName;
            ViewData["liveID"] = liveID;

            return View();
        }
        [HttpPost]
        public ActionResult GetAll(string liveID)
        {
            ViewData["liveID"] = liveID;
            OrderRepositories orderRepositories = new OrderRepositories();
            var order = orderRepositories.GetAll();
            order = order.Where((x) => x.LiveID == liveID);
            var result = JsonConvert.SerializeObject(order);
            return Json(result);
        }
    }
}