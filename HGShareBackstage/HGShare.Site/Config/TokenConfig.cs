using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.Site.Config
{
    /// <summary>
    /// 密令配置
    /// </summary>
    public class TokenConfig
    {
        /// <summary>
        /// 登录密令加密key
        /// </summary>
        public static string LoginTokenKey 
        {
            get { return "_manager_login_tokenkey"; }
        }
        /// <summary>
        /// 密码密令加密key
        /// </summary>
        public static string PwdTokenKey
        {
            get { return "_manager_password_tokenkey"; }
        }
        /// <summary>
        /// 激活密令加密key
        /// </summary>
        public static string ActivateTokenKey
        {
            get { return "activatetokenkey_tokenkey"; }
        }
    }
}
