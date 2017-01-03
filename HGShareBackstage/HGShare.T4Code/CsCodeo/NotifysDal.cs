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
    /// Notify 
    /// </summary>
    public class Notifys
    {

		/// <summary>
		/// 添加NotifyInfo
		/// </summary>
		/// <param name="notify"></param>
		/// <returns></returns>
		public static long AddNotify(NotifyInfo notify)
		{
			string sql = @"INSERT INTO [Notify]
			([FromUserId],[ToUserId],[CreateTime],[IsDelete],[IsRead],[IsSystem],[Content],[Title])
			VALUES
			(@FromUserId,@ToUserId,@CreateTime,@IsDelete,@IsRead,@IsSystem,@Content,@Title) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@FromUserId",notify.FromUserId , DbType.Int32);
			par.Add("@ToUserId",notify.ToUserId , DbType.Int32);
			par.Add("@CreateTime",notify.CreateTime , DbType.DateTime);
			par.Add("@IsDelete",notify.IsDelete , DbType.Boolean);
			par.Add("@IsRead",notify.IsRead , DbType.Boolean);
			par.Add("@IsSystem",notify.IsSystem , DbType.Boolean);
			par.Add("@Content",notify.Content , DbType.String);
			par.Add("@Title",notify.Title , DbType.String);
			return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改NotifyInfo
		/// </summary>
		/// <param name="notify"></param>
		/// <returns></returns>
		public static int UpdateNotify(NotifyInfo notify)
		{
			string sql = @"UPDATE  [Notify] SET 
						FromUserId=@FromUserId,
						ToUserId=@ToUserId,
						CreateTime=@CreateTime,
						IsDelete=@IsDelete,
						IsRead=@IsRead,
						IsSystem=@IsSystem,
						Content=@Content,
						Title=@Title
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", notify.Id, DbType.Int64);
			par.Add("@FromUserId",notify.FromUserId , DbType.Int32);
			par.Add("@ToUserId",notify.ToUserId , DbType.Int32);
			par.Add("@CreateTime",notify.CreateTime , DbType.DateTime);
			par.Add("@IsDelete",notify.IsDelete , DbType.Boolean);
			par.Add("@IsRead",notify.IsRead , DbType.Boolean);
			par.Add("@IsSystem",notify.IsSystem , DbType.Boolean);
			par.Add("@Content",notify.Content , DbType.String);
			par.Add("@Title",notify.Title , DbType.String);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取NotifyInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static NotifyInfo GetNotifyInfo(long id)
		{
			string sql = "select [Id],[FromUserId],[ToUserId],[CreateTime],[IsDelete],[IsRead],[IsSystem],[Content],[Title] FROM [Notify] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerQuerySql<NotifyInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除Notify
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteNotify(long id)
		{
			string sql="DELETE [Notify] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除Notify多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteNotifys(long[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [Notify] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取Notify分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Notify列表</returns>
       public static List<NotifyInfo> GetNotifyPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<NotifyInfo>(DbConfig.ArticleManagerConnString, "proc_GetNotifyPageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取Notify分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Notify列表</returns>
       public static List<NotifyInfo> GetNotifyPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[FromUserId],[ToUserId],[CreateTime],[IsDelete],[IsRead],[IsSystem],[Content],[Title]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<NotifyInfo>.GetPageList("[Notify]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
