using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IArticlesPublic
    {
        /// <summary>
        /// 文章评论数+1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        Task<bool> UpdateCommentNum(long aId, int num);
        /// <summary>
        /// 文章点击量+1
        /// </summary>
        /// <param name="aId"></param>
        /// <returns></returns>
        Task<bool> UpdateDot(long aId);
        /// <summary>
        /// 点赞数+1/-1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        Task<bool> UpdateDianZanNum(long aId,int num);
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> Add(VWModel.ArticleVModel model);

        /// <summary>
        /// 获取用户最后发帖时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        DateTime GetLastPostTime(int userId);
        /// <summary>
        /// 检测是否能发布
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="extTime">间隔（分钟）</param>
        /// <returns></returns>
        bool CheckCanPost(int userId, int extTime);
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        bool UpdateArticle(ArticleVModel article);

        /// <summary>
        /// 按Id查询文章
        /// </summary>
        /// <param name="id">分类</param>
        /// <returns></returns>
        ArticleVModel GetArticleInfoById(long id);
        /// <summary>
        /// 获取文章热字段DOt的值
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<ArticleHotFieldVModel>> GetArticleHotFieldDots(long[] ids);
    }
}
