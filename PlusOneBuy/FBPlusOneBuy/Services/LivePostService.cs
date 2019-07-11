using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;

namespace FBPlusOneBuy.Services
{
    public static class LivePostService
    {
        public static void CreateLivePost(string livePageID, string liveName/*, string fanPageName*/)
        {
            Context context = new Context();
            //var fanPageID = context.FanPages.FirstOrDefault(x => x.FanPageName == fanPageName);
            var livepost_repo = new LivePostsRepository();
            var livepost = new LivePost()
            {
                LivePageID = livePageID,
                LiveName = liveName,

                FanPageID = 1, //目前只有一個粉絲團，所以也暫時寫死，之後可選粉絲團後改fanPageID
                postTime = DateTime.Now
            };
            livepost_repo.Create(livepost);
        }
    }
}