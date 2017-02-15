using System.Threading.Tasks;
using HGShare.FileManager.Avatar;
using HGShare.Utils.Interface;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class Upload:IUpload
    {
        private readonly IFileCloud _fileCloud;
        public Upload(IFileCloud fileCloud)
        {
            _fileCloud = fileCloud;
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="imageBase64"></param>
        /// <returns></returns>
        public string UploadAvatar(string imageBase64)
        {
            var uploadAvatar = new UploadAvatar(imageBase64, _fileCloud);
            uploadAvatar.Save();

            return uploadAvatar.FileName;
        }
        /// <summary>
        /// 删除头像
        /// </summary>
        /// <param name="fileName"></param>
        public async Task DeleteAvatar(string fileName)
        {
            var uploadAvatar = new UploadAvatar(_fileCloud);
            await uploadAvatar.DeleteFileAndThumbnailsAsync(fileName);
        }
    }
}
