using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// UserPosition View Model
    /// </summary>
    public class UserPositionVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public long Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public int UserId { get; set; }
		/// <summary>
		/// 位置代码
		/// </summary>
		[Required(ErrorMessage = "位置代码不能为空！")]
		[Display(Name = "位置代码")]     
		public int Code { get; set; }
		/// <summary>
		/// 类型 0省 1城 2区
		/// </summary>
		[Required(ErrorMessage = "类型 0省 1城 2区不能为空！")]
		[Display(Name = "类型 0省 1城 2区")]     
		public short Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public DateTime CreateTime { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
