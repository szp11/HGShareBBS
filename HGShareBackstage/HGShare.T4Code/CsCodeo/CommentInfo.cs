using System;
namespace HGShare.Model
{    
	/// <summary>
    /// Comment 实体
    /// </summary>
    public class CommentInfo
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
		/// 评论用户Id
		/// </summary>        
		public int UserId { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>        
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 评论内容
		/// </summary>        
		public string Content { get; set; }
		/// <summary>
		/// 评论者Ip
		/// </summary>        
		public string IP { get; set; }
		/// <summary>
		/// 用户UA信息
		/// </summary>        
		public string UserAgent { get; set; }
		/// <summary>
		/// 审核状态0待审核 1已通过 2未通过
		/// </summary>        
		public short State { get; set; }
		/// <summary>
		/// 拒绝原因
		/// </summary>        
		public string RefuseReason { get; set; }
		/// <summary>
		/// 是否删除
		/// </summary>        
		public bool IsDelete { get; set; }
		/// <summary>
		/// 最后修改用户 0默认
		/// </summary>        
		public int LastEditUserId { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>        
		public DateTime LastEditTime { get; set; }
		/// <summary>
		/// 是否置顶
		/// </summary>        
		public bool IsStick { get; set; }
		/// <summary>
		/// 点赞数
		/// </summary>        
		public int DianZanNum { get; set; }
		 
    }
}
