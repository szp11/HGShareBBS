using HGShare.Common.Attributes;

namespace HGShare.Enums
{
    /// <summary>
    /// 提示消息类型
    /// </summary>
    public enum TipsType
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        [EnumDescription("错误提示")]
        Error=0,
        /// <summary>
        /// 警告提示
        /// </summary>
        [EnumDescription("警告提示")]
        Wran=1,
        /// <summary>
        /// 信息提示
        /// </summary>
        [EnumDescription("信息提示")]
        Info=3
    }
}
