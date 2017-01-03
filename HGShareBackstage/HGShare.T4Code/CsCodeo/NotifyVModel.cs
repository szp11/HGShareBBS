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
		/// <summary>
		/// 
		/// </summary>
		[MaxLength(2000, ErrorMessage = "最大长度为2000字符！")]
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public string Content { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[MaxLength(50, ErrorMessage = "最大长度为50字符！")]
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public string Title { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
