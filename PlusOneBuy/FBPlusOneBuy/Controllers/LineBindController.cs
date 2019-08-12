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
            LineBindingService.InsertStoreManager(lineProfile);

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
        }
        [HttpPost]
        public void DelManager(int StoreManagerID)
        {
            LineBindingService.removeManagerGroup(StoreManagerID);
            LineBindingService.removeManager(StoreManagerID);
        }
    }
}