namespace HGShare.Utils.Interface
{
    public interface IMail
    {
        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="encoding">编码</param>
        /// <param name="isBodyHtml">是否Html</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <returns>是否成功</returns>
        bool Send(string to, string subject, string body, string encoding = "UTF-8", bool isBodyHtml = true,
            bool enableSsl = false);

        /// <summary>
        /// 异步发送邮件 独立线程
        /// </summary>
        /// <param name="to">邮件接收人</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="encoding">编码</param>
        /// <param name="isBodyHtml">是否Html</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <returns></returns>
        void SendAsync(string to, string title, string body, string encoding = "UTF-8", bool isBodyHtml = true,
            bool enableSsl = false);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetFromEmail();
    }
}
