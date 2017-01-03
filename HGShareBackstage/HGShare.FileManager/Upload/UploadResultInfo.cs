using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.FileManager.Upload
{
    /// <summary>
    /// 文件上传返回信息
    /// </summary>
    public class UploadResultInfo
    {
        /// <summary>
        /// 文件数据的id
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 文图标件样式
        /// </summary>
        public string FileIco { get; set; }
        /// <summary>
        /// 文件信息
        /// </summary>
        public FileInfo FileInfo { get; set; }

    }
}
