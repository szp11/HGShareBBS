using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
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
			return DataProvider.Attachments.AddAttachment(attachment);
		}
		/// <summary>
       /// 修改AttachmentInfo
       /// </summary>
       /// <param name="attachment"></param>
       /// <returns></returns>
       public static int UpdateAttachment(AttachmentInfo attachment)
       {
           return DataProvider.Attachments.UpdateAttachment(attachment);
       }
		/// <summary>
		/// 根据id获取AttachmentInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static AttachmentInfo GetAttachmentInfo(long id)
		{
			return DataProvider.Attachments.GetAttachmentInfo(id);
		}
		/// <summary>
		/// 根据id删除Attachment
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteAttachment(long id)
		{
			return DataProvider.Attachments.DeleteAttachment(id);
		}
		/// <summary>
		/// 根据ids删除Attachment多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteAttachments(long[] ids)
		{
			return DataProvider.Attachments.DeleteAttachments(ids);
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
           return DataProvider.Attachments.GetAttachmentPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
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
           return DataProvider.Attachments.GetAttachmentPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="attachment"></param>
       /// <returns></returns>
       public static AttachmentVModel AttachmentInfoToVModel(AttachmentInfo attachment)
       {
           if(attachment==null)
               return new AttachmentVModel();
           return new AttachmentVModel
           {
				Id=attachment.Id,
			   FileName=attachment.FileName,
			   FileTitle=attachment.FileTitle,
			   Description=attachment.Description,
			   Type=attachment.Type,
			   Width=attachment.Width,
			   Height=attachment.Height,
			   FileSize=attachment.FileSize,
			   IsShow=attachment.IsShow,
			   AId=attachment.AId,
			   Score=attachment.Score,
			   State=attachment.State,
			   UserId=attachment.UserId,
			   InTime=attachment.InTime,
			   BType=attachment.BType,
			   LocalPath=attachment.LocalPath,
			   VirtualPath=attachment.VirtualPath,
			   Guid=attachment.Guid
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="attachmentInfos"></param>
       /// <returns></returns>
       public static List<AttachmentVModel> AttachmentInfosToVModels(List<AttachmentInfo> attachmentInfos)
       {
           return attachmentInfos.Select(AttachmentInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="attachment"></param>
       /// <returns></returns>
       public static AttachmentInfo AttachmentVModelToInfo(AttachmentVModel attachment)
       {
           if (attachment == null)
               return new AttachmentInfo();
           return new AttachmentInfo
           {
              Id=attachment.Id,
			   FileName=attachment.FileName,
			   FileTitle=attachment.FileTitle,
			   Description=attachment.Description,
			   Type=attachment.Type,
			   Width=attachment.Width,
			   Height=attachment.Height,
			   FileSize=attachment.FileSize,
			   IsShow=attachment.IsShow,
			   AId=attachment.AId,
			   Score=attachment.Score,
			   State=attachment.State,
			   UserId=attachment.UserId,
			   InTime=attachment.InTime,
			   BType=attachment.BType,
			   LocalPath=attachment.LocalPath,
			   VirtualPath=attachment.VirtualPath,
			   Guid=attachment.Guid
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="attachmentVModels"></param>
       /// <returns></returns>
       public static List<AttachmentInfo> AttachmentVModelsToInfos(List<AttachmentVModel> attachmentVModels)
       {
           return attachmentVModels.Select(AttachmentVModelToInfo).ToList();
       }
	   #endregion
    }
}
