using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Services;
using FBPlusOneBuy.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Context = FBPlusOneBuy.Models.Context;

namespace FBPlusOneBuy.Controllers
{
    public class ProductController : Controller
    {
        public Context db = new Context();
        //等到登入功能完成，判斷目前登入是誰，並綁定了什麼ShopID
        public string storeUrl; //目前先寫死商場

        public ProductController()
        {
            var userID = User.Identity.GetUserId();
            storeUrl = db.AspNetUsers.FirstOrDefault(x => x.Id == userID)?.ShopID;
        }
        [HttpPost]
        public ActionResult GetSKUListByMain(string FilterType, int id_value)
        {
            //List<ProductSKUList_Data> data1 = new List<ProductSKUList_Data>();
            List<ProductStock_Data> data = new List<ProductStock_Data>();

            if (FilterType == "salepage_id")
            {
                //ProductSKUList store = ProductService.GetSKUListByMain(id_value);
                //data = store.Data;
                ProductStock store = ProductService.GetStock(id_value, storeUrl);
                data = store.Data;


            }
            else if (FilterType == "shopCategoryId")
            {
                ProductCategoryViewModel pcvm = new ProductCategoryViewModel();
                //pcvm.ShopCategoryId = id_value;
                ProductCategory pc = ProductService.GetSKUList(pcvm, storeUrl);
                var result = pc.Data.DistinctBy(x => x.Id);
                foreach (var products in result)
                {
                    ProductStock stock = ProductService.GetStock(products.Id, storeUrl);
                    if (stock.Data != null)
                    {
                        foreach (var item in stock.Data)
                        {
                            data.Add(item);
                        }
                    }
                }
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
            ProductMain store = ProductService.GetMain(pvm.Salepage_id, storeUrl);
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
        public ActionResult UpdateKeyword(int skuId, string keyword, int presetQty)
        {
            if (ProductService.UpdateProductInfo(skuId, keyword, presetQty))
            {
                return Json("OK");
            }
            return Json("Failed");
        }

        [HttpPost]
        public ActionResult DeleteProduct(int skuId)
        {
            if (ProductService.DeleteProduct(skuId))
            {
                return Json(skuId);
            }

            return Json("Failed");
        }

        [HttpGet]
        public ActionResult GetPresetQty()
        {
            var PresetQty = ProductService.GetCurrentProducts()
                .Select(x => new {ID = x.SkuId, PresetQty = x.PresetQty});
            return Json(PresetQty,JsonRequestBehavior.AllowGet);
        }
    }
}