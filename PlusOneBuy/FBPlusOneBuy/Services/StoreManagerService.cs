using FBPlusOneBuy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Services
{
    public class StoreManagerService
    {
        public static int SearchLineID(string lineID)
        {
            StoreManagerRepository storeManagerRepo = new StoreManagerRepository();
            int storeManagerId = storeManagerRepo.SearchLineID(lineID);
            return storeManagerId;
        }
        public static int GetManagerId(string aspNetUserId)
        {
            StoreManagerRepository storeManagerRepo = new StoreManagerRepository();
            int managerId = storeManagerRepo.GetMangerIdByAspNetId(aspNetUserId);
            return managerId;
        }
        public static void UpdateManagerStatus(int StoreManagerID, string status)
        {
            StoreManagerRepository storeManagerRepo = new StoreManagerRepository();
            storeManagerRepo.UpdateManagerStatus(StoreManagerID, status);
        }
    }
}