using System;
using System.Collections.Generic;
using System.Linq;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{
    /// <summary>
    /// ArticleType 
    /// </summary>
    public class ArticleTypes
    {
        public static string RootType {
            get { return "根类型"; }
        }

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
            return DataProvider.ArticleTypes.GetArticleTypePageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
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
            return DataProvider.ArticleTypes.GetArticleTypePageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }

        /// <summary>
        /// 获取所有ArticleType
        /// </summary>
        /// <returns></returns>
        public static List<ArticleTypeInfo> GetAllArticleType()
        {
            return DataProvider.ArticleTypes.GetAllArticleType();
        }
        /// <summary>
        /// 获取所有ArticleType
        /// </summary>
        /// <param name="exclusiveid">要排除的id</param>
        /// <returns></returns>
        public static List<ArticleTypeInfo> GetAllArticleType(int? exclusiveid)
        {
            var moduls = GetAllArticleType();
            if (moduls == null) return new List<ArticleTypeInfo>();
            if (exclusiveid.HasValue)
                return moduls.Where(n => n.Id != exclusiveid).ToList();
            return moduls;
        }
        /// <summary>
        /// ArticleTypeInfos 转 TreeVModels
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        public static List<TreeVModel> ArticleTypeInfosToTreeVModels(List<ArticleTypeInfo> infos)
        {
            var result = new List<TreeVModel>
            {
                new TreeVModel
                {
                    text = RootType,
                    dataid = 0,
                    pid =-1,
                    state=new TreeState {opened = true},
                    children=new List<TreeVModel>(),
                    PIds=new []{-1},
                    Order=0
                }
            };
            if (infos == null || infos.Count == 0)
                return result;
            var parents = infos.Where(n => n.PId == 0);
            List<TreeVModel> trees = parents.Select(n => new TreeVModel
            {
                dataid = n.Id,
                pid = n.PId,
                text = n.Name,
                icon = string.Empty,
                state = new TreeState { opened = false },
                children = new List<TreeVModel>(),
                PIds = new[] { -1, 0 },
                Order = n.Sort
            }).ToList();
            foreach (var node in trees)
            {
                node.children = GetChildrens(node, infos);
            }
            result[0].children = trees.OrderByDescending(n => n.Order).ToList();
            return result;
        }
        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="infos"></param>
        /// <returns></returns>
        private static List<TreeVModel> GetChildrens(TreeVModel tree, List<ArticleTypeInfo> infos)
        {
            var pids = new List<int>();
            pids.AddRange(tree.PIds);
            pids.Add(tree.dataid);
            List<TreeVModel> result = infos.Where(n => n.PId == tree.dataid).Select(n => new TreeVModel
            {
                dataid = n.Id,
                pid = n.PId,
                text = n.Name,
                icon =string.Empty,
                state = new TreeState { opened = false },
                children = new List<TreeVModel>(),
                PIds = pids.ToArray(),
                Order = n.Sort
            }).ToList();
            foreach (var item in result)
            {
                if (infos.Any(n => n.PId == item.dataid))
                {
                    item.children = GetChildrens(item, infos);
                }
            }
            return result.OrderByDescending(n => n.Order).ToList();
        }


        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="articletypeInfo"></param>
        /// <returns></returns>
        public static ArticleTypeVModel ArticleTypeInfoToVModel(ArticleTypeInfo articletypeInfo)
        {
            if (articletypeInfo == null)
                return new ArticleTypeVModel();
            return new ArticleTypeVModel
            {
                Id = articletypeInfo.Id,
                Name = articletypeInfo.Name,
                PId = articletypeInfo.PId,
                Sort = articletypeInfo.Sort,
                PinYin = articletypeInfo.PinYin,
                PName = articletypeInfo.PId==0?RootType: articletypeInfo.PName,
                IsHomeMenu = articletypeInfo.IsHomeMenu,
                CreateTime = articletypeInfo.CreateTime,
                Ico = articletypeInfo.Ico,
                IsShow = articletypeInfo.IsShow
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
        /// <param name="articletypeVModel"></param>
        /// <returns></returns>
        public static ArticleTypeInfo ArticleTypeVModelToInfo(ArticleTypeVModel articletypeVModel)
        {
            if (articletypeVModel == null)
                return new ArticleTypeInfo();
            return new ArticleTypeInfo
            {
                Id = articletypeVModel.Id,
                Name = articletypeVModel.Name,
                PId = articletypeVModel.PId,
                Sort = articletypeVModel.Sort,
                PinYin = articletypeVModel.PinYin,
                PName = articletypeVModel.PId==0?RootType:articletypeVModel.PName,
                IsHomeMenu = articletypeVModel.IsHomeMenu,
                CreateTime = articletypeVModel.CreateTime,
                Ico = articletypeVModel.Ico,
                IsShow = articletypeVModel.IsShow
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

        #region 前端用
        /// <summary>
        /// 根据 IsHomeMenu 查询文章分类 按Sort 正排序，时间倒序
        /// </summary>
        /// <param name="isHomeMenu"></param>
        /// <returns></returns>
        public static List<ArticleTypeVModel> GetArticleTypesByIsHomeMenu(bool isHomeMenu)
        {
            return DataProvider.ArticleTypes.GetArticleTypesByIsHomeMenu(isHomeMenu);
        }

        /// <summary>
        /// 根据 isShow 查询文章分类 按Sort 正排序，时间倒序
        /// </summary>
        /// <param name="isShow"></param>
        /// <returns></returns>
        public static List<ArticleTypeInfo> GetArticleTypesByIsShow(bool isShow)
        {
            return DataProvider.ArticleTypes.GetArticleTypesByIsShow(isShow);
        }

        #endregion

    }
}
