using System;
namespace HGShare.Model
{
    /// <summary>
    /// EmailTemplate 实体
    /// </summary>
    public class EmailTemplateInfo
    {
        /// <summary>
        /// 
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>        
        public string Title { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>        
        public string Template { get; set; }
        /// <summary>
        /// 模板值的标识符 例如：($UserName;$UserId)
        /// </summary>        
        public string ValueIdentifier { get; set; }
        /// <summary>
        /// 模板说明
        /// </summary>        
        public string Explanation { get; set; }
        /// <summary>
        /// 是否是系统邮件模板
        /// </summary>        
        public bool IsSystem { get; set; }
        /// <summary>
        /// 是否是html
        /// </summary>        
        public bool IsHtml { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>        
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建用户Id
        /// </summary>        
        public int UserId { get; set; }
        /// <summary>
        /// 最后修改人Id
        /// </summary>        
        public int? LastEditUserId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>        
        public DateTime? LastEditTime { get; set; }

    }
}
