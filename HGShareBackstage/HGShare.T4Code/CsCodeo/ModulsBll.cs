using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// Modul 
    /// </summary>
    public class Moduls
    {

		/// <summary>
		/// 添加ModulInfo
		/// </summary>
		/// <param name="modul"></param>
		/// <returns></returns>
		public static int AddModul(ModulInfo modul)
		{
			return DataProvider.Moduls.AddModul(modul);
		}
		/// <summary>
       /// 修改ModulInfo
       /// </summary>
       /// <param name="modul"></param>
       /// <returns></returns>
       public static int UpdateModul(ModulInfo modul)
       {
           return DataProvider.Moduls.UpdateModul(modul);
       }
		/// <summary>
		/// 根据id获取ModulInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static ModulInfo GetModulInfo(int id)
		{
			return DataProvider.Moduls.GetModulInfo(id);
		}
		/// <summary>
		/// 根据id删除Modul
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteModul(int id)
		{
			return DataProvider.Moduls.DeleteModul(id);
		}
		/// <summary>
		/// 根据ids删除Modul多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteModuls(int[] ids)
		{
			return DataProvider.Moduls.DeleteModuls(ids);
		}
		/// <summary>
       /// 获取Modul分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Modul列表</returns>
       public static List<ModulInfo> GetModulPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.Moduls.GetModulPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取Modul分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Modul列表</returns>
       public static List<ModulInfo> GetModulPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.Moduls.GetModulPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="modul"></param>
       /// <returns></returns>
       public static ModulVModel ModulInfoToVModel(ModulInfo modul)
       {
           if(modul==null)
               return new ModulVModel();
           return new ModulVModel
           {
				Id=modul.Id,
			   ModulName=modul.ModulName,
			   Controller=modul.Controller,
			   Action=modul.Action,
			   Description=modul.Description,
			   CreateTime=modul.CreateTime,
			   PId=modul.PId,
			   OrderId=modul.OrderId,
			   IsShow=modul.IsShow,
			   Priority=modul.Priority,
			   IsDisplay=modul.IsDisplay,
			   Ico=modul.Ico
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="modulInfos"></param>
       /// <returns></returns>
       public static List<ModulVModel> ModulInfosToVModels(List<ModulInfo> modulInfos)
       {
           return modulInfos.Select(ModulInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="modul"></param>
       /// <returns></returns>
       public static ModulInfo ModulVModelToInfo(ModulVModel modul)
       {
           if (modul == null)
               return new ModulInfo();
           return new ModulInfo
           {
              Id=modul.Id,
			   ModulName=modul.ModulName,
			   Controller=modul.Controller,
			   Action=modul.Action,
			   Description=modul.Description,
			   CreateTime=modul.CreateTime,
			   PId=modul.PId,
			   OrderId=modul.OrderId,
			   IsShow=modul.IsShow,
			   Priority=modul.Priority,
			   IsDisplay=modul.IsDisplay,
			   Ico=modul.Ico
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="modulVModels"></param>
       /// <returns></returns>
       public static List<ModulInfo> ModulVModelsToInfos(List<ModulVModel> modulVModels)
       {
           return modulVModels.Select(ModulVModelToInfo).ToList();
       }
	   #endregion
    }
}
