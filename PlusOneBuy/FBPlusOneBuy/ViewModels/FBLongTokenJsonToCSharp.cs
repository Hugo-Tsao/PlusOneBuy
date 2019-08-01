using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class ExangeLongToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
    public class LongTokenToCode
    {
        public string code { get; set; }
    }
    public class CodeToLongToken
    {
        public string access_token { get; set; }
        public string machine_id { get; set; }
        public string expires_in { get; set; }
    }
    public class UserTokenToPageToken
    {
        public string access_token { get; set; }
        public string id { get; set; }
    }
}