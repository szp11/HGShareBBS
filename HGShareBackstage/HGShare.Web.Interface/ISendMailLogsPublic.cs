using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.Web.Interface
{
    public interface ISendMailLogsPublic
    {
        /// <summary>
        /// 写入邮件发送日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long Add(VWModel.SendMailLogVModel model);

        /// <summary>
        /// 根据发送日志检测用户是否可以发送邮件，限制发送频率与发送量
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="toDayMaxNum">今日发送最大量</param>
        /// <param name="time">时间间隔（分钟）</param>
        /// <param name="maxNum">指定时间间隔内最大发送量</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckUserEmailAvailable(int userId,int time,int maxNum,int toDayMaxNum, out string msg);


    }
}
