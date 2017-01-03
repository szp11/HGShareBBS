using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// Tag View Model
    /// </summary>
    public class TagVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public long Id { get; set; }
		/// <summary>
		/// 标签
		/// </summary>
		[MaxLength(10, ErrorMessage = "标签最大长度为10字符！")]
		[Required(ErrorMessage = "标签不能为空！")]
		[Display(Name = "标签")]     
		public string Tag { get; set; }
		/// <summary>
		/// 文章Id
		/// </summary>
		[Required(ErrorMessage = "文章Id不能为空！")]
		[Display(Name = "文章Id")]     
		public long AId { get; set; }
		/// <summary>
		/// 状态 0待审核 1审核通过 2审核未通过
		/// </summary>
		[Required(ErrorMessage = "状态 0待审核 1审核通过 2审核未通过不能为空！")]
		[Display(Name = "状态 0待审核 1审核通过 2审核未通过")]     
		public short State { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		[MaxLength(200, ErrorMessage = "说明最大长度为200字符！")]
		[Display(Name = "说明")]     
		public string Direction { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		[Required(ErrorMessage = "创建时间不能为空！")]
		[Display(Name = "创建时间")]     
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>
		[Required(ErrorMessage = "用户Id不能为空！")]
		[Display(Name = "用户Id")]     
		public int UserId { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
