using System.Net;
using System.Net.Mail;
using System.Text;
using HGShare.Com.Interface;
using HGShare.Common;

namespace HGShare.Com.Tools.Mail
{
    public class SmtpMail:IMail
    {
        public string MailServer { get; set; }
        public string MailUserName { get; set; }
        public string MailPassword { get; set; }
        public string MailName { get; set; }
        public int Port { get; set; }

        public SmtpMail()
        {
            MailServer = Configuration.AppSettings("MailServer");
            MailUserName = Configuration.AppSettings("MailUserName");
            MailPassword = Configuration.AppSettings("MailPassword");
            MailName = Configuration.AppSettings("MailName");
            Port = int.Parse(Configuration.AppSettings("Port"));

        }

        public bool Send(string to, string subject, string body, string encoding = "UTF-8", bool isBodyHtml = true,
            bool enableSsl = false)
        {
            var message = new MailMessage();
            // 接收人邮箱地址
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(MailUserName, MailName);
            message.BodyEncoding = Encoding.GetEncoding(encoding);
            message.Body = body;
            //GB2312
            message.SubjectEncoding = Encoding.GetEncoding(encoding);
            message.Subject = subject;
            message.IsBodyHtml = isBodyHtml;

            var smtpclient = new SmtpClient(MailServer, Port)
            {
                Credentials = new NetworkCredential(MailUserName, MailPassword),
                EnableSsl = enableSsl
            };
            //SSL连接
            smtpclient.Send(message);
            return true;
        }
        public async void SendAsync(string to, string title, string body, string encoding = "UTF-8", bool isBodyHtml = true,
            bool enableSsl = false)
        {
            var smtp = new SmtpClient
            {
                Host = MailServer,//邮箱的smtp地址
                Port = Port,//端口号
                Credentials = new NetworkCredential(MailUserName, MailPassword), //构建发件人的身份凭据类
                EnableSsl = enableSsl
            };
           
            //构建消息类
            var objMailMessage = new MailMessage
            {
                Priority = MailPriority.High,
                From = new MailAddress(MailUserName, MailName, Encoding.UTF8)
            };
            //设置优先级
            //消息发送人
            //收件人
            objMailMessage.To.Add(to);
            //标题
            objMailMessage.Subject = title.Trim();
            //标题字符编码
            objMailMessage.SubjectEncoding = Encoding.GetEncoding(encoding);
            //正文
            objMailMessage.Body = body.Trim();
            objMailMessage.IsBodyHtml = isBodyHtml;
            //内容字符编码
            objMailMessage.BodyEncoding = Encoding.GetEncoding(encoding);
            //发送
           await smtp.SendMailAsync(objMailMessage);
        }
    }
}
