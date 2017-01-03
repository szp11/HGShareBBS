using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// Modul View Model
    /// </summary>
    public class ModulVModel
    {
		/// <summary>
		/// 模块ID
		/// </summary>
		[Required(ErrorMessage = "模块ID不能为空！")]
		[Display(Name = "模块ID")]     
		public int Id { get; set; }
		/// <summary>
		/// 模块名称
		/// </summary>
		[MaxLength(50, ErrorMessage = "模块名称最大长度为50字符！")]
		[Required(ErrorMessage = "模块名称不能为空！")]
		[Display(Name = "模块名称")]     
		public string ModulName { get; set; }
		/// <summary>
		/// 访问控制器
		/// </summary>
		[MaxLength(50, ErrorMessage = "访问控制器最大长度为50字符！")]
		[Display(Name = "访问控制器")]     
		public string Controller { get; set; }
		/// <summary>
		/// 访问Action
		/// </summary>
		[MaxLength(50, ErrorMessage = "访问Action最大长度为50字符！")]
		[Display(Name = "访问Action")]     
		public string Action { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		[MaxLength(100, ErrorMessage = "描述最大长度为100字符！")]
		[Display(Name = "描述")]     
		public string Description { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		[Required(ErrorMessage = "创建时间不能为空！")]
		[Display(Name = "创建时间")]     
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 父级Id
		/// </summary>
		[Required(ErrorMessage = "父级Id不能为空！")]
		[Display(Name = "父级Id")]     
		public int PId { get; set; }
		/// <summary>
		/// 排序
		/// </summary>
		[Required(ErrorMessage = "排序不能为空！")]
		[Display(Name = "排序")]     
		public int OrderId { get; set; }
		/// <summary>
		/// 是否开启该模块
		/// </summary>
		[Required(ErrorMessage = "是否开启该模块不能为空！")]
		[Display(Name = "是否开启该模块")]     
		public bool IsShow { get; set; }
		/// <summary>
		/// 优先级
		/// </summary>
		[Required(ErrorMessage = "优先级不能为空！")]
		[Display(Name = "优先级")]     
		public int Priority { get; set; }
		/// <summary>
		/// 是否显示
		/// </summary>
		[Required(ErrorMessage = "是否显示不能为空！")]
		[Display(Name = "是否显示")]     
		public bool IsDisplay { get; set; }
		/// <summary>
		/// 菜单图标
		/// </summary>
		[MaxLength(100, ErrorMessage = "菜单图标最大长度为100字符！")]
		[Display(Name = "菜单图标")]     
		public string Ico { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
