using FBPlusOneBuy.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Repositories;
using Microsoft.AspNet.Identity;

namespace FBPlusOneBuy.Services
{
    public class LineRequestService
    {
        public static string Login()
        {
            string clientId = ConfigurationManager.AppSettings["line_client_id"];
            string clientSecret = ConfigurationManager.AppSettings["line_client_secret"];
            string redirectUri = ConfigurationManager.AppSettings["line_redirect_uri"];
            string state = "abcde";
            string url = "https://access.line.me/oauth2/v2.1/authorize?response_type=code&client_id="+ clientId + "&redirect_uri="+ redirectUri + "&state="+ state + "&scope=openid%20profile%20email";

            //var client = new RestClient(url);
            //var request = new RestRequest(Method.GET);

            //IRestResponse response = client.Execute(request);
            return url;

            //UserTokenToPageToken exangeResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserTokenToPageToken>(response.Content);
            //return exangeResponse.access_token;

        }
        public static string CodeToAccessToken(string code)
        {
            string clientId = ConfigurationManager.AppSettings["line_client_id"];
            string clientSecret = ConfigurationManager.AppSettings["line_client_secret"];
            string redirectUri = ConfigurationManager.AppSettings["line_redirect_uri"];
            var client = new RestClient("https://api.line.me/oauth2/v2.1/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "grant_type=authorization_code&code="+code+"&redirect_uri="+redirectUri+"&client_id="+clientId+"&client_secret="+clientSecret, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            LineCodeToAccessToken exangeResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LineCodeToAccessToken>(response.Content);

            return exangeResponse.access_token;
        }
        public static LineProfile UseTokenToGetProfile(string token)
        {
            var client = new RestClient("https://api.line.me/v2/profile");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer "+token);
            IRestResponse response = client.Execute(request);
            LineProfile exangeResponse= Newtonsoft.Json.JsonConvert.DeserializeObject<LineProfile>(response.Content);
            return exangeResponse;
        }
        public static void InsertStoreManager(LineProfile profile)
        {
            var sm_repo = new StoreManagerRepository();
            sm_repo.Insert(profile);
        }
        public static LineProfile GetBindingStoreMamager(string userid)
        {
            var sm_repo = new StoreManagerRepository();
            var profile=sm_repo.SelectBinding(userid);
            return profile;
        }


    }
}