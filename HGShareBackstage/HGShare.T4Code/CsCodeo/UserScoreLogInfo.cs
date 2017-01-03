using System;
namespace HGShare.Model
{    
	/// <summary>
    /// UserScoreLog 实体
    /// </summary>
    public class UserScoreLogInfo
    {
		/// <summary>
		/// 
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 用户id
		/// </summary>        
		public int UserId { get; set; }
		/// <summary>
		/// 增减值
		/// </summary>        
		public int Score { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>        
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 描述
		/// </summary>        
		public string Describe { get; set; }
		/// <summary>
		/// 增减前分值
		/// </summary>        
		public long OldScore { get; set; }
		/// <summary>
		/// 增减后分值
		/// </summary>        
		public long NewScore { get; set; }
		 
    }
}
