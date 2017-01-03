using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{
    /// <summary>
    /// UserActivateToken View Model
    /// </summary>
    public class UserActivateTokenVModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "不能为空！")]
        [Display(Name = "")]
        public long Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空！")]
        [Display(Name = "用户Id")]
        public int UserId { get; set; }
        /// <summary>
        /// 激活邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "激活邮箱最大长度为50字符！")]
        [Required(ErrorMessage = "激活邮箱不能为空！")]
        [Display(Name = "激活邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        [MaxLength(100, ErrorMessage = "令牌最大长度为100字符！")]
        [Required(ErrorMessage = "令牌不能为空！")]
        [Display(Name = "令牌")]
        public string Token { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Required(ErrorMessage = "是否有效不能为空！")]
        [Display(Name = "是否有效")]
        public bool Status { get; set; }
        /// <summary>
        /// 写入时间
        /// </summary>
        [Required(ErrorMessage = "写入时间不能为空！")]
        [Display(Name = "写入时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 判断是否是空对象
        /// </summary>
        public bool IsNull {
            get { return Id <= 0; }
        }
    }
}
