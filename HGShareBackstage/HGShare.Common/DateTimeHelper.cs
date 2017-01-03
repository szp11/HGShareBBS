using System;

namespace HGShare.Common
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            return GetTimeStamp(DateTime.Now);
        }
        /// <summary>
        /// 获取指定时间戳
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime date)
        {
            TimeSpan ts = date - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        /// <summary>
        /// 将时间戳转换成DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public DateTime GetTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            var toNow = new TimeSpan(lTime); 
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 获取某个时间星期几
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetCnWeek(DateTime date)
        {
            var weekdays = new[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return weekdays[(int)date.DayOfWeek];
        }
    }
}
