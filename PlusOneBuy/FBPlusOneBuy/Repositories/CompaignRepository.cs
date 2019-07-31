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
    public class CompaignRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        private SqlConnection conn;
        public IEnumerable<Compaign> GetALL()
        {
            using (conn=new SqlConnection(connectionString))
            {
                string url = "SELECT * FROM Compaign";
                return conn.Query<Compaign>(url);
            }
        }

    }
}