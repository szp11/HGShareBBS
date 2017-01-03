using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;

namespace HGShare.Web.Interface
{
    public interface IVip
    {
        /// <summary>
        /// 用户名存在？
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        bool UserNameIsHave(string userName, int? id);

        /// <summary>
        /// 昵称存在？
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        bool NickNameIsHave(string nickName, int? id);

        /// <summary>
        /// 邮箱存在？
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        bool EmailIsHave(string email, int? id);

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool AddUserInfo(UserVModel user);
    }
}
