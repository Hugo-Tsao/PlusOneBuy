using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Services
{
    public class LineBindingService
    {
        public static void InsertStoreManager(LineProfile profile)
        {
            var sm_repo = new StoreManagerRepository();
            sm_repo.Insert(profile);
        }
        public static BindAcoountStoreManagerViewModel GetBindingStoreMamager(string userid)
        {
            var sm_repo = new StoreManagerRepository();
            var profile = sm_repo.SelectBinding(userid);
            return profile;
        }
    }
}