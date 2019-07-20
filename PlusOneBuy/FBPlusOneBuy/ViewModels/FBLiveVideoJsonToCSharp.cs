using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class Video
    {
        public string id { get; set; }
    }

    public class LiveVideoDatum
    {
        public string status { get; set; }
        public DateTime creation_time { get; set; }
        public int live_views { get; set; }
        public Video video { get; set; }
        public string id { get; set; }
        public string description { get; set; }
    }

    public class LiveVideoRoot
    {
        public List<LiveVideoDatum> data { get; set; }
    }
}