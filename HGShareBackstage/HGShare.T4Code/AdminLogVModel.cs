using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// AdminLog View Model
    /// </summary>
    public class AdminLogVModel
    {
		/// <summary>
		/// 记录编号
		/// </summary>
		[Required(ErrorMessage = "记录编号不能为空！")]
		[Display(Name = "记录编号")]     
		public long Id { get; set; }
		/// <summary>
		/// 用户ID
		/// </summary>
		[Required(ErrorMessage = "用户ID不能为空！")]
		[Display(Name = "用户ID")]     
		public long UserId { get; set; }
		/// <summary>
		/// 访问控制器
		/// </summary>
		[MaxLength(50, ErrorMessage = "访问控制器最大长度为50字符！")]
		[Required(ErrorMessage = "访问控制器不能为空！")]
		[Display(Name = "访问控制器")]     
		public string Controllers { get; set; }
		/// <summary>
		/// 访问Action
		/// </summary>
		[MaxLength(50, ErrorMessage = "访问Action最大长度为50字符！")]
		[Required(ErrorMessage = "访问Action不能为空！")]
		[Display(Name = "访问Action")]     
		public string Action { get; set; }
		/// <summary>
		/// 请求参数
		/// </summary>
		[MaxLength(16, ErrorMessage = "请求参数最大长度为16字符！")]
		[Display(Name = "请求参数")]     
		public string Parameter { get; set; }
		/// <summary>
		/// 请求中主要ID
		/// </summary>
		[MaxLength(50, ErrorMessage = "请求中主要ID最大长度为50字符！")]
		[Display(Name = "请求中主要ID")]     
		public string ActionId { get; set; }
		/// <summary>
		/// IP
		/// </summary>
		[MaxLength(20, ErrorMessage = "IP最大长度为20字符！")]
		[Required(ErrorMessage = "IP不能为空！")]
		[Display(Name = "IP")]     
		public string Ip { get; set; }
		/// <summary>
		/// Url
		/// </summary>
		[MaxLength(500, ErrorMessage = "Url最大长度为500字符！")]
		[Required(ErrorMessage = "Url不能为空！")]
		[Display(Name = "Url")]     
		public string Url { get; set; }
		/// <summary>
		/// 记录时间
		/// </summary>
		[Required(ErrorMessage = "记录时间不能为空！")]
		[Display(Name = "记录时间")]     
		public DateTime InTime { get; set; }
		/// <summary>
		/// 请求方法 get/post....
		/// </summary>
		[MaxLength(10, ErrorMessage = "请求方法 get/post....最大长度为10字符！")]
		[Required(ErrorMessage = "请求方法 get/post....不能为空！")]
		[Display(Name = "请求方法 get/post....")]     
		public string Method { get; set; }
		/// <summary>
		/// 是否是Ajax访问 0默认访问 1Ajax访问
		/// </summary>
		[Required(ErrorMessage = "是否是Ajax访问 0默认访问 1Ajax访问不能为空！")]
		[Display(Name = "是否是Ajax访问 0默认访问 1Ajax访问")]     
		public int IsAjax { get; set; }
		/// <summary>
		/// UserAgent
		/// </summary>
		[MaxLength(500, ErrorMessage = "UserAgent最大长度为500字符！")]
		[Display(Name = "UserAgent")]     
		public string UserAgent { get; set; }
		/// <summary>
		/// 控制器描述
		/// </summary>
		[MaxLength(50, ErrorMessage = "控制器描述最大长度为50字符！")]
		[Display(Name = "控制器描述")]     
		public string ControllersDsc { get; set; }
		/// <summary>
		/// Action描述
		/// </summary>
		[MaxLength(50, ErrorMessage = "Action描述最大长度为50字符！")]
		[Display(Name = "Action描述")]     
		public string ActionDsc { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
