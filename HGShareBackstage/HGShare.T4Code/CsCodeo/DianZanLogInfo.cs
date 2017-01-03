using System;
namespace HGShare.Model
{    
	/// <summary>
    /// DianZanLog 实体
    /// </summary>
    public class DianZanLogInfo
    {
		/// <summary>
		/// 
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 主体Id
		/// </summary>        
		public long MId { get; set; }
		/// <summary>
		/// 次级主键Id
		/// </summary>        
		public long CId { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>        
		public int UserId { get; set; }
		/// <summary>
		/// Ip
		/// </summary>        
		public string Ip { get; set; }
		/// <summary>
		/// 是否取消
		/// </summary>        
		public bool IsCancel { get; set; }
		/// <summary>
		/// 
		/// </summary>        
		public DateTime? CancelTime { get; set; }
		/// <summary>
		/// 类型 1文章 2评论
		/// </summary>        
		public short Type { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>        
		public DateTime CreateTime { get; set; }
		 
    }
}
