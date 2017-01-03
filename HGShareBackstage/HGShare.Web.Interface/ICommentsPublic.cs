using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 评论接口
    /// </summary>
    public interface ICommentsPublic
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        long AddComment(CommentVModel comment);

        /// <summary>
        /// 逻辑删除评论
        /// </summary>
        /// <param name="id">评论id</param>
        /// <returns></returns>
        Task<bool> Delete(long id);

        /// <summary>
        /// 获取用户最后评论时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        DateTime GetLastPostTime(int userId);

        /// <summary>
        /// 修改评论点赞数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num">+-1</param>
        /// <returns></returns>
        bool UpdateDianZanNum(long id, int num);
        /// <summary>
        /// 检测是否能发布
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="extTime">间隔（分钟）</param>
        /// <returns></returns>
        bool CheckCanPost(int userId, int extTime);
        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CommentVModel GetComment(long id);
    }
}
