using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// Role View Model
    /// </summary>
    public class RoleVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public int Id { get; set; }
		/// <summary>
		/// 角色名
		/// </summary>
		[MaxLength(400, ErrorMessage = "角色名最大长度为400字符！")]
		[Required(ErrorMessage = "角色名不能为空！")]
		[Display(Name = "角色名")]     
		public string RName { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>
		[Required(ErrorMessage = "添加时间不能为空！")]
		[Display(Name = "添加时间")]     
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 是否禁用
		/// </summary>
		[Required(ErrorMessage = "是否禁用不能为空！")]
		[Display(Name = "是否禁用")]     
		public bool IsDel { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		[MaxLength(500, ErrorMessage = "描述最大长度为500字符！")]
		[Display(Name = "描述")]     
		public string Description { get; set; }
		/// <summary>
		/// 是否是超级角色
		/// </summary>
		[Required(ErrorMessage = "是否是超级角色不能为空！")]
		[Display(Name = "是否是超级角色")]     
		public bool IsSuper { get; set; }
		/// <summary>
		/// 发帖是否需要审核
		/// </summary>
		[Required(ErrorMessage = "发帖是否需要审核不能为空！")]
		[Display(Name = "发帖是否需要审核")]     
		public bool ArticleNeedVerified { get; set; }
		/// <summary>
		/// 评论是否需要审核
		/// </summary>
		[Required(ErrorMessage = "评论是否需要审核不能为空！")]
		[Display(Name = "评论是否需要审核")]     
		public bool CommentNeedVerified { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
