using System;
using System.ComponentModel.DataAnnotations;
namespace HGShare.VWModel
{
    /// <summary>
    /// Comment View Model
    /// </summary>
    public class CommentVModel
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
        /// 评论用户Id
        /// </summary>
        [Required(ErrorMessage = "评论用户Id不能为空！")]
        [Display(Name = "评论用户Id")]
        public int UserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "创建时间不能为空！")]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        [MaxLength(500, ErrorMessage = "评论内容最大长度为500字符！")]
        [Required(ErrorMessage = "评论内容不能为空！")]
        [Display(Name = "评论内容")]
        public string Content { get; set; }
        /// <summary>
        /// 评论者Ip
        /// </summary>
        [MaxLength(20, ErrorMessage = "评论者Ip最大长度为20字符！")]
        [Required(ErrorMessage = "评论者Ip不能为空！")]
        [Display(Name = "评论者Ip")]
        public string IP { get; set; }
        /// <summary>
        /// 用户UA信息
        /// </summary>
        [MaxLength(500, ErrorMessage = "用户UA信息最大长度为500字符！")]
        [Required(ErrorMessage = "用户UA信息不能为空！")]
        [Display(Name = "用户UA信息")]
        public string UserAgent { get; set; }
        /// <summary>
        /// 审核状态0待审核 1已通过 2未通过
        /// </summary>
        [Required(ErrorMessage = "审核状态0待审核 1已通过 2未通过不能为空！")]
        [Display(Name = "审核状态0待审核 1已通过 2未通过")]
        public short State { get; set; }
        /// <summary>
        /// 拒绝原因
        /// </summary>
        [MaxLength(200, ErrorMessage = "拒绝原因最大长度为200字符！")]
        [Display(Name = "拒绝原因")]
        public string RefuseReason { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Required(ErrorMessage = "是否删除不能为空！")]
        [Display(Name = "是否删除")]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 最后修改用户 0默认
        /// </summary>
        [Required(ErrorMessage = "最后修改用户 0默认不能为空！")]
        [Display(Name = "最后修改用户 0默认")]
        public int LastEditUserId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Required(ErrorMessage = "最后修改时间不能为空！")]
        [Display(Name = "最后修改时间")]
        public DateTime LastEditTime { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        [Required(ErrorMessage = "是否置顶不能为空！")]
        [Display(Name = "是否置顶")]
        public bool IsStick { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        [Required(ErrorMessage = "点赞数不能为空！")]
        [Display(Name = "点赞数")]
        public int DianZanNum { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string ArticleTitle { get; set; }
        /// <summary>
        /// 评论人昵称
        /// </summary>
        public string NickName { get; set; }

    }
}
