using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HGShare.VWModel
{
    public class UserVModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(15, ErrorMessage = "用户名最大长度为15字符！")]
        [Required(ErrorMessage = "用户名不能为空！")]
        [MinLength(5, ErrorMessage = "用户名长度过短,最小长度为5个字符!")]
        [Display(Name = "用户名")]
        [Remote("CheckUserName", "Users", ErrorMessage = "该用户名已存在!", HttpMethod = "post", AdditionalFields = "Name ,Id")]   
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(15, ErrorMessage = "密码最大长度为15字符！")]
        [Required(ErrorMessage = "密码不能为空！")]
        [MinLength(6, ErrorMessage = "密码长度过短,最小长度为6个字符!")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "两次输入密码不统一！")]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required(ErrorMessage = "请选择角色信息！")]
        [Display(Name = "角色")]
        public int RoleId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        [MaxLength(15, ErrorMessage = "用户昵称最大长度为15字符！")]
        [MinLength(3, ErrorMessage = "用户昵称长度过短,最小长度为3个字符!")]
        [Required(ErrorMessage = "用户昵称不能为空！")]
        [Remote("CheckNickName", "Users", ErrorMessage = "该用户昵称已存在!", HttpMethod = "post", AdditionalFields = "NickName ,Id")]
        [Display(Name = "用户昵称")]
        public string NickName { get; set; }
        /// <summary>
        /// 权限集合
        /// </summary>
        public List<RoleVModel> Roles { get; set; }
        /// <summary>
        /// 最后在线时间
        /// </summary>
        public DateTime? OnLineTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime? ActionTime { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string RName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100, ErrorMessage = "头像最大长度为100字符！")]
        [Display(Name = "头像")]
        public string Avatar { get; set; }
        /// <summary>
        /// 性别 
        /// </summary>
        [Required(ErrorMessage = "性别不能为空！")]
        [Display(Name = "性别")]
        public short Sex { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage="邮箱格式不正确！")]
        [MaxLength(50, ErrorMessage = "邮箱最大长度为50字符！")]
        [Remote("CheckEmail", "Users", ErrorMessage = "该邮箱已存在!", HttpMethod = "post", AdditionalFields = "Email ,Id")]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// Email是否激活
        /// </summary>
        [Display(Name = "Email是否激活")]
        public bool EmailStatus { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        [Display(Name = "积分")]
        public long Score { get; set; }
        /// <summary>
        /// 文章数
        /// </summary>
        [Display(Name = "文章数")]
        public int ArticleNum { get; set; }
        /// <summary>
        /// 回复数
        /// </summary>
        [Display(Name = "回复数")]
        public int CommentNum { get; set; }
        /// <summary>
        /// 禁用
        /// </summary>
        [Display(Name = "禁用")]
        public bool Disable { get; set; }
        /// <summary>
        /// 禁用原因
        /// </summary>
        [MaxLength(200, ErrorMessage = "禁用原因最大长度为200字符！")]
        [Display(Name = "禁用原因")]
        public string DisableReason { get; set; }
        /// <summary>
        /// 判断是否是空对象
        /// </summary>
        public bool IsNull {
            get { return Id <= 0; }
        }
    }
}
