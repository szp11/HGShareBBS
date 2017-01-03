using System.Collections.Generic;
using System.Threading.Tasks;
using HGShare.VWModel;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 评论接口
    /// </summary>
    public interface IComments
    {
        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="aId">文章id</param>
        /// <param name="authorId">作者id</param>
        /// <param name="order">排序方法</param>
        /// <param name="dataCount">总记录</param>
        /// <returns></returns>
        List<CommentVModel> GetComments(int pageIndex, int pageSize, long aId, int authorId, string order,out int dataCount);

        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CommentVModel GetComment(long id);

        /// <summary>
        /// 根据用户id获取评论
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<CommentVModel>> SearchCommentsByUserId(int userId,int state,int pageIndex, int pageSize);
        /// <summary>
        /// 按照用户id查询记录数
        /// </summary>
        /// <param name="userId">分类</param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <returns></returns>
        Task<int> SearchCommentsCountByUserId(int userId, int state);
    }
}
