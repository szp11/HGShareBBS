using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// UserOther View Model
    /// </summary>
    public class UserOtherVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public int Id { get; set; }
		/// <summary>
		/// 用户编号
		/// </summary>
		[Required(ErrorMessage = "用户编号不能为空！")]
		[Display(Name = "用户编号")]     
		public int UserId { get; set; }
		/// <summary>
		/// 个性介绍
		/// </summary>
		[MaxLength(500, ErrorMessage = "个性介绍最大长度为500字符！")]
		[Display(Name = "个性介绍")]     
		public string PersonalityIntroduce { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
