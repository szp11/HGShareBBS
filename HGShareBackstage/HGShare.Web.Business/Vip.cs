using HGShare.Business;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    public class Vip:IVip
    {
        /// <summary>
        /// 用户名存在？
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UserNameIsHave(string userName,int? id)
        {
            return Users.CheckName(userName, id);
        }

        /// <summary>
        /// 昵称存在？
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool NickNameIsHave(string nickName, int? id)
        {
            return Users.CheckNickName(nickName, id);
        }

        /// <summary>
        /// 邮箱存在？
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EmailIsHave(string email, int? id)
        {
            return Users.CheckEmail(email, id);
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUserInfo(UserVModel user)
        {
            return Users.AddUser(Users.UserVModelToInfo(user));
        }
    }
}
