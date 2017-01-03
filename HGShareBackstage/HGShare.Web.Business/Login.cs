using HGShare.Business;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    public class Login : ILogin
    {
        /// <summary>
        /// 检测用户是否登陆并返回用户信息（解析cookie信息，该信息用于展示，不能用于业务逻辑，业务逻辑请使用id获取用户信息）
        /// </summary>
        /// <returns></returns>
        public UserVModel CheckUserIsLoginAndGetUserInfo()
        {
            var userInfo = Users.CheckUserIsLoginAndGetUserInfo();
            return userInfo;
        }
        /// <summary>
        /// 注销当前用户
        /// </summary>
        public void LogOut()
        {
            Users.LogOut();
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserVModel LoginByUserName(string userName, string password)
        {
            var userInfo= Users.GetUserInfo(userName, password);

            if (userInfo != null)
            {
                Users.Login(userInfo);
                return Users.UserInfoToVModel(userInfo);
            }
            return null;
        }
        /// <summary>
        /// 刷新cookie中用户信息
        /// </summary>
        /// <param name="userId"></param>
        public void RefreshCookieUserInfo(int userId)
        {
           Users.RefreshCookieUserInfo(userId);
        }
    }
}
