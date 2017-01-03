using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// Comment 
    /// </summary>
    public class Comments
    {

		/// <summary>
		/// 添加CommentInfo
		/// </summary>
		/// <param name="comment"></param>
		/// <returns></returns>
		public static long AddComment(CommentInfo comment)
		{
			return DataProvider.Comments.AddComment(comment);
		}
		/// <summary>
       /// 修改CommentInfo
       /// </summary>
       /// <param name="comment"></param>
       /// <returns></returns>
       public static int UpdateComment(CommentInfo comment)
       {
           return DataProvider.Comments.UpdateComment(comment);
       }
		/// <summary>
		/// 根据id获取CommentInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static CommentInfo GetCommentInfo(long id)
		{
			return DataProvider.Comments.GetCommentInfo(id);
		}
		/// <summary>
		/// 根据id删除Comment
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteComment(long id)
		{
			return DataProvider.Comments.DeleteComment(id);
		}
		/// <summary>
		/// 根据ids删除Comment多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteComments(long[] ids)
		{
			return DataProvider.Comments.DeleteComments(ids);
		}
		/// <summary>
       /// 获取Comment分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Comment列表</returns>
       public static List<CommentInfo> GetCommentPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.Comments.GetCommentPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取Comment分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Comment列表</returns>
       public static List<CommentInfo> GetCommentPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.Comments.GetCommentPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="comment"></param>
       /// <returns></returns>
       public static CommentVModel CommentInfoToVModel(CommentInfo comment)
       {
           if(comment==null)
               return new CommentVModel();
           return new CommentVModel
           {
				Id=comment.Id,
			   AId=comment.AId,
			   UserId=comment.UserId,
			   CreateTime=comment.CreateTime,
			   Content=comment.Content,
			   IP=comment.IP,
			   UserAgent=comment.UserAgent,
			   State=comment.State,
			   RefuseReason=comment.RefuseReason,
			   IsDelete=comment.IsDelete,
			   LastEditUserId=comment.LastEditUserId,
			   LastEditTime=comment.LastEditTime,
			   IsStick=comment.IsStick,
			   DianZanNum=comment.DianZanNum
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="commentInfos"></param>
       /// <returns></returns>
       public static List<CommentVModel> CommentInfosToVModels(List<CommentInfo> commentInfos)
       {
           return commentInfos.Select(CommentInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="comment"></param>
       /// <returns></returns>
       public static CommentInfo CommentVModelToInfo(CommentVModel comment)
       {
           if (comment == null)
               return new CommentInfo();
           return new CommentInfo
           {
              Id=comment.Id,
			   AId=comment.AId,
			   UserId=comment.UserId,
			   CreateTime=comment.CreateTime,
			   Content=comment.Content,
			   IP=comment.IP,
			   UserAgent=comment.UserAgent,
			   State=comment.State,
			   RefuseReason=comment.RefuseReason,
			   IsDelete=comment.IsDelete,
			   LastEditUserId=comment.LastEditUserId,
			   LastEditTime=comment.LastEditTime,
			   IsStick=comment.IsStick,
			   DianZanNum=comment.DianZanNum
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="commentVModels"></param>
       /// <returns></returns>
       public static List<CommentInfo> CommentVModelsToInfos(List<CommentVModel> commentVModels)
       {
           return commentVModels.Select(CommentVModelToInfo).ToList();
       }
	   #endregion
    }
}
