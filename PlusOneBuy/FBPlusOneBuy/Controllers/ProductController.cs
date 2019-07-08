using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Services;

namespace FBPlusOneBuy.Controllers
{
    public class ProductController : Controller
    {
        [HttpPost]
        public ActionResult GetSKUListByMain(int salepage_id)
        {
            
            Store store = ProductService.GetSKUListByMain(salepage_id);
            return Json(store);
        }

        [HttpPost]
        public ActionResult GetStock(int salepage_id, int SkuId)
        {
            Store store = ProductService.GetStock(salepage_id, SkuId);
            return Json(store);
        }

        [HttpPost]
        public ActionResult GetMain(int salepage_id)
        {
            Store store = ProductService.GetMain(salepage_id);
            return Json(store);
        }
    }
}