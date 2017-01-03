using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    public class DianZanLogsPublic : IDianZanLogsPublic
    {
        /// <summary>
        /// 添加点赞记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Add(DianZanLogVModel model)
        {
            return HGShare.Business.DianZanLogs.AddDianZanLog(HGShare.Business.DianZanLogs.DianZanLogVModelToInfo(model));
        }
        /// <summary>
        /// 检查是否已经点赞
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mId"></param>
        /// <param name="cId"></param>
        /// <returns></returns>
        public long GetDianZanLogId(int userId, long mId, long cId)
        {
            return HGShare.Business.DianZanLogs.GetDianZanLogId(userId, mId, cId);
        }
        /// <summary>
        /// 更新取消状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCancel"></param>
        /// <returns></returns>
        public bool UpdateIsCancel(long id, bool isCancel)
        {
            return HGShare.Business.DianZanLogs.UpdateIsCancel(id, isCancel);
        }
        /// <summary>
        /// 获取用户所有点过赞的评论id
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="aId">文章id</param>
        /// <returns></returns>
        public List<long> GetUserAllDianZanCommentId(int userId, long aId)
        {
            return HGShare.Business.DianZanLogs.GetUserAllDianZanCommentId(userId, aId);
        }
    }
}
