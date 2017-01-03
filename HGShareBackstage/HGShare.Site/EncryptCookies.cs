using System;
using System.Web;
using HGShare.Common;
using HGShare.Site.Config;

namespace HGShare.Site
{
    /// <summary>
    /// cookie value加密处理
    /// </summary>
    public class EncryptCookies 
    {
        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="keystr"></param>
        /// <param name="values"></param>
        /// <param name="timeout"></param>
        /// <param name="domain"></param>
        public static void SetCookies(string keystr, string values, DateTime timeout, string domain)
        {
            CookieHelper.SetCookies(keystr, DesEncrypt.Encrypt(values, DesEncryptConfig.CookieValueKey), timeout, domain);
        }
        #region 设置Cookie值

        /// <summary>
        /// 写入Cookie
        /// </summary>
        /// <param name="keystr"></param>
        /// <param name="values"></param>
        /// <param name="timeout"></param>
        public static void SetCookies(string keystr, string values, DateTime timeout)
        {
            CookieHelper.SetCookies(keystr, DesEncrypt.Encrypt(values, DesEncryptConfig.CookieValueKey), timeout);
        }

        /// <summary>
        /// 写入Cookie
        /// </summary>
        /// <param name="key">Cookies名称</param>
        /// <param name="value">Cookies的值</param>
        public static void SetCookies(string key, string value)
        {
            CookieHelper.SetCookies(key, DesEncrypt.Encrypt(value, DesEncryptConfig.CookieValueKey));
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
            return CookieHelper.Get(name);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetValue(string strName)
        {
            string cookievalue = CookieHelper.GetValue(strName);
            if (string.IsNullOrEmpty(cookievalue))
                return string.Empty;
            return DesEncrypt.Decrypt(cookievalue, DesEncryptConfig.CookieValueKey);
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
            cookie.Value = DesEncrypt.Encrypt(cookie.Value, DesEncryptConfig.CookieValueKey);
            CookieHelper.Save(cookie,domain);
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
            CookieHelper.Remove(cookie, domain);
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
           CookieHelper.Remove(name,domain);
        }
        /// <summary>
        /// 移除Cookie
        /// </summary>
        /// <param name="name"></param>
        public static void Remove(string name)
        {
            CookieHelper.Remove(name);
        }
        #endregion
    }
}
