using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Common.Attributes;

namespace HGShare.Enums
{
    /// <summary>
    /// 文章类型
    /// </summary>
    public enum BType
    {
        /// <summary>
        /// 普通文章
        /// </summary>
        [EnumDescription("普通文章")]
        Normal = 1,
        /// <summary>
        /// 问答
        /// </summary>
        [EnumDescription("问答")]
        Ask=2
    }


}
