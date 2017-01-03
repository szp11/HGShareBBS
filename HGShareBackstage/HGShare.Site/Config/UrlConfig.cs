using HGShare.Common;

namespace HGShare.Site.Config
{
    /// <summary>
    /// url相关配置
    /// </summary>
    public class UrlConfig
    {
        /// <summary>
        /// BBS服务器地址
        /// </summary>
        /// <returns></returns>
        public static string BBSWebUrl
        {
            get { return Configuration.AppSettings("BBSWebUrl"); }
        }
        /// <summary>
        /// 静态文件服务器地址
        /// </summary>
        /// <returns></returns>
        public static string StaticFileHost
        {
            get { return Configuration.AppSettings("StaticFileHost"); }
        }
        /// <summary>
        /// 头像文件服务器地址
        /// </summary>
        /// <returns></returns>
        public static string AvatarFileHost
        {
            get { return Configuration.AppSettings("AvatarFileHost"); }
        }
    }
}
