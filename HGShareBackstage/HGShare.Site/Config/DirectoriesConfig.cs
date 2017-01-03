using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Common;

namespace HGShare.Site.Config
{
    /// <summary>
    /// 目录地址配置
    /// </summary>
    public class DirectoriesConfig
    {
        /// <summary>
        /// 后台上传路径
        /// </summary>
        /// <returns></returns>
        public static string BackstageUploadPath
        {
            get {return Configuration.AppSettings("UploadPath"); }
        }
        /// <summary>
        /// 用户头像目录
        /// </summary>
        /// <returns></returns>
        public static string UserAvatarPath
        {
            get { return Configuration.AppSettings("UserAvatarPath"); }
        }
    }
}
