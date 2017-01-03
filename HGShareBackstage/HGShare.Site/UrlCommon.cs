using System;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using HGShare.Site.Config;
using Newtonsoft.Json;

namespace HGShare.Site
{
    /// <summary>
    /// urlhelper 扩展方法
    /// </summary>
    public static class UrlCommon
    {
        /// <summary>
        /// 静态文件地址生成
        /// </summary>
        /// <param name="helper">System.Web.Mvc.UrlHelper</param>
        /// <param name="path">文件相对地址</param>
        /// <returns></returns>
        public static string StaticFile(this UrlHelper helper, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return "";
            string staticFileUrl = UrlConfig.StaticFileHost;
            if (string.IsNullOrWhiteSpace(staticFileUrl))
                path=helper.Content(path);
            else
            {
                path = staticFileUrl + path.TrimStart('~');
            }

            return path + "?v=" + WebSysConfig.SysVersion;
        }

        /// <summary>
        /// 后台调用前台接口，跳转至对应的前台地址
        /// </summary>
        /// <param name="helper">System.Web.Mvc.UrlHelper</param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string BbsWebAction(this UrlHelper helper, string actionName, string controllerName,object values)
        {
            var sb=new StringBuilder();
            if (values != null)
            {
                Type t = values.GetType();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    sb.AppendFormat("{0}:{1}|", pi.Name, pi.GetValue(values, null));
                }
            }
            string url = string.Format("http://{0}/api/gotourl?actionName={1}&controllerName={2}&values={3}", UrlConfig.BBSWebUrl, actionName, controllerName, sb.ToString().TrimEnd('|'));

            return url;
        }
    }
}