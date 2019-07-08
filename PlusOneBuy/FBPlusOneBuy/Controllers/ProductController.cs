using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
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
            List<Store_Data> data = new List<Store_Data>();
            Store store = ProductService.GetSKUListByMain(salepage_id);
            data = store.Data;
            if (data.Count != 0)
            {
                return Json(store);
            }
            else
            {
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetMain(int salepage_id)
        {
            List<Store_Data> data = new List<Store_Data>();
            Store store = ProductService.GetMain(salepage_id);
            data = store.Data;
            if (data.Count != 0)
            {
                return Json(store);
            }
            else
            {
                return Json("");
            }
        }
        //[HttpPost]
        //public ActionResult GetStock(int salepage_id, int SkuId)
        //{
        //    Store store = ProductService.GetStock(salepage_id, SkuId);
        //    return Json(store);
        //}

        
    }
}