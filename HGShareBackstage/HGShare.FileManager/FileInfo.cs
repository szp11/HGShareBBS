using HGShare.Enums;

namespace HGShare.FileManager
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// 文件新生成的名字（保存时的名字）
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件原名（上传时的名字）
        /// </summary>
        public string FileTitle { get; set; }
        /// <summary>
        /// 文件后缀名
        /// </summary>
        public string FileEx { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }

        #region 图片属性
        /// <summary>
        /// 图片宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 图片高
        /// </summary>
        public int Height { get; set; }
        #endregion

    }
}
