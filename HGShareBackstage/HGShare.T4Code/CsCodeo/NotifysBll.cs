using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// Notify 
    /// </summary>
    public class Notifys
    {

		/// <summary>
		/// 添加NotifyInfo
		/// </summary>
		/// <param name="notify"></param>
		/// <returns></returns>
		public static long AddNotify(NotifyInfo notify)
		{
			return DataProvider.Notifys.AddNotify(notify);
		}
		/// <summary>
       /// 修改NotifyInfo
       /// </summary>
       /// <param name="notify"></param>
       /// <returns></returns>
       public static int UpdateNotify(NotifyInfo notify)
       {
           return DataProvider.Notifys.UpdateNotify(notify);
       }
		/// <summary>
		/// 根据id获取NotifyInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static NotifyInfo GetNotifyInfo(long id)
		{
			return DataProvider.Notifys.GetNotifyInfo(id);
		}
		/// <summary>
		/// 根据id删除Notify
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteNotify(long id)
		{
			return DataProvider.Notifys.DeleteNotify(id);
		}
		/// <summary>
		/// 根据ids删除Notify多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteNotifys(long[] ids)
		{
			return DataProvider.Notifys.DeleteNotifys(ids);
		}
		/// <summary>
       /// 获取Notify分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Notify列表</returns>
       public static List<NotifyInfo> GetNotifyPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.Notifys.GetNotifyPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取Notify分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Notify列表</returns>
       public static List<NotifyInfo> GetNotifyPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.Notifys.GetNotifyPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="notify"></param>
       /// <returns></returns>
       public static NotifyVModel NotifyInfoToVModel(NotifyInfo notify)
       {
           if(notify==null)
               return new NotifyVModel();
           return new NotifyVModel
           {
				Id=notify.Id,
			   FromUserId=notify.FromUserId,
			   ToUserId=notify.ToUserId,
			   CreateTime=notify.CreateTime,
			   IsDelete=notify.IsDelete,
			   IsRead=notify.IsRead,
			   IsSystem=notify.IsSystem,
			   Content=notify.Content,
			   Title=notify.Title
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="notifyInfos"></param>
       /// <returns></returns>
       public static List<NotifyVModel> NotifyInfosToVModels(List<NotifyInfo> notifyInfos)
       {
           return notifyInfos.Select(NotifyInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="notify"></param>
       /// <returns></returns>
       public static NotifyInfo NotifyVModelToInfo(NotifyVModel notify)
       {
           if (notify == null)
               return new NotifyInfo();
           return new NotifyInfo
           {
              Id=notify.Id,
			   FromUserId=notify.FromUserId,
			   ToUserId=notify.ToUserId,
			   CreateTime=notify.CreateTime,
			   IsDelete=notify.IsDelete,
			   IsRead=notify.IsRead,
			   IsSystem=notify.IsSystem,
			   Content=notify.Content,
			   Title=notify.Title
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="notifyVModels"></param>
       /// <returns></returns>
       public static List<NotifyInfo> NotifyVModelsToInfos(List<NotifyVModel> notifyVModels)
       {
           return notifyVModels.Select(NotifyVModelToInfo).ToList();
       }
	   #endregion
    }
}
