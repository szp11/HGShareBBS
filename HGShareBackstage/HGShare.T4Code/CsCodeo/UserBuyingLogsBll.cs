using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// UserBuyingLog 
    /// </summary>
    public class UserBuyingLogs
    {

		/// <summary>
		/// 添加UserBuyingLogInfo
		/// </summary>
		/// <param name="userbuyinglog"></param>
		/// <returns></returns>
		public static int AddUserBuyingLog(UserBuyingLogInfo userbuyinglog)
		{
			return DataProvider.UserBuyingLogs.AddUserBuyingLog(userbuyinglog);
		}
		/// <summary>
       /// 修改UserBuyingLogInfo
       /// </summary>
       /// <param name="userbuyinglog"></param>
       /// <returns></returns>
       public static int UpdateUserBuyingLog(UserBuyingLogInfo userbuyinglog)
       {
           return DataProvider.UserBuyingLogs.UpdateUserBuyingLog(userbuyinglog);
       }
		/// <summary>
		/// 根据id获取UserBuyingLogInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static UserBuyingLogInfo GetUserBuyingLogInfo(int id)
		{
			return DataProvider.UserBuyingLogs.GetUserBuyingLogInfo(id);
		}
		/// <summary>
		/// 根据id删除UserBuyingLog
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteUserBuyingLog(int id)
		{
			return DataProvider.UserBuyingLogs.DeleteUserBuyingLog(id);
		}
		/// <summary>
		/// 根据ids删除UserBuyingLog多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteUserBuyingLogs(int[] ids)
		{
			return DataProvider.UserBuyingLogs.DeleteUserBuyingLogs(ids);
		}
		/// <summary>
       /// 获取UserBuyingLog分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>UserBuyingLog列表</returns>
       public static List<UserBuyingLogInfo> GetUserBuyingLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.UserBuyingLogs.GetUserBuyingLogPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取UserBuyingLog分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>UserBuyingLog列表</returns>
       public static List<UserBuyingLogInfo> GetUserBuyingLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.UserBuyingLogs.GetUserBuyingLogPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="userbuyinglog"></param>
       /// <returns></returns>
       public static UserBuyingLogVModel UserBuyingLogInfoToVModel(UserBuyingLogInfo userbuyinglog)
       {
           if(userbuyinglog==null)
               return new UserBuyingLogVModel();
           return new UserBuyingLogVModel
           {
				Id=userbuyinglog.Id,
			   AId=userbuyinglog.AId,
			   Score=userbuyinglog.Score,
			   UserId=userbuyinglog.UserId,
			   CreateTime=userbuyinglog.CreateTime
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="userbuyinglogInfos"></param>
       /// <returns></returns>
       public static List<UserBuyingLogVModel> UserBuyingLogInfosToVModels(List<UserBuyingLogInfo> userbuyinglogInfos)
       {
           return userbuyinglogInfos.Select(UserBuyingLogInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="userbuyinglog"></param>
       /// <returns></returns>
       public static UserBuyingLogInfo UserBuyingLogVModelToInfo(UserBuyingLogVModel userbuyinglog)
       {
           if (userbuyinglog == null)
               return new UserBuyingLogInfo();
           return new UserBuyingLogInfo
           {
              Id=userbuyinglog.Id,
			   AId=userbuyinglog.AId,
			   Score=userbuyinglog.Score,
			   UserId=userbuyinglog.UserId,
			   CreateTime=userbuyinglog.CreateTime
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="userbuyinglogVModels"></param>
       /// <returns></returns>
       public static List<UserBuyingLogInfo> UserBuyingLogVModelsToInfos(List<UserBuyingLogVModel> userbuyinglogVModels)
       {
           return userbuyinglogVModels.Select(UserBuyingLogVModelToInfo).ToList();
       }
	   #endregion
    }
}
