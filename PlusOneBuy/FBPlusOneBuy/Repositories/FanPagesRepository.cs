using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FBPlusOneBuy.Models;

namespace FBPlusOneBuy.Repositories
{
    public class FanPagesRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public bool isExist(string fanpageid,string userid)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select * from FanPages where FanPageID=@fanpageid and AspNetUserId=@userid";
                var fanpage = conn.QueryFirstOrDefault<FanPage>(sql, new { fanpageid, userid });
                if (fanpage!=null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public List<FanPage> Select(string aspUserId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select * from FanPages where AspNetUserId=@aspUserId";
                var fanpages = conn.Query<FanPage>(sql, new {aspUserId }).ToList();
                return fanpages;
            }

        }
        public FanPage Select(string aspUserId, string fanpagename)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select * from FanPages where AspNetUserId=@aspUserId and FanPageName=@fanpagename";
                var fanpage = conn.QueryFirstOrDefault<FanPage>(sql, new { aspUserId , fanpagename });
                return fanpage;
            }

        }
        public FanPage SelectBinding(string aspUserId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "select * from FanPages where AspNetUserId=@aspUserId and FbPageLongToken is not null";
                var fanpage = conn.QueryFirstOrDefault<FanPage>(sql, new { aspUserId });
                return fanpage;
            }

        }

        public void Insert(string fanpageid, string fanpagename, string aspUserId, string fbLongToken)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO FanPages(FanPageID, FanPageName,AspNetUserId,FbPageLongToken) VALUES ( @fpID, @fpName,@aspUserId,@fbLongToken)";
                conn.Execute(sql, new { fpID= fanpageid, fpName= fanpagename, aspUserId, fbLongToken });
            }
        }

        public void Update(string fanpageid, string aspUserId, string tokenValue)
        {
            using (conn = new SqlConnection(connectionString))
            {
                
                string sql = "UPDATE FanPages SET FbPageLongToken =@tokenValue  WHERE FanPageID=@fanpageid and AspNetUserId=@aspUserId";
                conn.Execute(sql, new { tokenValue, fanpageid, aspUserId});
            }
        }
        
    }
}