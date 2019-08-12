using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.DBModels;


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

    }
}