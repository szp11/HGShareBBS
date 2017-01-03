using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{
    /// <summary>
    /// UserPosition 
    /// </summary>
    public class UserPositions
    {

        /// <summary>
        /// 添加UserPositionInfo
        /// </summary>
        /// <param name="userposition"></param>
        /// <returns></returns>
        public static int AddUserPosition(UserPositionInfo userposition)
        {
            return DataProvider.UserPositions.AddUserPosition(userposition);
        }
        /// <summary>
        /// 修改UserPositionInfo
        /// </summary>
        /// <param name="userposition"></param>
        /// <returns></returns>
        public static int UpdateUserPosition(UserPositionInfo userposition)
        {
            return DataProvider.UserPositions.UpdateUserPosition(userposition);
        }
        /// <summary>
        /// 根据userid获取UserPositionInfo
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<UserPositionInfo> GetUserPositionInfos(int userid)
        {
            return DataProvider.UserPositions.GetUserPositionInfos(userid);
        }
        /// <summary>
        /// 根据userid删除UserPosition
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Int32 DeleteUserPosition(int userid)
        {
            return DataProvider.UserPositions.DeleteUserPosition(userid);
        }
        /// <summary>
        /// 根据userids删除UserPosition多条记录
        /// </summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        public static Int32 DeleteUserPositions(int[] userids)
        {
            return DataProvider.UserPositions.DeleteUserPositions(userids);
        }
        /// <summary>
        /// 获取UserPosition分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>UserPosition列表</returns>
        public static List<UserPositionInfo> GetUserPositionPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            return DataProvider.UserPositions.GetUserPositionPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
        }
        /// <summary>
        /// 获取UserPosition分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>UserPosition列表</returns>
        public static List<UserPositionInfo> GetUserPositionPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            return DataProvider.UserPositions.GetUserPositionPageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="userpositionInfo"></param>
        /// <returns></returns>
        public static UserPositionVModel UserPositionInfoToVModel(UserPositionInfo userpositionInfo)
        {
            if (userpositionInfo == null)
                return new UserPositionVModel();
            return new UserPositionVModel
            {
                UserId = userpositionInfo.UserId,
                Code = userpositionInfo.Code,
                Type = userpositionInfo.Type
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="userpositionInfos"></param>
        /// <returns></returns>
        public static List<UserPositionVModel> UserPositionInfosToVModels(List<UserPositionInfo> userpositionInfos)
        {
            return userpositionInfos.Select(UserPositionInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="userpositionVModel"></param>
        /// <returns></returns>
        public static UserPositionInfo UserPositionVModelToInfo(UserPositionVModel userpositionVModel)
        {
            if (userpositionVModel == null)
                return new UserPositionInfo();
            return new UserPositionInfo
            {
                UserId = userpositionVModel.UserId,
                Code = userpositionVModel.Code,
                Type = userpositionVModel.Type
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="userpositionVModels"></param>
        /// <returns></returns>
        public static List<UserPositionInfo> UserPositionVModelsToInfos(List<UserPositionVModel> userpositionVModels)
        {
            return userpositionVModels.Select(UserPositionVModelToInfo).ToList();
        }
        #endregion

        #region 前端
        /// <summary>
        /// 根据userid获取UserPosition
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<UserPositionVModel> GetUserPositions(int userid)
        {
            return DataProvider.UserPositions.GetUserPositions(userid);
        }

        #endregion
    }
}
