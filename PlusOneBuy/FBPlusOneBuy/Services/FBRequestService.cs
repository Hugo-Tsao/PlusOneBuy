using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static Dictionary<string, string> getLivePageID(string token)
        {
            //取得前十筆資料
            var postsNumber = 10;
            //建立最終回傳的資料型態
            Dictionary<string, string> livePageIDList = new Dictionary<string, string>();
            //連接的Url
            string url = "https://graph.facebook.com/v3.3/me?fields=posts.limit(" + postsNumber + ")&access_token=" + token;

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

        public static int GetLiveVideoViews(string livePageID,string token)
        {
            string videoid;
            string[] pageid_and_videoid = livePageID.Split('_');
            videoid = pageid_and_videoid[1];
            var liveVideos = new List<LiveVideoDatum>();
            string url = "https://graph.facebook.com/v3.3/me/live_videos?fields=status,description,creation_time,live_views,video&access_token=" + token;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JavaScriptSerializer js = new JavaScriptSerializer();
            LiveVideoRoot livedatas = Newtonsoft.Json.JsonConvert.DeserializeObject<LiveVideoRoot>(response.Content);

            if (livedatas.data != null)
            {
                liveVideos = livedatas.data;
                //Comments = data.comments.data;
            }
            int views = liveVideos.FirstOrDefault(x => x.video.id == videoid).live_views;
            return views;
        }
        public static string GetLongToken(string shortToken)
        {
            //string url = "https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=1320980108059770&client_secret=b452a9a47c4e19effbf8b7e804f0ebb2&fb_exchange_token="+ shortToken;
            string clientId = ConfigurationManager.AppSettings["fb_client_id"];
            string clientSecret = ConfigurationManager.AppSettings["fb_client_secret"];
            string url = "https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + clientId + "&client_secret=" + clientSecret + "&fb_exchange_token=" + shortToken;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JavaScriptSerializer js = new JavaScriptSerializer();
            ExangeLongToken exangeResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ExangeLongToken>(response.Content);
            return exangeResponse.access_token;

        }
        public static void DeleteTokenPermissions(string token)
        {
            string url = "https://graph.facebook.com/v3.3/me/permissions?access_token="+token;

            var client = new RestClient(url);
            var request = new RestRequest(Method.DELETE);

            IRestResponse response = client.Execute(request);

        }
        public static string LongTokenToCode(string longToken)
        {
            string clientId = ConfigurationManager.AppSettings["fb_client_id"];
            string clientSecret = ConfigurationManager.AppSettings["fb_client_secret"];
            string redirectUri= ConfigurationManager.AppSettings["fb_redirect_uri"];
            string url = "https://graph.facebook.com/oauth/client_code?access_token="+ longToken + "&client_secret="+ clientSecret + "&redirect_uri="+ redirectUri + "&client_id="+ clientId;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JavaScriptSerializer js = new JavaScriptSerializer();
            LongTokenToCode exangeResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LongTokenToCode>(response.Content);
            return exangeResponse.code;

        }
        public static string CodeToLongToken(string code)
        {
            string clientId = ConfigurationManager.AppSettings["fb_client_id"];
            string redirectUri = ConfigurationManager.AppSettings["fb_redirect_uri"];
            string url = "https://graph.facebook.com/oauth/access_token?code="+code+"&client_id="+ clientId + "&redirect_uri="+ redirectUri;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JavaScriptSerializer js = new JavaScriptSerializer();
            CodeToLongToken exangeResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CodeToLongToken>(response.Content);
            return exangeResponse.access_token;

        }

        public static string UserTokenToPageToken(string pageid,string usertoken)
        {
            string url = "https://graph.facebook.com/v3.3/" + pageid + "?fields=access_token&access_token=" + usertoken;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JavaScriptSerializer js = new JavaScriptSerializer();
            UserTokenToPageToken exangeResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserTokenToPageToken>(response.Content);
            return exangeResponse.access_token;

        }

    }
}