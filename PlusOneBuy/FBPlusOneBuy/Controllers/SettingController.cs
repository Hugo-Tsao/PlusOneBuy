using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace FBPlusOneBuy.Controllers
{
    public class SettingController : Controller
    {
        // GET: Setting
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string keyWord,string ProductName)
        {
            Session["keyWord"] = keyWord;
            ViewData["ProductName"] = ProductName;
            /*
             * 取得ProductName後判斷是否有庫存，如果有，導向+1蒐集頁面，如果沒有，導回此頁面。
             */
            return RedirectToAction("SelectWord");
        }


        public ActionResult SelectWord()
        {
            var keyWord = Session["keyWord"];
            ViewData["keyWord"] = keyWord;
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}