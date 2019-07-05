using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using FBPlusOneBuy.Repositories;
using RestSharp;

namespace FBPlusOneBuy.Services
{
    public class FBRequestService
    {
        public List<string> getLiveID(string fanPageName ,string token)
        {
            List<string> liveIDList = new List<string>();
            string url = "https://graph.facebook.com/v3.3/" + fanPageName + "?fields=posts&access_token=" + token;
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            //抓取完整封包
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
                    liveIDList.Add(item.id);
                }
            }
            return liveIDList;
        }
    }
}