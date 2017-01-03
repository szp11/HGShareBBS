using System;
namespace HGShare.Model
{    
	/// <summary>
    /// UserActivateToken 实体
    /// </summary>
    public class UserActivateTokenInfo
    {
		/// <summary>
		/// 
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>        
		public int UserId { get; set; }
		/// <summary>
		/// 激活邮箱
		/// </summary>        
		public string Email { get; set; }
		/// <summary>
		/// 令牌
		/// </summary>        
		public string Token { get; set; }
		/// <summary>
		/// 是否有效
		/// </summary>        
		public bool Status { get; set; }
		/// <summary>
		/// 写入时间
		/// </summary>        
		public DateTime CreateTime { get; set; }
		 
    }
}
