using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// User View Model
    /// </summary>
    public class UserVModel
    {
		/// <summary>
		/// 用户ID
		/// </summary>
		[Required(ErrorMessage = "用户ID不能为空！")]
		[Display(Name = "用户ID")]     
		public int Id { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		[MaxLength(50, ErrorMessage = "用户名最大长度为50字符！")]
		[Required(ErrorMessage = "用户名不能为空！")]
		[Display(Name = "用户名")]     
		public string Name { get; set; }
		/// <summary>
		/// 用户昵称
		/// </summary>
		[MaxLength(50, ErrorMessage = "用户昵称最大长度为50字符！")]
		[Required(ErrorMessage = "用户昵称不能为空！")]
		[Display(Name = "用户昵称")]     
		public string NickName { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		[MaxLength(50, ErrorMessage = "密码最大长度为50字符！")]
		[Required(ErrorMessage = "密码不能为空！")]
		[Display(Name = "密码")]     
		public string Password { get; set; }
		/// <summary>
		/// 角色ID
		/// </summary>
		[Required(ErrorMessage = "角色ID不能为空！")]
		[Display(Name = "角色ID")]     
		public int RoleId { get; set; }
		/// <summary>
		/// 最后在线时间
		/// </summary>
		[Display(Name = "最后在线时间")]     
		public DateTime? OnLineTime { get; set; }
		/// <summary>
		/// 最后操作时间
		/// </summary>
		[Display(Name = "最后操作时间")]     
		public DateTime? ActionTime { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		[Required(ErrorMessage = "创建时间不能为空！")]
		[Display(Name = "创建时间")]     
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 头像
		/// </summary>
		[MaxLength(100, ErrorMessage = "头像最大长度为100字符！")]
		[Display(Name = "头像")]     
		public string Avatar { get; set; }
		/// <summary>
		/// 性别 0 未知 1男 2女
		/// </summary>
		[Required(ErrorMessage = "性别 0 未知 1男 2女不能为空！")]
		[Display(Name = "性别 0 未知 1男 2女")]     
		public short Sex { get; set; }
		/// <summary>
		/// 邮箱
		/// </summary>
		[MaxLength(50, ErrorMessage = "邮箱最大长度为50字符！")]
		[Required(ErrorMessage = "邮箱不能为空！")]
		[Display(Name = "邮箱")]     
		public string Email { get; set; }
		/// <summary>
		/// Email是否激活
		/// </summary>
		[Required(ErrorMessage = "Email是否激活不能为空！")]
		[Display(Name = "Email是否激活")]     
		public bool EmailStatus { get; set; }
		/// <summary>
		/// 积分
		/// </summary>
		[Required(ErrorMessage = "积分不能为空！")]
		[Display(Name = "积分")]     
		public long Score { get; set; }
		/// <summary>
		/// 文章数
		/// </summary>
		[Required(ErrorMessage = "文章数不能为空！")]
		[Display(Name = "文章数")]     
		public int ArticleNum { get; set; }
		/// <summary>
		/// 回复数
		/// </summary>
		[Required(ErrorMessage = "回复数不能为空！")]
		[Display(Name = "回复数")]     
		public int CommentNum { get; set; }
		/// <summary>
		/// 禁用
		/// </summary>
		[Required(ErrorMessage = "禁用不能为空！")]
		[Display(Name = "禁用")]     
		public bool Disable { get; set; }
		/// <summary>
		/// 禁用原因
		/// </summary>
		[MaxLength(200, ErrorMessage = "禁用原因最大长度为200字符！")]
		[Display(Name = "禁用原因")]     
		public string DisableReason { get; set; }
		/// <summary>
		/// QQ号
		/// </summary>
		[MaxLength(15, ErrorMessage = "QQ号最大长度为15字符！")]
		[Display(Name = "QQ号")]     
		public string QQ { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
