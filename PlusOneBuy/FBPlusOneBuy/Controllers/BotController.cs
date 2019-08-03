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
        public ActionResult checkMeanger(string groupId, string managerUserId)
        {
            StoreMeanger StoreMeanger = BotService.CheckMeanger(groupId, managerUserId);
            if (StoreMeanger.message != null)
            {
                return Json("find",JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Not Found", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public void botPushCampaign(string groupId,string msg)
        {
            BotService.BotPushMsg(groupId, msg);
        }
    }
}