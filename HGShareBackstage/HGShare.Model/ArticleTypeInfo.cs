using System;

namespace HGShare.Model
{
    /// <summary>
    /// ArticleType 实体
    /// </summary>
    public class ArticleTypeInfo
    {

        /// <summary>
        /// 类型Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型父级
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string PinYin { get; set; }


        /// <summary>
        /// 父级类型名
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 是否是主页菜单
        /// </summary>
        public bool IsHomeMenu { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>        
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>        
        public string Ico { get; set; }
        /// <summary>
        /// 前台发布时可选
        /// </summary>        
        public bool IsShow { get; set; }
    }
}
