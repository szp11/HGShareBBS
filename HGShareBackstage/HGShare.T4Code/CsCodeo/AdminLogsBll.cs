using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// AdminLog 
    /// </summary>
    public class AdminLogs
    {

		/// <summary>
		/// 添加AdminLogInfo
		/// </summary>
		/// <param name="adminlog"></param>
		/// <returns></returns>
		public static int AddAdminLog(AdminLogInfo adminlog)
		{
			return DataProvider.AdminLogs.AddAdminLog(adminlog);
		}
		/// <summary>
       /// 修改AdminLogInfo
       /// </summary>
       /// <param name="adminlog"></param>
       /// <returns></returns>
       public static int UpdateAdminLog(AdminLogInfo adminlog)
       {
           return DataProvider.AdminLogs.UpdateAdminLog(adminlog);
       }
		/// <summary>
		/// 根据id获取AdminLogInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static AdminLogInfo GetAdminLogInfo(int id)
		{
			return DataProvider.AdminLogs.GetAdminLogInfo(id);
		}
		/// <summary>
		/// 根据id删除AdminLog
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteAdminLog(int id)
		{
			return DataProvider.AdminLogs.DeleteAdminLog(id);
		}
		/// <summary>
		/// 根据ids删除AdminLog多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteAdminLogs(int[] ids)
		{
			return DataProvider.AdminLogs.DeleteAdminLogs(ids);
		}
		/// <summary>
       /// 获取AdminLog分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>AdminLog列表</returns>
       public static List<AdminLogInfo> GetAdminLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.AdminLogs.GetAdminLogPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取AdminLog分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>AdminLog列表</returns>
       public static List<AdminLogInfo> GetAdminLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.AdminLogs.GetAdminLogPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="adminlog"></param>
       /// <returns></returns>
       public static AdminLogVModel AdminLogInfoToVModel(AdminLogInfo adminlog)
       {
           if(adminlog==null)
               return new AdminLogVModel();
           return new AdminLogVModel
           {
				Id=adminlog.Id,
			   UserId=adminlog.UserId,
			   Controllers=adminlog.Controllers,
			   Action=adminlog.Action,
			   Parameter=adminlog.Parameter,
			   ActionId=adminlog.ActionId,
			   Ip=adminlog.Ip,
			   Url=adminlog.Url,
			   InTime=adminlog.InTime,
			   Method=adminlog.Method,
			   IsAjax=adminlog.IsAjax,
			   UserAgent=adminlog.UserAgent,
			   ControllersDsc=adminlog.ControllersDsc,
			   ActionDsc=adminlog.ActionDsc
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="adminlogInfos"></param>
       /// <returns></returns>
       public static List<AdminLogVModel> AdminLogInfosToVModels(List<AdminLogInfo> adminlogInfos)
       {
           return adminlogInfos.Select(AdminLogInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="adminlog"></param>
       /// <returns></returns>
       public static AdminLogInfo AdminLogVModelToInfo(AdminLogVModel adminlog)
       {
           if (adminlog == null)
               return new AdminLogInfo();
           return new AdminLogInfo
           {
              Id=adminlog.Id,
			   UserId=adminlog.UserId,
			   Controllers=adminlog.Controllers,
			   Action=adminlog.Action,
			   Parameter=adminlog.Parameter,
			   ActionId=adminlog.ActionId,
			   Ip=adminlog.Ip,
			   Url=adminlog.Url,
			   InTime=adminlog.InTime,
			   Method=adminlog.Method,
			   IsAjax=adminlog.IsAjax,
			   UserAgent=adminlog.UserAgent,
			   ControllersDsc=adminlog.ControllersDsc,
			   ActionDsc=adminlog.ActionDsc
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="adminlogVModels"></param>
       /// <returns></returns>
       public static List<AdminLogInfo> AdminLogVModelsToInfos(List<AdminLogVModel> adminlogVModels)
       {
           return adminlogVModels.Select(AdminLogVModelToInfo).ToList();
       }
	   #endregion
    }
}
