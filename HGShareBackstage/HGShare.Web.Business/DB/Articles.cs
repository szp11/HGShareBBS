using System.Collections.Generic;
using System.Threading.Tasks;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business.DB
{
    public class Articles:IArticles
    {
        /// <summary>
        /// 按照分类和类型查询 时间倒叙输出
        /// </summary>
        /// <param name="type">分类</param>
        /// <param name="bType">类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="isJingHua">是精华 0全部 1非精华 2精华</param>
        /// <param name="dataCount">数据量</param>
        /// <returns></returns>
        public List<ArticleVModel> SearchArticlesByTypes(int type, int bType, int pageIndex, int pageSize, int isJingHua, out int dataCount)
        {
            var list = HGShare.Business.Articles.GetArticlePageList(pageIndex, pageSize, type, bType, isJingHua, out dataCount);
            return list;
        }
        /// <summary>
        /// 按Id查询文章
        /// </summary>
        /// <param name="id">分类</param>
        /// <returns></returns>
        public ArticleVModel GetArticleInfoById(long id)
        {
            var model = HGShare.Business.Articles.GetArticleVModel(id);
            return model;
        }
        /// <summary>
        /// 近期浏览量排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public List<ArticleVModel> ArticlesDotHotTop(int days, int pageSize)
        {
            return HGShare.Business.Articles.ArticlesDotHotTop(days, pageSize);
        }
        /// <summary>
        /// 近期评论数排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public List<ArticleVModel> ArticlesCommentNumHotTop(int days, int pageSize)
        {
            return HGShare.Business.Articles.ArticlesCommentNumHotTop(days, pageSize);
        }

        public  async Task<IEnumerable<ArticleVModel>> SearchArticlesByUserId(int userId, int state, int pageIndex, int pageSize)
        {
            return await HGShare.Business.Articles.SearchArticlesByUserId(userId, state, pageIndex, pageSize);
        }

        public async Task<int> SearchArticlesCountByUserId(int userId, int state)
        {
            return await HGShare.Business.Articles.SearchArticlesCountByUserId(userId, state);
        }
    }
}
