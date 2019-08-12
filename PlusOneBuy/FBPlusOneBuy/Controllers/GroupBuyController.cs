using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Models;
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
            int id = LineBindingService.GGetIdByGroupId(LineGroupID);
            cvm.GroupID = id;
            cvm.PostTime = DateTime.UtcNow.AddHours(8);
            CampaignService campaignService = new CampaignService();
            campaignService.InsertCampaign(cvm);
            BotService.BotPushMsg(LineGroupID, cvm.Detail);
            return RedirectToAction("FanPageName", "Setting");
        }

        [HttpPost]
        public ActionResult PushMessageToLineGroup(int GroupOrderID)
        {
            
            string lineGroupId = string.Empty;
            string msg = BotService.SetMsgFormat(GroupOrderID, ref lineGroupId);
            BotService.BotPushMsg(lineGroupId,msg);
            return Json("OK");
        }

        [HttpPost]
        public ActionResult GetShoppingCartLink(int campaignId)
        {
            CampaignService campaignService = new CampaignService();
            Product product = campaignService.GetProductByCampaignID(campaignId);
            GroupOrderService groupOrderService = new GroupOrderService();
            int amount = groupOrderService.GetIsGroupORder(campaignId);
            string url = string.Empty;
            if (amount != 0)
            {
                url = FBSendMsgService.getAddToCartLink(product.ProductPageID, product.ProductID, amount);
            }
            else
            {
                return Json("nothing");
            }
            
            return Json(url);
        }

        [HttpPost]
        public ActionResult PushArrivedMessageToLineGroup(List<int> GroupOrderIDs,int campaignId)
        {
            string lineGroupId = string.Empty;
            GroupOrderService groupOrderService = new GroupOrderService();
            if (GroupOrderIDs==null)
            {
                GroupOrderIDs = groupOrderService.GetGroupOrderIds(campaignId);
            }
            foreach (int GroupOrderID in GroupOrderIDs)
            {
                string msg = BotService.SetArrivedMsgFormat(GroupOrderID, ref lineGroupId);
                BotService.BotPushMsg(lineGroupId, msg);

                groupOrderService.UpdateShipDateTime(GroupOrderID, DateTime.UtcNow.AddHours(8));
            }


            return Json("OK");
        }
    }
}