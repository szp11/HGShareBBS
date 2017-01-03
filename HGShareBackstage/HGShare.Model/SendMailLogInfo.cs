using System;
namespace HGShare.Model
{
    /// <summary>
    /// SendMailLog 实体
    /// </summary>
    public class SendMailLogInfo
    {
        /// <summary>
        /// 
        /// </summary>        
        public long Id { get; set; }
        /// <summary>
        /// 接收用户id
        /// </summary>        
        public int UserId { get; set; }
        /// <summary>
        /// 发送用户Id
        /// </summary>        
        public int SendUserId { get; set; }
        /// <summary>
        /// 邮件模板Id
        /// </summary>        
        public int TemplateId { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>        
        public string ToEmail { get; set; }
        /// <summary>
        /// 发送人邮箱
        /// </summary>        
        public string FromEmail { get; set; }
        /// <summary>
        /// 邮件发送状态
        /// </summary>        
        public short Status { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>        
        public string Title { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>        
        public string Body { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public string Ip { get; set; }
        /// <summary>
        /// 是否是系统邮件
        /// </summary>        
        public bool IsSystem { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>        
        public DateTime CreateTime { get; set; }

    }
}
