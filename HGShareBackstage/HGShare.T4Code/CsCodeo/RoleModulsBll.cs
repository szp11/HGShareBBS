using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// RoleModul 
    /// </summary>
    public class RoleModuls
    {

		/// <summary>
		/// 添加RoleModulInfo
		/// </summary>
		/// <param name="rolemodul"></param>
		/// <returns></returns>
		public static int AddRoleModul(RoleModulInfo rolemodul)
		{
			return DataProvider.RoleModuls.AddRoleModul(rolemodul);
		}
		/// <summary>
       /// 修改RoleModulInfo
       /// </summary>
       /// <param name="rolemodul"></param>
       /// <returns></returns>
       public static int UpdateRoleModul(RoleModulInfo rolemodul)
       {
           return DataProvider.RoleModuls.UpdateRoleModul(rolemodul);
       }
		/// <summary>
		/// 根据id获取RoleModulInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static RoleModulInfo GetRoleModulInfo(int id)
		{
			return DataProvider.RoleModuls.GetRoleModulInfo(id);
		}
		/// <summary>
		/// 根据id删除RoleModul
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteRoleModul(int id)
		{
			return DataProvider.RoleModuls.DeleteRoleModul(id);
		}
		/// <summary>
		/// 根据ids删除RoleModul多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteRoleModuls(int[] ids)
		{
			return DataProvider.RoleModuls.DeleteRoleModuls(ids);
		}
		/// <summary>
       /// 获取RoleModul分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>RoleModul列表</returns>
       public static List<RoleModulInfo> GetRoleModulPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.RoleModuls.GetRoleModulPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取RoleModul分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>RoleModul列表</returns>
       public static List<RoleModulInfo> GetRoleModulPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.RoleModuls.GetRoleModulPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="rolemodul"></param>
       /// <returns></returns>
       public static RoleModulVModel RoleModulInfoToVModel(RoleModulInfo rolemodul)
       {
           if(rolemodul==null)
               return new RoleModulVModel();
           return new RoleModulVModel
           {
				Id=rolemodul.Id,
			   MId=rolemodul.MId,
			   RId=rolemodul.RId
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="rolemodulInfos"></param>
       /// <returns></returns>
       public static List<RoleModulVModel> RoleModulInfosToVModels(List<RoleModulInfo> rolemodulInfos)
       {
           return rolemodulInfos.Select(RoleModulInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="rolemodul"></param>
       /// <returns></returns>
       public static RoleModulInfo RoleModulVModelToInfo(RoleModulVModel rolemodul)
       {
           if (rolemodul == null)
               return new RoleModulInfo();
           return new RoleModulInfo
           {
              Id=rolemodul.Id,
			   MId=rolemodul.MId,
			   RId=rolemodul.RId
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="rolemodulVModels"></param>
       /// <returns></returns>
       public static List<RoleModulInfo> RoleModulVModelsToInfos(List<RoleModulVModel> rolemodulVModels)
       {
           return rolemodulVModels.Select(RoleModulVModelToInfo).ToList();
       }
	   #endregion
    }
}
