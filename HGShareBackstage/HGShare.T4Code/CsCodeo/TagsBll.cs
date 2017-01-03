using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// Tag 
    /// </summary>
    public class Tags
    {

		/// <summary>
		/// 添加TagInfo
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static long AddTag(TagInfo tag)
		{
			return DataProvider.Tags.AddTag(tag);
		}
		/// <summary>
       /// 修改TagInfo
       /// </summary>
       /// <param name="tag"></param>
       /// <returns></returns>
       public static int UpdateTag(TagInfo tag)
       {
           return DataProvider.Tags.UpdateTag(tag);
       }
		/// <summary>
		/// 根据id获取TagInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static TagInfo GetTagInfo(long id)
		{
			return DataProvider.Tags.GetTagInfo(id);
		}
		/// <summary>
		/// 根据id删除Tag
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteTag(long id)
		{
			return DataProvider.Tags.DeleteTag(id);
		}
		/// <summary>
		/// 根据ids删除Tag多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteTags(long[] ids)
		{
			return DataProvider.Tags.DeleteTags(ids);
		}
		/// <summary>
       /// 获取Tag分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Tag列表</returns>
       public static List<TagInfo> GetTagPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.Tags.GetTagPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取Tag分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Tag列表</returns>
       public static List<TagInfo> GetTagPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.Tags.GetTagPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="tag"></param>
       /// <returns></returns>
       public static TagVModel TagInfoToVModel(TagInfo tag)
       {
           if(tag==null)
               return new TagVModel();
           return new TagVModel
           {
				Id=tag.Id,
			   Tag=tag.Tag,
			   AId=tag.AId,
			   State=tag.State,
			   Direction=tag.Direction,
			   CreateTime=tag.CreateTime,
			   UserId=tag.UserId
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="tagInfos"></param>
       /// <returns></returns>
       public static List<TagVModel> TagInfosToVModels(List<TagInfo> tagInfos)
       {
           return tagInfos.Select(TagInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="tag"></param>
       /// <returns></returns>
       public static TagInfo TagVModelToInfo(TagVModel tag)
       {
           if (tag == null)
               return new TagInfo();
           return new TagInfo
           {
              Id=tag.Id,
			   Tag=tag.Tag,
			   AId=tag.AId,
			   State=tag.State,
			   Direction=tag.Direction,
			   CreateTime=tag.CreateTime,
			   UserId=tag.UserId
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="tagVModels"></param>
       /// <returns></returns>
       public static List<TagInfo> TagVModelsToInfos(List<TagVModel> tagVModels)
       {
           return tagVModels.Select(TagVModelToInfo).ToList();
       }
	   #endregion
    }
}
