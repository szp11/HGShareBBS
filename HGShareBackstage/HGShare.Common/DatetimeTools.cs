using System;

namespace HGShare.Common
{
    public class DatetimeTools
    {
        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
            if (span.TotalDays > 30)
            return "1个月前";
            if (span.TotalDays > 14)
            return "2周前";
            if (span.TotalDays > 7)
            return "1周前";
            if (span.TotalDays > 1)
            return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            if (span.TotalHours > 1)
            return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            if (span.TotalMinutes > 1)
            return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            if (span.TotalSeconds >= 1)
            return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            return "1秒前";
        }
    }
}
