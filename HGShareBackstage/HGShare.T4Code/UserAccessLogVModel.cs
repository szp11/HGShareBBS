using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// UserAccessLog View Model
    /// </summary>
    public class UserAccessLogVModel
    {
		/// <summary>
		/// 编号
		/// </summary>
		[Required(ErrorMessage = "编号不能为空！")]
		[Display(Name = "编号")]     
		public long Id { get; set; }
		/// <summary>
		/// 访问地址
		/// </summary>
		[MaxLength(200, ErrorMessage = "访问地址最大长度为200字符！")]
		[Required(ErrorMessage = "访问地址不能为空！")]
		[Display(Name = "访问地址")]     
		public string Url { get; set; }
		/// <summary>
		/// 来源地址
		/// </summary>
		[MaxLength(200, ErrorMessage = "来源地址最大长度为200字符！")]
		[Required(ErrorMessage = "来源地址不能为空！")]
		[Display(Name = "来源地址")]     
		public string Referer { get; set; }
		/// <summary>
		/// UA
		/// </summary>
		[MaxLength(500, ErrorMessage = "UA最大长度为500字符！")]
		[Display(Name = "UA")]     
		public string UserAgent { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		[Required(ErrorMessage = "创建时间不能为空！")]
		[Display(Name = "创建时间")]     
		public DateTime InsertTime { get; set; }
		/// <summary>
		/// 运行时间
		/// </summary>
		[Required(ErrorMessage = "运行时间不能为空！")]
		[Display(Name = "运行时间")]     
		public int RuanTime { get; set; }
		/// <summary>
		/// Ip
		/// </summary>
		[MaxLength(20, ErrorMessage = "Ip最大长度为20字符！")]
		[Required(ErrorMessage = "Ip不能为空！")]
		[Display(Name = "Ip")]     
		public string Ip { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>
		[Required(ErrorMessage = "用户Id不能为空！")]
		[Display(Name = "用户Id")]     
		public int UserId { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
