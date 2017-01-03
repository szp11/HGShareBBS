using System;
namespace HGShare.Model
{    
	/// <summary>
    /// UserAccessLog 实体
    /// </summary>
    public class UserAccessLogInfo
    {
		/// <summary>
		/// 编号
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 访问地址
		/// </summary>        
		public string Url { get; set; }
		/// <summary>
		/// 来源地址
		/// </summary>        
		public string Referer { get; set; }
		/// <summary>
		/// UA
		/// </summary>        
		public string UserAgent { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>        
		public int UserId { get; set; }
		/// <summary>
		/// Ip
		/// </summary>        
		public string Ip { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>        
		public DateTime InsertTime { get; set; }
		/// <summary>
		/// 其它信息
		/// </summary>        
		public string Other { get; set; }
		/// <summary>
		/// 类型 0后台记录 1js记录
		/// </summary>        
		public short Type { get; set; }
		 
    }
}
