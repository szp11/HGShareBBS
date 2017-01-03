using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HGShare.DataProvider.DapperHelper;
using HGShare.Model;
namespace HGShare.DataProvider
{
    /// <summary>
    /// UserActivateToken 
    /// </summary>
    public class UserActivateTokens
    {

        /// <summary>
        /// 添加UserActivateTokenInfo
        /// </summary>
        /// <param name="useractivatetoken"></param>
        /// <returns></returns>
        public static long AddUserActivateToken(UserActivateTokenInfo useractivatetoken)
        {
            string sql = @"INSERT INTO [UserActivateToken]
			([UserId],[Email],[Token])
			VALUES
			(@UserId,@Email,@Token) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@UserId", useractivatetoken.UserId, DbType.Int32);
            par.Add("@Email", useractivatetoken.Email, DbType.AnsiString);
            par.Add("@Token", useractivatetoken.Token, DbType.AnsiString);
            return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 修改UserActivateTokenInfo
        /// </summary>
        /// <param name="useractivatetoken"></param>
        /// <returns></returns>
        public static int UpdateUserActivateToken(UserActivateTokenInfo useractivatetoken)
        {
            string sql = @"UPDATE  [UserActivateToken] SET 
						UserId=@UserId,
						Email=@Email,
						Token=@Token,
						Status=@Status,
						CreateTime=@CreateTime
 WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", useractivatetoken.Id, DbType.Int64);
            par.Add("@UserId", useractivatetoken.UserId, DbType.Int32);
            par.Add("@Email", useractivatetoken.Email, DbType.AnsiString);
            par.Add("@Token", useractivatetoken.Token, DbType.AnsiString);
            par.Add("@Status", useractivatetoken.Status, DbType.Boolean);
            par.Add("@CreateTime", useractivatetoken.CreateTime, DbType.DateTime);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据id获取UserActivateTokenInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserActivateTokenInfo GetUserActivateTokenInfo(long id)
        {
            string sql = "select [Id],[UserId],[Email],[Token],[Status],[CreateTime] FROM [UserActivateToken] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int64);
            return DapWrapper.InnerQuerySql<UserActivateTokenInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
        }
        /// <summary>
        /// 根据id删除UserActivateToken
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteUserActivateToken(long id)
        {
            string sql = "DELETE [UserActivateToken] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int64);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据ids删除UserActivateToken多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteUserActivateTokens(long[] ids)
        {
            string sql = "DELETE [UserActivateToken] WHERE Id IN (" + string.Join(",", ids) + ")";
            return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 获取UserActivateToken分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>UserActivateToken列表</returns>
        public static List<UserActivateTokenInfo> GetUserActivateTokenPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            recordCount = 0;
            var par = new DynamicParameters();
            par.Add("@PageIndex", pageIndex, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@BeginTime", beginTime, DbType.DateTime);
            par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
            par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
            var result = DapWrapper.InnerQueryProc<UserActivateTokenInfo>(DbConfig.ArticleManagerConnString, "proc_GetUserActivateTokenPageList", par);
            recordCount = par.Get<int>("@TotalCount");
            return result;
        }
        /// <summary>
        /// 获取UserActivateToken分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>UserActivateToken列表</returns>
        public static List<UserActivateTokenInfo> GetUserActivateTokenPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            const string fieldKey = "Id";
            const string fieldShow = "[Id],[UserId],[Email],[Token],[Status],[CreateTime]";
            const string fieldOrder = "Id desc";
            const string @where = "";
            return Paging<UserActivateTokenInfo>.GetPageList("[UserActivateToken]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
        }


        /// <summary>
        /// 修改 Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool UpdateStatus(long id, bool status)
        {
            string sql = @"UPDATE  [UserActivateToken] SET 
						Status=@Status
                        WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            par.Add("@Status", status, DbType.Boolean);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par)>0;
        }

        /// <summary>
        /// 修改 Status 为无效 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool UpdateStatusToFalseByUserId(int userId)
        {
            string sql = @"UPDATE  [UserActivateToken] SET 
						Status=0
                        WHERE UserId=@UserId AND Status=1";
            var par = new DynamicParameters();
            par.Add("@UserId", userId, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par) > 0;
        }
        /// <summary>
        /// 获取UserActivateToken id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public static long GetUserActivateTokenId(int userId, string email, string token, int expireTime)
        {
            string sql = @"SELECT Id FROM [UserActivateToken]  WHERE UserId=@UserId AND Email=@Email AND Token=@Token AND Status=1 AND DATEDIFF(MINUTE,CreateTime,GETDATE())<=@Time";
            var par = new DynamicParameters();
            par.Add("@UserId", userId, DbType.Int32);
            par.Add("@Email", email, DbType.String);
            par.Add("@Token", token, DbType.String);
            par.Add("@Time", expireTime, DbType.Int32);
            return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 获取UserActivateToken id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public static long GetUserActivateTokenId(string token, int expireTime)
        {
            string sql = @"SELECT Id FROM [UserActivateToken]  WHERE Token=@Token AND Status=1 AND DATEDIFF(MINUTE,CreateTime,GETDATE())<=@Time";
            var par = new DynamicParameters();
            par.Add("@Token", token, DbType.String);
            par.Add("@Time", expireTime, DbType.Int32);
            return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
        }
    }
}
