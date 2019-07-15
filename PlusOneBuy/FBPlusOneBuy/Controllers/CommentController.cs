using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPlusOneBuy.ViewModels;
using FBPlusOneBuy.Services;
using Newtonsoft.Json;

namespace FBPlusOneBuy.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            var current_liveposts = CommentListService.ListLivePosts();
            ViewData["Live"] = current_liveposts;
            return View();
        }
        [HttpPost]
        public ActionResult GetAllListPosts()
        {
            var current_liveposts = CommentListService.ListLivePosts();
            var result = JsonConvert.SerializeObject(current_liveposts);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetAllComments(string livePageId)
        {
            var commentsRoot= CommentListService.ListComments(livePageId);
            var result = JsonConvert.SerializeObject(commentsRoot);
            return Json(result);
        }

    }
}