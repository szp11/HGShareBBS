using System.Globalization;
using HGShare.Site.Config;

namespace HGShare.Site
{
    /// <summary>
    /// 业务中处理方法组
    /// </summary>
    public class Tools
    {
        #region 用户头像生成相关
        /// <summary>
        /// 生成用户头像
        /// </summary>
        /// <param name="avatar"></param>
        /// <param name="sex"></param>
        /// <returns></returns>
        public static string GenerateAvatar(string avatar=null,short sex=0)
        {
            //服务器+目录+头像
            string path = "";

            if (WebSysConfig.AvatarIsShowCloudUrl)
            {
                path = WebSysConfig.AvatarCloudUrl;
            }
            else
            {
                path = UrlConfig.AvatarFileHost
                          + DirectoriesConfig.UserAvatarPath;
            }
            return path + (avatar ?? WebSysConfig.DefaultAvatar.Replace("{sex}", sex.ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// 生成用户头像
        /// </summary>
        /// <param name="avatar"></param>
        /// <param name="size"></param>
        /// <param name="sex"></param>
        /// <returns></returns>
        public static string GenerateAvatar(string avatar, int size, short sex = 0)
        {
            if(!string.IsNullOrEmpty(avatar))
            { return GenerateAvatar(string.Format("{0}.{1}x{1}.{2}", avatar, size, WebSysConfig.AvatarFormat)); }
            else
            {
                return GenerateAvatar(avatar, sex);
            }
        }
        #endregion
    }
}
