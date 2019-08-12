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

        internal  string GroupId { get; set; }

        public LineGroupService(string groupId)
        {
            GroupId = groupId;
        }
        public bool SearchLineGroup()
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            LineGroup lineGroup = lineGroup_repo.SearchLineGroup(GroupId);
            if (lineGroup != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public LineGroup GetGroupByID()
        {
            LineGroupRepository lineGroup_repo = new LineGroupRepository();
            LineGroup lineGroup = lineGroup_repo.SearchLineGroup(GroupId);
            return lineGroup;
        }

    }
}