using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// UserScoreLog 
    /// </summary>
    public class UserScoreLogs
    {

		/// <summary>
		/// 添加UserScoreLogInfo
		/// </summary>
		/// <param name="userscorelog"></param>
		/// <returns></returns>
		public static long AddUserScoreLog(UserScoreLogInfo userscorelog)
		{
			return DataProvider.UserScoreLogs.AddUserScoreLog(userscorelog);
		}
		/// <summary>
       /// 修改UserScoreLogInfo
       /// </summary>
       /// <param name="userscorelog"></param>
       /// <returns></returns>
       public static int UpdateUserScoreLog(UserScoreLogInfo userscorelog)
       {
           return DataProvider.UserScoreLogs.UpdateUserScoreLog(userscorelog);
       }
		/// <summary>
		/// 根据id获取UserScoreLogInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static UserScoreLogInfo GetUserScoreLogInfo(long id)
		{
			return DataProvider.UserScoreLogs.GetUserScoreLogInfo(id);
		}
		/// <summary>
		/// 根据id删除UserScoreLog
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteUserScoreLog(long id)
		{
			return DataProvider.UserScoreLogs.DeleteUserScoreLog(id);
		}
		/// <summary>
		/// 根据ids删除UserScoreLog多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteUserScoreLogs(long[] ids)
		{
			return DataProvider.UserScoreLogs.DeleteUserScoreLogs(ids);
		}
		/// <summary>
       /// 获取UserScoreLog分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>UserScoreLog列表</returns>
       public static List<UserScoreLogInfo> GetUserScoreLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.UserScoreLogs.GetUserScoreLogPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取UserScoreLog分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>UserScoreLog列表</returns>
       public static List<UserScoreLogInfo> GetUserScoreLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.UserScoreLogs.GetUserScoreLogPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="userscorelog"></param>
       /// <returns></returns>
       public static UserScoreLogVModel UserScoreLogInfoToVModel(UserScoreLogInfo userscorelog)
       {
           if(userscorelog==null)
               return new UserScoreLogVModel();
           return new UserScoreLogVModel
           {
				Id=userscorelog.Id,
			   UserId=userscorelog.UserId,
			   Score=userscorelog.Score,
			   CreateTime=userscorelog.CreateTime,
			   Describe=userscorelog.Describe,
			   OldScore=userscorelog.OldScore,
			   NewScore=userscorelog.NewScore
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="userscorelogInfos"></param>
       /// <returns></returns>
       public static List<UserScoreLogVModel> UserScoreLogInfosToVModels(List<UserScoreLogInfo> userscorelogInfos)
       {
           return userscorelogInfos.Select(UserScoreLogInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="userscorelog"></param>
       /// <returns></returns>
       public static UserScoreLogInfo UserScoreLogVModelToInfo(UserScoreLogVModel userscorelog)
       {
           if (userscorelog == null)
               return new UserScoreLogInfo();
           return new UserScoreLogInfo
           {
              Id=userscorelog.Id,
			   UserId=userscorelog.UserId,
			   Score=userscorelog.Score,
			   CreateTime=userscorelog.CreateTime,
			   Describe=userscorelog.Describe,
			   OldScore=userscorelog.OldScore,
			   NewScore=userscorelog.NewScore
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="userscorelogVModels"></param>
       /// <returns></returns>
       public static List<UserScoreLogInfo> UserScoreLogVModelsToInfos(List<UserScoreLogVModel> userscorelogVModels)
       {
           return userscorelogVModels.Select(UserScoreLogVModelToInfo).ToList();
       }
	   #endregion
    }
}
