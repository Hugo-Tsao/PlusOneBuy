using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Services
{
    public class CompaignService
    {
        public static List<Compaign> GetCompaign(DateTime time)
        {
            CompaignRepository Compaign_repo = new CompaignRepository();
            var result = Compaign_repo.GetALL();
            result=result.Where((x) => DateTime.Compare(x.EndTime, time) > 0);
            return result.ToList();
        }
    }
}