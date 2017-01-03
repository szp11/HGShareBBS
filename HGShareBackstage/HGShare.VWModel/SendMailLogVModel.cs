using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{
    /// <summary>
    /// SendMailLog View Model
    /// </summary>
    public class SendMailLogVModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "不能为空！")]
        [Display(Name = "")]
        public long Id { get; set; }
        /// <summary>
        /// 接收用户id
        /// </summary>
        [Required(ErrorMessage = "接收用户id不能为空！")]
        [Display(Name = "接收用户id")]
        public int UserId { get; set; }
        /// <summary>
        /// 发送用户Id
        /// </summary>
        [Required(ErrorMessage = "发送用户Id不能为空！")]
        [Display(Name = "发送用户Id")]
        public int SendUserId { get; set; }
        /// <summary>
        /// 邮件模板Id
        /// </summary>
        [Required(ErrorMessage = "邮件模板Id不能为空！")]
        [Display(Name = "邮件模板Id")]
        public int TemplateId { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        [MaxLength(50, ErrorMessage = "接收人最大长度为50字符！")]
        [Required(ErrorMessage = "接收人不能为空！")]
        [Display(Name = "接收人")]
        public string ToEmail { get; set; }
        /// <summary>
        /// 发送人邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "发送人邮箱最大长度为50字符！")]
        [Required(ErrorMessage = "发送人邮箱不能为空！")]
        [Display(Name = "发送人邮箱")]
        public string FromEmail { get; set; }
        /// <summary>
        /// 邮件发送状态
        /// </summary>
        [Required(ErrorMessage = "邮件发送状态不能为空！")]
        [Display(Name = "邮件发送状态")]
        public short Status { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        [MaxLength(500, ErrorMessage = "邮件标题最大长度为500字符！")]
        [Required(ErrorMessage = "邮件标题不能为空！")]
        [Display(Name = "邮件标题")]
        public string Title { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        [MaxLength(16, ErrorMessage = "邮件内容最大长度为16字符！")]
        [Required(ErrorMessage = "邮件内容不能为空！")]
        [Display(Name = "邮件内容")]
        public string Body { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20, ErrorMessage = "最大长度为20字符！")]
        [Required(ErrorMessage = "不能为空！")]
        [Display(Name = "")]
        public string Ip { get; set; }
        /// <summary>
        /// 是否是系统邮件
        /// </summary>
        [Required(ErrorMessage = "是否是系统邮件不能为空！")]
        [Display(Name = "是否是系统邮件")]
        public bool IsSystem { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        [Required(ErrorMessage = "发送时间不能为空！")]
        [Display(Name = "发送时间")]
        public DateTime CreateTime { get; set; }

    }
}
