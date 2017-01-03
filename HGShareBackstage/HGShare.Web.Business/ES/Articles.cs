using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Common;
using HGShare.VWModel;
using HGShare.Web.Interface;
using Nest;

namespace HGShare.Web.Business.ES
{
    public class Articles : IArticles
    {
        private static readonly IElasticClient EsClient = EsHelper.CreateElasticClient(Site.Config.EsIndexConfig.ArticleIndexName);
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
            var mustFilters = new List<Func<QueryContainerDescriptor<ArticleVModel>, QueryContainer>>();
            if (type > 0)
            {
                mustFilters.Add(f=>f.Term(t=>t.Field(fd=>fd.Type).Value(type)));
            }
            if (bType > 0)
            {
                mustFilters.Add(f=>f.Term(t=>t.Field(fd=>fd.BType).Value(bType)));
            }
            if (isJingHua > 0)
            {
                mustFilters.Add(f=>f.Term(t=>t.Field(fd=>fd.IsJiaJing).Value(isJingHua==2)));
            }
            mustFilters.Add(f => f.Term(t => t.Field(fd => fd.IsDelete).Value(false)));
            mustFilters.Add(f => f.Term(t => t.Field(fd => fd.State).Value(1)));
            int form = (pageIndex-1)*pageSize;
            var response=EsClient.Search<ArticleVModel>(s => s.Query(q => q.Bool(b => b.Filter(mustFilters))).Size(pageSize).From(form).Sort(st=>st.Descending(desc=>desc.IsStick).Descending(desc=>desc.Id)));

            //var countResponse = EsClient.Count<ArticleVModel>(s => s.Query(q => q.Bool(b => b.Filter(mustFilters))));

            dataCount = (int) response.Total;

            return response.Documents.ToList();
        }
        /// <summary>
        /// 按Id查询文章
        /// </summary>
        /// <param name="id">分类</param>
        /// <returns></returns>
        public ArticleVModel GetArticleInfoById(long id)
        {
            var response = EsClient.Get(new DocumentPath<ArticleVModel>(id));

            return response.Source;
        }
        /// <summary>
        /// 近期浏览量排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public List<ArticleVModel> ArticlesDotHotTop(int days, int pageSize)
        {
            var now = DateTime.Now;
            var begin = now.AddDays(-days);
            var response = EsClient.Search<ArticleVModel>(s => s.Query(q => q.Bool(b => b.Filter(f => 
                f.Term(t => t.Field(fd => fd.IsDelete).Value(false)),
                f => f.DateRange(date => date.GreaterThanOrEquals(begin).LessThanOrEquals(now))
                ))).Size(pageSize).Sort(st=>st.Descending(desc=>desc.Dot)));
            return response.Documents.ToList();
        }
        /// <summary>
        /// 近期评论数排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public List<ArticleVModel> ArticlesCommentNumHotTop(int days, int pageSize)
        {
            var now = DateTime.Now;
            var begin = now.AddDays(-days);
            var response = EsClient.Search<ArticleVModel>(s => s.Query(q => q.Bool(b => b.Filter(f =>
                f.Term(t => t.Field(fd => fd.IsDelete).Value(false)),
                f => f.DateRange(date => date.GreaterThanOrEquals(begin).LessThanOrEquals(now))
                ))).Size(pageSize).Sort(st => st.Descending(desc => desc.CommentNum)));
            return response.Documents.ToList();
        }

        public async Task<IEnumerable<ArticleVModel>> SearchArticlesByUserId(int userId, int state, int pageIndex, int pageSize)
        {
            var mustFilters = new List<Func<QueryContainerDescriptor<ArticleVModel>, QueryContainer>>();
            if (userId > 0)
            {
                mustFilters.Add(f => f.Term(t => t.Field(fd => fd.UserId).Value(userId)));
            }
            if (state > -1)
            {
                mustFilters.Add(f => f.Term(t => t.Field(fd => fd.State).Value(state)));
            }
            mustFilters.Add(f => f.Term(t => t.Field(fd => fd.IsDelete).Value(false)));

            int form = (pageIndex - 1) * pageSize;
            var response =await  EsClient.SearchAsync<ArticleVModel>(s => s.Query(q => q.Bool(b => b.Filter(mustFilters))).Size(pageSize).From(form).Sort(st => st.Descending(desc => desc.Id)));

            return  response.Documents;
        }

        public async Task<int> SearchArticlesCountByUserId(int userId, int state)
        {
            var mustFilters = new List<Func<QueryContainerDescriptor<ArticleVModel>, QueryContainer>>();
            if (userId > 0)
            {
                mustFilters.Add(f => f.Term(t => t.Field(fd => fd.UserId).Value(userId)));
            }
            if (state > -1)
            {
                mustFilters.Add(f => f.Term(t => t.Field(fd => fd.State).Value(state)));
            }
            mustFilters.Add(f => f.Term(t => t.Field(fd => fd.IsDelete).Value(false)));

            var response = await EsClient.CountAsync<ArticleVModel>(s => s.Query(q => q.Bool(b => b.Filter(mustFilters))));

            return (int)response.Count;
        }
    }
}
