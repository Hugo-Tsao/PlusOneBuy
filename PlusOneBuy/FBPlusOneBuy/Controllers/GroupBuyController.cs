using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Controllers
{
    public class GroupBuyController : Controller
    {
        [HttpGet]
        public ActionResult SettingPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SettingPage(CampaignViewModel cvm)
        {
            CampaignRepository compaign_repo = new CampaignRepository();
            ViewData["result"] = compaign_repo.InsertGroupBuy(cvm);
            return View();
        }
    }
}