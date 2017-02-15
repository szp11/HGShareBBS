namespace HGShare.Utils.Mail
{
    public class SmtpMailConfig
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string MailServer { get; set; }
        /// <summary>
        /// 用户名(email地址)
        /// </summary>
        public string MailUserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string MailPassword { get; set; }
        /// <summary>
        /// 名称（显示名称）
        /// </summary>
        public string MailName { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
    }
}
