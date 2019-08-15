using FBPlusOneBuy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using FBPlusOneBuy.DBModels;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Services
{
    public class CampaignService
    {
        internal CampaignRepository Campaign_repo;

        public CampaignService()
        {
            Campaign_repo = new CampaignRepository();
        }
        public List<Campaign> GetAllCampaign(string groupid,DateTime time)
        {
            var result = Campaign_repo.GetALL(groupid);
            result=result.Where((x) => DateTime.Compare(x.EndTime, time) > 0);
            return result.ToList();
        }

        public List<Campaign> GetWorkingCampaign(string groupid)
        {
            DateTime now = DateTime.UtcNow.AddHours(8);
            List<Campaign> result = Campaign_repo.GetALL(groupid).Where(x => DateTime.Compare(x.EndTime, now) > 0).ToList();
            return result;
        }

        public void InsertCampaign(CampaignViewModel cvm)
        {
            try
            {
                Campaign_repo.InsertGroupBuy(cvm);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static int[] GetStoreManagerIDs(string userId)
        {
            CampaignRepository campaign_repo = new CampaignRepository();
            int[] storeManagerIDs = campaign_repo.SelectStoreManagerIDs(userId);
            return storeManagerIDs;
        }
        public static List<LineListCampaignViewModel> GetAllCampaigns(int[] storeManagerID)
        {
            CampaignRepository campaign_repo = new CampaignRepository();
            var campaigns = campaign_repo.SelectCampaigns(storeManagerID);
            return campaigns;
        }

        public Product GetProductByCampaignID(int campaignId)
        {
            Product product = Campaign_repo.GetProductByCampaignID(campaignId);
            return product;
        }
        public CampaignViewModel GetCampaign(int campaignId)
        {
            CampaignViewModel campaign = Campaign_repo.SelectCampaign(campaignId);
            return campaign;
        }
    
    }
}