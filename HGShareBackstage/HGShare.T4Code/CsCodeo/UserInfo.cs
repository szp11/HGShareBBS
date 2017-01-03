using System;
namespace HGShare.Model
{    
	/// <summary>
    /// User 实体
    /// </summary>
    public class UserInfo
    {
		/// <summary>
		/// 用户ID
		/// </summary>        
		public int Id { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>        
		public string Name { get; set; }
		/// <summary>
		/// 用户昵称
		/// </summary>        
		public string NickName { get; set; }
		/// <summary>
		/// 密码
		/// </summary>        
		public string Password { get; set; }
		/// <summary>
		/// 角色ID
		/// </summary>        
		public int RoleId { get; set; }
		/// <summary>
		/// 最后在线时间
		/// </summary>        
		public DateTime? OnLineTime { get; set; }
		/// <summary>
		/// 最后操作时间
		/// </summary>        
		public DateTime? ActionTime { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>        
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 头像
		/// </summary>        
		public string Avatar { get; set; }
		/// <summary>
		/// 性别 0 未知 1男 2女
		/// </summary>        
		public short Sex { get; set; }
		/// <summary>
		/// 邮箱
		/// </summary>        
		public string Email { get; set; }
		/// <summary>
		/// Email是否激活
		/// </summary>        
		public bool EmailStatus { get; set; }
		/// <summary>
		/// 积分
		/// </summary>        
		public long Score { get; set; }
		/// <summary>
		/// 文章数
		/// </summary>        
		public int ArticleNum { get; set; }
		/// <summary>
		/// 回复数
		/// </summary>        
		public int CommentNum { get; set; }
		/// <summary>
		/// 禁用
		/// </summary>        
		public bool Disable { get; set; }
		/// <summary>
		/// 禁用原因
		/// </summary>        
		public string DisableReason { get; set; }
		/// <summary>
		/// QQ号
		/// </summary>        
		public string QQ { get; set; }
		 
    }
}
