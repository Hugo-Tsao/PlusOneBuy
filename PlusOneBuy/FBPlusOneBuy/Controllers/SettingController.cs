using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using FBPlusOneBuy.Services;
using FBPlusOneBuy.Repositories;
using Newtonsoft.Json;
using FBPlusOneBuy.Models;
using Microsoft.AspNet.Identity;

namespace FBPlusOneBuy.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FanPageName()
        {
            string userid = User.Identity.GetUserId();
            var fpage_repo = new FanPagesRepository();
            var fpage = fpage_repo.SelectBinding(userid);

            ViewBag.bindingPage = fpage;
            return View();
        }
        [HttpPost]
        public void GetFanPageData(string token, string fanpageid, string fanpagename)
        {
            //Session["token"] = token;
            NewFanPage(fanpageid, fanpagename,token);
            //Session["token"] = "EAASxbKYYpHoBAI27CZBoK8ZBzFmJjEMIR30woKcIfDPx4mtljSUOsGxVGsKHmy1JgCay8KTilT9l3nbkSfGzBZC6wVSDUcl3ZAa7C5OyZAv8CV7K0duuyW2jHFGqZCwhIKiM6jPonrHLp7s5UEudWL5UHkT8IuZBGmBTOEHS0IjYZCsYbcQfo3j9";
            //Session["FanPageID"] = fanpageid;
            //Session["fanpagename"] = fanpagename;
            //return Json("OK");

        }
        [HttpPost]
        public void RemovePageBind(string fanpagename)
        {
            var fpage_repo = new FanPagesRepository();
            string userid = User.Identity.GetUserId();
            string tokenValue = null;
            string userIdValue = null;
            var fpage = fpage_repo.Select(userid,fanpagename);
            string decodeToken = ReCodeService.Base64Decode(fpage.FbPageLongToken);
            FBRequestService.DeleteTokenPermissions(decodeToken);
            fpage_repo.Update(fanpagename, userid, tokenValue, userIdValue);

        }
        public ActionResult Video()
        {
            string userid = User.Identity.GetUserId();

            //var fpage_repo = new FanPagesRepository();

            //string token = Session["token"].ToString();

            //var fpage = fpage_repo.SelectBinding(userid);
            //string decodetoken = ReCodeService.Base64Decode(fpage.FbPageLongToken);
            string token=FanPageService.GetToken(userid);
            var result = FBRequestService.getLivePageID(token);
            ViewData["liveIDList"] = result;
            ViewData["fanpagename"] = FanPageService.GetPageName(userid);
            return View();
        }
        public void NewFanPage(string fanpageid, string fanpagename, string token)
        {
            string userid = User.Identity.GetUserId();
            var fpage_repo = new FanPagesRepository();
            if (!fpage_repo.isExist(fanpageid))
            {
                string longToken = FBRequestService.GetLongToken(token);

                //byte[] longToken_bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(longToken);
                //編成 Base64 字串
                //string encodeToken = Convert.ToBase64String(longToken_bytes);
                string encodeToken = ReCodeService.Base64Encode(longToken);
                fpage_repo.Insert(fanpageid, fanpagename, userid, encodeToken);
            }
            else
            {
                string longToken = FBRequestService.GetLongToken(token);
                string encodeToken = ReCodeService.Base64Encode(longToken);
                fpage_repo.Update(fanpageid, userid, encodeToken);

            }
        }



    }
}