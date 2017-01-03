using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HGShare.BBS.Models.Search;

namespace HGShare.BBS.Models
{
    public class ArticleWebEntity
    {
        /// <summary>
        /// 文章
        /// </summary>
        public VWModel.ArticleVModel Article { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public VWModel.UserVModel User { get; set; }

        /// <summary>
        /// 评论搜索条件
        /// </summary>
        public CommentSearch CommentSearch { get; set; }

    }
}