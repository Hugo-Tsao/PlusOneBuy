﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.Models;
using FBPlusOneBuy.Repositories;

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
            if (lineGroup != default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}