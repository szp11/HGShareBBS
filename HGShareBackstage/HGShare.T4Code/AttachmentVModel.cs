using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// Attachment View Model
    /// </summary>
    public class AttachmentVModel
    {
		/// <summary>
		/// 附件Id
		/// </summary>
		[Required(ErrorMessage = "附件Id不能为空！")]
		[Display(Name = "附件Id")]     
		public long Id { get; set; }
		/// <summary>
		/// 文件存储名
		/// </summary>
		[MaxLength(50, ErrorMessage = "文件存储名最大长度为50字符！")]
		[Required(ErrorMessage = "文件存储名不能为空！")]
		[Display(Name = "文件存储名")]     
		public string FileName { get; set; }
		/// <summary>
		/// 文件原名
		/// </summary>
		[MaxLength(50, ErrorMessage = "文件原名最大长度为50字符！")]
		[Required(ErrorMessage = "文件原名不能为空！")]
		[Display(Name = "文件原名")]     
		public string FileTitle { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		[MaxLength(500, ErrorMessage = "描述最大长度为500字符！")]
		[Display(Name = "描述")]     
		public string Description { get; set; }
		/// <summary>
		/// 文件类型
		/// </summary>
		[MaxLength(10, ErrorMessage = "文件类型最大长度为10字符！")]
		[Required(ErrorMessage = "文件类型不能为空！")]
		[Display(Name = "文件类型")]     
		public string Type { get; set; }
		/// <summary>
		/// img 宽
		/// </summary>
		[Required(ErrorMessage = "img 宽不能为空！")]
		[Display(Name = "img 宽")]     
		public int Width { get; set; }
		/// <summary>
		/// img 高
		/// </summary>
		[Required(ErrorMessage = "img 高不能为空！")]
		[Display(Name = "img 高")]     
		public int Height { get; set; }
		/// <summary>
		/// 文件大小
		/// </summary>
		[Required(ErrorMessage = "文件大小不能为空！")]
		[Display(Name = "文件大小")]     
		public long FileSize { get; set; }
		/// <summary>
		/// 是否显示0 不显示 1显示
		/// </summary>
		[Required(ErrorMessage = "是否显示0 不显示 1显示不能为空！")]
		[Display(Name = "是否显示0 不显示 1显示")]     
		public bool IsShow { get; set; }
		/// <summary>
		/// 所属文章Id
		/// </summary>
		[Required(ErrorMessage = "所属文章Id不能为空！")]
		[Display(Name = "所属文章Id")]     
		public long AId { get; set; }
		/// <summary>
		/// 文件来源（用于记录程序中各个上传口）
		/// </summary>
		[Required(ErrorMessage = "文件来源（用于记录程序中各个上传口）不能为空！")]
		[Display(Name = "文件来源（用于记录程序中各个上传口）")]     
		public int Score { get; set; }
		/// <summary>
		/// 状态 0 初始状态 1已审核通过 2审核未通过（用于文件审核）
		/// </summary>
		[Required(ErrorMessage = "状态 0 初始状态 1已审核通过 2审核未通过（用于文件审核）不能为空！")]
		[Display(Name = "状态 0 初始状态 1已审核通过 2审核未通过（用于文件审核）")]     
		public int State { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>
		[Required(ErrorMessage = "用户Id不能为空！")]
		[Display(Name = "用户Id")]     
		public int UserId { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>
		[Required(ErrorMessage = "添加时间不能为空！")]
		[Display(Name = "添加时间")]     
		public DateTime InTime { get; set; }
		/// <summary>
		/// 0附件 1图片
		/// </summary>
		[Required(ErrorMessage = "0附件 1图片不能为空！")]
		[Display(Name = "0附件 1图片")]     
		public int BType { get; set; }
		/// <summary>
		/// 文件存储绝对路径（暂时无用）
		/// </summary>
		[MaxLength(200, ErrorMessage = "文件存储绝对路径（暂时无用）最大长度为200字符！")]
		[Display(Name = "文件存储绝对路径（暂时无用）")]     
		public string LocalPath { get; set; }
		/// <summary>
		/// 文件相对目录
		/// </summary>
		[MaxLength(200, ErrorMessage = "文件相对目录最大长度为200字符！")]
		[Display(Name = "文件相对目录")]     
		public string VirtualPath { get; set; }
		/// <summary>
		/// 上传唯一标示(上传页面初始化后产生)
		/// </summary>
		[Display(Name = "上传唯一标示(上传页面初始化后产生)")]     
		public Guid? Guid { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
