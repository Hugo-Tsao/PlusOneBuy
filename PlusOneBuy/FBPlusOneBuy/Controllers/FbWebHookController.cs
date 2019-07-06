using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FBPlusOneBuy.Controllers
{
    public class FbWebHookController : ApiController
    {
        public string Get()
        {
            string hub_mode = "";
            string hub_challenge = "";
            string hub_verify_token = "";

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

            return hub_mode+ hub_challenge + hub_verify_token;
            //string result = Request.Params["hub.challenge"];
            //var resp = new HttpResponseMessage(HttpStatusCode.OK);
            //resp.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/plain");
            //return resp;
        }

        // GET: api/FbWebHook
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/FbWebHook/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [Route("api/caaaa")]
        [HttpPost]
        // POST: api/FbWebHook
        public void Post([FromBody]string value)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"recipient\":\"3032519476788720\"," +
                              "\"message\":\"HELLO\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
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
