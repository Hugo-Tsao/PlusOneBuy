using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;
using RestSharp;

namespace FBPlusOneBuy.Services
{
    public static class FBRequestService
    {
        public static Dictionary<string, string> getLivePageID(string fanPageName, string token)
        {
            //取得前十筆資料
            var postsNumber = 10;
            //建立最終回傳的資料型態
            Dictionary<string, string> livePageIDList = new Dictionary<string, string>();
            //連接的Url
            string url = "https://graph.facebook.com/v3.3/" + fanPageName + "?fields=posts.limit(" + postsNumber + ")&access_token=" + token;

            //建立RestClient和請求類型
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            //執行並抓取完整封包
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            //開始轉換Json至C#物件
            JavaScriptSerializer js = new JavaScriptSerializer();
            RootObject data = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(response.Content);

            //利用迴圈取得直播中的ID
            foreach (var item in data.posts.data)
            {
                //Console.WriteLine(item.created_time);
                //Console.WriteLine(item.story);
                //Console.WriteLine(item.id);

                //判斷此post是否是正在直播中的貼文
                if (item.story != null && (item.story.Contains("is live now") || item.story.Contains("現正直播") || item.story.Contains("正在直播")))
                {
                    livePageIDList.Add(item.id, item.message);
                }
            }
            return livePageIDList;
        }

        //先做一次取全部留言 (預期在1000則留言以內)
        public static List<Datum> getAllComments(string livePageID, string token)
        {
            var Comments = new List<Datum>();
            string url = "https://graph.facebook.com/v3.3/" + livePageID + "?fields=comments&access_token=" + token;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JavaScriptSerializer js = new JavaScriptSerializer();
            RootObject data = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(response.Content);

            if (data.comments != null)
            {
                Comments = data.comments.data;
            }
            return Comments;
        }
    }
}