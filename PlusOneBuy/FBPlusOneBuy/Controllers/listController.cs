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
        // GET: list
        public ActionResult Index()
        {
            //OrderRepositories orderRepositories = new OrderRepositories();
            //var order = orderRepositories.GetAll();
            //ViewData["Orderlist"] = order;
            return View();
        }
        [HttpPost]
        public ActionResult GetAll()
        {
            OrderRepositories orderRepositories = new OrderRepositories();
            var order = orderRepositories.GetAll();
            var result = JsonConvert.SerializeObject(order);
            return Json(result);
        }
    }
}