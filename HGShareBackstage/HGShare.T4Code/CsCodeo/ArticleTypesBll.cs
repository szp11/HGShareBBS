using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// ArticleType 
    /// </summary>
    public class ArticleTypes
    {

		/// <summary>
		/// 添加ArticleTypeInfo
		/// </summary>
		/// <param name="articletype"></param>
		/// <returns></returns>
		public static int AddArticleType(ArticleTypeInfo articletype)
		{
			return DataProvider.ArticleTypes.AddArticleType(articletype);
		}
		/// <summary>
       /// 修改ArticleTypeInfo
       /// </summary>
       /// <param name="articletype"></param>
       /// <returns></returns>
       public static int UpdateArticleType(ArticleTypeInfo articletype)
       {
           return DataProvider.ArticleTypes.UpdateArticleType(articletype);
       }
		/// <summary>
		/// 根据id获取ArticleTypeInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static ArticleTypeInfo GetArticleTypeInfo(int id)
		{
			return DataProvider.ArticleTypes.GetArticleTypeInfo(id);
		}
		/// <summary>
		/// 根据id删除ArticleType
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteArticleType(int id)
		{
			return DataProvider.ArticleTypes.DeleteArticleType(id);
		}
		/// <summary>
		/// 根据ids删除ArticleType多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteArticleTypes(int[] ids)
		{
			return DataProvider.ArticleTypes.DeleteArticleTypes(ids);
		}
		/// <summary>
       /// 获取ArticleType分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>ArticleType列表</returns>
       public static List<ArticleTypeInfo> GetArticleTypePageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.ArticleTypes.GetArticleTypePageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取ArticleType分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>ArticleType列表</returns>
       public static List<ArticleTypeInfo> GetArticleTypePageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.ArticleTypes.GetArticleTypePageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="articletype"></param>
       /// <returns></returns>
       public static ArticleTypeVModel ArticleTypeInfoToVModel(ArticleTypeInfo articletype)
       {
           if(articletype==null)
               return new ArticleTypeVModel();
           return new ArticleTypeVModel
           {
				Id=articletype.Id,
			   Name=articletype.Name,
			   PId=articletype.PId,
			   Sort=articletype.Sort,
			   PinYin=articletype.PinYin,
			   IsHomeMenu=articletype.IsHomeMenu,
			   CreateTime=articletype.CreateTime,
			   Ico=articletype.Ico,
			   IsShow=articletype.IsShow
			              };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="articletypeInfos"></param>
       /// <returns></returns>
       public static List<ArticleTypeVModel> ArticleTypeInfosToVModels(List<ArticleTypeInfo> articletypeInfos)
       {
           return articletypeInfos.Select(ArticleTypeInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="articletype"></param>
       /// <returns></returns>
       public static ArticleTypeInfo ArticleTypeVModelToInfo(ArticleTypeVModel articletype)
       {
           if (articletype == null)
               return new ArticleTypeInfo();
           return new ArticleTypeInfo
           {
              Id=articletype.Id,
			   Name=articletype.Name,
			   PId=articletype.PId,
			   Sort=articletype.Sort,
			   PinYin=articletype.PinYin,
			   IsHomeMenu=articletype.IsHomeMenu,
			   CreateTime=articletype.CreateTime,
			   Ico=articletype.Ico,
			   IsShow=articletype.IsShow
			              };
       }
	   /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="articletypeVModels"></param>
       /// <returns></returns>
       public static List<ArticleTypeInfo> ArticleTypeVModelsToInfos(List<ArticleTypeVModel> articletypeVModels)
       {
           return articletypeVModels.Select(ArticleTypeVModelToInfo).ToList();
       }
	   #endregion
    }
}
