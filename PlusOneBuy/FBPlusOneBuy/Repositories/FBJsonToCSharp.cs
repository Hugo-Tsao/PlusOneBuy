using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Repositories
{
    public class From
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Datum
    {
        public DateTime created_time { get; set; }
        public From from { get; set; }
        public string story { get; set; }
        public string message { get; set; }
        public string id { get; set; }
    }

    public class Comments
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }

        //Comments裡面可以再包Comments的屬性，意思就是當作這個Comment(留言)的"回覆"，
        //但由於只能疊帶一層，所以暫時不在此加上Comments的屬性
    }

    public class RootObject
    {
        public Posts posts { get; set; }
        public Comments comments { get; set; }
        public string id { get; set; }
    }
    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class Posts
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }
}