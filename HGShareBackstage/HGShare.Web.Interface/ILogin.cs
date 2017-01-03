using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.Web.Interface
{
    public interface ILogin
    {
        /// <summary>
        /// 检测用户是否登陆并返回用户信息（解析cookie信息，该信息用于展示，不能用于业务逻辑，业务逻辑请使用id获取用户信息）
        /// </summary>
        /// <returns></returns>
        VWModel.UserVModel CheckUserIsLoginAndGetUserInfo();
        /// <summary>
        /// 登出
        /// </summary>
        void LogOut();
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        VWModel.UserVModel LoginByUserName(string userName, string password);
        /// <summary>
        /// 刷新cookie中用户信息
        /// </summary>
        /// <param name="userId"></param>
        void RefreshCookieUserInfo(int userId);
    }
}
