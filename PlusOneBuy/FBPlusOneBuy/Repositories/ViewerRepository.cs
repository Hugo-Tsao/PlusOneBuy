using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FBPlusOneBuy.DBModels;

namespace FBPlusOneBuy.Repositories
{
    public class ViewerRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;

        public void Create(Viewer viewer)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Viewers(LiveID,NumberOfViewers) VALUES(@LiveID, @NumberOfViewers)";
                conn.Execute(sql, new {LiveID = viewer.LiveID, NumberOfViewers = viewer.NumberOfViewers});
            }
        }
    }
}