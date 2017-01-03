using System;
using System.Threading.Tasks;
using HGShare.Business;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    public class CommentsPublic:ICommentsPublic
    {
        private static IArticlesPublic _articlesPublic;
        private static IUsersPublic _usersPublic;

        public CommentsPublic(IArticlesPublic articlesPublic,IUsersPublic usersPublic)
        {
            _articlesPublic = articlesPublic;
            _usersPublic = usersPublic;
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public long AddComment(CommentVModel comment)
        {
            //发布
            long cId = Comments.AddComment(Comments.CommentVModelToInfo(comment));
            if (cId > 0)
            {
                //更新文章评论数
                _articlesPublic.UpdateCommentNum(comment.AId,1);
                //更新用户评论数
                _usersPublic.UpdateCommentNum(comment.UserId, 1);
            }
            return cId;
        }
        /// <summary>
        /// 逻辑删除评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> Delete(long id)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 获取用户最后评论时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DateTime GetLastPostTime(int userId)
        {
            return Comments.GetLastPostTime(userId);
        }
        /// <summary>
        /// 修改评论点赞数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num">+-1</param>
        /// <returns></returns>
        public bool UpdateDianZanNum(long id, int num)
        {
            return Comments.UpdateDianZanNum(id, num);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="extTime">间隔（秒）</param>
        /// <returns></returns>
        public bool CheckCanPost(int userId, int extTime)
        {
            DateTime lastTime = GetLastPostTime(userId);
            if ((DateTime.Now - lastTime).TotalSeconds > extTime)
                return true;
            return false;
        }

        public CommentVModel GetComment(long id)
        {
            return Comments.CommentInfoToVModel(Comments.GetCommentInfo(id));
        }
    }
}
