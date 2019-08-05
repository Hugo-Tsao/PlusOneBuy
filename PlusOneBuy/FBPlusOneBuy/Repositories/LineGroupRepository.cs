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
    public class LineGroupRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;

        public LineGroup SearchLineGroup(string groupId)
        {
            using (conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM LineGroup WHERE LineGroupID = @groupId";
                var LineGroup = conn.QueryFirstOrDefault<LineGroup>(sql, new {groupId});
                return LineGroup;
            }
        }
    }
}