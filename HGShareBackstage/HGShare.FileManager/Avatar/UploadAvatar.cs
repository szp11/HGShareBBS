using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using HGShare.Com.Interface;
using HGShare.Site.Config;

namespace HGShare.FileManager.Avatar
{
    /// <summary>
    /// 头像上传
    /// </summary>
    public class UploadAvatar
    {
        private static  IFileCloud _fileCloud ;

        private readonly string _savePath;
        public readonly string FileName;
        private readonly string _imageBase64;
        private readonly int [] _thumbnail;

        public UploadAvatar()
        {
            _savePath = FileTools.GetFilePath(DirectoriesConfig.UserAvatarPath);
            FileName = string.Format("{0}.{1}", Guid.NewGuid().ToString().Replace("-", ""), WebSysConfig.AvatarFormat);
            _thumbnail = WebSysConfig.AvatarThumbnailSizes;
            _fileCloud = FileCloudService.GetFileCloudService();
        }

        public UploadAvatar(string base64):this()
        {
            _imageBase64 = base64;
        }

        public void Save()
        {
            if (!Directory.Exists(_savePath))
                Directory.CreateDirectory(_savePath);
            Image image= Common.ImageTools.Base64ToImage(_imageBase64);
            //保存原图
            image.Save(Path.Combine(_savePath, FileName));
            //缩略图
            if (_thumbnail != null && _thumbnail.Length > 0)
            {
                foreach (int size in _thumbnail)
                {
                    Bitmap bitmap=  Common.ImageTools.GetThumbnail(image, size, size);
                    string thFileName = string.Format("{0}.{1}x{2}.{3}", FileName, size, size, WebSysConfig.AvatarFormat);
                    bitmap.Save(Path.Combine(_savePath,thFileName));
                    bitmap.Dispose();
                    InsertFileCloud(Path.Combine(_savePath, thFileName), thFileName);
                }
            }
            image.Dispose();
            InsertFileCloud(Path.Combine(_savePath, FileName), FileName);
        }
        /// <summary>
        /// 删除原图片和缩略图
        /// </summary>
        /// <param name="fileName"></param>
        public async Task DeleteFileAndThumbnailsAsync(string fileName)
        {
            //原图
            string su = Path.Combine(_savePath, fileName);

           await Task.Run(() =>
            {
                if (File.Exists(su))
                {
                    File.Delete(su);
                    DeleteFileCloud(fileName);
                }
                //缩略图
                if (_thumbnail != null && _thumbnail.Length > 0)
                {
                    foreach (int size in _thumbnail)
                    {
                        string thFileName = string.Format("{0}.{1}x{2}.{3}", fileName, size, size, WebSysConfig.AvatarFormat);
                        if (File.Exists(Path.Combine(_savePath, thFileName)))
                        {
                            File.Delete(Path.Combine(_savePath, thFileName));
                            DeleteFileCloud(thFileName);
                        }

                    }
                }
            });
        }

        /// <summary>
        /// 写入云空间
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        private void InsertFileCloud(string path,string name)
        {
            if (WebSysConfig.AvatarIsSyncCloud)
            {
                
                _fileCloud.SaveFile(path, name, WebSysConfig.AvatarBucketName);
            }
        }
        /// <summary>
        /// 删除云服务上文件
        /// </summary>
        /// <param name="fileName"></param>
        private async void DeleteFileCloud(string fileName)
        {
           await Task.Run(() =>
            {
                if (WebSysConfig.AvatarIsSyncCloud)
                {
                    _fileCloud.DeleteFile(fileName, WebSysConfig.AvatarBucketName);

                }
            });
            
        }
    }
}
