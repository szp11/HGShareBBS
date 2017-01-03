using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
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
			return DataProvider.Articles.AddArticle(article);
		}
		/// <summary>
       /// 修改ArticleInfo
       /// </summary>
       /// <param name="article"></param>
       /// <returns></returns>
       public static int UpdateArticle(ArticleInfo article)
       {
           return DataProvider.Articles.UpdateArticle(article);
       }
		/// <summary>
		/// 根据id获取ArticleInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static ArticleInfo GetArticleInfo(long id)
		{
			return DataProvider.Articles.GetArticleInfo(id);
		}
		/// <summary>
		/// 根据id删除Article
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteArticle(long id)
		{
			return DataProvider.Articles.DeleteArticle(id);
		}
		/// <summary>
		/// 根据ids删除Article多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteArticles(long[] ids)
		{
			return DataProvider.Articles.DeleteArticles(ids);
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
           return DataProvider.Articles.GetArticlePageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
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
           return DataProvider.Articles.GetArticlePageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="article"></param>
       /// <returns></returns>
       public static ArticleVModel ArticleInfoToVModel(ArticleInfo article)
       {
           if(article==null)
               return new ArticleVModel();
           return new ArticleVModel
           {
				Id=article.Id,
			   Title=article.Title,
			   Content=article.Content,
			   Type=article.Type,
			   CommentNum=article.CommentNum,
			   Dot=article.Dot,
			   CreateTime=article.CreateTime,
			   UserId=article.UserId,
			   ImgNum=article.ImgNum,
			   AttachmentNum=article.AttachmentNum,
			   LastEditUserId=article.LastEditUserId,
			   LastEditTime=article.LastEditTime,
			   Guid=article.Guid,
			   IsDelete=article.IsDelete,
			   State=article.State,
			   RefuseReason=article.RefuseReason,
			   BType=article.BType,
			   DianZanNum=article.DianZanNum,
			   Score=article.Score,
			   IsStick=article.IsStick,
			   IsJiaJing=article.IsJiaJing,
			   IsCloseComment=article.IsCloseComment,
			   CloseCommentReason=article.CloseCommentReason
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="articleInfos"></param>
       /// <returns></returns>
       public static List<ArticleVModel> ArticleInfosToVModels(List<ArticleInfo> articleInfos)
       {
           return articleInfos.Select(ArticleInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="article"></param>
       /// <returns></returns>
       public static ArticleInfo ArticleVModelToInfo(ArticleVModel article)
       {
           if (article == null)
               return new ArticleInfo();
           return new ArticleInfo
           {
              Id=article.Id,
			   Title=article.Title,
			   Content=article.Content,
			   Type=article.Type,
			   CommentNum=article.CommentNum,
			   Dot=article.Dot,
			   CreateTime=article.CreateTime,
			   UserId=article.UserId,
			   ImgNum=article.ImgNum,
			   AttachmentNum=article.AttachmentNum,
			   LastEditUserId=article.LastEditUserId,
			   LastEditTime=article.LastEditTime,
			   Guid=article.Guid,
			   IsDelete=article.IsDelete,
			   State=article.State,
			   RefuseReason=article.RefuseReason,
			   BType=article.BType,
			   DianZanNum=article.DianZanNum,
			   Score=article.Score,
			   IsStick=article.IsStick,
			   IsJiaJing=article.IsJiaJing,
			   IsCloseComment=article.IsCloseComment,
			   CloseCommentReason=article.CloseCommentReason
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="articleVModels"></param>
       /// <returns></returns>
       public static List<ArticleInfo> ArticleVModelsToInfos(List<ArticleVModel> articleVModels)
       {
           return articleVModels.Select(ArticleVModelToInfo).ToList();
       }
	   #endregion
    }
}
