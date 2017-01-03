using System;
namespace HGShare.Model
{    
	/// <summary>
    /// Attachment 实体
    /// </summary>
    public class AttachmentInfo
    {
		/// <summary>
		/// 附件Id
		/// </summary>        
		public long Id { get; set; }
		/// <summary>
		/// 文件存储名
		/// </summary>        
		public string FileName { get; set; }
		/// <summary>
		/// 文件原名
		/// </summary>        
		public string FileTitle { get; set; }
		/// <summary>
		/// 描述
		/// </summary>        
		public string Description { get; set; }
		/// <summary>
		/// 文件类型
		/// </summary>        
		public string Type { get; set; }
		/// <summary>
		/// img 宽
		/// </summary>        
		public int Width { get; set; }
		/// <summary>
		/// img 高
		/// </summary>        
		public int Height { get; set; }
		/// <summary>
		/// 文件大小
		/// </summary>        
		public long FileSize { get; set; }
		/// <summary>
		/// 是否显示0 不显示 1显示
		/// </summary>        
		public bool IsShow { get; set; }
		/// <summary>
		/// 所属文章Id
		/// </summary>        
		public long AId { get; set; }
		/// <summary>
		/// 文件来源（用于记录程序中各个上传口）
		/// </summary>        
		public int Score { get; set; }
		/// <summary>
		/// 状态 0 初始状态 1已审核通过 2审核未通过（用于文件审核）
		/// </summary>        
		public int State { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>        
		public int UserId { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>        
		public DateTime InTime { get; set; }
		/// <summary>
		/// 0附件 1图片
		/// </summary>        
		public int BType { get; set; }
		/// <summary>
		/// 文件存储绝对路径（暂时无用）
		/// </summary>        
		public string LocalPath { get; set; }
		/// <summary>
		/// 文件相对目录
		/// </summary>        
		public string VirtualPath { get; set; }
		/// <summary>
		/// 上传唯一标示(上传页面初始化后产生)
		/// </summary>        
		public Guid? Guid { get; set; }
		 
    }
}
