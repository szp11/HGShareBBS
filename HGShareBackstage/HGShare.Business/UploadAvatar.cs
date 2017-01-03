using System;
using System.Drawing;
using System.IO;
using System.Web;
using HGShare.Com.Interface;
using HGShare.Site.Config;

namespace HGShare.Business
{
    /// <summary>
    /// 头像上传
    /// </summary>
    public class UploadAvatar
    {
        private readonly string _savePath;
        public readonly string FileName;
        private readonly string _imageBase64;
        private readonly int [] _thumbnail;

        public UploadAvatar(string base64)
        {
            _savePath = HttpContext.Current.Server.MapPath(DirectoriesConfig.UserAvatarPath);
            FileName = string.Format("{0}.{1}", Guid.NewGuid().ToString().Replace("-", ""), WebSysConfig.AvatarFormat);
            _imageBase64 = base64;
            _thumbnail = WebSysConfig.AvatarThumbnailSizes;
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
        /// 写入云空间
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        private void InsertFileCloud(string path,string name)
        {
            if (WebSysConfig.AvatarIsSyncCloud)
            {
                IFileCloud fileCloud = FileCloudService.GetFileCloudService();
                fileCloud.SaveFile(path, name, WebSysConfig.AvatarBucketName);
            }
        }
    }
}
