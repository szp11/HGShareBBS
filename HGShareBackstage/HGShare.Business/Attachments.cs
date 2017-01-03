using System;
using System.Collections.Generic;
using System.Linq;
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
        public static int AddAttachment(AttachmentInfo attachment)
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
        public static AttachmentInfo GetAttachmentInfo(int id)
        {
            return DataProvider.Attachments.GetAttachmentInfo(id);
        }
        /// <summary>
        /// 根据id删除Attachment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteAttachment(int id)
        {
            return DataProvider.Attachments.DeleteAttachment(id);
        }

        /// <summary>
        /// 根据guid获取AttachmentInfo 集合
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static List<AttachmentInfo> GetAttachmentByGuid(Guid guid)
        {
            return DataProvider.Attachments.GetAttachmentByGuid(guid);
        }

        /// <summary>
        /// 根据ids删除Attachment多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteAttachments(int[] ids)
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
            return DataProvider.Attachments.GetAttachmentPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
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
            return DataProvider.Attachments.GetAttachmentPageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="attachmentInfo"></param>
        /// <returns></returns>
        public static AttachmentVModel AttachmentInfoToVModel(AttachmentInfo attachmentInfo)
        {
            if (attachmentInfo == null)
                return new AttachmentVModel();
            return new AttachmentVModel
            {
                Id = attachmentInfo.Id,
                FileName = attachmentInfo.FileName,
                FileTitle = attachmentInfo.FileTitle,
                Description = attachmentInfo.Description,
                Type = attachmentInfo.Type,
                Width = attachmentInfo.Width,
                Height = attachmentInfo.Height,
                FileSize = attachmentInfo.FileSize,
                IsShow = attachmentInfo.IsShow,
                AId = attachmentInfo.AId,
                Score = attachmentInfo.Score,
                State = attachmentInfo.State,
                UserId = attachmentInfo.UserId,
                Guid = attachmentInfo.Guid,
                InTime = attachmentInfo.InTime,
                BType = attachmentInfo.BType,
                LocalPath = attachmentInfo.LocalPath,
                VirtualPath = attachmentInfo.VirtualPath
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
        /// <param name="attachmentVModel"></param>
        /// <returns></returns>
        public static AttachmentInfo AttachmentVModelToInfo(AttachmentVModel attachmentVModel)
        {
            if (attachmentVModel == null)
                return new AttachmentInfo();
            return new AttachmentInfo
            {
                Id = attachmentVModel.Id,
                FileName = attachmentVModel.FileName,
                FileTitle = attachmentVModel.FileTitle,
                Description = attachmentVModel.Description,
                Type = attachmentVModel.Type,
                Width = attachmentVModel.Width,
                Height = attachmentVModel.Height,
                FileSize = attachmentVModel.FileSize,
                IsShow = attachmentVModel.IsShow,
                AId = attachmentVModel.AId,
                Score = attachmentVModel.Score,
                State = attachmentVModel.State,
                UserId = attachmentVModel.UserId,
                Guid = attachmentVModel.Guid,
                InTime = attachmentVModel.InTime,
                BType = attachmentVModel.BType,
                LocalPath = attachmentVModel.LocalPath,
                VirtualPath = attachmentVModel.VirtualPath
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
