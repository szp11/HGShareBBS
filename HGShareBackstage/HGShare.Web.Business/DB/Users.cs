using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business.DB
{
    public class Users :IUsers
    {
        /// <summary>
        /// 根据用户多个id获取多个用户信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<UserVModel> GetUsersByIds(int[] ids)
        {
            return HGShare.Business.Users.GetUsersByIds(ids);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserVModel GetUserById(int id)
        {
            int[] ids = {id};
            var users = GetUsersByIds(ids);
            if (users != null && users.Count == 1)
                return users.FirstOrDefault();
            return null;
        }
        /// <summary>
        /// 根据用户id获取用户位置信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserPositionVModel>> GetUserPositionById(int id)
        {
            return  await Task.Run(() =>
                HGShare.Business.UserPositions.GetUserPositions(id)
                );
        }
        /// <summary>
        /// 根据用户id获取用户其它信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserOtherVModel> GetUserOtherById(int id)
        {
            return await Task.Run(()=>HGShare.Business.UserOthers.UserOtherInfoToVModel(HGShare.Business.UserOthers.GetUserOtherInfo(id)));
        }
        /// <summary>
        /// 用户近期评论榜
        /// </summary>
        /// <param name="days"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<UserVModel> CommentHotTop(int days, int pageSize)
        {
            return HGShare.Business.Users.CommentHotTop(days, pageSize);
        }

        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserVModel GetUserByEmail(string email)
        {
            return HGShare.Business.Users.UserInfoToVModel(HGShare.Business.Users.GetUserInfo(email));
        }
    }
}
