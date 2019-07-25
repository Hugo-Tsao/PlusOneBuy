using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Services;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Controllers
{
    public class ProductController : Controller
    {
        [HttpPost]
        public ActionResult GetSKUListByMain(string FilterType,int id_value)
        {
            List<ProductSKUList_Data> data = new List<ProductSKUList_Data>();

            if (FilterType == "salepage_id")
            {
                ProductSKUList store = ProductService.GetSKUListByMain(id_value);
                data = store.Data;

            }
            else if (FilterType == "shopCategoryId")
            {
                ProductCategoryViewModel pcvm = new ProductCategoryViewModel();
                pcvm.ShopCategoryId = id_value;
                ProductCategory pc = ProductService.GetSKUList(pcvm);
                data = pc.Data;
            }
            if (data != null)
            {
                return Json(data);
            }
            else
            {
                return Json("");
            }
        }
        [HttpPost]
        public ActionResult GetMain(ProductViewModel pvm)
        {
            ProductMain_Data data = new ProductMain_Data();
            ProductMain store = ProductService.GetMain(pvm.Salepage_id);
            data = store.Data;
            if (data != default(ProductMain_Data))
            {
                pvm.ProductName = data.Title;
                pvm.UnitPrice = data.Price;
                var currentProduct = ProductService.GetCurrentProducts();
                currentProduct.AddProduct(pvm);
                return Json(store);
            }
            else
            {
                return Json("");
            }
        }

        [HttpPost]
        public ActionResult UpdateKeyword(int skuId,string keyword)
        {

            if (ProductService.UpdateKeyword(skuId, keyword))
            {
                return Json("OK");
            }
            return Json("Failed");
        }
        //[HttpPost]
        //public ActionResult GetStock(int salepage_id, int SkuId)
        //{
        //    Store store = ProductService.GetStock(salepage_id, SkuId);
        //    return Json(store);
        //}

        [HttpPost]
        public ActionResult DeleteProduct(int skuId)
        {
            if (ProductService.DeleteProduct(skuId))
            {
                return Json(skuId);
            }

            return Json("Failed");
        }
    }
}