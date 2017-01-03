using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// Area View Model
    /// </summary>
    public class AreaVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public int Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[MaxLength(50, ErrorMessage = "最大长度为50字符！")]
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public int Code { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[MaxLength(50, ErrorMessage = "最大长度为50字符！")]
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public string PinYin { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[MaxLength(10, ErrorMessage = "最大长度为10字符！")]
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public string SortPinYin { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[MaxLength(10, ErrorMessage = "最大长度为10字符！")]
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public string Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public int ParentCode { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
