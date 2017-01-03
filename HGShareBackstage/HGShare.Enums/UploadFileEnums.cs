using HGShare.Common.Attributes;

namespace HGShare.Enums
{
    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [EnumDescription("图片")]
        Image = 0,
        /// <summary>
        /// 压缩文件
        /// </summary>
        [EnumDescription("压缩文件")]
        Rar = 1,
        /// <summary>
        /// 文档
        /// </summary>
        [EnumDescription("文档")]
        Document = 2,
        /// <summary>
        /// 可执行程序
        /// </summary>
        [EnumDescription("可执行程序")]
        Exe = 3,
        /// <summary>
        /// 其它
        /// </summary>
        [EnumDescription("其它")]
        Other = 4

    }
    /// <summary>
    /// 上传来源
    /// </summary>
    public enum UploadScore
    {
        /// <summary>
        /// 系统后台
        /// </summary>
        [EnumDescription("系统后台")]
        System=0,
        /// <summary>
        /// 用户上传
        /// </summary>
        [EnumDescription("用户上传")]
        User = 1,
        /// <summary>
        /// 其它未知
        /// </summary>
        [EnumDescription("其它未知")]
        Other = 1
    }
    /// <summary>
    /// 附件审核状态
    /// </summary>
    public enum AttachmentState
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        [EnumDescription("初始状态")]
        Init=0,
        /// <summary>
        /// 审核通过
        /// </summary>
        [EnumDescription("审核通过")]
        Ok=1,
        /// <summary>
        /// 审核未通过
        /// </summary>
        [EnumDescription("审核未通过")]
        No=2
    }
}
