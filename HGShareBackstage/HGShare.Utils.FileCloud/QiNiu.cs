using System;
using HGShare.Utils.Interface;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;

namespace HGShare.Utils.FileCloud
{
    public class QiNiu : IFileCloud
    {
        private readonly QiNiuConfig _config;
        private readonly Mac _mac;
        public QiNiu(QiNiuConfig config)
        {
            _config = config;
            _config = new QiNiuConfig();
            if (string.IsNullOrEmpty(_config.Ak) || string.IsNullOrEmpty(_config.Sk))
                throw new Exception("七牛云配置项为配置！");
            _mac = new Mac(_config.Ak, _config.Sk); 
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="localFilePath"></param>
        /// <param name="fileName"></param>
        /// <param name="bucketName"></param>
        public void SaveFile(string localFilePath, string fileName, string bucketName)
        {
            
            if(string.IsNullOrEmpty(_config.Ak) || string.IsNullOrEmpty(_config.Sk))
                throw new Exception("七牛云配置项为配置！");

            // 上传策略
            var putPolicy = new PutPolicy
            {
                // 设置要上传的目标空间
                Scope = bucketName,
                // 文件上传完毕后，在多少天后自动被删除
                DeleteAfterDays=0
            };
            // 上传策略的过期时间(单位:秒)
            putPolicy.SetExpires(3600);
            // 生成上传凭证
            string uploadToken = Auth.createUploadToken(putPolicy, _mac);
            // 开始上传文件
            var um = new UploadManager();
            um.uploadFile(localFilePath, fileName, uploadToken, null, null);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="bucketName"></param>
        public void DeleteFile(string fileName, string bucketName)
        {
            var bm = new BucketManager(_mac);
            // 返回结果存储在result中
            HttpResult result = bm.delete(bucketName, fileName);
        }
    }
    /// <summary>
    /// 七牛配置
    /// </summary>
    public class QiNiuConfig
    {
        public string Ak {
            //get
            //{
            //    return Configuration.AppSettings("Cloud_QiNiu_AK");
            //}
            get; set;
        }
        public string Sk {
            //get
            //{
            //    return Configuration.AppSettings("Cloud_QiNiu_SK");
            //}
            get; set;
        }
    }
}
