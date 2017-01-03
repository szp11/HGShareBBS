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
    /// UserPosition 
    /// </summary>
    public class UserPositions
    {

		/// <summary>
		/// 添加UserPositionInfo
		/// </summary>
		/// <param name="userposition"></param>
		/// <returns></returns>
		public static long AddUserPosition(UserPositionInfo userposition)
		{
			string sql = @"INSERT INTO [UserPosition]
			([UserId],[Code],[Type],[CreateTime])
			VALUES
			(@UserId,@Code,@Type,@CreateTime) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@UserId",userposition.UserId , DbType.Int32);
			par.Add("@Code",userposition.Code , DbType.Int32);
			par.Add("@Type",userposition.Type , DbType.Int16);
			par.Add("@CreateTime",userposition.CreateTime , DbType.DateTime);
			return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改UserPositionInfo
		/// </summary>
		/// <param name="userposition"></param>
		/// <returns></returns>
		public static int UpdateUserPosition(UserPositionInfo userposition)
		{
			string sql = @"UPDATE  [UserPosition] SET 
						UserId=@UserId,
						Code=@Code,
						Type=@Type,
						CreateTime=@CreateTime
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", userposition.Id, DbType.Int64);
			par.Add("@UserId",userposition.UserId , DbType.Int32);
			par.Add("@Code",userposition.Code , DbType.Int32);
			par.Add("@Type",userposition.Type , DbType.Int16);
			par.Add("@CreateTime",userposition.CreateTime , DbType.DateTime);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取UserPositionInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static UserPositionInfo GetUserPositionInfo(long id)
		{
			string sql = "select [Id],[UserId],[Code],[Type],[CreateTime] FROM [UserPosition] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerQuerySql<UserPositionInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除UserPosition
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteUserPosition(long id)
		{
			string sql="DELETE [UserPosition] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除UserPosition多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteUserPositions(long[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [UserPosition] WHERE Id IN ("+string.Join(",",ids)+")";
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
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[UserId],[Code],[Type],[CreateTime]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<UserPositionInfo>.GetPageList("[UserPosition]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
