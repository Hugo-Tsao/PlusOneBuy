using FBPlusOneBuy.Models;
using FBPlusOneBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBPlusOneBuy.Controllers
{
    public class BotController : Controller
    {
        [HttpGet]
        public void checkMeanger(string groupId, string managerUserId)
        {
            StoreMeanger StoreMeanger = BotService.CheckMeanger(groupId, managerUserId);

        }
        [HttpPost]
        public void botPushCampaign(string groupId,string msg)
        {
            BotService.BotPushMsg(groupId, msg);
        }
    }
}