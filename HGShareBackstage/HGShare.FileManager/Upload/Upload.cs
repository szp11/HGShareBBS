using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using HGShare.Business;
using HGShare.Enums;
using HGShare.Model;

namespace HGShare.FileManager.Upload
{
    public class Upload
    {
        /// <summary>
        /// 文件
        /// </summary>
        private readonly HttpPostedFile _file;
        /// <summary>
        /// 文件信息
        /// </summary>
        public FileInfo FileInfo;
        /// <summary>
        /// 文件储存目录（最外层目录）
        /// </summary>
        public string BasePath;
        /// <summary>
        /// 保存地址（最终目录，会根据文件类型生成）
        /// </summary>
        public string SavePath;

        /// <summary>
        /// 文件上传器
        /// </summary>
        /// <param name="file">文件HttpPostedFile</param>
        /// <param name="basepath">存储地址</param>
        /// <param name="namePre">文件名前缀</param>
        /// <param name="nameLase">文件名后缀</param>
        public Upload(HttpPostedFile file,string basepath,string namePre="",string nameLase="")
        {
            _file = file;
            BasePath = basepath;
            Init(namePre, nameLase);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="guid">文件组唯一标示</param>
        /// <param name="score">文件上传来源</param>
        /// <param name="userid">上传者</param>
        /// <returns>文件数据存储id</returns>
        public int Save(Guid guid, string score, int userid)
        {
            //目录创建
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);

            _file.SaveAs(Path.Combine(SavePath, FileInfo.FileName));
            UploadScore uploadScore;
            if (!Enum.TryParse(score, false, out uploadScore))
                uploadScore = UploadScore.Other;
            int attachmentid = Attachments.AddAttachment(new AttachmentInfo()
            {
                FileName = FileInfo.FileName,
                FileTitle = FileInfo.FileTitle,
                FileSize = FileInfo.FileSize,
                Height = FileInfo.Height,
                Width = FileInfo.Width,
                Guid = guid,
                Type = FileInfo.FileEx,
                BType = (int)FileInfo.FileType,
                Score = (int)uploadScore,
                State = (int)AttachmentState.Init,
                VirtualPath = SavePath,
                UserId = userid
            });
            return attachmentid;
        }

        #region 私有处理
        private void Init(string namePre="",string nameLase="")
        {
            FileInfo=new FileInfo
            {
                FileTitle = _file.FileName,
                FileEx = Path.GetExtension(_file.FileName),
                FileSize = _file.ContentLength
            };
            FileInfo.FileType = FileTools.GetFileType(FileInfo.FileEx);
            FileInfo.FileName = CreateFileName(namePre, nameLase);
            SavePath = CreateSavePath();

            if (FileInfo.FileType == FileType.Image)
            {
                var imageHandle =new ImageHandle(_file.InputStream);
                FileInfo.Width = imageHandle.Width;
                FileInfo.Height = imageHandle.Height;
                imageHandle.Dispose();
            }

        }
        /// <summary>
        /// 生成保存地址
        /// </summary>
        /// <returns></returns>
        private string CreateSavePath()
        {
            string typepath = FileInfo.FileType.ToString();
            return Path.Combine(BasePath, typepath);
        }
        /// <summary>
        /// 生成文件名
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        private string CreateFileName(string pre="",string last="")
        {
            string center = new Guid().ToString().ToLower();
            string filename = string.Format("{0}{1}{2}.{3}", pre, center, last, FileInfo.FileEx);
            return filename;

        }
        #endregion

    }
}
