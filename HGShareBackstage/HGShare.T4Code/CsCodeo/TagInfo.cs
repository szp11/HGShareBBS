using System;
namespace HGShare.Model
{    
	/// <summary>
    /// Tag 实体
    /// </summary>
    public class TagInfo
    {
		/// <summary>
		/// 
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 标签
		/// </summary>        
		public string Tag { get; set; }
		/// <summary>
		/// 文章Id
		/// </summary>        
		public long AId { get; set; }
		/// <summary>
		/// 状态 0待审核 1审核通过 2审核未通过
		/// </summary>        
		public short State { get; set; }
		/// <summary>
		/// 说明
		/// </summary>        
		public string Direction { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>        
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>        
		public int UserId { get; set; }
		 
    }
}
