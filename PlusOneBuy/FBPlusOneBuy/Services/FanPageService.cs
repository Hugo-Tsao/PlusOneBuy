using FBPlusOneBuy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBPlusOneBuy.Services
{
    public class FanPageService
    {
        public static string GetToken(string userid)
        {
            var fpage_repo = new FanPagesRepository();
            var fpage = fpage_repo.SelectBinding(userid);
            string decodetoken = ReCodeService.Base64Decode(fpage.FbPageLongToken);

            string code = FBRequestService.LongTokenToCode(decodetoken);
            string tempuserToken = FBRequestService.CodeToLongToken(code);
            string pagetoken = FBRequestService.UserTokenToPageToken(fpage.FanPageID,tempuserToken);

            return pagetoken;
        }
        public static string GetPageName(string userid)
        {
            var fpage_repo = new FanPagesRepository();
            var fpage = fpage_repo.SelectBinding(userid);
            string pagename = fpage.FanPageName;
            return pagename;
        }

        public static string GetPageId(string userid)
        {
            var fpage_repo = new FanPagesRepository();
            var fpage = fpage_repo.SelectBinding(userid);
            string pageId = fpage.FanPageID;
            return pageId;
        }
    }
}