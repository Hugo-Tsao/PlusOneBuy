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
        [HttpGet]
        public ActionResult LineBinding(string code)
        {
            string accessToken = LineRequestService.CodeToAccessToken(code);
            var lineProfile = LineRequestService.UseTokenToGetProfile(accessToken);

            var userId=LineBindingService.SearchLineID(lineProfile.userId);
            if (userId == 0)
            {
                LineBindingService.InsertStoreManager(lineProfile);
            }
            else
            {
                LineBindingService.UpdateManagerStatus(userId, "True");
            }


            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            string url = LineRequestService.Login();
            return Json(url);

        }
        [HttpPost]
        public void InsertGroupName(string aspNetUserId, string groupName)
        {
            int managerId = LineBindingService.GetManagerId(aspNetUserId);
            LineBindingService.InsertGroupName(managerId, groupName);
        }

        [HttpPost]
        public void DelNullGroup(int groupId)
        {
            LineBindingService.DelNullGroup(groupId);
        }
        [HttpPost]
        public void UpdateGroupStatus(int groupId,string Status)
        {
            LineBindingService.UpdateGroupStatus(groupId, Status);
            string LineGroupID = LineBindingService.GetLineGroupIDByID(groupId);
            BotService.LeaveGroup(LineGroupID);
        }
        [HttpPost]
        public void UpdateManagerStatus(int StoreManagerID,string Status)
        {
            LineBindingService.UpdateManagerStatus(StoreManagerID, Status);
            LineBindingService.UpdateManagerAllGroupStatus(StoreManagerID, Status);
        }
    }
}