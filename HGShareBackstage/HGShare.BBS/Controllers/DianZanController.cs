using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HGShare.BBS.Controllers.Base;
using HGShare.Site.ActionResult;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.BBS.Controllers
{
    public class DianZanController : BaseController
    {
        private static readonly IDianZanLogsPublic DianZanLogsPublic = IocContainer.Service<IDianZanLogsPublic>();
        private static readonly ICommentsPublic CommentsPublic = IocContainer.Service<ICommentsPublic>();

        /// <summary>
        /// 评论点赞
        /// </summary>
        /// <param name="mId">主题id</param>
        /// <param name="cId">评论id</param>
        /// <param name="zan">true 赞/false 取消赞</param>
        /// <returns></returns>
        [RoleAuthorize]   
        [HttpPost]
        public ActionResult Comment(long mId,long cId,bool zan)
        {
            //检查是否已经点赞

            long logId= DianZanLogsPublic.GetDianZanLogId(CurrentUserInfo.Id, mId, cId);
            if (logId > 0)
            {
                //更新
                bool s= DianZanLogsPublic.UpdateIsCancel(logId, !zan);
                if(!s)
                    return Json(new JsonResultModel { Message = "人品不好点赞失败，稍后再试！" });
            }
            else
            {
                //添加
                logId= DianZanLogsPublic.Add(new DianZanLogVModel()
                {
                    UserId = CurrentUserInfo.Id,
                    MId = mId,
                    CId = cId,
                    IsCancel = !zan,
                    Ip = Common.Fetch.Ip,
                    Type = 2
                });
                if(logId<=0)
                    return Json(new JsonResultModel { Message = "人品不好点赞失败，稍后再试！" });
            }

            //更新评论的点赞数
            bool status= CommentsPublic.UpdateDianZanNum(cId,(zan?1:-1));
            if(!status)    
                return Json(new JsonResultModel { Message = "人品不好点赞失败，稍后再试！" } );
            return Json(new JsonResultModel { Message = "成功", ResultState=true });
        }
    }
}