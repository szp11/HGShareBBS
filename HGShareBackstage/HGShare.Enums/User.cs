using HGShare.Common.Attributes;

namespace HGShare.Enums
{
    /// <summary>
    /// 用户性别
    /// </summary>
    public enum UserSex
    {
        [EnumDescription("未知")]
        None=0,
        [EnumDescription("男")]
        Male=1,
        [EnumDescription("女")]
        Female=2
    }
}
