using System.Collections.Generic;
using System.Threading.Tasks;
using HGShare.VWModel;

namespace HGShare.Web.Interface
{
    /// <summary>
    /// 用户 接口
    /// </summary>
    public interface IUsers
    {
        /// <summary>
        /// 根据用户多个id获取多个用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<UserVModel> GetUsersByIds(int[] ids);
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserVModel GetUserById(int id);
        /// <summary>
        /// 根据用户id获取用户位置信息
        /// </summary>
        /// <returns></returns>
        Task<List<UserPositionVModel>> GetUserPositionById(int id);
        /// <summary>
        /// 根据用户id获取用户其它信息
        /// </summary>
        /// <returns></returns>
        Task<UserOtherVModel> GetUserOtherById(int id);

        /// <summary>
        /// 用户近期评论榜
        /// </summary>
        /// <param name="days"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<UserVModel> CommentHotTop(int days, int pageSize);
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        UserVModel GetUserByEmail(string email);

    }
}
