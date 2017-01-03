using HGShare.Business;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business
{
    /// <summary>
    /// 用户激活令牌（用于用户需要通过令牌认证的业务）
    /// 1，邮箱激活：通过向邮箱发送激活地址，通过令牌+email+userid认证
    /// 2，找回密码：通过向邮箱发送找回密码地址 ，通过令牌+email认证
    /// </summary>
    public class UserActivateTokensPublic:IUserActivateTokensPublic
    {
        /// <summary>
        /// 添加令牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Add(UserActivateTokenVModel model)
        {
            return
                UserActivateTokens.AddUserActivateToken(
                    UserActivateTokens.UserActivateTokenVModelToInfo(model));
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatus(long id, bool status)
        {
            return UserActivateTokens.UpdateStatus(id, status);
        }
        /// <summary>
        /// 将这个用户的所有令牌都修改为无效 （ Status =0）（已废弃使用）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UpdateStatusToFalseByUserId(int userId)
        {
            return UserActivateTokens.UpdateStatusToFalseByUserId(userId);
        }
        /// <summary>
        /// 检测用户令牌 返回令牌id
        /// 邮件激活令牌，每次都是唯一的，
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="email">email</param>
        /// <param name="token">令牌</param>
        /// <param name="expireTime">过期时间（分钟）</param>
        /// <returns></returns>
        public long CheckToken(int userId, string email, string token, int expireTime)
        {
            return UserActivateTokens.GetUserActivateTokenId(userId, email, token, expireTime);
        }
        /// <summary>
        /// 检测用户令牌 返回令牌id
        /// 邮件激活令牌，每次都是唯一的，
        /// </summary>
        /// <param name="token">令牌</param>
        /// <param name="expireTime">过期时间（分钟）</param>
        /// <returns></returns>
        public long CheckToken(string token, int expireTime)
        {
            return UserActivateTokens.GetUserActivateTokenId(token, expireTime);
        }
        /// <summary>
        /// 获取Token信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserActivateTokenVModel GetUserActivateTokenVModel(long id)
        {
            return
                UserActivateTokens.UserActivateTokenInfoToVModel(
                    UserActivateTokens.GetUserActivateTokenInfo(id));
        }
    }
}
