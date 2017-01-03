using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HGShare.BBS.Models
{
    public class ArticleSearchEntity
    {
        /// <summary>
        /// 分类
        /// </summary>
        public int? type
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public int? bType { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int? pageIndex { get; set; }
        /// <summary>
        /// 是精华 0全部 1非精华 2精华
        /// </summary>
        public int? isJingHua { get; set; }
    }
}