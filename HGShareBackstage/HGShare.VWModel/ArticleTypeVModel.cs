using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.VWModel
{
    public class ArticleTypeVModel
    {
        /// <summary>
        /// 类型Id
        /// </summary>
        [Display(Name = "类型Id")]
        public int Id { get; set; }
        /// <summary>
        /// 类型名
        /// </summary>
        [MaxLength(100, ErrorMessage = "类型名最大长度为100字符！")]
        [Required(ErrorMessage = "类型名不能为空！")]
        [Display(Name = "类型名")]
        public string Name { get; set; }
        /// <summary>
        /// 类型父级
        /// </summary>
        [Required(ErrorMessage = "类型父级不能为空！")]
        [Display(Name = "类型父级")]
        public int PId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空！")]
        [Display(Name = "排序")]
        public int Sort { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        [Display(Name = "拼音")]
        [Required(ErrorMessage = "拼音不能为空！")]
        public string PinYin { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 是否是主页菜单
        /// </summary>
        [Required(ErrorMessage = "是否是主页菜单不能为空！")]
        [Display(Name = "是否是主页菜单")]
        public bool IsHomeMenu { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Display(Name = "添加时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100, ErrorMessage = "最大长度为100字符！")]
        [Display(Name = "前台图标")]
        public string Ico { get; set; }
        /// <summary>
        /// 前台发布时可选
        /// </summary>
        [Required(ErrorMessage = "前台发布时可选不能为空！")]
        [Display(Name = "前台发布时可选")]
        public bool IsShow { get; set; }
    }
}
