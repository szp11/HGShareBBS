using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{
    /// <summary>
    /// DianZanLog 
    /// </summary>
    public class DianZanLogs
    {

        /// <summary>
        /// 添加DianZanLogInfo
        /// </summary>
        /// <param name="dianzanlog"></param>
        /// <returns></returns>
        public static long AddDianZanLog(DianZanLogInfo dianzanlog)
        {
            return DataProvider.DianZanLogs.AddDianZanLog(dianzanlog);
        }
        /// <summary>
        /// 修改DianZanLogInfo
        /// </summary>
        /// <param name="dianzanlog"></param>
        /// <returns></returns>
        public static int UpdateDianZanLog(DianZanLogInfo dianzanlog)
        {
            return DataProvider.DianZanLogs.UpdateDianZanLog(dianzanlog);
        }
        /// <summary>
        /// 根据id获取DianZanLogInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DianZanLogInfo GetDianZanLogInfo(long id)
        {
            return DataProvider.DianZanLogs.GetDianZanLogInfo(id);
        }
        /// <summary>
        /// 根据id删除DianZanLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteDianZanLog(long id)
        {
            return DataProvider.DianZanLogs.DeleteDianZanLog(id);
        }
        /// <summary>
        /// 根据ids删除DianZanLog多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteDianZanLogs(long[] ids)
        {
            return DataProvider.DianZanLogs.DeleteDianZanLogs(ids);
        }
        /// <summary>
        /// 获取DianZanLog分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>DianZanLog列表</returns>
        public static List<DianZanLogInfo> GetDianZanLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            return DataProvider.DianZanLogs.GetDianZanLogPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
        }
        /// <summary>
        /// 获取DianZanLog分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>DianZanLog列表</returns>
        public static List<DianZanLogInfo> GetDianZanLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            return DataProvider.DianZanLogs.GetDianZanLogPageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="dianzanlog"></param>
        /// <returns></returns>
        public static DianZanLogVModel DianZanLogInfoToVModel(DianZanLogInfo dianzanlog)
        {
            if (dianzanlog == null)
                return new DianZanLogVModel();
            return new DianZanLogVModel
            {
                Id = dianzanlog.Id,
                MId = dianzanlog.MId,
                CId = dianzanlog.CId,
                UserId = dianzanlog.UserId,
                Ip = dianzanlog.Ip,
                IsCancel = dianzanlog.IsCancel,
                CancelTime = dianzanlog.CancelTime,
                Type = dianzanlog.Type,
                CreateTime = dianzanlog.CreateTime
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="dianzanlogInfos"></param>
        /// <returns></returns>
        public static List<DianZanLogVModel> DianZanLogInfosToVModels(List<DianZanLogInfo> dianzanlogInfos)
        {
            return dianzanlogInfos.Select(DianZanLogInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="dianzanlog"></param>
        /// <returns></returns>
        public static DianZanLogInfo DianZanLogVModelToInfo(DianZanLogVModel dianzanlog)
        {
            if (dianzanlog == null)
                return new DianZanLogInfo();
            return new DianZanLogInfo
            {
                Id = dianzanlog.Id,
                MId = dianzanlog.MId,
                CId = dianzanlog.CId,
                UserId = dianzanlog.UserId,
                Ip = dianzanlog.Ip,
                IsCancel = dianzanlog.IsCancel,
                CancelTime = dianzanlog.CancelTime,
                Type = dianzanlog.Type,
                CreateTime = dianzanlog.CreateTime
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="dianzanlogVModels"></param>
        /// <returns></returns>
        public static List<DianZanLogInfo> DianZanLogVModelsToInfos(List<DianZanLogVModel> dianzanlogVModels)
        {
            return dianzanlogVModels.Select(DianZanLogVModelToInfo).ToList();
        }
        #endregion

        /// <summary>
        /// 更新取消状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCancel"></param>
        /// <returns></returns>
        public static bool UpdateIsCancel(long id, bool isCancel)
        {
            return DataProvider.DianZanLogs.UpdateIsCancel(id, isCancel);
        }
        /// <summary>
        /// 检查是否已经点赞
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mId"></param>
        /// <param name="cId"></param>
        /// <returns></returns>
        public static long GetDianZanLogId(int userId, long mId, long cId)
        {
            return DataProvider.DianZanLogs.GetDianZanLogId(userId, mId, cId);
        }
        /// <summary>
        /// 获取用户所有点过赞的评论id
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="aId">文章id</param>
        /// <returns></returns>
        public static List<long> GetUserAllDianZanCommentId(int userId, long aId)
        {
            return DataProvider.DianZanLogs.GetUserAllDianZanCommentId(userId, aId);
        }
    }
}
