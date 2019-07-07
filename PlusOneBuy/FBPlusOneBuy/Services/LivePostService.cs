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
        public static void CreateLivePost(string liveID/*, string liveName*/)
        {
            var livepost_repo = new LivePostsRepository();
            var livepost = new LivePost()
            {
                LiveID = liveID,
                //LiveName = liveName,
                FanPageID = 1, //目前只有一個粉絲團，所以也暫時寫死
                postTime = DateTime.Now
            };
            livepost_repo.Create(livepost);
        }
    }
}