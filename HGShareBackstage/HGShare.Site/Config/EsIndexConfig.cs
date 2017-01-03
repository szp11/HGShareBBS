using System;
using HGShare.Common;

namespace HGShare.Site.Config
{
    /// <summary>
    /// es 索引配置
    /// </summary>
    public static class EsIndexConfig
    {
        /// <summary>
        /// 是否使用ES数据
        /// </summary>
        public static bool IsUseEsData
        {
            get
            {
                bool _switch;
                Boolean.TryParse(Configuration.AppSettings("ESSwitch"), out _switch);
                return _switch;
            }
        }

        public static string ArticleIndexName
        {
            get { return "articles"; }
        }
        public static string CommentIndexName
        {
            get { return "comments"; }
        }
    }
}
