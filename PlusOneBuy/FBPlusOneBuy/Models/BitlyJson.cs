using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Models
{
    public class References
    {
        public string organization { get; set; }
        public string group { get; set; }

    }

    public class Group
    {
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public List<object> bsds { get; set; }
        public string guid { get; set; }
        public string organization_guid { get; set; }
        public string name { get; set; }
        public bool is_active { get; set; }
        public string role { get; set; }
        public References references { get; set; }
    }

    public class BitGroups
    {
        public List<Group> groups { get; set; }
    }

    public class BitShorten
    {
        public DateTime created_at { get; set; }
        public string id { get; set; }
        public string link { get; set; }
        public List<object> custom_bitlinks { get; set; }
        public string long_url { get; set; }
        public bool archived { get; set; }
        public List<object> tags { get; set; }
        public List<object> deeplinks { get; set; }
        public References references { get; set; }
    }
}