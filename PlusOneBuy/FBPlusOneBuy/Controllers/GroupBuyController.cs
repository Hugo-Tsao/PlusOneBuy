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
            CampaignService campaignService = new CampaignService();
            ViewData["result"] = campaignService.InsertCampaign(cvm);
            return View();
        }
    }
}