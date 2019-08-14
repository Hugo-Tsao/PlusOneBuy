using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FBPlusOneBuy.DBModels;
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
            int id = LineBindingService.GetIdByGroupId(LineGroupID);
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
            GroupOrderService groupOrderService = new GroupOrderService();
            groupOrderService.UpdateBtnGroupClickDateTime(GroupOrderID, DateTime.UtcNow.AddHours(8));

            return Json("OK");
        }

        [HttpPost]
        public ActionResult GetShoppingCartLink(int campaignId, string groupOrderID, int productGroup)
        {
            CampaignService campaignService = new CampaignService();
            Product product = campaignService.GetProductByCampaignID(campaignId);
            GroupOrderService groupOrderService = new GroupOrderService();
            List<int> groupOrderIDs = new List<int>();
            int amount = 0;
            if (string.IsNullOrEmpty(groupOrderID))
            {
                amount = groupOrderService.GetIsGroupORder(campaignId);
                groupOrderIDs= groupOrderService.GetGroupOrderIdsByOrderIsNull(campaignId);
                
            }
            else
            {
                amount = productGroup;
                groupOrderIDs.Add(Convert.ToInt32(groupOrderID));
            }

            string url = string.Empty;
            if (amount != 0)
            {
                DateTime dtNow = DateTime.UtcNow.AddHours(8);
                url = FBSendMsgService.getAddToCartLink(product.ProductPageID, product.ProductID, amount);
                foreach (int id in groupOrderIDs)
                {
                    groupOrderService.UpdateBtnOrderClickDateTime(id, dtNow);
                }
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
                GroupOrderIDs = groupOrderService.GetGroupOrderIdsByShipIsNull(campaignId);
                if (GroupOrderIDs.Count == 0)
                {
                    return Json("nothing");
                }
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