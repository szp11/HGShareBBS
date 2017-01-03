using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Common;

namespace HGShare.Site.Config
{
    /// <summary>
    /// 系统相关配置
    /// </summary>
    public class WebSysConfig
    {
        /// <summary>
        /// 网站名
        /// </summary>
        public static string WebName
        {
            get
            {
                return Configuration.AppSettings("WebName");
            }
        }
        /// <summary>
        /// 网站标题
        /// </summary>
        public static string WebHomeTitle
        {
            get
            {
                return Configuration.AppSettings("WebHomeTitle");
            }
        }
        /// <summary>
        /// 网站积分名
        /// </summary>
        public static string ScoreName
        {
            get
            {
                return Configuration.AppSettings("ScoreName");
            }

        }
        /// <summary>
        /// 评论最大字符数
        /// </summary>
        public static int CommentMaxLength
        {
            get
            {
                return int.Parse(Configuration.AppSettings("CommentMaxLength"));
            }

        }
        /// <summary>
        /// 评论最小字符数
        /// </summary>
        public static int CommentMinLength
        {
            get
            {
                return int.Parse(Configuration.AppSettings("CommentMinLength"));
            }

        }
        /// <summary>
        /// 系统版本号
        /// </summary>
        public static string SysVersion
        {
            get { return Configuration.AppSettings("SysVersion"); }
        }


        #region 头像相关
        /// <summary>
        /// 默认用户头像
        /// </summary>
        public static string DefaultAvatar
        {
            get
            {
                return Configuration.AppSettings("DefaultAvatar");
            }
        }
        /// <summary>
        /// 用户头像预览图尺寸
        /// </summary>
        public static int[] AvatarThumbnailSizes
        {
            get
            {
                string config = Configuration.AppSettings("AvatarThumbnailSizes");
                if (string.IsNullOrEmpty(config))
                    return null;
                return config.Split(';').Select(int.Parse).ToArray();

            }
        }
        /// <summary>
        /// 头像存储格式
        /// </summary>
        public static string AvatarFormat
        {
            get { return Configuration.AppSettings("AvatarFormat"); }
        }
        /// <summary>
        /// 头像是否同步云空间
        /// </summary>
        public static bool AvatarIsSyncCloud
        {
            get { return Configuration.AppSettings("AvatarIsSyncCloud").ToLower() == "true"; }
        }
        /// <summary>
        /// 头像是否使用云空间地址展示
        /// </summary>
        public static bool AvatarIsShowCloudUrl
        {
            get { return Configuration.AppSettings("AvatarIsShowCloudUrl").ToLower() == "true"; }
        }
        /// <summary>
        /// 头像使用云空间空间名称
        /// </summary>
        public static string AvatarBucketName
        {
            get { return Configuration.AppSettings("AvatarBucketName"); }
        }
        /// <summary>
        /// 头像云空间地址
        /// </summary>
        public static string AvatarCloudUrl
        {
            get { return Configuration.AppSettings("AvatarCloudUrl"); }
        }
        /// <summary>
        /// 头像大小限制
        /// </summary>
        public static long AvatarMaxSize
        {
            get { return long.Parse(Configuration.AppSettings("AvatarMaxSize")); }
        }
        #endregion


        #region 激活相关
        /// <summary>
        /// 激活令牌过期时间(分钟)
        /// </summary>
        public static int ActivateTokenExpireTime {
            get
            {
                return Configuration.AppSettingsToInt("ActivateTokenExpireTime");
            }
        }
        #endregion
        /// <summary>
        /// 找回密码令牌过期时间(分钟)
        /// </summary>
        public static int RetrievePasswordTokenExpireTime
        {
            get
            {
                return Configuration.AppSettingsToInt("RetrievePasswordTokenExpireTime");
            }
        }
        
    }
}
