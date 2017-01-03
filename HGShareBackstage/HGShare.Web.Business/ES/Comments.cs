using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HGShare.Common;
using HGShare.VWModel;
using HGShare.Web.Interface;
using Nest;

namespace HGShare.Web.Business.ES
{
    public class Comments:IComments
    {
        private static readonly IElasticClient EsClient = EsHelper.CreateElasticClient(Site.Config.EsIndexConfig.CommentIndexName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="aId"></param>
        /// <param name="authorId"></param>
        /// <param name="order"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<CommentVModel> GetComments(int pageIndex, int pageSize, long aId, int authorId, string order,out int dataCount)
        {
            var mustFilters = new List<Func<QueryContainerDescriptor<CommentVModel>, QueryContainer>>();
            if (aId > 0)
            {
                mustFilters.Add(f => f.Term(t => t.Field(fd => fd.AId).Value(aId)));
            }
            if (authorId > 0)
            {
                mustFilters.Add(f => f.Term(t => t.Field(fd => fd.UserId).Value(authorId)));
            }
            
            mustFilters.Add(f => f.Term(t => t.Field(fd => fd.IsDelete).Value(false)));

            mustFilters.Add(f => f.Term(t => t.Field(fd => fd.State).Value(1)));

            var sorts=new List<SortDescriptor<CommentVModel>>();
            sorts.Add(new SortDescriptor<CommentVModel>().Descending(d => d.Id));

            Func<SortDescriptor<CommentVModel>, IPromise<IList<ISort>>> sort;
            if (order.ToLower() == "asc")
                sort = sd => sd.Descending(d => d.IsStick).Ascending(d => d.Id);
            else
                sort = sd => sd.Descending(d => d.IsStick).Descending(d => d.Id);


            int form = (pageIndex - 1) * pageSize;
            var response = EsClient.Search<CommentVModel>(s => s.Query(q => q.Bool(b => b.Filter(mustFilters))).Size(pageSize).From(form).Sort(sort));

            //var countResponse = EsClient.Count<ArticleVModel>(s => s.Query(q => q.Bool(b => b.Filter(mustFilters))));

            dataCount = (int)response.Total;

            return response.Documents.ToList();
        }
        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommentVModel GetComment(long id)
        {
            var response = EsClient.Get(new DocumentPath<CommentVModel>(id));

            return response.Source;
        }
        /// <summary>
        /// 根据用户id获取评论
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CommentVModel>> SearchCommentsByUserId(int userId, int state, int pageIndex, int pageSize)
        {
            var mustFilters = new List<Func<QueryContainerDescriptor<CommentVModel>, QueryContainer>>();
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
            var response = await EsClient.SearchAsync<CommentVModel>(s => s.Query(q => q.Bool(b => b.Filter(mustFilters))).Size(pageSize).From(form).Sort(st => st.Descending(desc => desc.Id)));

            return response.Documents;
        }

        public async Task<int> SearchCommentsCountByUserId(int userId, int state)
        {
            var mustFilters = new List<Func<QueryContainerDescriptor<CommentVModel>, QueryContainer>>();
            if (userId > 0)
            {
                mustFilters.Add(f => f.Term(t => t.Field(fd => fd.UserId).Value(userId)));
            }
            if (state > -1)
            {
                mustFilters.Add(f => f.Term(t => t.Field(fd => fd.State).Value(state)));
            }
            mustFilters.Add(f => f.Term(t => t.Field(fd => fd.IsDelete).Value(false)));

            var response = await EsClient.CountAsync<CommentVModel>(s => s.Query(q => q.Bool(b => b.Filter(mustFilters))));

            return (int)response.Count;
        }
    }
}
