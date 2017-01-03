using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
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
            return DataProvider.SendMailLogs.AddSendMailLog(sendmaillog);
        }
        /// <summary>
        /// 修改SendMailLogInfo
        /// </summary>
        /// <param name="sendmaillog"></param>
        /// <returns></returns>
        public static int UpdateSendMailLog(SendMailLogInfo sendmaillog)
        {
            return DataProvider.SendMailLogs.UpdateSendMailLog(sendmaillog);
        }
        /// <summary>
        /// 根据id获取SendMailLogInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SendMailLogInfo GetSendMailLogInfo(long id)
        {
            return DataProvider.SendMailLogs.GetSendMailLogInfo(id);
        }
        /// <summary>
        /// 根据id删除SendMailLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteSendMailLog(long id)
        {
            return DataProvider.SendMailLogs.DeleteSendMailLog(id);
        }
        /// <summary>
        /// 根据ids删除SendMailLog多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteSendMailLogs(long[] ids)
        {
            return DataProvider.SendMailLogs.DeleteSendMailLogs(ids);
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
            return DataProvider.SendMailLogs.GetSendMailLogPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
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
            return DataProvider.SendMailLogs.GetSendMailLogPageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="sendmaillog"></param>
        /// <returns></returns>
        public static SendMailLogVModel SendMailLogInfoToVModel(SendMailLogInfo sendmaillog)
        {
            if (sendmaillog == null)
                return new SendMailLogVModel();
            return new SendMailLogVModel
            {
                Id = sendmaillog.Id,
                UserId = sendmaillog.UserId,
                SendUserId = sendmaillog.SendUserId,
                TemplateId = sendmaillog.TemplateId,
                ToEmail = sendmaillog.ToEmail,
                FromEmail = sendmaillog.FromEmail,
                Status = sendmaillog.Status,
                Title = sendmaillog.Title,
                Body = sendmaillog.Body,
                Ip = sendmaillog.Ip,
                IsSystem = sendmaillog.IsSystem,
                CreateTime = sendmaillog.CreateTime
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="sendmaillogInfos"></param>
        /// <returns></returns>
        public static List<SendMailLogVModel> SendMailLogInfosToVModels(List<SendMailLogInfo> sendmaillogInfos)
        {
            return sendmaillogInfos.Select(SendMailLogInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="sendmaillog"></param>
        /// <returns></returns>
        public static SendMailLogInfo SendMailLogVModelToInfo(SendMailLogVModel sendmaillog)
        {
            if (sendmaillog == null)
                return new SendMailLogInfo();
            return new SendMailLogInfo
            {
                Id = sendmaillog.Id,
                UserId = sendmaillog.UserId,
                SendUserId = sendmaillog.SendUserId,
                TemplateId = sendmaillog.TemplateId,
                ToEmail = sendmaillog.ToEmail,
                FromEmail = sendmaillog.FromEmail,
                Status = sendmaillog.Status,
                Title = sendmaillog.Title,
                Body = sendmaillog.Body,
                Ip = sendmaillog.Ip,
                IsSystem = sendmaillog.IsSystem,
                CreateTime = sendmaillog.CreateTime
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="sendmaillogVModels"></param>
        /// <returns></returns>
        public static List<SendMailLogInfo> SendMailLogVModelsToInfos(List<SendMailLogVModel> sendmaillogVModels)
        {
            return sendmaillogVModels.Select(SendMailLogVModelToInfo).ToList();
        }
        #endregion


        /// <summary>
        /// 根据用户id获取当天用户操作发送邮件数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetSendMailLogToDayCountByUserId(int userId)
        {
            return DataProvider.SendMailLogs.GetSendMailLogToDayCountByUserId(userId);
        }

        /// <summary>
        /// 根据用户id获取当天用户操作发送邮件数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="time">多少分钟之内</param>
        /// <returns></returns>
        public static int GetSendMailLogCountByUserIdAndTime(int userId, int time)
        {
            return DataProvider.SendMailLogs.GetSendMailLogCountByUserIdAndTime(userId, time);
        }
    }
}
