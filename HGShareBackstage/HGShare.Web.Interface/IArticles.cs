using System.Collections.Generic;
using System.Threading.Tasks;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 文章 接口
    /// </summary>
    public interface IArticles
    {
        /// <summary>
        /// 按照分类和类型查询 时间倒叙输出
        /// </summary>
        /// <param name="type">分类</param>
        /// <param name="bType">类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="isJignHua">是精华 0全部 1非精华 2精华</param>
        /// <param name="dataCount">数据量</param>
        /// <returns></returns>
        List<VWModel.ArticleVModel> SearchArticlesByTypes(int type, int bType, int pageIndex, int pageSize,int isJignHua, out int dataCount);
        /// <summary>
        /// 按Id查询文章
        /// </summary>
        /// <param name="id">分类</param>
        /// <returns></returns>
        VWModel.ArticleVModel GetArticleInfoById(long id);
        /// <summary>
        /// 近期浏览量排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        List<VWModel.ArticleVModel> ArticlesDotHotTop(int days, int pageSize);
        /// <summary>
        /// 近期评论数排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        List<VWModel.ArticleVModel> ArticlesCommentNumHotTop(int days, int pageSize);

        /// <summary>
        /// 按照用户id查询 时间倒叙输出
        /// </summary>
        /// <param name="userId">分类</param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        Task<IEnumerable<VWModel.ArticleVModel>> SearchArticlesByUserId(int userId,int state, int pageIndex, int pageSize);
        /// <summary>
        /// 按照用户id查询记录数
        /// </summary>
        /// <param name="userId">分类</param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <returns></returns>
        Task<int> SearchArticlesCountByUserId(int userId, int state);
    }
}
