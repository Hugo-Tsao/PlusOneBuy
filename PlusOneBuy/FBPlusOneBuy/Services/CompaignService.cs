using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Services
{
    public class CampaignService
    {
        internal string GroupID { get; }

        public CampaignService(string groupId)
        {
            GroupID = groupId;
        }
        public List<Campaign> GetAllCampaign(DateTime time)
        {
            CampaignRepository Compaign_repo = new CampaignRepository();
            var result = Compaign_repo.GetALL(GroupID);
            result=result.Where((x) => DateTime.Compare(x.EndTime, time) > 0);
            return result.ToList();
        }

        public bool InsertCampaign(CampaignViewModel cvm)
        {
            try
            {
                CampaignRepository campaign_repo = new CampaignRepository();
                campaign_repo.InsertGroupBuy(cvm);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}