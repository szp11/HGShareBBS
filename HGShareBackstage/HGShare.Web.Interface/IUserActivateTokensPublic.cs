using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 用户激活密令
    /// </summary>
    public interface IUserActivateTokensPublic
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long Add(UserActivateTokenVModel model);
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateStatus(long id,bool status);

        /// <summary>
        /// 将这个用户的所有令牌都修改为无效 （ Status =0）（已废弃使用）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UpdateStatusToFalseByUserId(int userId);
        /// <summary>
        /// 检测用户令牌 返回令牌id
        /// 邮件激活令牌，每次都是唯一的，
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="email">email</param>
        /// <param name="token">令牌</param>
        /// <param name="expireTime">过期时间（分钟）</param>
        /// <returns></returns>
        long CheckToken(int userId,string email,string token,int expireTime);
        /// <summary>
        /// 检测用户令牌 返回令牌id
        /// 邮件激活令牌，每次都是唯一的，
        /// </summary>
        /// <param name="token">令牌</param>
        /// <param name="expireTime">过期时间（分钟）</param>
        /// <returns></returns>
        long CheckToken(string token, int expireTime);
        /// <summary>
        /// 获取Token信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserActivateTokenVModel GetUserActivateTokenVModel(long id);

    }
}
