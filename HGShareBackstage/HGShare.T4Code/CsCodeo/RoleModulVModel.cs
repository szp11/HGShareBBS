using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// RoleModul View Model
    /// </summary>
    public class RoleModulVModel
    {
		/// <summary>
		/// ID
		/// </summary>
		[Required(ErrorMessage = "ID不能为空！")]
		[Display(Name = "ID")]     
		public int Id { get; set; }
		/// <summary>
		/// 模块ID
		/// </summary>
		[Required(ErrorMessage = "模块ID不能为空！")]
		[Display(Name = "模块ID")]     
		public int MId { get; set; }
		/// <summary>
		/// 角色ID
		/// </summary>
		[Required(ErrorMessage = "角色ID不能为空！")]
		[Display(Name = "角色ID")]     
		public int RId { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
