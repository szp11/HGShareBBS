using System;
using System.Web;


namespace HGShare.Common
{
    #region Cookies 操作

    /// <summary>
    /// Author:Roger
    /// Cookie操作
    /// </summary>
    public class CookieHelper
    {
        #region 设置Cookie值

        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="keystr"></param>
        /// <param name="values"></param>
        /// <param name="timeout"></param>
        /// <param name="domain"></param>
        public static void SetCookies(string keystr, string values, DateTime timeout, string domain)
        {
            HttpResponse response = HttpContext.Current.Response;
            {
                HttpCookie cookie = response.Cookies[keystr];
                if (cookie != null)
                {
                    cookie.Value = values;
                    cookie.Domain = domain;
                    cookie.Expires = timeout;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }

        /// <summary>
        /// 写入Cookie
        /// </summary>
        /// <param name="keystr"></param>
        /// <param name="values"></param>
        /// <param name="timeout"></param>
        public static void SetCookies(string keystr, string values, DateTime timeout)
        {
            HttpResponse response = HttpContext.Current.Response;
            {
                HttpCookie cookie = response.Cookies[keystr];
                if (cookie != null)
                {
                    cookie.Value = values;
                    cookie.Expires = timeout;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }

        /// <summary>
        /// 写入Cookie
        /// </summary>
        /// <param name="key">Cookies名称</param>
        /// <param name="value">Cookies的值</param>
        public static void SetCookies(string key, string value)
        {
            HttpCookie hc = new HttpCookie(key);
            hc.Value = HttpUtility.UrlEncode(value);
            HttpContext.Current.Response.Cookies.Add(hc);
        }

        #endregion

        #region 获取Cookie值

        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetValue(string strName)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
                return cookie != null ? cookie.Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        #endregion

        #region 设置Cookie值

        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpCookie Set(string name)
        {
            return new HttpCookie(name);
        }

        #endregion

        #region 保存Cookie值

        /// <summary>
        ///  保存Cookie值
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="domain"></param>
        public static void Save(HttpCookie cookie, string domain)
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            if (domain != host)
            {
                cookie.Domain = domain;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        #region 移除Cookie值

        /// <summary>
        /// 移除Cookie值
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="domain"></param>
        public static void Remove(HttpCookie cookie, string domain)
        {
            if (cookie != null)
            {
                cookie.Expires = new DateTime(1980, 1, 1);
                Save(cookie, domain);
            }
        }

        #endregion

        #region 移除Cookie值

        /// <summary>
        /// 移除Cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="domain"></param>
        public static void Remove(string name, string domain)
        {
            Remove(Get(name), domain);
        }
        /// <summary>
        /// 移除Cookie
        /// </summary>
        /// <param name="name"></param>
        public static void Remove(string name)
        {
            HttpContext.Current.Response.Cookies.Remove(name);
        }

        #endregion
    }
    #endregion
}
