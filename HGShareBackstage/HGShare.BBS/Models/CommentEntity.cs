using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HGShare.VWModel;

namespace HGShare.BBS.Models
{
    public class CommentEntity
    {
        public CommentVModel Comment { get; set; }

        public UserVModel User { get; set; }
        /// <summary>
        /// 如果登录了，判断用户是否赞了该评论
        /// </summary>
        public bool IsZan { get; set; }
    }
}