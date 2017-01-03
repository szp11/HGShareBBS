using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// EmailTemplate View Model
    /// </summary>
    public class EmailTemplateVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public int Id { get; set; }
		/// <summary>
		/// 邮件标题
		/// </summary>
		[MaxLength(200, ErrorMessage = "邮件标题最大长度为200字符！")]
		[Required(ErrorMessage = "邮件标题不能为空！")]
		[Display(Name = "邮件标题")]     
		public string Title { get; set; }
		/// <summary>
		/// 模板内容
		/// </summary>
		[MaxLength(16, ErrorMessage = "模板内容最大长度为16字符！")]
		[Required(ErrorMessage = "模板内容不能为空！")]
		[Display(Name = "模板内容")]     
		public string Template { get; set; }
		/// <summary>
		/// 模板值的标识符 例如：($UserName;$UserId)
		/// </summary>
		[MaxLength(500, ErrorMessage = "模板值的标识符 例如：($UserName;$UserId)最大长度为500字符！")]
		[Required(ErrorMessage = "模板值的标识符 例如：($UserName;$UserId)不能为空！")]
		[Display(Name = "模板值的标识符 例如：($UserName;$UserId)")]     
		public string ValueIdentifier { get; set; }
		/// <summary>
		/// 模板说明
		/// </summary>
		[MaxLength(50, ErrorMessage = "模板说明最大长度为50字符！")]
		[Required(ErrorMessage = "模板说明不能为空！")]
		[Display(Name = "模板说明")]     
		public string Explanation { get; set; }
		/// <summary>
		/// 是否是系统邮件模板
		/// </summary>
		[Required(ErrorMessage = "是否是系统邮件模板不能为空！")]
		[Display(Name = "是否是系统邮件模板")]     
		public bool IsSystem { get; set; }
		/// <summary>
		/// 是否是html
		/// </summary>
		[Required(ErrorMessage = "是否是html不能为空！")]
		[Display(Name = "是否是html")]     
		public bool IsHtml { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>
		[Required(ErrorMessage = "添加时间不能为空！")]
		[Display(Name = "添加时间")]     
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 创建用户Id
		/// </summary>
		[Required(ErrorMessage = "创建用户Id不能为空！")]
		[Display(Name = "创建用户Id")]     
		public int UserId { get; set; }
		/// <summary>
		/// 最后修改人Id
		/// </summary>
		[Display(Name = "最后修改人Id")]     
		public int? LastEditUserId { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		[Display(Name = "最后修改时间")]     
		public DateTime? LastEditTime { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
