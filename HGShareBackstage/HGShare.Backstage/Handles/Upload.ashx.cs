using System;
using System.Text;
using System.Web;
using HGShare.Backstage.Controllers.Base;
using HGShare.Business;
using HGShare.FileManager.Upload;
using HGShare.Model;
using HGShare.Site.ActionResult;
using HGShare.Site.Config;
using HGShare.Utils.Interface;
using Newtonsoft.Json;

namespace HGShare.Backstage.Handles
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class Upload : IHttpHandler
    {
        private static readonly ILog Log = LogBaseController.Log;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            context.Response.Charset = "UTF-8";
            HttpPostedFile file = context.Request.Files["Filedata"];
            string fileguid = context.Request["fileguid"];
            string score = context.Request["score"];
            string uploadPath = HttpContext.Current.Server.MapPath(DirectoriesConfig.BackstageUploadPath);

            UserInfo userInfo = Users.GetCurrentLoginUserInfo();
            var result = new JsonResultModel<UploadResultInfo>();
            if (userInfo==null)
            {
                result.Message = "请登录后上传！";
                context.Response.Write(JsonConvert.SerializeObject(result));
                context.Response.End();
                return;
            }
            if (file != null)
            {
                try
                {
                    //文件上传
                    var upload = new FileManager.Upload.Upload(file, uploadPath, userInfo.Name);
                    Log.InfoFormat("开始上传：用户id={0},保存地址={1},GUID={2}", userInfo.Id, upload.SavePath, fileguid);
                    int attachmentid = upload.Save(new Guid(fileguid), score, userInfo.Id);
                    Log.InfoFormat("上传完成：文件id={0}", attachmentid);
                    //返回信息
                    result.ResultState = true;
                    result.Body = new UploadResultInfo()
                    {
                        FileId = attachmentid,
                        FileInfo = upload.FileInfo,
                        FileIco = "",
                        FileUrl = upload.SavePath
                    };
                }
                catch (Exception ex)
                {
                    result.ResultState = false;
                    result.Message = "上传附件发生异常！信息："+ex.Message;
                    Log.Error(string.Format("上传附件发生异常！用户id={0},GUID={1}", userInfo.Id, fileguid), ex);
                }
                
            }
            else
                result.Message = "文件信息无效！";
            context.Response.Write(JsonConvert.SerializeObject(result));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}