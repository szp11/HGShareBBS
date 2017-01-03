using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HGShare.VWModel
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class RoleVModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [MaxLength(200, ErrorMessage = "角色名最大长度为200字符！")]
        [Required(ErrorMessage = "角色名不能为空！")]
        [Display(Name = "角色名")]
        public string RName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500, ErrorMessage = "描述最大长度为500字符！")]
        [Display(Name = "描述")]
        public string Description { get; set; }

        /// <summary>
        /// 是否是超级角色
        /// </summary>
        [Required(ErrorMessage = "请选择是否为超级角色！")]
        [Display(Name = "是否是超级角色")]
        public bool IsSuper { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 发帖是否需要审核
        /// </summary>
        [Display(Name = "发帖是否需要审核")]
        public bool ArticleNeedVerified { get; set; }
        /// <summary>
        /// 评论是否需要审核
        /// </summary>
        [Display(Name = "评论是否需要审核")]
        public bool CommentNeedVerified { get; set; }

        public bool IsNull
        {
            get { return Id <= 0; }
        }
    }
}
