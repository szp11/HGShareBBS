using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HGShare.DataProvider.DapperHelper;
using HGShare.Model;
using HGShare.VWModel;

namespace HGShare.DataProvider
{
    /// <summary>
    /// UserPosition 
    /// </summary>
    public class UserPositions
    {

        /// <summary>
        /// 添加UserPositionInfo
        /// </summary>
        /// <param name="userposition"></param>
        /// <returns></returns>
        public static int AddUserPosition(UserPositionInfo userposition)
        {
            string sql = @"INSERT INTO [UserPosition]
			([UserId],[Code],[Type])
			VALUES
			(@UserId,@Code,@Type) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@UserId", userposition.UserId, DbType.Int32);
            par.Add("@Code", userposition.Code, DbType.Int32);
            par.Add("@Type", userposition.Type, DbType.Int16);
            return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 修改UserPositionInfo
        /// </summary>
        /// <param name="userposition"></param>
        /// <returns></returns>
        public static int UpdateUserPosition(UserPositionInfo userposition)
        {
            string sql = @"UPDATE  [UserPosition] SET 
						Code=@Code,
						Type=@Type
 WHERE UserId=@UserId";
            var par = new DynamicParameters();
            par.Add("@UserId", userposition.UserId, DbType.Int32);
            par.Add("@Code", userposition.Code, DbType.Int32);
            par.Add("@Type", userposition.Type, DbType.Int16);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据userid获取UserPositionInfo
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<UserPositionInfo> GetUserPositionInfos(int userid)
        {
            string sql = "select [UserId],[Code],[Type] FROM [UserPosition] WHERE UserId=@UserId";
            var par = new DynamicParameters();
            par.Add("@UserId", userid, DbType.Int32);
            return DapWrapper.InnerQuerySql<UserPositionInfo>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据userid删除UserPosition
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Int32 DeleteUserPosition(int userid)
        {
            string sql = "DELETE [UserPosition] WHERE UserId=@UserId";
            var par = new DynamicParameters();
            par.Add("@UserId", userid, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据userids删除UserPosition多条记录
        /// </summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        public static Int32 DeleteUserPositions(int[] userids)
        {
            string sql = "DELETE [UserPosition] WHERE UserId IN (" + string.Join(",", userids) + ")";
            return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 获取UserPosition分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>UserPosition列表</returns>
        public static List<UserPositionInfo> GetUserPositionPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            recordCount = 0;
            var par = new DynamicParameters();
            par.Add("@PageIndex", pageIndex, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@BeginTime", beginTime, DbType.DateTime);
            par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
            par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
            var result = DapWrapper.InnerQueryProc<UserPositionInfo>(DbConfig.ArticleManagerConnString, "proc_GetUserPositionPageList", par);
            recordCount = par.Get<int>("@TotalCount");
            return result;
        }
        /// <summary>
        /// 获取UserPosition分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>UserPosition列表</returns>
        public static List<UserPositionInfo> GetUserPositionPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            const string fieldKey = "UserId";
            const string fieldShow = "[UserId],[Code],[Type]";
            const string fieldOrder = "UserId desc";
            const string @where = "";
            return Paging<UserPositionInfo>.GetPageList("[UserPosition]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
        }
        #region 前端
        /// <summary>
        /// 根据userid获取UserPosition
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<UserPositionVModel> GetUserPositions(int userid)
        {
            string sql = "select [UserId],[Code],[Type],dbo.GetPositionNameByCode(Code) as Name FROM [UserPosition] WHERE UserId=@UserId";
            var par = new DynamicParameters();
            par.Add("@UserId", userid, DbType.Int32);
            return DapWrapper.InnerQuerySql<UserPositionVModel>(DbConfig.ArticleManagerConnString, sql, par);
        }

        #endregion
    }
}
