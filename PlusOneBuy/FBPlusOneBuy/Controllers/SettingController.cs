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
            return View();
        }
        [HttpPost]
        public ActionResult GetFanPageData(string token, string fanpageid, string fanpagename)
        {
            Session["token"] = token;
            NewFanPage(fanpageid, fanpagename);
            //Session["token"] = "EAASxbKYYpHoBAI27CZBoK8ZBzFmJjEMIR30woKcIfDPx4mtljSUOsGxVGsKHmy1JgCay8KTilT9l3nbkSfGzBZC6wVSDUcl3ZAa7C5OyZAv8CV7K0duuyW2jHFGqZCwhIKiM6jPonrHLp7s5UEudWL5UHkT8IuZBGmBTOEHS0IjYZCsYbcQfo3j9";
            Session["FanPageID"] = fanpageid;
            Session["fanpagename"] = fanpagename;
            return Json(Url.Action("video", "Setting"));

        }
        public ActionResult Video()
        {
            string token = Session["token"].ToString();
            var result = FBRequestService.getLivePageID(token);
            ViewData["liveIDList"] = result;
            ViewData["fanpagename"] = Session["fanpagename"];
            return View();
        }
        public void NewFanPage(string fanpageid, string fanpagename)
        {
            var fpage_repo = new FanPagesRepository();
            if (!fpage_repo.isExist(fanpageid))
            {
                fpage_repo.Insert(fanpageid, fanpagename);
            }
        }


        
    }
}