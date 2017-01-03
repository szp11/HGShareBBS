using HGShare.VWModel;
using HGShare.Web.Interface;
using HGShare.Business;
namespace HGShare.Web.Business
{
    public class SendMailLogsPublic:ISendMailLogsPublic
    {
        /// <summary>
        /// 写入邮件发送日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Add(SendMailLogVModel model)
        {
            return SendMailLogs.AddSendMailLog(SendMailLogs.SendMailLogVModelToInfo(model));
        }
        /// <summary>
        /// 根据发送日志检测用户是否可以发送邮件，限制发送频率与发送量
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="toDayMaxNum">今日发送最大量</param>
        /// <param name="time">时间间隔(分钟)</param>
        /// <param name="maxNum">指定时间间隔内最大发送量</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CheckUserEmailAvailable(int userId, int time, int maxNum, int toDayMaxNum, out string msg)
        {
            int _maxNum = SendMailLogs.GetSendMailLogCountByUserIdAndTime(userId, time);
            if (_maxNum >= maxNum)
            {
                msg = "您的操作太频繁了，请稍后再试！";
                return false;
            }
            int _toDayMaxNum = SendMailLogs.GetSendMailLogToDayCountByUserId(userId);
            if (_toDayMaxNum >= toDayMaxNum)
            {
                msg = "您今日发送邮件已达到最大数量,发送失败！";
                return false;
            }
            msg = "可以发送！";
            return true;
        }
    }
}
