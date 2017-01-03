using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.Com.Interface
{
    /// <summary>
    /// 文件云
    /// </summary>
    public interface IFileCloud
    {
        /// <summary>
        /// 保存本地文件到云
        /// </summary>
        /// <param name="localFilePath">本地文件地址</param>
        /// <param name="fileName">文件名</param>
        /// <param name="bucketName">目标空间名</param>
        void SaveFile(string localFilePath, string fileName, string bucketName);
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="bucketName">空间名</param>
        void DeleteFile(string fileName, string bucketName);
    }
}
