using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class SendMessageViewModel
    {
        public string LineGroupId { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    public class NameAndQtyViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}