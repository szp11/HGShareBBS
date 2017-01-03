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
    /// Article 
    /// </summary>
    public class Articles
    {

		/// <summary>
		/// 添加ArticleInfo
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		public static long AddArticle(ArticleInfo article)
		{
			string sql = @"INSERT INTO [Article]
			([Title],[Content],[Type],[CommentNum],[Dot],[CreateTime],[UserId],[ImgNum],[AttachmentNum],[LastEditUserId],[LastEditTime],[Guid],[IsDelete],[State],[RefuseReason],[BType],[DianZanNum],[Score],[IsStick],[IsJiaJing],[IsCloseComment],[CloseCommentReason])
			VALUES
			(@Title,@Content,@Type,@CommentNum,@Dot,@CreateTime,@UserId,@ImgNum,@AttachmentNum,@LastEditUserId,@LastEditTime,@Guid,@IsDelete,@State,@RefuseReason,@BType,@DianZanNum,@Score,@IsStick,@IsJiaJing,@IsCloseComment,@CloseCommentReason) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@Title",article.Title , DbType.String);
			par.Add("@Content",article.Content , DbType.AnsiString);
			par.Add("@Type",article.Type , DbType.Int32);
			par.Add("@CommentNum",article.CommentNum , DbType.Int32);
			par.Add("@Dot",article.Dot , DbType.Int32);
			par.Add("@CreateTime",article.CreateTime , DbType.DateTime);
			par.Add("@UserId",article.UserId , DbType.Int32);
			par.Add("@ImgNum",article.ImgNum , DbType.Int32);
			par.Add("@AttachmentNum",article.AttachmentNum , DbType.Int32);
			par.Add("@LastEditUserId",article.LastEditUserId , DbType.Int32);
			par.Add("@LastEditTime",article.LastEditTime , DbType.DateTime);
			par.Add("@Guid",article.Guid , DbType.Guid);
			par.Add("@IsDelete",article.IsDelete , DbType.Boolean);
			par.Add("@State",article.State , DbType.Int16);
			par.Add("@RefuseReason",article.RefuseReason , DbType.String);
			par.Add("@BType",article.BType , DbType.Int16);
			par.Add("@DianZanNum",article.DianZanNum , DbType.Int32);
			par.Add("@Score",article.Score , DbType.Int32);
			par.Add("@IsStick",article.IsStick , DbType.Boolean);
			par.Add("@IsJiaJing",article.IsJiaJing , DbType.Boolean);
			par.Add("@IsCloseComment",article.IsCloseComment , DbType.Boolean);
			par.Add("@CloseCommentReason",article.CloseCommentReason , DbType.String);
			return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改ArticleInfo
		/// </summary>
		/// <param name="article"></param>
		/// <returns></returns>
		public static int UpdateArticle(ArticleInfo article)
		{
			string sql = @"UPDATE  [Article] SET 
						Title=@Title,
						Content=@Content,
						Type=@Type,
						CommentNum=@CommentNum,
						Dot=@Dot,
						CreateTime=@CreateTime,
						UserId=@UserId,
						ImgNum=@ImgNum,
						AttachmentNum=@AttachmentNum,
						LastEditUserId=@LastEditUserId,
						LastEditTime=@LastEditTime,
						Guid=@Guid,
						IsDelete=@IsDelete,
						State=@State,
						RefuseReason=@RefuseReason,
						BType=@BType,
						DianZanNum=@DianZanNum,
						Score=@Score,
						IsStick=@IsStick,
						IsJiaJing=@IsJiaJing,
						IsCloseComment=@IsCloseComment,
						CloseCommentReason=@CloseCommentReason
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", article.Id, DbType.Int64);
			par.Add("@Title",article.Title , DbType.String);
			par.Add("@Content",article.Content , DbType.AnsiString);
			par.Add("@Type",article.Type , DbType.Int32);
			par.Add("@CommentNum",article.CommentNum , DbType.Int32);
			par.Add("@Dot",article.Dot , DbType.Int32);
			par.Add("@CreateTime",article.CreateTime , DbType.DateTime);
			par.Add("@UserId",article.UserId , DbType.Int32);
			par.Add("@ImgNum",article.ImgNum , DbType.Int32);
			par.Add("@AttachmentNum",article.AttachmentNum , DbType.Int32);
			par.Add("@LastEditUserId",article.LastEditUserId , DbType.Int32);
			par.Add("@LastEditTime",article.LastEditTime , DbType.DateTime);
			par.Add("@Guid",article.Guid , DbType.Guid);
			par.Add("@IsDelete",article.IsDelete , DbType.Boolean);
			par.Add("@State",article.State , DbType.Int16);
			par.Add("@RefuseReason",article.RefuseReason , DbType.String);
			par.Add("@BType",article.BType , DbType.Int16);
			par.Add("@DianZanNum",article.DianZanNum , DbType.Int32);
			par.Add("@Score",article.Score , DbType.Int32);
			par.Add("@IsStick",article.IsStick , DbType.Boolean);
			par.Add("@IsJiaJing",article.IsJiaJing , DbType.Boolean);
			par.Add("@IsCloseComment",article.IsCloseComment , DbType.Boolean);
			par.Add("@CloseCommentReason",article.CloseCommentReason , DbType.String);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取ArticleInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static ArticleInfo GetArticleInfo(long id)
		{
			string sql = "select [Id],[Title],[Content],[Type],[CommentNum],[Dot],[CreateTime],[UserId],[ImgNum],[AttachmentNum],[LastEditUserId],[LastEditTime],[Guid],[IsDelete],[State],[RefuseReason],[BType],[DianZanNum],[Score],[IsStick],[IsJiaJing],[IsCloseComment],[CloseCommentReason] FROM [Article] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerQuerySql<ArticleInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除Article
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteArticle(long id)
		{
			string sql="DELETE [Article] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除Article多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteArticles(long[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [Article] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取Article分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Article列表</returns>
       public static List<ArticleInfo> GetArticlePageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<ArticleInfo>(DbConfig.ArticleManagerConnString, "proc_GetArticlePageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取Article分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Article列表</returns>
       public static List<ArticleInfo> GetArticlePageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[Title],[Content],[Type],[CommentNum],[Dot],[CreateTime],[UserId],[ImgNum],[AttachmentNum],[LastEditUserId],[LastEditTime],[Guid],[IsDelete],[State],[RefuseReason],[BType],[DianZanNum],[Score],[IsStick],[IsJiaJing],[IsCloseComment],[CloseCommentReason]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<ArticleInfo>.GetPageList("[Article]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
