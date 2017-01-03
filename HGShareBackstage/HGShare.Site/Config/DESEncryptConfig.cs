namespace HGShare.Site.Config
{
    /// <summary>
    /// 对称加密密匙配置
    /// </summary>
    public class DesEncryptConfig
    {
        /// <summary>
        /// cookei值加密密令
        /// </summary>
        public static string CookieValueKey 
        {
            get { return "_cookie_token"; }
        }
    }
}
