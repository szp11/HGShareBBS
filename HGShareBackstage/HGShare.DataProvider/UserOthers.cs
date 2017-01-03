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
    /// UserOther 
    /// </summary>
    public class UserOthers
    {

        /// <summary>
        /// 添加UserOtherInfo
        /// </summary>
        /// <param name="userother"></param>
        /// <returns></returns>
        public static int AddUserOther(UserOtherInfo userother)
        {
            string sql = @"INSERT INTO [UserOther]
			([UserId],[PersonalityIntroduce])
			VALUES
			(@UserId,@PersonalityIntroduce) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@UserId", userother.UserId, DbType.Int32);
            par.Add("@PersonalityIntroduce", userother.PersonalityIntroduce, DbType.String);
            return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 修改UserOtherInfo
        /// </summary>
        /// <param name="userother"></param>
        /// <returns></returns>
        public static int UpdateUserOther(UserOtherInfo userother)
        {
            string sql = @"UPDATE  [UserOther] SET 
						PersonalityIntroduce=@PersonalityIntroduce
 WHERE UserId=@UserId";
            var par = new DynamicParameters();
            par.Add("@UserId", userother.UserId, DbType.Int32);
            par.Add("@PersonalityIntroduce", userother.PersonalityIntroduce, DbType.String);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据userid获取UserOtherInfo
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static UserOtherInfo GetUserOtherInfo(int userid)
        {
            string sql = "select [UserId],[PersonalityIntroduce] FROM [UserOther] WHERE UserId=@UserId";
            var par = new DynamicParameters();
            par.Add("@UserId", userid, DbType.Int32);
            return DapWrapper.InnerQuerySql<UserOtherInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
        }
        /// <summary>
        /// 根据userid删除UserOther
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Int32 DeleteUserOther(int userid)
        {
            string sql = "DELETE [UserOther] WHERE UserId=@UserId";
            var par = new DynamicParameters();
            par.Add("@UserId", userid, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据userids删除UserOther多条记录
        /// </summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        public static Int32 DeleteUserOthers(int[] userids)
        {
            string sql = "DELETE [UserOther] WHERE UserId IN (" + string.Join(",", userids) + ")";
            return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 获取UserOther分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>UserOther列表</returns>
        public static List<UserOtherInfo> GetUserOtherPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            recordCount = 0;
            var par = new DynamicParameters();
            par.Add("@PageIndex", pageIndex, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@BeginTime", beginTime, DbType.DateTime);
            par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
            par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
            var result = DapWrapper.InnerQueryProc<UserOtherInfo>(DbConfig.ArticleManagerConnString, "proc_GetUserOtherPageList", par);
            recordCount = par.Get<int>("@TotalCount");
            return result;
        }
        /// <summary>
        /// 获取UserOther分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>UserOther列表</returns>
        public static List<UserOtherInfo> GetUserOtherPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            const string fieldKey = "UserId";
            const string fieldShow = "[UserId],[PersonalityIntroduce]";
            const string fieldOrder = "UserId desc";
            const string @where = "";
            return Paging<UserOtherInfo>.GetPageList("[UserOther]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
        }
    }
}
