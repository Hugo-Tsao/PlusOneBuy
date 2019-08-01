using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Services
{
    public class ReCodeService
    {
        public static string Base64Encode(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(text);
            //編成 Base64 字串
            string encode = Convert.ToBase64String(bytes);
            return encode;
        }
        public static string Base64Decode(string text)
        {
            string decode = System.Text.Encoding.GetEncoding("utf-8").GetString(Convert.FromBase64String(text));
            return decode;
        }
    }
}