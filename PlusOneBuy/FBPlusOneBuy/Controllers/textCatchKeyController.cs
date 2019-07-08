using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBPlusOneBuy.Controllers
{
    public class textCatchKeyController : Controller
    {
        // GET: textCatchKey
        public ActionResult Index()
        {
            //var value = ConfigurationManager.AppSettings["X-API-KEY"];
            //ViewData["key"] = value;
            return View();
        }
    }
}