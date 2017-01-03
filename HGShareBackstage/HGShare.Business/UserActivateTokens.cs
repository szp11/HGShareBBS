using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{
    /// <summary>
    /// UserActivateToken 
    /// </summary>
    public class UserActivateTokens
    {

        /// <summary>
        /// 添加UserActivateTokenInfo
        /// </summary>
        /// <param name="useractivatetoken"></param>
        /// <returns></returns>
        public static long AddUserActivateToken(UserActivateTokenInfo useractivatetoken)
        {
            return DataProvider.UserActivateTokens.AddUserActivateToken(useractivatetoken);
        }
        /// <summary>
        /// 修改UserActivateTokenInfo
        /// </summary>
        /// <param name="useractivatetoken"></param>
        /// <returns></returns>
        public static int UpdateUserActivateToken(UserActivateTokenInfo useractivatetoken)
        {
            return DataProvider.UserActivateTokens.UpdateUserActivateToken(useractivatetoken);
        }
        /// <summary>
        /// 根据id获取UserActivateTokenInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserActivateTokenInfo GetUserActivateTokenInfo(long id)
        {
            return DataProvider.UserActivateTokens.GetUserActivateTokenInfo(id);
        }
        /// <summary>
        /// 根据id删除UserActivateToken
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteUserActivateToken(long id)
        {
            return DataProvider.UserActivateTokens.DeleteUserActivateToken(id);
        }
        /// <summary>
        /// 根据ids删除UserActivateToken多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteUserActivateTokens(long[] ids)
        {
            return DataProvider.UserActivateTokens.DeleteUserActivateTokens(ids);
        }
        /// <summary>
        /// 获取UserActivateToken分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>UserActivateToken列表</returns>
        public static List<UserActivateTokenInfo> GetUserActivateTokenPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            return DataProvider.UserActivateTokens.GetUserActivateTokenPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
        }
        /// <summary>
        /// 获取UserActivateToken分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>UserActivateToken列表</returns>
        public static List<UserActivateTokenInfo> GetUserActivateTokenPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            return DataProvider.UserActivateTokens.GetUserActivateTokenPageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="useractivatetoken"></param>
        /// <returns></returns>
        public static UserActivateTokenVModel UserActivateTokenInfoToVModel(UserActivateTokenInfo useractivatetoken)
        {
            if (useractivatetoken == null)
                return new UserActivateTokenVModel();
            return new UserActivateTokenVModel
            {
                Id = useractivatetoken.Id,
                UserId = useractivatetoken.UserId,
                Email = useractivatetoken.Email,
                Token = useractivatetoken.Token,
                Status = useractivatetoken.Status,
                CreateTime = useractivatetoken.CreateTime
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="useractivatetokenInfos"></param>
        /// <returns></returns>
        public static List<UserActivateTokenVModel> UserActivateTokenInfosToVModels(List<UserActivateTokenInfo> useractivatetokenInfos)
        {
            return useractivatetokenInfos.Select(UserActivateTokenInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="useractivatetoken"></param>
        /// <returns></returns>
        public static UserActivateTokenInfo UserActivateTokenVModelToInfo(UserActivateTokenVModel useractivatetoken)
        {
            if (useractivatetoken == null)
                return new UserActivateTokenInfo();
            return new UserActivateTokenInfo
            {
                Id = useractivatetoken.Id,
                UserId = useractivatetoken.UserId,
                Email = useractivatetoken.Email,
                Token = useractivatetoken.Token,
                Status = useractivatetoken.Status,
                CreateTime = useractivatetoken.CreateTime
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="useractivatetokenVModels"></param>
        /// <returns></returns>
        public static List<UserActivateTokenInfo> UserActivateTokenVModelsToInfos(List<UserActivateTokenVModel> useractivatetokenVModels)
        {
            return useractivatetokenVModels.Select(UserActivateTokenVModelToInfo).ToList();
        }
        #endregion

        /// <summary>
        /// 修改Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool UpdateStatus(long id, bool status)
        {
            return DataProvider.UserActivateTokens.UpdateStatus(id, status);
        }
        /// <summary>
        /// 修改 Status 为无效 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool UpdateStatusToFalseByUserId(int userId)
        {
            return DataProvider.UserActivateTokens.UpdateStatusToFalseByUserId(userId);
        }
        /// <summary>
        /// 获取UserActivateToken id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public static long GetUserActivateTokenId(int userId, string email, string token, int expireTime)
        {
            return DataProvider.UserActivateTokens.GetUserActivateTokenId(userId, email, token, expireTime);
        }
        /// <summary>
        /// 获取UserActivateToken id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public static long GetUserActivateTokenId(string token, int expireTime)
        {
            return DataProvider.UserActivateTokens.GetUserActivateTokenId(token, expireTime);
        }
    }
}
