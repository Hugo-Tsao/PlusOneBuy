using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.Repositories;

namespace FBPlusOneBuy.Controllers
{
    public class listController : Controller
    {
        // GET: list
        public ActionResult Index()
        {
            ProductRepositories productRepositories = new ProductRepositories();
            var product=productRepositories.GetAll();
            return View();
        }
    }
}