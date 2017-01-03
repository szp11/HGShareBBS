using System;
namespace HGShare.Model
{    
	/// <summary>
    /// AdminLog 实体
    /// </summary>
    public class AdminLogInfo
    {
		/// <summary>
		/// 记录编号
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 用户ID
		/// </summary>        
		public long UserId { get; set; }
		/// <summary>
		/// 访问控制器
		/// </summary>        
		public string Controllers { get; set; }
		/// <summary>
		/// 访问Action
		/// </summary>        
		public string Action { get; set; }
		/// <summary>
		/// 请求参数
		/// </summary>        
		public string Parameter { get; set; }
		/// <summary>
		/// 请求中主要ID
		/// </summary>        
		public string ActionId { get; set; }
		/// <summary>
		/// IP
		/// </summary>        
		public string Ip { get; set; }
		/// <summary>
		/// Url
		/// </summary>        
		public string Url { get; set; }
		/// <summary>
		/// 记录时间
		/// </summary>        
		public DateTime InTime { get; set; }
		/// <summary>
		/// 请求方法 get/post....
		/// </summary>        
		public string Method { get; set; }
		/// <summary>
		/// 是否是Ajax访问
		/// </summary>        
		public bool IsAjax { get; set; }
		/// <summary>
		/// UserAgent
		/// </summary>        
		public string UserAgent { get; set; }
		/// <summary>
		/// 控制器描述
		/// </summary>        
		public string ControllersDsc { get; set; }
		/// <summary>
		/// Action描述
		/// </summary>        
		public string ActionDsc { get; set; }
		 
    }
}
