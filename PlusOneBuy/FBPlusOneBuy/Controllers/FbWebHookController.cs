using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FBPlusOneBuy.Models;
using RestSharp;

namespace FBPlusOneBuy.Controllers
{
    public class FbWebHookController : ApiController
    {
        // GET: api/FbWebHook
        public int Get()
        {          
            string hub_mode = "";
            string hub_challenge = "";
            string hub_verify_token = "";
            string my_verify_token = "abcdedfg";
            int result = -1;

            try
            {
                IEnumerable<KeyValuePair<string, string>> queryString = Request.GetQueryNameValuePairs();

                foreach (KeyValuePair<string, string> item in queryString)
                {
                    switch (item.Key)
                    {
                        case "hub.mode":
                            hub_mode = item.Value;
                            break;
                        case "hub.challenge":
                            hub_challenge = item.Value;
                            break;
                        case "hub.verify_token":
                            hub_verify_token = item.Value;
                            break;
                    }
                }
                
            }
            catch (Exception ex)
            {
            }
            if (hub_verify_token == my_verify_token)
            {
                result = Convert.ToInt32(hub_challenge);
            }
            return result;


            //return hub_mode+ hub_challenge + hub_verify_token;
            //string result = Request.Params["hub.challenge"];
            //var resp = new HttpResponseMessage(HttpStatusCode.OK);
            //resp.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/plain");
            //return resp;
        }

        [Route("api/abcd")]
        public string Getapple()
        {

            var sendMsg = new FbSendMessage.SendObject()
            {
                message=new FbSendMessage.Message { text="HI"},
                recipient= new FbSendMessage.Recipient { id="12345"}
            };
            var result = JsonConvert.SerializeObject(sendMsg);
            
            
            
            return result;


        }


        [HttpPost]
        // POST: api/FbWebHook
        public HttpStatusCode Post()
        {
            var msg = new FbSendMessage.SendObject()
            {
                message = new FbSendMessage.Message { text = "HI" },
                recipient = new FbSendMessage.Recipient { id = "3032519476788720" }
            };
            var jsonMsg= JsonConvert.SerializeObject(msg);


            var client = new RestClient("https://graph.facebook.com/v3.3/me/messages?access_token=EAASxbKYYpHoBANiN3ZCn5MHw1Bv7p6O8kSirivuVBUFJoYsVangrQk7Mb2XyKGUjNiPSnXuRQIzpSUx3Ryba6wg1uQeE9JzxZAQjSojZAX0OndZCJ0rXxtgZCUqgGVp6BkSYUtAZA1wbadkjzZClIcQMUToO2nGNqh8LxhrQaZCAtyC2h2aQZBZAtL");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("content-length", "92");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("Host", "graph.facebook.com");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", jsonMsg, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.StatusCode;





            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://graph.facebook.com/v3.3/me/messages?access_token=EAASxbKYYpHoBAKWHpnQKL30NY7TcJc4REzHzo94C2iBeyRiJs3Ai5cZBlsh3sZBZCz5W4yaPL0WVnym0UWZBLS4jGUffZC14YZAmvKLUYMFgtDbNYZBubMWHCzL8ZBYXBSKWnkZCJguFkTZCu06fqjRJkKKMOYW3MN5SBQW9eA5Kr7qX9LfzAw3ON4uRMpD7M2MaPv8wDrWGwsYQZDZD");
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    string json = "{\"recipient\":\"3032519476788720\"," +
            //                  "\"message\":\"HELLO\"}";

            //    streamWriter.Write(json);
            //}

            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    var result = streamReader.ReadToEnd();
            //}
            //return Ok();
        }

        // PUT: api/FbWebHook/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FbWebHook/5
        public void Delete(int id)
        {
        }
    }
}
