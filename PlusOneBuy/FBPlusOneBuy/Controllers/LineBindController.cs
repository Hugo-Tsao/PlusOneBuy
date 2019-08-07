using FBPlusOneBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Models;

namespace FBPlusOneBuy.Controllers
{
    public class LineBindController : Controller
    {
        [HttpPost]
        public void InsertGroupName(string aspNetUserId, string groupName)
        {
            int managerId = LineBindingService.GetManagerId(aspNetUserId);
            LineBindingService.InsertGroupName(managerId, groupName);
        }

        //[HttpGet]
        //public ActionResult checkMeanger(string groupId, string managerUserId)
        //{
        //    StoreMeanger StoreMeanger = BotService.CheckMeanger(groupId, managerUserId);
        //    if (StoreMeanger.message != null)
        //    {
        //        return Json("Find", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json("Not Find", JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}