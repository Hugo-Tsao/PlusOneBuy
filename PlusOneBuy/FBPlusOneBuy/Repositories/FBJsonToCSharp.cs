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