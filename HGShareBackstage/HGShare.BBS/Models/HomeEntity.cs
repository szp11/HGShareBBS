using System.Collections.Generic;
using HGShare.VWModel;
using Webdiyer.WebControls.Mvc;

namespace HGShare.BBS.Models
{
    public class HomeEntity
    {

        public UserVModel User { get; set; }

        public UserOtherVModel UserOther { get; set; }

        public List<UserPositionVModel> Positions { get; set; }
        /// <summary>
        /// 文章
        /// </summary>
        public PagedList<ArticleVModel> Articles { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public PagedList<CommentVModel> Comments { get; set; }
    }
}