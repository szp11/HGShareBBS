using System;

namespace HGShare.Model
{
    /// <summary>
    /// Article 实体
    /// </summary>
    public class ArticleInfo
    {

        /// <summary>
        /// 自增id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentNum { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int Dot { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 图片数量
        /// </summary>
        public int ImgNum { get; set; }

        /// <summary>
        /// 附件数量
        /// </summary>
        public int AttachmentNum { get; set; }

        /// <summary>
        /// 最后修改用户Id
        /// </summary>
        public int LastEditUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
        /// <summary>
        /// 文章唯一标示
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsStick { get; set; }
        /// <summary>
        /// 是否加精
        /// </summary>
        public bool IsJiaJing { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>        
        public bool IsDelete { get; set; }
        /// <summary>
        /// 审核状态 0待审核 1审核通过 2审核未通过
        /// </summary>        
        public short State { get; set; }
        /// <summary>
        /// 拒绝通过原因
        /// </summary>        
        public string RefuseReason { get; set; }
        /// <summary>
        /// 文章类型 :  1 普通文章 ,2 问答
        /// </summary>        
        public short BType { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>        
        public int DianZanNum { get; set; }
        /// <summary>
        /// 浏览消费积分
        /// </summary>        
        public int Score { get; set; }
        /// <summary>
        /// 是否关闭评论
        /// </summary>        
        public bool IsCloseComment { get; set; }
        /// <summary>
        /// 关闭评论原因
        /// </summary>        
        public string CloseCommentReason { get; set; }
    }
}
