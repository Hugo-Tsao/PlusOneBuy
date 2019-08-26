﻿using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using FBPlusOneBuy.DBModels;

namespace FBPlusOneBuy.Services
{
    public class LineBindingService
    {

        public static string GetLineGroupIDByID(int groupId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            string LineGroupID = lineGroup_repo.GetLineGroupIDByID(groupId);
            return LineGroupID;
        }

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
            List<LineGroup> result = new List<LineGroup>();
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            var nullGroupName = lineGroup_repo.GetNullGroup(managerId);
            if (nullGroupName != null)
            {
                result = nullGroupName.ToList();
            }
            return result;
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
        public static int GetIdByGroupId(string groupId)
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

        public static void UpdateManagerAllGroupStatus(int StoreManagerID, string status)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.UpdateManagerAllGroupStatus(StoreManagerID, status);
        }
        public static void UpdateBotGroupStatus(int groupId, string status,DateTime time)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.UpdateBotGroupStatus(groupId, status, time);
        }
        public static void UpdateBotGroup(int groupId, string LineGroupID ,string status, DateTime time)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.UpdateBotGroup(groupId, LineGroupID, status, time);
        }

    }
}