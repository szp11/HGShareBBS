using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 用户请求日志
    /// </summary>
    public interface IUserAccessLogsPublic
    {
        /// <summary>
        /// 添加用户请求日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> AddAsync(UserAccessLogVModel model);
    }
}
