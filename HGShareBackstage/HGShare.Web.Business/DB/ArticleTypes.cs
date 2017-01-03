using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business.DB
{
    public class ArticleTypes : IArticleTypes
    {
        /// <summary>
        /// 根据 IsHomeMenu 查询文章分类 按Sort 正排序，时间倒序
        /// </summary>
        /// <param name="isHomeMenu"></param>
        /// <returns></returns>
        public List<ArticleTypeVModel> GetArticleTypesByIsHomeMenu(bool isHomeMenu)
        {
            return HGShare.Business.ArticleTypes.GetArticleTypesByIsHomeMenu(isHomeMenu);
        }
        /// <summary>
        /// 根据 Id 查询文章分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArticleTypeVModel GetArticleTypeVModelById(int id)
        {
            return HGShare.Business.ArticleTypes.ArticleTypeInfoToVModel(HGShare.Business.ArticleTypes.GetArticleTypeInfo(id));
        }
        /// <summary>
        /// 获取类型树
        /// </summary>
        /// <param name="isShow"></param>
        /// <returns></returns>
        public List<TreeVModel> GetArticleTypeTreeVModel(bool isShow)
        {
            var infos = HGShare.Business.ArticleTypes.GetArticleTypesByIsShow(isShow);
            return HGShare.Business.ArticleTypes.ArticleTypeInfosToTreeVModels(infos);
        }
    }
}
