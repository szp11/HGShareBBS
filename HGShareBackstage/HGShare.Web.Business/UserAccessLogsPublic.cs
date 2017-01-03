using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    public class UserAccessLogsPublic:IUserAccessLogsPublic
    {
        public async Task<long> AddAsync(UserAccessLogVModel model)
        {
            return
                await
                    HGShare.Business.UserAccessLogs.AddUserAccessLogAsync(
                        HGShare.Business.UserAccessLogs.UserAccessLogVModelToInfo(model));
        }
    }
}
