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
    /// UserScoreLog 
    /// </summary>
    public class UserScoreLogs
    {

		/// <summary>
		/// 添加UserScoreLogInfo
		/// </summary>
		/// <param name="userscorelog"></param>
		/// <returns></returns>
		public static long AddUserScoreLog(UserScoreLogInfo userscorelog)
		{
			string sql = @"INSERT INTO [UserScoreLog]
			([UserId],[Score],[CreateTime],[Describe],[OldScore],[NewScore])
			VALUES
			(@UserId,@Score,@CreateTime,@Describe,@OldScore,@NewScore) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@UserId",userscorelog.UserId , DbType.Int32);
			par.Add("@Score",userscorelog.Score , DbType.Int32);
			par.Add("@CreateTime",userscorelog.CreateTime , DbType.DateTime);
			par.Add("@Describe",userscorelog.Describe , DbType.String);
			par.Add("@OldScore",userscorelog.OldScore , DbType.Int64);
			par.Add("@NewScore",userscorelog.NewScore , DbType.Int64);
			return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改UserScoreLogInfo
		/// </summary>
		/// <param name="userscorelog"></param>
		/// <returns></returns>
		public static int UpdateUserScoreLog(UserScoreLogInfo userscorelog)
		{
			string sql = @"UPDATE  [UserScoreLog] SET 
						UserId=@UserId,
						Score=@Score,
						CreateTime=@CreateTime,
						Describe=@Describe,
						OldScore=@OldScore,
						NewScore=@NewScore
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", userscorelog.Id, DbType.Int64);
			par.Add("@UserId",userscorelog.UserId , DbType.Int32);
			par.Add("@Score",userscorelog.Score , DbType.Int32);
			par.Add("@CreateTime",userscorelog.CreateTime , DbType.DateTime);
			par.Add("@Describe",userscorelog.Describe , DbType.String);
			par.Add("@OldScore",userscorelog.OldScore , DbType.Int64);
			par.Add("@NewScore",userscorelog.NewScore , DbType.Int64);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取UserScoreLogInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static UserScoreLogInfo GetUserScoreLogInfo(long id)
		{
			string sql = "select [Id],[UserId],[Score],[CreateTime],[Describe],[OldScore],[NewScore] FROM [UserScoreLog] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerQuerySql<UserScoreLogInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除UserScoreLog
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteUserScoreLog(long id)
		{
			string sql="DELETE [UserScoreLog] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除UserScoreLog多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteUserScoreLogs(long[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [UserScoreLog] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取UserScoreLog分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>UserScoreLog列表</returns>
       public static List<UserScoreLogInfo> GetUserScoreLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<UserScoreLogInfo>(DbConfig.ArticleManagerConnString, "proc_GetUserScoreLogPageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取UserScoreLog分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>UserScoreLog列表</returns>
       public static List<UserScoreLogInfo> GetUserScoreLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[UserId],[Score],[CreateTime],[Describe],[OldScore],[NewScore]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<UserScoreLogInfo>.GetPageList("[UserScoreLog]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
