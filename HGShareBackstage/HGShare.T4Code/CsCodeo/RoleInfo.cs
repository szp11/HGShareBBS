using System;
namespace HGShare.Model
{    
	/// <summary>
    /// Role 实体
    /// </summary>
    public class RoleInfo
    {
		/// <summary>
		/// 
		/// </summary>        
		public int Id { get; set; }
		/// <summary>
		/// 角色名
		/// </summary>        
		public string RName { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>        
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 是否禁用
		/// </summary>        
		public bool IsDel { get; set; }
		/// <summary>
		/// 描述
		/// </summary>        
		public string Description { get; set; }
		/// <summary>
		/// 是否是超级角色
		/// </summary>        
		public bool IsSuper { get; set; }
		/// <summary>
		/// 发帖是否需要审核
		/// </summary>        
		public bool ArticleNeedVerified { get; set; }
		/// <summary>
		/// 评论是否需要审核
		/// </summary>        
		public bool CommentNeedVerified { get; set; }
		 
    }
}
