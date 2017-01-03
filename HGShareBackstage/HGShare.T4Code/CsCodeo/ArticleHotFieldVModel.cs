using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// ArticleHotField View Model
    /// </summary>
    public class ArticleHotFieldVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public long Id { get; set; }
		/// <summary>
		/// 文章Id
		/// </summary>
		[Required(ErrorMessage = "文章Id不能为空！")]
		[Display(Name = "文章Id")]     
		public long AId { get; set; }
		/// <summary>
		/// 实时点击量
		/// </summary>
		[Required(ErrorMessage = "实时点击量不能为空！")]
		[Display(Name = "实时点击量")]     
		public long Dot { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public DateTime UpdateTime { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
