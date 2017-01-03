using System;

namespace HGShare.Common.Attributes
{
    /// <summary>
    /// 枚举文本描述特性
    /// </summary>
    public class EnumDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public EnumDescriptionAttribute(string desc)
        {
            Description = desc;
        }
    }
}
