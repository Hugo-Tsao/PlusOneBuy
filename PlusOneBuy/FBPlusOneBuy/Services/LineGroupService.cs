using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Services
{
    public class LineGroupService
    {
        public static int GetManagerId(string aspNetUserId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            int managerId = lineGroup_repo.GetMangerIdByAspNetId(aspNetUserId);
            return managerId;
        }
        public static void InsertGroupName(int managerId, string groupName)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.InsertGroupName(managerId,groupName);
        }
        public static List<LineGroup> GetGroupList(int managerId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            List<LineGroup> groupName = lineGroup_repo.GetGroupList(managerId).ToList();
            return groupName;
        }
        public static List<string> GetNullGroup(int managerId)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            List<string> nullGroupName = lineGroup_repo.GetNullGroup(managerId).ToList();
            return nullGroupName;
        }
        public static List<CompareStoreManager> GroupNullCompare()
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            List<CompareStoreManager> userId = lineGroup_repo.GroupNullCompare();
            return userId;
        }
        public static void CompareUpdateGroupid(string groupId, int storeManagerId,DateTime? timestampTotime)
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            lineGroup_repo.CompareUpdateGroupid(groupId, storeManagerId, timestampTotime);
           
        }
        //public static DateTime CheckBinding(string groupName, int managerId)
        //{
        //    LineGroupRepository lineGroup_repo = new LineGroupRepository();
        //    DateTime joinDate =lineGroup_repo.CheckBinding(groupName, managerId);
        //    return joinDate;

        //}
    }
}