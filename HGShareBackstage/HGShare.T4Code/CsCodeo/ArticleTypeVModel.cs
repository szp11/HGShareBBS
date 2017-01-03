using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// ArticleType View Model
    /// </summary>
    public class ArticleTypeVModel
    {
		/// <summary>
		/// 类型Id
		/// </summary>
		[Required(ErrorMessage = "类型Id不能为空！")]
		[Display(Name = "类型Id")]     
		public int Id { get; set; }
		/// <summary>
		/// 类型名
		/// </summary>
		[MaxLength(100, ErrorMessage = "类型名最大长度为100字符！")]
		[Required(ErrorMessage = "类型名不能为空！")]
		[Display(Name = "类型名")]     
		public string Name { get; set; }
		/// <summary>
		/// 类型父级
		/// </summary>
		[Required(ErrorMessage = "类型父级不能为空！")]
		[Display(Name = "类型父级")]     
		public int PId { get; set; }
		/// <summary>
		/// 排序
		/// </summary>
		[Required(ErrorMessage = "排序不能为空！")]
		[Display(Name = "排序")]     
		public int Sort { get; set; }
		/// <summary>
		/// 拼音
		/// </summary>
		[MaxLength(200, ErrorMessage = "拼音最大长度为200字符！")]
		[Required(ErrorMessage = "拼音不能为空！")]
		[Display(Name = "拼音")]     
		public string PinYin { get; set; }
		/// <summary>
		/// 是否是主页菜单
		/// </summary>
		[Required(ErrorMessage = "是否是主页菜单不能为空！")]
		[Display(Name = "是否是主页菜单")]     
		public bool IsHomeMenu { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>
		[Required(ErrorMessage = "添加时间不能为空！")]
		[Display(Name = "添加时间")]     
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 前台图标
		/// </summary>
		[MaxLength(100, ErrorMessage = "前台图标最大长度为100字符！")]
		[Display(Name = "前台图标")]     
		public string Ico { get; set; }
		/// <summary>
		/// 前台发布时可选
		/// </summary>
		[Required(ErrorMessage = "前台发布时可选不能为空！")]
		[Display(Name = "前台发布时可选")]     
		public bool IsShow { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
