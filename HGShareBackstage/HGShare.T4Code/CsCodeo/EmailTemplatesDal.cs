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
    /// EmailTemplate 
    /// </summary>
    public class EmailTemplates
    {

		/// <summary>
		/// 添加EmailTemplateInfo
		/// </summary>
		/// <param name="emailtemplate"></param>
		/// <returns></returns>
		public static int AddEmailTemplate(EmailTemplateInfo emailtemplate)
		{
			string sql = @"INSERT INTO [EmailTemplate]
			([Title],[Template],[ValueIdentifier],[Explanation],[IsSystem],[IsHtml],[CreateTime],[UserId],[LastEditUserId],[LastEditTime])
			VALUES
			(@Title,@Template,@ValueIdentifier,@Explanation,@IsSystem,@IsHtml,@CreateTime,@UserId,@LastEditUserId,@LastEditTime) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@Title",emailtemplate.Title , DbType.String);
			par.Add("@Template",emailtemplate.Template , DbType.AnsiString);
			par.Add("@ValueIdentifier",emailtemplate.ValueIdentifier , DbType.String);
			par.Add("@Explanation",emailtemplate.Explanation , DbType.String);
			par.Add("@IsSystem",emailtemplate.IsSystem , DbType.Boolean);
			par.Add("@IsHtml",emailtemplate.IsHtml , DbType.Boolean);
			par.Add("@CreateTime",emailtemplate.CreateTime , DbType.DateTime);
			par.Add("@UserId",emailtemplate.UserId , DbType.Int32);
			par.Add("@LastEditUserId",emailtemplate.LastEditUserId , DbType.Int32);
			par.Add("@LastEditTime",emailtemplate.LastEditTime , DbType.DateTime);
			return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改EmailTemplateInfo
		/// </summary>
		/// <param name="emailtemplate"></param>
		/// <returns></returns>
		public static int UpdateEmailTemplate(EmailTemplateInfo emailtemplate)
		{
			string sql = @"UPDATE  [EmailTemplate] SET 
						Title=@Title,
						Template=@Template,
						ValueIdentifier=@ValueIdentifier,
						Explanation=@Explanation,
						IsSystem=@IsSystem,
						IsHtml=@IsHtml,
						CreateTime=@CreateTime,
						UserId=@UserId,
						LastEditUserId=@LastEditUserId,
						LastEditTime=@LastEditTime
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", emailtemplate.Id, DbType.Int32);
			par.Add("@Title",emailtemplate.Title , DbType.String);
			par.Add("@Template",emailtemplate.Template , DbType.AnsiString);
			par.Add("@ValueIdentifier",emailtemplate.ValueIdentifier , DbType.String);
			par.Add("@Explanation",emailtemplate.Explanation , DbType.String);
			par.Add("@IsSystem",emailtemplate.IsSystem , DbType.Boolean);
			par.Add("@IsHtml",emailtemplate.IsHtml , DbType.Boolean);
			par.Add("@CreateTime",emailtemplate.CreateTime , DbType.DateTime);
			par.Add("@UserId",emailtemplate.UserId , DbType.Int32);
			par.Add("@LastEditUserId",emailtemplate.LastEditUserId , DbType.Int32);
			par.Add("@LastEditTime",emailtemplate.LastEditTime , DbType.DateTime);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取EmailTemplateInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static EmailTemplateInfo GetEmailTemplateInfo(int id)
		{
			string sql = "select [Id],[Title],[Template],[ValueIdentifier],[Explanation],[IsSystem],[IsHtml],[CreateTime],[UserId],[LastEditUserId],[LastEditTime] FROM [EmailTemplate] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerQuerySql<EmailTemplateInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除EmailTemplate
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteEmailTemplate(int id)
		{
			string sql="DELETE [EmailTemplate] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除EmailTemplate多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteEmailTemplates(int[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [EmailTemplate] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取EmailTemplate分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>EmailTemplate列表</returns>
       public static List<EmailTemplateInfo> GetEmailTemplatePageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<EmailTemplateInfo>(DbConfig.ArticleManagerConnString, "proc_GetEmailTemplatePageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取EmailTemplate分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>EmailTemplate列表</returns>
       public static List<EmailTemplateInfo> GetEmailTemplatePageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[Title],[Template],[ValueIdentifier],[Explanation],[IsSystem],[IsHtml],[CreateTime],[UserId],[LastEditUserId],[LastEditTime]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<EmailTemplateInfo>.GetPageList("[EmailTemplate]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
