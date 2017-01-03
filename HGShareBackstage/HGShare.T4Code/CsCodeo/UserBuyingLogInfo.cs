using System;
namespace HGShare.Model
{    
	/// <summary>
    /// UserBuyingLog 实体
    /// </summary>
    public class UserBuyingLogInfo
    {
		/// <summary>
		/// 
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 文章Id
		/// </summary>        
		public long AId { get; set; }
		/// <summary>
		/// 花费积分
		/// </summary>        
		public int Score { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>        
		public int UserId { get; set; }
		/// <summary>
		/// 
		/// </summary>        
		public DateTime CreateTime { get; set; }
		 
    }
}
