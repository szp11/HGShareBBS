using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// UserScoreLog View Model
    /// </summary>
    public class UserScoreLogVModel
    {
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public long Id { get; set; }
		/// <summary>
		/// 用户id
		/// </summary>
		[Required(ErrorMessage = "用户id不能为空！")]
		[Display(Name = "用户id")]     
		public int UserId { get; set; }
		/// <summary>
		/// 增减值
		/// </summary>
		[Required(ErrorMessage = "增减值不能为空！")]
		[Display(Name = "增减值")]     
		public int Score { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>
		[Required(ErrorMessage = "添加时间不能为空！")]
		[Display(Name = "添加时间")]     
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		[MaxLength(200, ErrorMessage = "描述最大长度为200字符！")]
		[Display(Name = "描述")]     
		public string Describe { get; set; }
		/// <summary>
		/// 增减前分值
		/// </summary>
		[Required(ErrorMessage = "增减前分值不能为空！")]
		[Display(Name = "增减前分值")]     
		public long OldScore { get; set; }
		/// <summary>
		/// 增减后分值
		/// </summary>
		[Required(ErrorMessage = "增减后分值不能为空！")]
		[Display(Name = "增减后分值")]     
		public long NewScore { get; set; }
		 
		/// <summary>
		/// 根据主键是否是默认值判断是不是空对象Id==0
		/// </summary>
		public bool IsNull 
		{ 
			get{return Id==0;}
		}
    }
}
