using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// Notify View Model
    /// </summary>
    public class NotifyVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public long Id { get; set; }
		/// <summary>
		/// 发送者
		/// </summary>
		[Required(ErrorMessage = "发送者不能为空！")]
		[Display(Name = "发送者")]     
		public int FromUserId { get; set; }
		/// <summary>
		/// 接受者
		/// </summary>
		[Required(ErrorMessage = "接受者不能为空！")]
		[Display(Name = "接受者")]     
		public int ToUserId { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		[Required(ErrorMessage = "创建时间不能为空！")]
		[Display(Name = "创建时间")]     
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 是否删除
		/// </summary>
		[Required(ErrorMessage = "是否删除不能为空！")]
		[Display(Name = "是否删除")]     
		public bool IsDelete { get; set; }
		/// <summary>
		/// 是否已读
		/// </summary>
		[Required(ErrorMessage = "是否已读不能为空！")]
		[Display(Name = "是否已读")]     
		public bool IsRead { get; set; }
		/// <summary>
		/// 是否是系统消息
		/// </summary>
		[Required(ErrorMessage = "是否是系统消息不能为空！")]
		[Display(Name = "是否是系统消息")]     
		public bool IsSystem { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
