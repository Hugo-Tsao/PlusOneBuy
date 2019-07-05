using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Repositories;

namespace FBPlusOneBuy.Services
{
    public class CommentFilterService
    {
        public List<Datum> KeywordFilter(List<Datum> datas)
        {
            List<Datum> result = new List<Datum>();
            foreach (var data in datas)
            {
                if (data.message.Equals("+1"))
                {
                    result.Add(data);
                }
            }
            return result;
        }
    }
}