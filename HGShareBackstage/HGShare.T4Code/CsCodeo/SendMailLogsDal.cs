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
    /// SendMailLog 
    /// </summary>
    public class SendMailLogs
    {

		/// <summary>
		/// 添加SendMailLogInfo
		/// </summary>
		/// <param name="sendmaillog"></param>
		/// <returns></returns>
		public static long AddSendMailLog(SendMailLogInfo sendmaillog)
		{
			string sql = @"INSERT INTO [SendMailLog]
			([UserId],[SendUserId],[TemplateId],[ToEmail],[FromEmail],[Status],[Title],[Body],[Ip],[IsSystem],[CreateTime])
			VALUES
			(@UserId,@SendUserId,@TemplateId,@ToEmail,@FromEmail,@Status,@Title,@Body,@Ip,@IsSystem,@CreateTime) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@UserId",sendmaillog.UserId , DbType.Int32);
			par.Add("@SendUserId",sendmaillog.SendUserId , DbType.Int32);
			par.Add("@TemplateId",sendmaillog.TemplateId , DbType.Int32);
			par.Add("@ToEmail",sendmaillog.ToEmail , DbType.AnsiString);
			par.Add("@FromEmail",sendmaillog.FromEmail , DbType.AnsiString);
			par.Add("@Status",sendmaillog.Status , DbType.Int16);
			par.Add("@Title",sendmaillog.Title , DbType.String);
			par.Add("@Body",sendmaillog.Body , DbType.AnsiString);
			par.Add("@Ip",sendmaillog.Ip , DbType.AnsiString);
			par.Add("@IsSystem",sendmaillog.IsSystem , DbType.Boolean);
			par.Add("@CreateTime",sendmaillog.CreateTime , DbType.DateTime);
			return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改SendMailLogInfo
		/// </summary>
		/// <param name="sendmaillog"></param>
		/// <returns></returns>
		public static int UpdateSendMailLog(SendMailLogInfo sendmaillog)
		{
			string sql = @"UPDATE  [SendMailLog] SET 
						UserId=@UserId,
						SendUserId=@SendUserId,
						TemplateId=@TemplateId,
						ToEmail=@ToEmail,
						FromEmail=@FromEmail,
						Status=@Status,
						Title=@Title,
						Body=@Body,
						Ip=@Ip,
						IsSystem=@IsSystem,
						CreateTime=@CreateTime
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", sendmaillog.Id, DbType.Int64);
			par.Add("@UserId",sendmaillog.UserId , DbType.Int32);
			par.Add("@SendUserId",sendmaillog.SendUserId , DbType.Int32);
			par.Add("@TemplateId",sendmaillog.TemplateId , DbType.Int32);
			par.Add("@ToEmail",sendmaillog.ToEmail , DbType.AnsiString);
			par.Add("@FromEmail",sendmaillog.FromEmail , DbType.AnsiString);
			par.Add("@Status",sendmaillog.Status , DbType.Int16);
			par.Add("@Title",sendmaillog.Title , DbType.String);
			par.Add("@Body",sendmaillog.Body , DbType.AnsiString);
			par.Add("@Ip",sendmaillog.Ip , DbType.AnsiString);
			par.Add("@IsSystem",sendmaillog.IsSystem , DbType.Boolean);
			par.Add("@CreateTime",sendmaillog.CreateTime , DbType.DateTime);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取SendMailLogInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static SendMailLogInfo GetSendMailLogInfo(long id)
		{
			string sql = "select [Id],[UserId],[SendUserId],[TemplateId],[ToEmail],[FromEmail],[Status],[Title],[Body],[Ip],[IsSystem],[CreateTime] FROM [SendMailLog] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerQuerySql<SendMailLogInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除SendMailLog
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteSendMailLog(long id)
		{
			string sql="DELETE [SendMailLog] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除SendMailLog多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteSendMailLogs(long[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [SendMailLog] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取SendMailLog分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>SendMailLog列表</returns>
       public static List<SendMailLogInfo> GetSendMailLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<SendMailLogInfo>(DbConfig.ArticleManagerConnString, "proc_GetSendMailLogPageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取SendMailLog分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>SendMailLog列表</returns>
       public static List<SendMailLogInfo> GetSendMailLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[UserId],[SendUserId],[TemplateId],[ToEmail],[FromEmail],[Status],[Title],[Body],[Ip],[IsSystem],[CreateTime]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<SendMailLogInfo>.GetPageList("[SendMailLog]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
