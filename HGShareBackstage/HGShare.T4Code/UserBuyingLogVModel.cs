using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// UserBuyingLog View Model
    /// </summary>
    public class UserBuyingLogVModel
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
		/// 花费积分
		/// </summary>
		[Required(ErrorMessage = "花费积分不能为空！")]
		[Display(Name = "花费积分")]     
		public int Score { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>
		[Required(ErrorMessage = "用户Id不能为空！")]
		[Display(Name = "用户Id")]     
		public int UserId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required(ErrorMessage = "不能为空！")]
		[Display(Name = "")]     
		public DateTime CreateTime { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
