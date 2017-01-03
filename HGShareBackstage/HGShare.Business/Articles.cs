using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{
    /// <summary>
    /// Article 
    /// </summary>
    public class Articles
    {

        /// <summary>
        /// 添加ArticleInfo
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static long AddArticle(ArticleInfo article)
        {
            return DataProvider.Articles.AddArticle(article);
        }
        /// <summary>
        /// 修改ArticleInfo
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static int UpdateArticle(ArticleInfo article)
        {
            return DataProvider.Articles.UpdateArticle(article);
        }
        /// <summary>
        /// 根据id获取ArticleInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArticleInfo GetArticleInfo(long id)
        {
            return DataProvider.Articles.GetArticleInfo(id);
        }
        /// <summary>
        /// 根据id删除Article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteArticle(long id)
        {
            return DataProvider.Articles.DeleteArticle(id);
        }
        /// <summary>
        /// 根据ids删除Article多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteArticles(long[] ids)
        {
            return DataProvider.Articles.DeleteArticles(ids);
        }
        
        /// <summary>
        /// 获取Article分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <param name="where">搜索条件 默认为空</param>
        /// <returns>Article列表</returns>
        public static List<ArticleVModel> GetArticlePageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count, string where = "")
        {
            return DataProvider.Articles.GetArticlePageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count, where);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="articleInfo"></param>
        /// <returns></returns>
        public static ArticleVModel ArticleInfoToVModel(ArticleInfo articleInfo)
        {
            if (articleInfo == null)
                return new ArticleVModel();
            return new ArticleVModel
            {
                Id = articleInfo.Id,
                Title = articleInfo.Title,
                Content = articleInfo.Content,
                Type = articleInfo.Type,
                CommentNum = articleInfo.CommentNum,
                Dot = articleInfo.Dot,
                CreateTime = articleInfo.CreateTime,
                UserId = articleInfo.UserId,
                ImgNum = articleInfo.ImgNum,
                AttachmentNum = articleInfo.AttachmentNum,
                LastEditUserId = articleInfo.LastEditUserId,
                Guid = articleInfo.Guid,
                State = articleInfo.State,
                Score = articleInfo.Score,
                IsStick = articleInfo.IsStick,
                IsCloseComment = articleInfo.IsCloseComment,
                CloseCommentReason = articleInfo.CloseCommentReason
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="articleInfos"></param>
        /// <returns></returns>
        public static List<ArticleVModel> ArticleInfosToVModels(List<ArticleInfo> articleInfos)
        {
            return articleInfos.Select(ArticleInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="articleVModel"></param>
        /// <returns></returns>
        public static ArticleInfo ArticleVModelToInfo(ArticleVModel articleVModel)
        {
            if (articleVModel == null)
                return new ArticleInfo();
            return new ArticleInfo
            {
                Id = articleVModel.Id,
                Title = articleVModel.Title,
                Content = articleVModel.Content,
                Type = articleVModel.Type,
                CommentNum = articleVModel.CommentNum,
                Dot = articleVModel.Dot,
                CreateTime = articleVModel.CreateTime,
                UserId = articleVModel.UserId,
                ImgNum = articleVModel.ImgNum,
                AttachmentNum = articleVModel.AttachmentNum,
                LastEditUserId = articleVModel.LastEditUserId,
                Guid = articleVModel.Guid,
                State = articleVModel.State,
                Score = articleVModel.Score,
                IsStick = articleVModel.IsStick,
                IsCloseComment = articleVModel.IsCloseComment,
                CloseCommentReason = articleVModel.CloseCommentReason
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="articleVModels"></param>
        /// <returns></returns>
        public static List<ArticleInfo> ArticleVModelsToInfos(List<ArticleVModel> articleVModels)
        {
            return articleVModels.Select(ArticleVModelToInfo).ToList();
        }
        #endregion

        /// <summary>
        /// 更新文章状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UpdateState(long id, int userId, bool state, string reason = null)
        {
            return DataProvider.Articles.UpdateState(id, userId, state, reason);
        }

        /// <summary>
        /// 更新文章状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UpdateState(long[] ids, int userId, bool state, string reason = null)
        {
            return DataProvider.Articles.UpdateState(ids, userId, state, reason);
        }

        /// <summary>
        /// 更新文章是否关闭评论状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="isCloseComment"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UpdateIsCloseComment(long id, int userId, bool isCloseComment, string reason = null)
        {
            return DataProvider.Articles.UpdateIsCloseComment(id, userId, isCloseComment, reason);
        }
        /// <summary>
        /// 更新文章精华状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="isJiaJing"></param>
        /// <returns></returns>
        public static bool UpdateIsJiaJing(long id, int userId, bool isJiaJing)
        {
            return DataProvider.Articles.UpdateIsJiaJing(id, userId, isJiaJing);
        }

        /// <summary>
        /// 更新文章置顶状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="isStick"></param>
        /// <returns></returns>
        public static bool UpdateIsStick(long id, int userId, bool isStick)
        {
            return DataProvider.Articles.UpdateIsStick(id, userId, isStick);
        }

        #region 前端使用
        /// <summary>
        /// 获取Article分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="bType"></param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="type"></param>
        /// <returns>Article列表</returns>
        public static List<ArticleVModel> GetArticlePageList(int pageIndex, int pageSize, int type, int bType, int isJingHua, out int recordCount)
        {
            return DataProvider.Articles.GetArticlePageList(pageIndex, pageSize, type, bType,isJingHua, out recordCount);
        }
        /// <summary>
        /// 根据用户Id获取Article分页列表(自定义存储过程)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns>Article列表</returns>
        public async static Task<IEnumerable<ArticleVModel>> SearchArticlesByUserId(int userId, int state, int pageIndex, int pageSize)
        {
            return await DataProvider.Articles.SearchArticlesByUserId(userId, state, pageIndex, pageSize);
        }
        /// <summary>
        /// 按照用户id查询记录数
        /// </summary>
        /// <param name="userId">分类</param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <returns></returns>
        public static async Task<int> SearchArticlesCountByUserId(int userId, int state)
        {
            return await DataProvider.Articles.SearchArticlesCountByUserId(userId, state);
        }
        /// <summary>
        /// 根据id获取ArticleVModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArticleVModel GetArticleVModel(long id)
        {
            return DataProvider.Articles.GetArticleVModel(id);
        }
        /// <summary>
        /// 文章评论数+1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1-1</param>
        /// <returns></returns>
        public static int UpdateCommentNum(long aId, int num)
        {
            return DataProvider.Articles.UpdateCommentNum(aId, num);
        }
        /// <summary>
        /// 文章点击量+1
        /// </summary>
        /// <param name="aId"></param>
        /// <returns></returns>
        public async static Task<int> UpdateDot(long aId)
        {
            return await DataProvider.Articles.UpdateDot(aId);
        }
        /// <summary>
        /// 点赞数+1/-1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        public static int UpdateDianZanNum(long aId, int num)
        {
            return DataProvider.Articles.UpdateDianZanNum(aId, num);
        }
        /// <summary>
        /// 近期浏览量排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public static List<ArticleVModel> ArticlesDotHotTop(int days, int pageSize)
        {
            return DataProvider.Articles.ArticlesDotHotTop(days, pageSize);
        }
        /// <summary>
        /// 近期评论数排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public static List<ArticleVModel> ArticlesCommentNumHotTop(int days, int pageSize)
        {
            return DataProvider.Articles.ArticlesCommentNumHotTop(days, pageSize);
        }
        /// <summary>
        /// 获取用户最后发帖时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DateTime GetLastPostTime(int userId)
        {
            return DataProvider.Articles.GetLastPostTime(userId);
        }

        /// <summary>
        /// 修改文章主体信息
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static int UpdateArticlePart(ArticleVModel article)
        {
            return DataProvider.Articles.UpdateArticlePart(article);
        }
        /// <summary>
        /// 获取文章热字段DOt的值
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<ArticleHotFieldVModel>> GetArticleHotFieldDots(long[] ids)
        {
            return await DataProvider.Articles.GetArticleHotFieldDots(ids);
        }
        #endregion

       
    }
}
