using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 上传
    /// </summary>
    public interface IUpload
    {
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="imageBase64">base64格式图片数据</param>
        /// <returns>文件名称</returns>
        string UploadAvatar(string imageBase64);
        /// <summary>
        /// 删除头像
        /// </summary>
        /// <param name="fileName"></param>
        Task DeleteAvatar(string fileName);
    }
}
