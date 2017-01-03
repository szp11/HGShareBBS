using System;
namespace HGShare.Model
{    
	/// <summary>
    /// ArticleHotField 实体
    /// </summary>
    public class ArticleHotFieldInfo
    {
		/// <summary>
		/// 
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 文章Id
		/// </summary>        
		public long AId { get; set; }
		/// <summary>
		/// 实时点击量
		/// </summary>        
		public long Dot { get; set; }
		/// <summary>
		/// 
		/// </summary>        
		public DateTime UpdateTime { get; set; }
		 
    }
}
