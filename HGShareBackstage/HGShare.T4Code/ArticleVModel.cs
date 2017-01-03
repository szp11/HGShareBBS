using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{    
	/// <summary>
    /// Article View Model
    /// </summary>
    public class ArticleVModel
    {
		/// <summary>
		/// 自增id
		/// </summary>
		[Required(ErrorMessage = "自增id不能为空！")]
		[Display(Name = "自增id")]     
		public long Id { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		[MaxLength(200, ErrorMessage = "标题最大长度为200字符！")]
		[Required(ErrorMessage = "标题不能为空！")]
		[Display(Name = "标题")]     
		public string Title { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		[MaxLength(16, ErrorMessage = "内容最大长度为16字符！")]
		[Required(ErrorMessage = "内容不能为空！")]
		[Display(Name = "内容")]     
		public string Content { get; set; }
		/// <summary>
		/// 类型(自定义类型)
		/// </summary>
		[Required(ErrorMessage = "类型(自定义类型)不能为空！")]
		[Display(Name = "类型(自定义类型)")]     
		public int Type { get; set; }
		/// <summary>
		/// 评论数
		/// </summary>
		[Required(ErrorMessage = "评论数不能为空！")]
		[Display(Name = "评论数")]     
		public int CommentNum { get; set; }
		/// <summary>
		/// 浏览量
		/// </summary>
		[Required(ErrorMessage = "浏览量不能为空！")]
		[Display(Name = "浏览量")]     
		public int Dot { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>
		[Required(ErrorMessage = "添加时间不能为空！")]
		[Display(Name = "添加时间")]     
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>
		[Required(ErrorMessage = "用户Id不能为空！")]
		[Display(Name = "用户Id")]     
		public int UserId { get; set; }
		/// <summary>
		/// 图片数量
		/// </summary>
		[Required(ErrorMessage = "图片数量不能为空！")]
		[Display(Name = "图片数量")]     
		public int ImgNum { get; set; }
		/// <summary>
		/// 附件数量
		/// </summary>
		[Required(ErrorMessage = "附件数量不能为空！")]
		[Display(Name = "附件数量")]     
		public int AttachmentNum { get; set; }
		/// <summary>
		/// 最后修改用户Id
		/// </summary>
		[Required(ErrorMessage = "最后修改用户Id不能为空！")]
		[Display(Name = "最后修改用户Id")]     
		public int LastEditUserId { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		[Required(ErrorMessage = "最后修改时间不能为空！")]
		[Display(Name = "最后修改时间")]     
		public DateTime LastEditTime { get; set; }
		/// <summary>
		/// 文章唯一标示
		/// </summary>
		[Required(ErrorMessage = "文章唯一标示不能为空！")]
		[Display(Name = "文章唯一标示")]     
		public Guid Guid { get; set; }
		/// <summary>
		/// 是否删除
		/// </summary>
		[Required(ErrorMessage = "是否删除不能为空！")]
		[Display(Name = "是否删除")]     
		public bool IsDelete { get; set; }
		/// <summary>
		/// 审核状态 0待审核 1审核通过 2审核未通过
		/// </summary>
		[Required(ErrorMessage = "审核状态 0待审核 1审核通过 2审核未通过不能为空！")]
		[Display(Name = "审核状态 0待审核 1审核通过 2审核未通过")]     
		public short State { get; set; }
		/// <summary>
		/// 拒绝通过原因
		/// </summary>
		[MaxLength(200, ErrorMessage = "拒绝通过原因最大长度为200字符！")]
		[Display(Name = "拒绝通过原因")]     
		public string RefuseReason { get; set; }
		/// <summary>
		/// 文章类型 :  1 普通文章 ,2 问答
		/// </summary>
		[Required(ErrorMessage = "文章类型 :  1 普通文章 ,2 问答不能为空！")]
		[Display(Name = "文章类型 :  1 普通文章 ,2 问答")]     
		public short BType { get; set; }
		/// <summary>
		/// 点赞数
		/// </summary>
		[Required(ErrorMessage = "点赞数不能为空！")]
		[Display(Name = "点赞数")]     
		public int DianZanNum { get; set; }
		/// <summary>
		/// 浏览消费积分
		/// </summary>
		[Required(ErrorMessage = "浏览消费积分不能为空！")]
		[Display(Name = "浏览消费积分")]     
		public int Score { get; set; }
		/// <summary>
		/// 是否置顶
		/// </summary>
		[Required(ErrorMessage = "是否置顶不能为空！")]
		[Display(Name = "是否置顶")]     
		public bool IsStick { get; set; }
		/// <summary>
		/// 是否加精
		/// </summary>
		[Required(ErrorMessage = "是否加精不能为空！")]
		[Display(Name = "是否加精")]     
		public bool IsJiaJing { get; set; }
		/// <summary>
		/// 是否关闭评论
		/// </summary>
		[Required(ErrorMessage = "是否关闭评论不能为空！")]
		[Display(Name = "是否关闭评论")]     
		public bool IsCloseComment { get; set; }
		/// <summary>
		/// 关闭评论原因
		/// </summary>
		[MaxLength(200, ErrorMessage = "关闭评论原因最大长度为200字符！")]
		[Display(Name = "关闭评论原因")]     
		public string CloseCommentReason { get; set; }
		 
		public bool IsNull { get
		{
		return 
		} }
    }
}
