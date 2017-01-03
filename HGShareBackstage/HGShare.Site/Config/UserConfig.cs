using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Common;

namespace HGShare.Site.Config
{
    /// <summary>
    /// 用户相关配置
    /// </summary>
    public class UserConfig
    {
       

        #region 用户发送邮件数量限制相关
        /// <summary>
        /// 用户发送邮件时检测发送量的间隔（分钟）
        /// </summary>
        public static int SendEmailInterval
        {
            get { return int.Parse(Configuration.AppSettings("SendEmailInterval")); }
        }
        /// <summary>
        /// 用户发送邮件时检测发送量的间隔能发送的最大数量
        /// </summary>
        public static int SendEmailIntervalMaxNum
        {
            get { return int.Parse(Configuration.AppSettings("SendEmailIntervalMaxNum")); }
        }
        /// <summary>
        /// 用户发送邮件时检测当天发送量能发送的最大数量
        /// </summary>
        public static int SendEmailToDayMaxNum
        {
            get { return int.Parse(Configuration.AppSettings("SendEmailToDayMaxNum")); }
        }

        #endregion

        /// <summary>
        /// 用户发帖限制间隔（秒）
        /// </summary>
        public static int AddArticleInterval
        {
            get { return int.Parse(Configuration.AppSettings("AddArticleInterval")); }
        }
        /// <summary>
        /// 用户评论限制间隔（秒）
        /// </summary>
        public static int AddCommentInterval
        {
            get { return int.Parse(Configuration.AppSettings("AddCommentInterval")); }
        }
    }
}
