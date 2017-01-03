using System;
namespace HGShare.Model
{    
	/// <summary>
    /// Modul 实体
    /// </summary>
    public class ModulInfo
    {
		/// <summary>
		/// 模块ID
		/// </summary>        
		public int Id { get; set; }
		/// <summary>
		/// 模块名称
		/// </summary>        
		public string ModulName { get; set; }
		/// <summary>
		/// 访问控制器
		/// </summary>        
		public string Controller { get; set; }
		/// <summary>
		/// 访问Action
		/// </summary>        
		public string Action { get; set; }
		/// <summary>
		/// 描述
		/// </summary>        
		public string Description { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>        
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 父级Id
		/// </summary>        
		public int PId { get; set; }
		/// <summary>
		/// 排序
		/// </summary>        
		public int OrderId { get; set; }
		/// <summary>
		/// 是否开启该模块
		/// </summary>        
		public bool IsShow { get; set; }
		/// <summary>
		/// 优先级
		/// </summary>        
		public int Priority { get; set; }
		/// <summary>
		/// 是否显示
		/// </summary>        
		public bool IsDisplay { get; set; }
		/// <summary>
		/// 菜单图标
		/// </summary>        
		public string Ico { get; set; }
		 
    }
}
