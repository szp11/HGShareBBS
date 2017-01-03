using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// Area 
    /// </summary>
    public class Areas
    {

		/// <summary>
		/// 添加AreaInfo
		/// </summary>
		/// <param name="area"></param>
		/// <returns></returns>
		public static int AddArea(AreaInfo area)
		{
			return DataProvider.Areas.AddArea(area);
		}
		/// <summary>
       /// 修改AreaInfo
       /// </summary>
       /// <param name="area"></param>
       /// <returns></returns>
       public static int UpdateArea(AreaInfo area)
       {
           return DataProvider.Areas.UpdateArea(area);
       }
		/// <summary>
		/// 根据id获取AreaInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static AreaInfo GetAreaInfo(int id)
		{
			return DataProvider.Areas.GetAreaInfo(id);
		}
		/// <summary>
		/// 根据id删除Area
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteArea(int id)
		{
			return DataProvider.Areas.DeleteArea(id);
		}
		/// <summary>
		/// 根据ids删除Area多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteAreas(int[] ids)
		{
			return DataProvider.Areas.DeleteAreas(ids);
		}
		/// <summary>
       /// 获取Area分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Area列表</returns>
       public static List<AreaInfo> GetAreaPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.Areas.GetAreaPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取Area分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Area列表</returns>
       public static List<AreaInfo> GetAreaPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.Areas.GetAreaPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="area"></param>
       /// <returns></returns>
       public static AreaVModel AreaInfoToVModel(AreaInfo area)
       {
           if(area==null)
               return new AreaVModel();
           return new AreaVModel
           {
				Id=area.Id,
			   Name=area.Name,
			   Code=area.Code,
			   PinYin=area.PinYin,
			   SortPinYin=area.SortPinYin,
			   Sort=area.Sort,
			   ParentCode=area.ParentCode
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="areaInfos"></param>
       /// <returns></returns>
       public static List<AreaVModel> AreaInfosToVModels(List<AreaInfo> areaInfos)
       {
           return areaInfos.Select(AreaInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="area"></param>
       /// <returns></returns>
       public static AreaInfo AreaVModelToInfo(AreaVModel area)
       {
           if (area == null)
               return new AreaInfo();
           return new AreaInfo
           {
              Id=area.Id,
			   Name=area.Name,
			   Code=area.Code,
			   PinYin=area.PinYin,
			   SortPinYin=area.SortPinYin,
			   Sort=area.Sort,
			   ParentCode=area.ParentCode
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="areaVModels"></param>
       /// <returns></returns>
       public static List<AreaInfo> AreaVModelsToInfos(List<AreaVModel> areaVModels)
       {
           return areaVModels.Select(AreaVModelToInfo).ToList();
       }
	   #endregion
    }
}
