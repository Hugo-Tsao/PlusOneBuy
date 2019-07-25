using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.ViewModels;
using FBPlusOneBuy.Repositories;

namespace FBPlusOneBuy.Services
{
    public class CommentListService
    {
        public static List<LivePostViewModel> ListLivePosts()
        {
            var live_repo = new LivePostsRepository();
            var current_liveposts=live_repo.Select();
            return current_liveposts;
        }
        public static CommentsRoot ListComments(string livePageId)
        {
            var live_repo = new LivePostsRepository();
            var current_comments = live_repo.SelectComments(livePageId);
            DateTime start, end;
            string livename;
            start= current_comments.Min((x) => x.postTime);
            end =current_comments.Max((x) => x.endTime);
            livename = current_comments.Select((x)=>x.LiveName).First();
            var productsNameList = current_comments.Select((x) => x.ProductName).Distinct().ToList();
            var cmtsRoot = new CommentsRoot()
            {
                comments= current_comments,
                postInfos=new LivePostInfo {LiveName= livename, postTime=start,endTime=end, ProductsName= productsNameList}
            };
            return cmtsRoot;

        }
    }
}