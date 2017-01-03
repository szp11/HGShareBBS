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
    /// Attachment 
    /// </summary>
    public class Attachments
    {

		/// <summary>
		/// 添加AttachmentInfo
		/// </summary>
		/// <param name="attachment"></param>
		/// <returns></returns>
		public static long AddAttachment(AttachmentInfo attachment)
		{
			string sql = @"INSERT INTO [Attachment]
			([FileName],[FileTitle],[Description],[Type],[Width],[Height],[FileSize],[IsShow],[AId],[Score],[State],[UserId],[InTime],[BType],[LocalPath],[VirtualPath],[Guid])
			VALUES
			(@FileName,@FileTitle,@Description,@Type,@Width,@Height,@FileSize,@IsShow,@AId,@Score,@State,@UserId,@InTime,@BType,@LocalPath,@VirtualPath,@Guid) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@FileName",attachment.FileName , DbType.AnsiString);
			par.Add("@FileTitle",attachment.FileTitle , DbType.String);
			par.Add("@Description",attachment.Description , DbType.String);
			par.Add("@Type",attachment.Type , DbType.AnsiString);
			par.Add("@Width",attachment.Width , DbType.Int32);
			par.Add("@Height",attachment.Height , DbType.Int32);
			par.Add("@FileSize",attachment.FileSize , DbType.Int64);
			par.Add("@IsShow",attachment.IsShow , DbType.Boolean);
			par.Add("@AId",attachment.AId , DbType.Int64);
			par.Add("@Score",attachment.Score , DbType.Int32);
			par.Add("@State",attachment.State , DbType.Int32);
			par.Add("@UserId",attachment.UserId , DbType.Int32);
			par.Add("@InTime",attachment.InTime , DbType.DateTime);
			par.Add("@BType",attachment.BType , DbType.Int32);
			par.Add("@LocalPath",attachment.LocalPath , DbType.AnsiString);
			par.Add("@VirtualPath",attachment.VirtualPath , DbType.AnsiString);
			par.Add("@Guid",attachment.Guid , DbType.Guid);
			return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改AttachmentInfo
		/// </summary>
		/// <param name="attachment"></param>
		/// <returns></returns>
		public static int UpdateAttachment(AttachmentInfo attachment)
		{
			string sql = @"UPDATE  [Attachment] SET 
						FileName=@FileName,
						FileTitle=@FileTitle,
						Description=@Description,
						Type=@Type,
						Width=@Width,
						Height=@Height,
						FileSize=@FileSize,
						IsShow=@IsShow,
						AId=@AId,
						Score=@Score,
						State=@State,
						UserId=@UserId,
						InTime=@InTime,
						BType=@BType,
						LocalPath=@LocalPath,
						VirtualPath=@VirtualPath,
						Guid=@Guid
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", attachment.Id, DbType.Int64);
			par.Add("@FileName",attachment.FileName , DbType.AnsiString);
			par.Add("@FileTitle",attachment.FileTitle , DbType.String);
			par.Add("@Description",attachment.Description , DbType.String);
			par.Add("@Type",attachment.Type , DbType.AnsiString);
			par.Add("@Width",attachment.Width , DbType.Int32);
			par.Add("@Height",attachment.Height , DbType.Int32);
			par.Add("@FileSize",attachment.FileSize , DbType.Int64);
			par.Add("@IsShow",attachment.IsShow , DbType.Boolean);
			par.Add("@AId",attachment.AId , DbType.Int64);
			par.Add("@Score",attachment.Score , DbType.Int32);
			par.Add("@State",attachment.State , DbType.Int32);
			par.Add("@UserId",attachment.UserId , DbType.Int32);
			par.Add("@InTime",attachment.InTime , DbType.DateTime);
			par.Add("@BType",attachment.BType , DbType.Int32);
			par.Add("@LocalPath",attachment.LocalPath , DbType.AnsiString);
			par.Add("@VirtualPath",attachment.VirtualPath , DbType.AnsiString);
			par.Add("@Guid",attachment.Guid , DbType.Guid);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取AttachmentInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static AttachmentInfo GetAttachmentInfo(long id)
		{
			string sql = "select [Id],[FileName],[FileTitle],[Description],[Type],[Width],[Height],[FileSize],[IsShow],[AId],[Score],[State],[UserId],[InTime],[BType],[LocalPath],[VirtualPath],[Guid] FROM [Attachment] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerQuerySql<AttachmentInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除Attachment
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteAttachment(long id)
		{
			string sql="DELETE [Attachment] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除Attachment多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteAttachments(long[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [Attachment] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取Attachment分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Attachment列表</returns>
       public static List<AttachmentInfo> GetAttachmentPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<AttachmentInfo>(DbConfig.ArticleManagerConnString, "proc_GetAttachmentPageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取Attachment分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Attachment列表</returns>
       public static List<AttachmentInfo> GetAttachmentPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[FileName],[FileTitle],[Description],[Type],[Width],[Height],[FileSize],[IsShow],[AId],[Score],[State],[UserId],[InTime],[BType],[LocalPath],[VirtualPath],[Guid]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<AttachmentInfo>.GetPageList("[Attachment]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
