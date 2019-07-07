using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Models
{
    public class FbSendMessage
    {
        public class SendObject
        {
            public Recipient recipient { get; set; }
            public Message message { get; set; }
        }
        public class Recipient
        {
            public string id { get; set; }
        }

        public class Message
        {
            public string text { get; set; }
        }

        
    }
}