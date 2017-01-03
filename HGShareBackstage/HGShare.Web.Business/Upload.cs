using System.Threading.Tasks;
using HGShare.FileManager.Avatar;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class Upload:IUpload
    {
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="imageBase64"></param>
        /// <returns></returns>
        public string UploadAvatar(string imageBase64)
        {
            var uploadAvatar = new UploadAvatar(imageBase64);
            uploadAvatar.Save();

            return uploadAvatar.FileName;
        }
        /// <summary>
        /// 删除头像
        /// </summary>
        /// <param name="fileName"></param>
        public async Task DeleteAvatar(string fileName)
        {
            var uploadAvatar = new UploadAvatar();
            await uploadAvatar.DeleteFileAndThumbnailsAsync(fileName);
        }
    }
}
