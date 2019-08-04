using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.Services;
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
            CampaignService campaignService = new CampaignService("Cdce46b42293efcd6ff973d08be1e0642");
            ViewData["result"] = campaignService.InsertCampaign(cvm);
            return View();
        }
    }
}