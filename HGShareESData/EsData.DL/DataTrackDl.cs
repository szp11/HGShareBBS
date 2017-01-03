using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using EsData.Utils;
using EsData.Core;

namespace EsData.DL
{
    /// <summary>
    /// 数据跟踪
    /// </summary>
    public class DataTrackDl
    {
        /// <summary>
        /// 获取最大版本号
        /// </summary>
        /// <param name="dbConnectionKey"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static long GetCurrentMaxVersion(string dbConnectionKey, string tableName)
        {
            string connectionString = SqlUtils.GetConnString(dbConnectionKey);

            string sql = string.Format(@"SELECT ISNULL(Max(SYS_CHANGE_VERSION),0) AS [Version]
                                FROM CHANGETABLE(CHANGES dbo.{0},0) as ct", tableName);
            long version;
            using (IDbConnection cnn = SqlUtils.GetSqlConnection(connectionString))
            {
                version = cnn.QuerySingle<long>(sql, null, null, null, CommandType.Text);
            }
            return version;
        }

        /// <summary>
        /// 获取变更记录
        /// </summary>
        /// <param name="dbConnectionKey"></param>
        /// <param name="tableName"></param>
        /// <param name="version"></param>
        /// <param name="pkName"></param>
        /// <returns></returns>
        public static List<DataChangeMsg> GetDataChangeMsgs(string dbConnectionKey, string tableName, long version, string pkName)
        {
            string connectionString = SqlUtils.GetConnString(dbConnectionKey);

            string sql = string.Format(@"SELECT
                                SYS_CHANGE_VERSION AS [Version],
                                SYS_CHANGE_OPERATION AS [Type],
                                {0} AS PkValue,
                                '{0}' AS PkName,
                                '{1}' AS TableName
                                FROM CHANGETABLE(CHANGES dbo.{1},{2}) as ct", pkName, tableName, version);


            List<DataChangeMsg> list;
            using (IDbConnection cnn = SqlUtils.GetSqlConnection(connectionString))
            {
                list = cnn.Query<DataChangeMsg>(sql,
                                       null,
                                        null,
                                        true,
                                        null,
                                        CommandType.Text).ToList();

            }
            return list;
        }

        /// <summary>
        /// 获取变更后的新数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static List<T> GetDataChangeMsgs<T>(DataChangeMsg msg)
        {
            string connectionString = SqlUtils.GetConnString(msg.DbConnectionKey);

            string sql = string.Format(@"SELECT * FROM {0} WHERE {1}={2}", msg.TableName, msg.PkName, msg.PkValue);

            List<T> list;

            using (IDbConnection cnn = SqlUtils.GetSqlConnection(connectionString))
            {
                list = cnn.Query<T>(sql,
                                       null,
                                        null,
                                        true,
                                        null,
                                        CommandType.Text).ToList();

            }
            return list;
        }
    }
}
