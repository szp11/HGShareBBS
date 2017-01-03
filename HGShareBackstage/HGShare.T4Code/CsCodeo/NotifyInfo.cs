using System;
namespace HGShare.Model
{    
	/// <summary>
    /// Notify 实体
    /// </summary>
    public class NotifyInfo
    {
		/// <summary>
		/// 
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 发送者
		/// </summary>        
		public int FromUserId { get; set; }
		/// <summary>
		/// 接受者
		/// </summary>        
		public int ToUserId { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>        
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 是否删除
		/// </summary>        
		public bool IsDelete { get; set; }
		/// <summary>
		/// 是否已读
		/// </summary>        
		public bool IsRead { get; set; }
		/// <summary>
		/// 是否是系统消息
		/// </summary>        
		public bool IsSystem { get; set; }
		/// <summary>
		/// 
		/// </summary>        
		public string Content { get; set; }
		/// <summary>
		/// 
		/// </summary>        
		public string Title { get; set; }
		 
    }
}
