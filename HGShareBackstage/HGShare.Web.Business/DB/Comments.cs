using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business.DB
{
    public class Comments:IComments
    {
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
            return HGShare.Business.Comments.GetCommentPageList(pageIndex,pageSize, aId, authorId, order, out dataCount);
        }
        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommentVModel GetComment(long id)
        {
            return HGShare.Business.Comments.CommentInfoToVModel(HGShare.Business.Comments.GetCommentInfo(id));
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
            return await HGShare.Business.Comments.SearchCommentsByUserId(userId, state, pageIndex, pageSize);
        }

        public async Task<int> SearchCommentsCountByUserId(int userId, int state)
        {
            return await HGShare.Business.Comments.SearchCommentsCountByUserId(userId, state);
        }
    }
}
