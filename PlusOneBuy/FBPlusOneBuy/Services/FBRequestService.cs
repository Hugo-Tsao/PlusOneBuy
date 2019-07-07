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
        public static Dictionary<string, string> getLiveID(string fanPageName, string token)
        {
            //取得前十筆資料
            var postsNumber = 10;
            //建立最終回傳的資料型態
            Dictionary<string, string> liveIDList = new Dictionary<string, string>();
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
                if (item.story != null && item.story.Contains("is live now"))
                {
                    liveIDList.Add(item.id, item.message);
                }
            }
            return liveIDList;
        }

        //先做一次取全部留言 (預期在1000則留言以內)
        public static List<Datum> getAllComments(string liveID, string token)
        {
            var Comments = new List<Datum>();
            string url = "https://graph.facebook.com/v3.3/" + liveID + "?fields=comments&access_token=" + token;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JavaScriptSerializer js = new JavaScriptSerializer();
            RootObject data = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(response.Content);

            Comments = data.comments.data;

            return Comments;
        }

        //待寫
        public static List<OrderList> getNewOrderList(string liveID, string token, string keywords)
        {
            var orderList = new List<OrderList>();
            var allComments = getAllComments(liveID, token);
            if (allComments.Count != 0)
            {
                orderList = CommentFilterService.KeywordFilter(keywords, allComments, liveID);
            }
            return orderList;
        }

        public static string UTF8ConvertToString(string word)
        {
            byte[] byt = System.Text.UnicodeEncoding.Unicode.GetBytes(word);
            string result = UnicodeEncoding.Unicode.GetString(byt);
            return result;
        }
    }
}