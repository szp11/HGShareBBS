using System;
using System.Configuration;

namespace HGShare.Common
{
    public class Configuration
    {
        /// <summary>
        /// 获取当前应用程序默认配置的数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static String AppSettings(String key)
        {
            string value = ConfigurationManager.AppSettings[key];
            return (!string.IsNullOrEmpty(value)) ? value.Trim() : "";
        }
        /// <summary>
        /// 获取当前应用程序默认配置的数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static int AppSettingsToInt(String key)
        {
            string value = AppSettings(key);
            int result;
            int.TryParse(value, out result);
            return result;
        }
    }
}
