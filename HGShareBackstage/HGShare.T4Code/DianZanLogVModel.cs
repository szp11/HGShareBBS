using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// DianZanLog View Model
    /// </summary>
    public class DianZanLogVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public long Id { get; set; }
		/// <summary>
		/// 主体Id
		/// </summary>
		[Required(ErrorMessage = "主体Id不能为空！")]
		[Display(Name = "主体Id")]     
		public long MId { get; set; }
		/// <summary>
		/// 次级主键Id
		/// </summary>
		[Required(ErrorMessage = "次级主键Id不能为空！")]
		[Display(Name = "次级主键Id")]     
		public long CId { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>
		[Required(ErrorMessage = "用户Id不能为空！")]
		[Display(Name = "用户Id")]     
		public int UserId { get; set; }
		/// <summary>
		/// Ip
		/// </summary>
		[MaxLength(20, ErrorMessage = "Ip最大长度为20字符！")]
		[Required(ErrorMessage = "Ip不能为空！")]
		[Display(Name = "Ip")]     
		public string Ip { get; set; }
		/// <summary>
		/// 是否取消
		/// </summary>
		[Required(ErrorMessage = "是否取消不能为空！")]
		[Display(Name = "是否取消")]     
		public bool IsCancel { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]     
		public DateTime? CancelTime { get; set; }
		/// <summary>
		/// 类型 1文章 2评论
		/// </summary>
		[Required(ErrorMessage = "类型 1文章 2评论不能为空！")]
		[Display(Name = "类型 1文章 2评论")]     
		public short Type { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		[Required(ErrorMessage = "创建时间不能为空！")]
		[Display(Name = "创建时间")]     
		public DateTime CreateTime { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
