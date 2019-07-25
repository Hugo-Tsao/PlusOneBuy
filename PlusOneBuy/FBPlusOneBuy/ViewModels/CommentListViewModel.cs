using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.ViewModels
{
    public class CommentsRoot
    {
        public LivePostInfo postInfos { get; set; }
        public List<CommentListViewModel> comments { get; set; }
    }
    public class LivePostInfo
    {
        public string LiveName { get; set; }
        public List<string> ProductsName { get; set; }
        public DateTime postTime { get; set; }
        public DateTime endTime { get; set; }
    }
    public class CommentListViewModel
    {        
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string Keyword { get; set; }
        public string LiveName { get; set; }
        public DateTime postTime { get; set; }
        public DateTime endTime { get; set; }
        


    }
}