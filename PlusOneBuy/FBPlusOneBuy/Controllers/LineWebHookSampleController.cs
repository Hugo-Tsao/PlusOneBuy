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

                //設定ChannelAccessToken(或抓取Web.Config)
                this.ChannelAccessToken = channelAccessToken;
                //取得Line Event(範例，只取第一個)
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();

                DateTime time = DateTime.UtcNow.AddHours(8);
                //GroupID暫時先寫死(因為關係到權限和綁定)
                CampaignService campaignService = new CampaignService("Cdce46b42293efcd6ff973d08be1e0642");
                var result = campaignService.GetAllCampaign(time);

                Regex re;
                LineUserInfo UserInfo = null;

                //回覆訊息
                if (LineEvent.type == "message")
                {
                    string groupid = LineEvent.source.groupId;

                    if (LineEvent.message.type == "text")
                    {
                        /*
                        if (尋找群組(LineEvent))
                        {
                           正在進行的活動s=尋找符合此群組的活動並且正在進行(groupid)
                           for(正在進行的活動s)
                            {
                                if(正在進行活動)
                                {
                                    if(過濾留言(留言))
                                    {
                                        if(留言者是否在livecustomer)
                                        { no                                           
                                            新增會員
                                        }

                                        if(有沒有order(有)並且此團訂單數量(還沒滿))
                                        {yes
                                            新增group order(訂單,群組,活動,顧客,產品,數量,時間)
                                        }
                                        else
                                        {no
                                            新增訂單
                                            新增group order(訂單,群組,活動,顧客,產品,數量,時間)
                                        }                                      
                                    }
                                }                             
                            }
                        }*/


                        for (var i = 0; i < result.Count; i++)
                        {
                            string pattern = "^" + result[i].Keyword + "\\s?\\+\\d{1}\\s*$";
                            re = new Regex(pattern);

                            if (/*groupid.Equals(result[i].GroupID) && */re.IsMatch(LineEvent.message.text))
                            {
                                var qty = int.Parse(LineEvent.message.text.Substring(LineEvent.message.text.IndexOf("+", StringComparison.Ordinal)));
                                UserInfo = isRock.LineBot.Utility.GetGroupMemberProfile(LineEvent.source.groupId, LineEvent.source.userId, ChannelAccessToken);

                                DateTime timestampTotime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                                timestampTotime = timestampTotime.AddSeconds(LineEvent.timestamp / 1000).AddHours(8).ToLocalTime();

                                foreach (var AdminUserId in AdminUser)
                                {
                                    this.PushMessage(AdminUserId, "活動編號:" + result[i].CampaignID + "\n群組編號:\n" + LineEvent.source.groupId + "\n顧客編號:\n" + LineEvent.source.userId +
                                        "\n顧客照片:\n" + UserInfo.pictureUrl + "\n名字:" + UserInfo.displayName + "\n購買:" + result[i].Keyword + "\n數量:" + qty + "\n留言時間:" + timestampTotime);
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
                if (LineEvent.type == "join")
                {
                    this.PushMessage("Uc9d21bb74f13334be35b46b6581b9416", LineEvent.source.groupId);
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
