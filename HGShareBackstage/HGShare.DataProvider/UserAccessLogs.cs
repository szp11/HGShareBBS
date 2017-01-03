using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HGShare.DataProvider.DapperHelper;
using HGShare.Model;
namespace HGShare.DataProvider
{
    /// <summary>
    /// UserAccessLog 
    /// </summary>
    public class UserAccessLogs
    {

        /// <summary>
        /// 添加UserAccessLogInfo
        /// </summary>
        /// <param name="useraccesslog"></param>
        /// <returns></returns>
        public static long AddUserAccessLog(UserAccessLogInfo useraccesslog)
        {
            string sql = @"INSERT INTO [UserAccessLog]
			([Url],[Referer],[UserAgent],[UserId],[Ip],[InsertTime],[Other],[Type])
			VALUES
			(@Url,@Referer,@UserAgent,@UserId,@Ip,@InsertTime,@Other,@Type) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@Url", useraccesslog.Url, DbType.AnsiString);
            par.Add("@Referer", useraccesslog.Referer, DbType.AnsiString);
            par.Add("@UserAgent", useraccesslog.UserAgent, DbType.AnsiString);
            par.Add("@UserId", useraccesslog.UserId, DbType.Int32);
            par.Add("@Ip", useraccesslog.Ip, DbType.AnsiString);
            par.Add("@InsertTime", useraccesslog.InsertTime, DbType.DateTime);
            par.Add("@Other", useraccesslog.Other, DbType.String);
            par.Add("@Type", useraccesslog.Type, DbType.Int16);
            return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 添加UserAccessLogInfo
        /// </summary>
        /// <param name="useraccesslog"></param>
        /// <returns></returns>
        public async static Task<long> AddUserAccessLogAsync(UserAccessLogInfo useraccesslog)
        {
            string sql = @"INSERT INTO [UserAccessLog]
			([Url],[Referer],[UserAgent],[UserId],[Ip],[Other],[Type])
			VALUES
			(@Url,@Referer,@UserAgent,@UserId,@Ip,@Other,@Type) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@Url", useraccesslog.Url, DbType.AnsiString);
            par.Add("@Referer", useraccesslog.Referer, DbType.AnsiString);
            par.Add("@UserAgent", useraccesslog.UserAgent, DbType.AnsiString);
            par.Add("@UserId", useraccesslog.UserId, DbType.Int32);
            par.Add("@Ip", useraccesslog.Ip, DbType.AnsiString);
            par.Add("@Other", useraccesslog.Other, DbType.String);
            par.Add("@Type", useraccesslog.Type, DbType.Int16);
            return await DapWrapper.InnerQueryScalarSqlAsync<int>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 修改UserAccessLogInfo
        /// </summary>
        /// <param name="useraccesslog"></param>
        /// <returns></returns>
        public static int UpdateUserAccessLog(UserAccessLogInfo useraccesslog)
        {
            string sql = @"UPDATE  [UserAccessLog] SET 
						Url=@Url,
						Referer=@Referer,
						UserAgent=@UserAgent,
						UserId=@UserId,
						Ip=@Ip,
						InsertTime=@InsertTime,
						Other=@Other,
						Type=@Type
 WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", useraccesslog.Id, DbType.Int64);
            par.Add("@Url", useraccesslog.Url, DbType.AnsiString);
            par.Add("@Referer", useraccesslog.Referer, DbType.AnsiString);
            par.Add("@UserAgent", useraccesslog.UserAgent, DbType.AnsiString);
            par.Add("@UserId", useraccesslog.UserId, DbType.Int32);
            par.Add("@Ip", useraccesslog.Ip, DbType.AnsiString);
            par.Add("@InsertTime", useraccesslog.InsertTime, DbType.DateTime);
            par.Add("@Other", useraccesslog.Other, DbType.String);
            par.Add("@Type", useraccesslog.Type, DbType.Int16);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据id获取UserAccessLogInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserAccessLogInfo GetUserAccessLogInfo(long id)
        {
            string sql = "select [Id],[Url],[Referer],[UserAgent],[UserId],[Ip],[InsertTime],[Other],[Type] FROM [UserAccessLog] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int64);
            return DapWrapper.InnerQuerySql<UserAccessLogInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
        }
        /// <summary>
        /// 根据id删除UserAccessLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteUserAccessLog(long id)
        {
            string sql = "DELETE [UserAccessLog] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int64);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据ids删除UserAccessLog多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteUserAccessLogs(long[] ids)
        {
            if (ids.Length == 0)
                return 0;
            string sql = "DELETE [UserAccessLog] WHERE Id IN (" + string.Join(",", ids) + ")";
            return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 获取UserAccessLog分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>UserAccessLog列表</returns>
        public static List<UserAccessLogInfo> GetUserAccessLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            recordCount = 0;
            var par = new DynamicParameters();
            par.Add("@PageIndex", pageIndex, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@BeginTime", beginTime, DbType.DateTime);
            par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
            par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
            var result = DapWrapper.InnerQueryProc<UserAccessLogInfo>(DbConfig.ArticleManagerConnString, "proc_GetUserAccessLogPageList", par);
            recordCount = par.Get<int>("@TotalCount");
            return result;
        }
        /// <summary>
        /// 获取UserAccessLog分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>UserAccessLog列表</returns>
        public static List<UserAccessLogInfo> GetUserAccessLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            const string fieldKey = "Id";
            const string fieldShow = "[Id],[Url],[Referer],[UserAgent],[UserId],[Ip],[InsertTime],[Other],[Type]";
            const string fieldOrder = "Id desc";
            const string @where = "";
            return Paging<UserAccessLogInfo>.GetPageList("[UserAccessLog]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
        }
    }
}
