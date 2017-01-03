using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{
    /// <summary>
    /// UserAccessLog 
    /// </summary>
    public class UserAccessLogs
    {

        /// <summary>
        /// 添加UserAccessLogInfo
        /// </summary>
        /// <param name="useraccesslog"></param>
        /// <returns></returns>
        public static long AddUserAccessLog(UserAccessLogInfo useraccesslog)
        {
            return DataProvider.UserAccessLogs.AddUserAccessLog(useraccesslog);
        }
        /// <summary>
        /// 添加UserAccessLogInfo
        /// </summary>
        /// <param name="useraccesslog"></param>
        /// <returns></returns>
        public static async Task<long> AddUserAccessLogAsync(UserAccessLogInfo useraccesslog)
        {
            return await DataProvider.UserAccessLogs.AddUserAccessLogAsync(useraccesslog);
        }
        /// <summary>
        /// 修改UserAccessLogInfo
        /// </summary>
        /// <param name="useraccesslog"></param>
        /// <returns></returns>
        public static int UpdateUserAccessLog(UserAccessLogInfo useraccesslog)
        {
            return DataProvider.UserAccessLogs.UpdateUserAccessLog(useraccesslog);
        }
        /// <summary>
        /// 根据id获取UserAccessLogInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserAccessLogInfo GetUserAccessLogInfo(long id)
        {
            return DataProvider.UserAccessLogs.GetUserAccessLogInfo(id);
        }
        /// <summary>
        /// 根据id删除UserAccessLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteUserAccessLog(long id)
        {
            return DataProvider.UserAccessLogs.DeleteUserAccessLog(id);
        }
        /// <summary>
        /// 根据ids删除UserAccessLog多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteUserAccessLogs(long[] ids)
        {
            return DataProvider.UserAccessLogs.DeleteUserAccessLogs(ids);
        }
        /// <summary>
        /// 获取UserAccessLog分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>UserAccessLog列表</returns>
        public static List<UserAccessLogInfo> GetUserAccessLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            return DataProvider.UserAccessLogs.GetUserAccessLogPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
        }
        /// <summary>
        /// 获取UserAccessLog分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>UserAccessLog列表</returns>
        public static List<UserAccessLogInfo> GetUserAccessLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            return DataProvider.UserAccessLogs.GetUserAccessLogPageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="useraccesslog"></param>
        /// <returns></returns>
        public static UserAccessLogVModel UserAccessLogInfoToVModel(UserAccessLogInfo useraccesslog)
        {
            if (useraccesslog == null)
                return new UserAccessLogVModel();
            return new UserAccessLogVModel
            {
                Id = useraccesslog.Id,
                Url = useraccesslog.Url,
                Referer = useraccesslog.Referer,
                UserAgent = useraccesslog.UserAgent,
                UserId = useraccesslog.UserId,
                Ip = useraccesslog.Ip,
                InsertTime = useraccesslog.InsertTime,
                Other = useraccesslog.Other,
                Type = useraccesslog.Type
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="useraccesslogInfos"></param>
        /// <returns></returns>
        public static List<UserAccessLogVModel> UserAccessLogInfosToVModels(List<UserAccessLogInfo> useraccesslogInfos)
        {
            return useraccesslogInfos.Select(UserAccessLogInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="useraccesslog"></param>
        /// <returns></returns>
        public static UserAccessLogInfo UserAccessLogVModelToInfo(UserAccessLogVModel useraccesslog)
        {
            if (useraccesslog == null)
                return new UserAccessLogInfo();
            return new UserAccessLogInfo
            {
                Id = useraccesslog.Id,
                Url = useraccesslog.Url,
                Referer = useraccesslog.Referer,
                UserAgent = useraccesslog.UserAgent,
                UserId = useraccesslog.UserId,
                Ip = useraccesslog.Ip,
                InsertTime = useraccesslog.InsertTime,
                Other = useraccesslog.Other,
                Type = useraccesslog.Type
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="useraccesslogVModels"></param>
        /// <returns></returns>
        public static List<UserAccessLogInfo> UserAccessLogVModelsToInfos(List<UserAccessLogVModel> useraccesslogVModels)
        {
            return useraccesslogVModels.Select(UserAccessLogVModelToInfo).ToList();
        }
        #endregion
    }
}
