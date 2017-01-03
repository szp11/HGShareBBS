namespace HGShare.Site.Config
{
    /// <summary>
    /// cookie名配置
    /// </summary>
    public class CookieConfig
    {
        /// <summary>
        /// 用户idcookie名
        /// </summary>
        public static string UserIdCkName
        {
            get { return "_hg_userid"; }
        }
        /// <summary>
        /// 时间戳cookie名
        /// </summary>
        public static string StampCkName
        {
            get { return "_hg_stamp"; }
        }
        /// <summary>
        /// 密令cookie名
        /// </summary>
        public static string TokenCkName
        {
            get { return "_hg_token"; }
        }
        /// <summary>
        /// 登陆cookie过期时间（毫秒）
        /// </summary>
        public static int LoginCookieExpiredTime
        {
            get { return 1000*60*60*24*2; }
        }
        /// <summary>
        /// 用户其它信息
        /// </summary>
        public static string UserOther
        {
            get { return "_hg_user_other"; }
        }
    }
}
