using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HGShare.VWModel
{
    public class ModulVModel
    {
        /// <summary>
        /// 模块ID
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>  
        [MaxLength(15, ErrorMessage = "模块名称最大长度为15字符！")]
        [Required(ErrorMessage = "模块名称不能为空！")]
        [Display(Name = "模块名称")]
        public string ModulName { get; set; }
        /// <summary>
        /// 访问控制器
        /// </summary> 
        [MaxLength(30, ErrorMessage = "控制器最大长度为30字符！")]
        [Display(Name = "控制器")]       
        public string Controller { get; set; }
        /// <summary>
        /// 访问Action
        /// </summary>  
        [MaxLength(30, ErrorMessage = "Action最大长度为30字符！")]
        [Display(Name = "Action")]        
        public string Action { get; set; }
        /// <summary>
        /// 描述
        /// </summary>    
        [MaxLength(100, ErrorMessage = "描述最大长度为100字符！")]
        [Display(Name = "描述")]     
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>        
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>   
        [Required(ErrorMessage = "父级模块为必选项！")]
        [Display(Name = "父级模块")]       
        public int PId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>   
        [Display(Name = "排序")]     
        public int OrderId { get; set; }
        /// <summary>
        /// 是否开启该模块
        /// </summary>  
        [Required(ErrorMessage = "是否开启该模块为必选项！")]
        [Display(Name = "是否开启该模块")]       
        public bool IsShow { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>   
        [Display(Name = "优先级")]       
        public int Priority { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        [Required(ErrorMessage = "是否显示该模块为必选项！")]
        [Display(Name = "是否显示模块")]  
        public bool IsDisplay { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary> 
        [MaxLength(100, ErrorMessage = "描述最大长度为20字符！")]
        [Display(Name = "菜单图标")]       
        public string Ico { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public string PName { get; set; }

    }
}
