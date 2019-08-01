using FBPlusOneBuy.Services;
using isRock.LineBot;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace FBPlusOneBuy.Controllers
{
    public class LineBotWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        string channelAccessToken = ConfigurationManager.AppSettings["channelAccessToken"];
        //string AdminUserId = ConfigurationManager.AppSettings["AdminUserId"];
        List<string> AdminUser = new List<string>()
        { ConfigurationManager.AppSettings["AdminUserId"],ConfigurationManager.AppSettings["AdminUserId2"],ConfigurationManager.AppSettings["AdminUserId3"]};

        

        [Route("api/LineWebHook")]
        [HttpPost]
        public IHttpActionResult POST()
        {
            try
            {
                DateTime time = DateTime.UtcNow.AddHours(8);
                var result = CompaignService.GetCompaign(time);
                
                Regex re;
                LineUserInfo UserInfo = null;

                

                //設定ChannelAccessToken(或抓取Web.Config)
                this.ChannelAccessToken = channelAccessToken;
                //取得Line Event(範例，只取第一個)
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                //回覆訊息
                if (LineEvent.type == "message")
                {
                    string groupid = LineEvent.source.groupId;

                    if (LineEvent.message.type == "text")
                    {                        
                        for (var i = 0; i < result.Count; i++)
                        {
                            //string Keyword = ;
                            string pattern = "^" + result[i].Keyword + "\\s?\\+\\d{1}\\s*$";
                            re = new Regex(pattern);

                            if (groupid.Equals(result[i].GroupID) && re.IsMatch(LineEvent.message.text))
                            {
                                var qty = int.Parse(LineEvent.message.text.Substring(LineEvent.message.text.IndexOf("+", StringComparison.Ordinal)));
                                UserInfo = isRock.LineBot.Utility.GetGroupMemberProfile(LineEvent.source.groupId, LineEvent.source.userId, ChannelAccessToken);

                                //DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                                //dateTime = dateTime.AddSeconds(LineEvent.timestamp / 1000).ToLocalTime();

                                foreach (var AdminUserId in AdminUser)
                                {
                                    this.PushMessage(AdminUserId, "活動編號:"+result[i].CompaignID+"\n群組編號:\n" + LineEvent.source.groupId + "\n顧客編號:\n" + LineEvent.source.userId +
                                        "\n顧客照片:\n" + UserInfo.pictureUrl + "\n名字:" + UserInfo.displayName + "\n購買:" + result[i].Keyword + "\n數量:" + qty+"\n留言時間:\n"/*+dateTime*/);
                                }

                                this.PushMessage(LineEvent.source.userId, "恭喜你在買越多省越多成功下單" + "\n購買:" + result[i].Keyword + "\n數量:" + qty);
                            }
                        }
                        
                        

                    }
                    if (LineEvent.message.type == "sticker")
                    {
                        return Ok();
                    }                    
                    if (LineEvent.message.type == "image")
                    {
                        return Ok();
                    }
                }
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                foreach (var AdminUserId in AdminUser)
                {
                    this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                }
                    //如果發生錯誤，傳訊息給Admin
                    
                //response OK
                return Ok();
            }
        }
    }
}
