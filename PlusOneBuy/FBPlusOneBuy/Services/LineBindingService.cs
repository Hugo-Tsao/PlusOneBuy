using FBPlusOneBuy.Models;
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

        public static int GetManagerId(string aspNetUserId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            int managerId = lineGroup_repo.GetMangerIdByAspNetId(aspNetUserId);
            return managerId;
        }
        public static void InsertGroupName(int managerId, string groupName)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.InsertGroupName(managerId, groupName);
        }
        public static List<LineGroup> GetGroupList(int managerId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            List<LineGroup> groupName = lineGroup_repo.GetGroupList(managerId).ToList();
            return groupName;
        }
        public static List<LineGroup> GetNullGroup(int managerId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            List<LineGroup> nullGroupName = lineGroup_repo.GetNullGroup(managerId).ToList();
            return nullGroupName;
        }
        public static List<CompareStoreManager> GroupNullCompare()
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            List<CompareStoreManager> userId = lineGroup_repo.GroupNullCompare();
            return userId;
        }
        public static void CompareUpdateGroupid(string groupId, int storeManagerId, DateTime? timestampTotime)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.CompareUpdateGroupid(groupId, storeManagerId, timestampTotime);

        }
        public static int GGetIdByGroupId(string groupId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            int id=lineGroup_repo.GetIdByGroupId(groupId);
            return id;
        }

        public static void DelNullGroup(int groupId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.DelNullGroup(groupId);
        }
        public static void UpdateGroupStatus(int groupId, string status)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.UpdateGroupStatus(groupId, status);
        }
        public static void removeManager(int StoreManagerID)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.removeManager(StoreManagerID);
        }
        public static void removeManagerGroup(int StoreManagerID)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.removeManagerGroup(StoreManagerID);
        }

    }
}