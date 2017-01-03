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
    /// UserBuyingLog 
    /// </summary>
    public class UserBuyingLogs
    {

		/// <summary>
		/// 添加UserBuyingLogInfo
		/// </summary>
		/// <param name="userbuyinglog"></param>
		/// <returns></returns>
		public static int AddUserBuyingLog(UserBuyingLogInfo userbuyinglog)
		{
			string sql = @"INSERT INTO [UserBuyingLog]
			([AId],[Score],[UserId],[CreateTime])
			VALUES
			(@AId,@Score,@UserId,@CreateTime) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@AId",userbuyinglog.AId , DbType.Int64);
			par.Add("@Score",userbuyinglog.Score , DbType.Int32);
			par.Add("@UserId",userbuyinglog.UserId , DbType.Int32);
			par.Add("@CreateTime",userbuyinglog.CreateTime , DbType.DateTime);
			return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改UserBuyingLogInfo
		/// </summary>
		/// <param name="userbuyinglog"></param>
		/// <returns></returns>
		public static int UpdateUserBuyingLog(UserBuyingLogInfo userbuyinglog)
		{
			string sql = @"UPDATE  [UserBuyingLog] SET 
						AId=@AId,
						Score=@Score,
						UserId=@UserId,
						CreateTime=@CreateTime
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", userbuyinglog.Id, DbType.Int32);
			par.Add("@AId",userbuyinglog.AId , DbType.Int64);
			par.Add("@Score",userbuyinglog.Score , DbType.Int32);
			par.Add("@UserId",userbuyinglog.UserId , DbType.Int32);
			par.Add("@CreateTime",userbuyinglog.CreateTime , DbType.DateTime);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取UserBuyingLogInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static UserBuyingLogInfo GetUserBuyingLogInfo(int id)
		{
			string sql = "select [Id],[AId],[Score],[UserId],[CreateTime] FROM [UserBuyingLog] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerQuerySql<UserBuyingLogInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除UserBuyingLog
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteUserBuyingLog(int id)
		{
			string sql="DELETE [UserBuyingLog] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除UserBuyingLog多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteUserBuyingLogs(int[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [UserBuyingLog] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取UserBuyingLog分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>UserBuyingLog列表</returns>
       public static List<UserBuyingLogInfo> GetUserBuyingLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<UserBuyingLogInfo>(DbConfig.ArticleManagerConnString, "proc_GetUserBuyingLogPageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取UserBuyingLog分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>UserBuyingLog列表</returns>
       public static List<UserBuyingLogInfo> GetUserBuyingLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[AId],[Score],[UserId],[CreateTime]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<UserBuyingLogInfo>.GetPageList("[UserBuyingLog]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
