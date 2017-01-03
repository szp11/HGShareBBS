using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 文章分类 接口
    /// </summary>
    public interface IArticleTypes
    {
        /// <summary>
        /// 根据 IsHomeMenu 查询文章分类 按Sort 正排序，时间倒序
        /// </summary>
        /// <param name="isHomeMenu"></param>
        /// <returns></returns>
        List<VWModel.ArticleTypeVModel> GetArticleTypesByIsHomeMenu(bool isHomeMenu);
        /// <summary>
        /// 根据 Id 查询文章分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        VWModel.ArticleTypeVModel GetArticleTypeVModelById(int id);
        /// <summary>
        /// 获取类型树
        /// </summary>
        /// <param name="isShow"></param>
        /// <returns></returns>
        List<VWModel.TreeVModel> GetArticleTypeTreeVModel(bool isShow);
    }
}
