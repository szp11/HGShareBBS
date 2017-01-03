using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace HGShare.VWModel
{
    public class ArticleVModel
    {
        /// <summary>
        /// 自增id
        /// </summary>
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
        [MaxLength(20000, ErrorMessage = "内容最大长度为20000字符！")]
        [Required(ErrorMessage = "内容不能为空！")]
        [Display(Name = "内容")]
        public string Content { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage = "类型不能为空！")]
        [Display(Name = "类型")]
        public int Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [Display(Name = "类型名称")]
        public string TypeName { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        [Display(Name = "评论数")]
        public int CommentNum { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        [Display(Name = "浏览量")]
        public int Dot { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Display(Name = "添加时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public int UserId { get; set; }
        /// <summary>
        /// 用户昵称或用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 图片数量
        /// </summary>
        [Display(Name = "图片数量")]
        public int ImgNum { get; set; }
        /// <summary>
        /// 附件数量
        /// </summary>
        [Display(Name = "附件数量")]
        public int AttachmentNum { get; set; }
        /// <summary>
        /// 最后修改用户Id
        /// </summary>
        [Display(Name = "最后修改用户Id")]
        public int LastEditUserId { get; set; }
        /// <summary>
        /// 最后修改用户昵称或用户名
        /// </summary>
        [Display(Name = "最后修改用户昵称")]
        public string LastEditUserName { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime LastEditTime { get; set; }
       
        
        /// <summary>
        /// 文章唯一标示
        /// </summary>
        [Display(Name = "文章唯一标示")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        [Display(Name = "是否置顶")]
        public bool IsStick { get; set; }
        /// <summary>
        /// 是否加精
        /// </summary>
        [Display(Name = "是否加精")]
        public bool IsJiaJing { get; set; }
       
        
        /// <summary>
        /// 是否删除
        /// </summary>
        [Display(Name = "是否删除")]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 审核状态 0待审核 1审核通过 2审核未通过
        /// </summary>
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
        [Display(Name = "文章类型 :  1 普通文章 ,2 问答")]
        public short BType { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        [Display(Name = "点赞数")]
        public int DianZanNum { get; set; }
        /// <summary>
        /// 浏览消费积分
        /// </summary>
        [Display(Name = "浏览消费积分")]
        public int Score { get; set; }
        /// <summary>
        /// 是否关闭评论
        /// </summary>
        [Display(Name = "是否关闭评论")]
        public bool IsCloseComment { get; set; }
        /// <summary>
        /// 关闭评论原因
        /// </summary>
        [MaxLength(200, ErrorMessage = "关闭评论原因最大长度为200字符！")]
        [Display(Name = "关闭评论原因")]
        public string CloseCommentReason { get; set; }

        [Boolean(Ignore = true)]
        public bool IsNull {
            get { return Id <= 0; }
        }
    }
}
