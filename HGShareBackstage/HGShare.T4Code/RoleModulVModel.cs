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
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
