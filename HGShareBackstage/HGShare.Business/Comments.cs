using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{
    /// <summary>
    /// Comment 
    /// </summary>
    public class Comments
    {

        /// <summary>
        /// 添加CommentInfo
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static long AddComment(CommentInfo comment)
        {
            return DataProvider.Comments.AddComment(comment);
        }
        /// <summary>
        /// 修改CommentInfo
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static int UpdateComment(CommentInfo comment)
        {
            return DataProvider.Comments.UpdateComment(comment);
        }
        /// <summary>
        /// 根据id获取CommentInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CommentInfo GetCommentInfo(long id)
        {
            return DataProvider.Comments.GetCommentInfo(id);
        }
        /// <summary>
        /// 根据id删除Comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteComment(long id)
        {
            return DataProvider.Comments.DeleteComment(id);
        }
        /// <summary>
        /// 根据ids删除Comment多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteComments(long[] ids)
        {
            return DataProvider.Comments.DeleteComments(ids);
        }
        /// <summary>
        /// 获取Comment分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>Comment列表</returns>
        public static List<CommentInfo> GetCommentPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            return DataProvider.Comments.GetCommentPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
        }
        /// <summary>
        /// 获取Comment分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>Comment列表</returns>
        public static List<CommentVModel> GetCommentPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            return DataProvider.Comments.GetCommentPageList(pageIndex, pageSize, beginTime, endTime, out pageCount, out count);
        }
        #region 实体转换
        /// <summary>
        /// DataModel 转 ViewModel
        /// </summary>
        /// <param name="commentInfo"></param>
        /// <returns></returns>
        public static CommentVModel CommentInfoToVModel(CommentInfo commentInfo)
        {
            if (commentInfo == null)
                return new CommentVModel();
            return new CommentVModel
            {
                Id = commentInfo.Id,
                AId = commentInfo.AId,
                UserId = commentInfo.UserId,
                CreateTime = commentInfo.CreateTime,
                Content = commentInfo.Content,
                IP = commentInfo.IP,
                UserAgent = commentInfo.UserAgent,
                State = commentInfo.State,
                RefuseReason = commentInfo.RefuseReason,
                IsDelete = commentInfo.IsDelete,
                LastEditUserId = commentInfo.LastEditUserId,
                LastEditTime = commentInfo.LastEditTime,
                IsStick = commentInfo.IsStick,
                DianZanNum = commentInfo.DianZanNum
            };
        }
        /// <summary>
        /// DataModels 转 ViewModels
        /// </summary>
        /// <param name="commentInfos"></param>
        /// <returns></returns>
        public static List<CommentVModel> CommentInfosToVModels(List<CommentInfo> commentInfos)
        {
            return commentInfos.Select(CommentInfoToVModel).ToList();
        }
        /// <summary>
        /// ViewModel 转 DataModel
        /// </summary>
        /// <param name="commentVModel"></param>
        /// <returns></returns>
        public static CommentInfo CommentVModelToInfo(CommentVModel commentVModel)
        {
            if (commentVModel == null)
                return new CommentInfo();
            return new CommentInfo
            {
                Id = commentVModel.Id,
                AId = commentVModel.AId,
                UserId = commentVModel.UserId,
                CreateTime = commentVModel.CreateTime,
                Content = commentVModel.Content,
                IP = commentVModel.IP,
                UserAgent = commentVModel.UserAgent,
                State = commentVModel.State,
                RefuseReason = commentVModel.RefuseReason,
                IsDelete = commentVModel.IsDelete,
                LastEditUserId = commentVModel.LastEditUserId,
                LastEditTime = commentVModel.LastEditTime,
                IsStick = commentVModel.IsStick,
                DianZanNum = commentVModel.DianZanNum
            };
        }
        /// <summary>
        /// ViewModels 转 DataModels
        /// </summary>
        /// <param name="commentVModels"></param>
        /// <returns></returns>
        public static List<CommentInfo> CommentVModelsToInfos(List<CommentVModel> commentVModels)
        {
            return commentVModels.Select(CommentVModelToInfo).ToList();
        }
        #endregion
        #region 前端

        /// <summary>
        /// 获取Comment分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="order"></param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="aId"></param>
        /// <param name="authorId"></param>
        /// <returns>Comment列表</returns>
        public static List<CommentVModel> GetCommentPageList(int pageIndex, int pageSize, long aId, int authorId,
            string order, out int recordCount)
        {
            return DataProvider.Comments.GetCommentPageList(pageIndex, pageSize, aId, authorId, order, out recordCount);
        }
        /// <summary>
        /// 获取Comment分页列表(自定义存储过程)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>Comment列表</returns>
        public async static Task<IEnumerable<CommentVModel>> SearchCommentsByUserId(int userId, int state, int pageIndex, int pageSize)
        {
            return await DataProvider.Comments.SearchCommentsByUserId(userId, state, pageIndex, pageSize);
        }
        /// <summary>
        /// 获取Comment分页列表(自定义存储过程)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <returns>Comment列表</returns>
        public async static Task<int> SearchCommentsCountByUserId(int userId, int state)
        {
            return await DataProvider.Comments.SearchCommentsCountByUserId(userId, state);
        }
        /// <summary>
        /// 获取用户最后评论时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DateTime GetLastPostTime(int userId)
        {
            return DataProvider.Comments.GetLastPostTime(userId);
        }

        /// <summary>
        /// 修改评论点赞数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num">+-1</param>
        /// <returns></returns>
        public static bool UpdateDianZanNum(long id, int num)
        {
            return DataProvider.Comments.UpdateDianZanNum(id, num);
        }
        #endregion
        /// <summary>
        /// 更新评论状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UpdateState(long id, int userId, bool state, string reason = null)
        {
            return DataProvider.Comments.UpdateState(id, userId, state, reason);
        }

        /// <summary>
        /// 更新评论状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UpdateState(long[] ids, int userId, bool state, string reason = null)
        {
            return DataProvider.Comments.UpdateState(ids, userId, state, reason);
        }
    }
}
