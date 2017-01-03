using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 点赞日志
    /// </summary>
    public interface IDianZanLogsPublic
    {
        /// <summary>
        /// 添加点赞记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long Add(DianZanLogVModel model);

        /// <summary>
        /// 检查是否已经点赞
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mId"></param>
        /// <param name="cId"></param>
        /// <returns></returns>
        long GetDianZanLogId(int userId, long mId, long cId);

        /// <summary>
        /// 更新取消状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCancel"></param>
        /// <returns></returns>
        bool UpdateIsCancel(long id, bool isCancel);

        /// <summary>
        /// 获取用户所有点过赞的评论id
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="aId">文章id</param>
        /// <returns></returns>
        List<long> GetUserAllDianZanCommentId(int userId, long aId);
    }
}
