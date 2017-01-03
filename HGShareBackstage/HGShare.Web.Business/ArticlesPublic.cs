using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HGShare.Business;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    /// <summary>
    /// 数据源Sqlserver
    /// </summary>
    public class ArticlesPublic : IArticlesPublic
    {
        /// <summary>
        /// 文章评论数+1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        public async Task<bool> UpdateCommentNum(long aId,int num)
        {
            return await Task.Run(()=>Articles.UpdateCommentNum(aId,num))>0;
        }
        /// <summary>
        /// 文章点击量+1
        /// </summary>
        /// <param name="aId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDot(long aId)
        {
            return await Articles.UpdateDot(aId)>0;
        }
        /// <summary>
        /// 点赞数+1/-1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        public async Task<bool> UpdateDianZanNum(long aId, int num)
        {
            return await Task.Run(() => Articles.UpdateDianZanNum(aId, num)) > 0;
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Add(ArticleVModel model)
        {
            return await Task.Run(() => Articles.AddArticle(Articles.ArticleVModelToInfo(model)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DateTime GetLastPostTime(int userId)
        {
            return Articles.GetLastPostTime(userId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="extTime">间隔（秒）</param>
        /// <returns></returns>
        public bool CheckCanPost(int userId,int extTime)
        {
            DateTime lastTime = GetLastPostTime(userId);
            if ((DateTime.Now - lastTime).TotalSeconds > extTime)
                return true;
            return false;
        }
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public bool UpdateArticle(ArticleVModel article)
        {
            return Articles.UpdateArticlePart(article) > 0;
        }

        /// <summary>
        /// 按Id查询文章
        /// </summary>
        /// <param name="id">分类</param>
        /// <returns></returns>
        public ArticleVModel GetArticleInfoById(long id)
        {
            var model = Articles.GetArticleVModel(id);
            return model;
        }
        /// <summary>
        /// 获取文章热字段DOt的值
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ArticleHotFieldVModel>> GetArticleHotFieldDots(long[] ids)
        {
            return await Articles.GetArticleHotFieldDots(ids);
        }
    }
}
