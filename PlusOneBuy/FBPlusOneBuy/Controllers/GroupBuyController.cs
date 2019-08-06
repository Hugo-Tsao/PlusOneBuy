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
        [HttpPost]
        public ActionResult SettingPage(string LineGroupID,string GroupName)
        {
            ViewBag.GroupName = GroupName;
            ViewBag.LineGroupID = LineGroupID;
            return View();
        }
        [HttpPost]
        public ActionResult SettingCampaign(CampaignViewModel cvm,string LineGroupID)
        {
            cvm.PostTime = DateTime.Now;
            CampaignService campaignService = new CampaignService(LineGroupID);
            ViewData["result"] = campaignService.InsertCampaign(cvm);
            BotService.BotPushMsg(LineGroupID, cvm.Detail);
            return View();
        }
    }
}