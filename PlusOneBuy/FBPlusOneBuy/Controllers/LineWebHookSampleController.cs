using FBPlusOneBuy.Models;
using FBPlusOneBuy.Services;
using isRock.LineBot;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using FBPlusOneBuy.Repositories;
using FBPlusOneBuy.ViewModels;

namespace FBPlusOneBuy.Controllers
{
    public class LineBotWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        string channelAccessToken = ConfigurationManager.AppSettings["channelAccessToken"];
        
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
                //CampaignService campaignService = new CampaignService("Cdce46b42293efcd6ff973d08be1e0642");
                //var result = campaignService.GetAllCampaign(time);

                Regex re;
                LineUserInfo UserInfo = null;

                //回覆訊息
                if (LineEvent.type == "message")
                {




                   
                    if (LineEvent.source.groupId != null)
                        {
                            //使用者ID和群組ID
                            string userId = LineEvent.source.userId;
                            string groupId = LineEvent.source.groupId;
                            UserInfo = isRock.LineBot.Utility.GetGroupMemberProfile(groupId, userId, ChannelAccessToken);
                            LineGroupService lineGroupService = new LineGroupService(groupId);
                            if (lineGroupService.SearchLineGroup())  //尋找群組
                            {
                                //取得正在執行的活動
                                CampaignService campaignService = new CampaignService();
                                List<Campaign> campaigns = campaignService.GetWorkingCampaign(groupId);  

                                foreach (Campaign campaign in campaigns)
                                {
                                    //取得留言數量(組)
                                    ////因為目前Line團購只有+1+2的關鍵字，所以在第三個參數(keywordPattern)寫+1的Pattern
                                    int number = CommentFilterService.KeywordFilter(LineEvent.message.text,campaign.Keyword,"+1");
                                    if (number != 0 && number <= campaign.ProductGroup)
                                    {
                                        //將搜尋到或新增的Customer存在lineCustomer裡
                                        LineCustomerViewModel lineCustomer = new LineCustomerViewModel();
                                        //搜尋是此訊息的人是否已存在LineCustomer資料表中
                                        if (!BotService.SearchLineCustomer(userId, UserInfo.displayName,ref lineCustomer))
                                        {
                                            //否，新增一筆
                                            lineCustomer = BotService.InsertLineCustomer(userId, UserInfo.displayName);
                                        }
                                        DateTime messageDateTime = BotService.TimestampToDateTime(LineEvent.timestamp);
                                        
                                        //開始建訂單
                                        GroupOrderViewModel groupOrder = new GroupOrderViewModel();
                                        GroupOrderService groupOrderService = new GroupOrderService();
                                        Product product = new Product();
                                        product = ProductService.GetProductById(campaign.ProductID);

                                        //若此留言要成單，所需的成團數量空間
                                        int remaningNumber = campaign.ProductGroup - number;
                                        //如果訂單人數還未滿，就拿此單來用，否則建新的
                                        if (groupOrderService.GetGroupOrder(campaign.CampaignID, remaningNumber, ref groupOrder))
                                        {
                                                decimal amount = product.UnitPrice * number;
                                                groupOrderService.InsertGroupOrder(campaign.CampaignID, messageDateTime, campaign.ProductGroup);
                                                groupOrderService.GetGroupOrder(campaign.CampaignID, remaningNumber, ref groupOrder);
                                        }

                                        //條件都達成，開始下單
                                        GroupOrderDetail groupOrderDetail = new GroupOrderDetail()
                                        {
                                            GroupOrderID = groupOrder.GroupOrderID,
                                            LineCustomerID = lineCustomer.LineCustomerID,
                                            ProductName = product.ProductName,
                                            UnitPrice = product.UnitPrice,
                                            Quantity = number,
                                            MessageDateTime = messageDateTime
                                        };
                                        GroupOrderDetailService groupOrderDetailService = new GroupOrderDetailService();
                                        groupOrderDetailService.InsertGroupOrderDetail(groupOrderDetail);
                                        
                                        //更新清單資料
                                        int currentNumberOfProduct = groupOrder.NumberOfProduct + number;
                                        decimal Amount = groupOrder.Amount + groupOrderDetail.UnitPrice * groupOrderDetail.Quantity;
                                        bool isGroup = false;
                                        if (currentNumberOfProduct == campaign.ProductGroup)
                                        {
                                            isGroup = true;

                                        }
                                        groupOrderService.UpdateGroupOrder(groupOrder.GroupOrderID,currentNumberOfProduct, Amount, isGroup);

                                        break;
                                    }
                                }


                                //for (var i = 0; i < campaigns.Count; i++)
                                //{
                                //    string pattern = "^" + campaigns[i].Keyword + "\\s?\\+\\d{1}\\s*$";
                                //    re = new Regex(pattern);

                                //    if (re.IsMatch(LineEvent.message.text))
                                //    {
                                //        UserInfo = isRock.LineBot.Utility.GetGroupMemberProfile(groupId, userId, ChannelAccessToken);

                                //        DateTime timestampTotime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                                //        timestampTotime = timestampTotime.AddSeconds(LineEvent.timestamp / 1000).AddHours(8).ToLocalTime();

                                //        DateTime dt = (new DateTime(1970, 1, 1, 0, 0, 0)).AddHours(8).AddSeconds(LineEvent.timestamp);

                                //        var qty = int.Parse(LineEvent.message.text.Substring(LineEvent.message.text.IndexOf("+", StringComparison.Ordinal)));

                                //        BotService.CheckLineCustomer(userId, UserInfo.displayName);

                                //        BotService.CheckGroupOrder(campaigns[i].CampaignID, timestampTotime, userId, campaigns[i].ProductID, qty);

                                //        foreach (var AdminUserId in AdminUser)
                                //        {
                                //            this.PushMessage(AdminUserId, "活動編號:" + campaigns[i].CampaignID + "\n群組編號:\n" + LineEvent.source.groupId + "\n顧客編號:\n" + LineEvent.source.userId +
                                //                "\n顧客照片:\n" + UserInfo.pictureUrl + "\n名字:" + UserInfo.displayName + "\n購買:" + campaigns[i].Keyword + "\n數量:" + qty + "\n留言時間:" + timestampTotime);
                                //        }

                                //        this.PushMessage(LineEvent.source.userId, "恭喜你在買越多省越多成功下單" + "\n購買:" + campaigns[i].Keyword + "\n數量:" + qty);
                                //    }

                                //}
                                /*if (尋找群組(LineEvent))
                                {
                                   正在進行的活動s=尋找符合此群組的活動並且正在進行(groupid)
                                   for(正在進行的活動s)
                                    {
                                        if(正在進行活動)
                                        {
                                            if(過濾留言(留言))
                                            {
                                                if(留言者是否在linecustomer)
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
                    DateTime timestampTotime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    timestampTotime = timestampTotime.AddSeconds(LineEvent.timestamp / 1000).ToLocalTime();
                    string groupId = LineEvent.source.groupId;

                    List<CompareStoreManager> managerId = LineBindingService.GroupNullCompare();
                    foreach (var item in managerId)
                    {
                        StoreMeanger checkProfile = BotService.CheckMeanger(groupId, item.LineID);
                        if (checkProfile.message != "Not found")
                        {
                            LineBindingService.CompareUpdateGroupid(groupId, item.StoreManagerID, timestampTotime);
                        }
                        
                    }

                }

                return Ok();
            }
            catch (Exception ex)
            {
                //foreach (var AdminUserId in AdminUser)
                //{
                //    this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //}
                this.PushMessage("Uc9d21bb74f13334be35b46b6581b9416", "發生錯誤:\n" + ex.Message);
                return Ok();
            }
        }
    }
}
