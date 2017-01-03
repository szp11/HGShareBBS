using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EsData.Utils
{
    public class SqlUtils
    {
        public static SqlConnection GetSqlConnection(string connstr)
        {
            var conn = new SqlConnection(connstr);
            return conn;
        }

        public static String GetConnString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}
