using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{
    /// <summary>
    /// UserOther 
    /// </summary>
    public class UserOthers
    {

        /// <summary>
        /// 添加UserOtherInfo
        /// </summary>
        /// <param name="userother"></param>
        /// <returns></returns>
        public static int AddUserOther(UserOtherInfo userother)
        {
            return DataProvider.UserOthers.AddUserOther(userother);
        }
        /// <summary>
        /// 修改UserOtherInfo
        /// </summary>
        /// <param name="userother"></param>
        /// <returns></returns>
        public static int UpdateUserOther(UserOtherInfo userother)
        {
            return DataProvider.UserOthers.UpdateUserOther(userother);
        }
        /// <summary>
        /// 根据id获取UserOtherInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserOtherInfo GetUserOtherInfo(int id)
        {
            return DataProvider.UserOthers.GetUserOtherInfo(id);
        }
        /// <summary>
        /// 根据id删除UserOther
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteUserOther(int id)
        {
            return DataProvider.UserOthers.DeleteUserOther(id);
        }
        /// <summary>
        /// 根据ids删除UserOther多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteUserOthers(int[] ids)
        {
            return DataProvider.UserOthers.DeleteUserOthers(ids);
        }
        /// <summary>
        /// 获取UserOther分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>UserOther列表</returns>
        public static List<UserOtherInfo> GetUserOtherPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            return DataProvider.UserOthers.GetUserOtherPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
        }
        /// <summary>
        /// 获取UserOther分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>UserOther列表</returns>
        public static List<UserOtherInfo> GetUserOtherPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            return DataProvider.UserOthers.GetUserOtherPageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="userotherInfo"></param>
        /// <returns></returns>
        public static UserOtherVModel UserOtherInfoToVModel(UserOtherInfo userotherInfo)
        {
            if (userotherInfo == null)
                return new UserOtherVModel();
            return new UserOtherVModel
            {
                UserId = userotherInfo.UserId,
                PersonalityIntroduce = userotherInfo.PersonalityIntroduce
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="userotherInfos"></param>
        /// <returns></returns>
        public static List<UserOtherVModel> UserOtherInfosToVModels(List<UserOtherInfo> userotherInfos)
        {
            return userotherInfos.Select(UserOtherInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="userotherVModel"></param>
        /// <returns></returns>
        public static UserOtherInfo UserOtherVModelToInfo(UserOtherVModel userotherVModel)
        {
            if (userotherVModel == null)
                return new UserOtherInfo();
            return new UserOtherInfo
            {
                UserId = userotherVModel.UserId,
                PersonalityIntroduce = userotherVModel.PersonalityIntroduce
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="userotherVModels"></param>
        /// <returns></returns>
        public static List<UserOtherInfo> UserOtherVModelsToInfos(List<UserOtherVModel> userotherVModels)
        {
            return userotherVModels.Select(UserOtherVModelToInfo).ToList();
        }
        #endregion
    }
}
