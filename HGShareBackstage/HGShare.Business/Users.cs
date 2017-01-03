using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HGShare.Common;
using HGShare.Model;
using HGShare.Site;
using HGShare.Site.Config;
using HGShare.Site.Token;
using HGShare.VWModel;
using Newtonsoft.Json;

namespace HGShare.Business
{
   public  class Users
    {
       /// <summary>
       /// 判断是否含有该用户
       /// </summary>
       /// <param name="username"></param>
       /// <returns></returns>
        public static bool IsHaveUser(string username)
        {
            return DataProvider.Users.CheckName(username,null);
        }
        /// <summary>
       /// NickName检测
       /// </summary>
       /// <param name="nickName"></param>
       /// <param name="id">需排除的自身userid</param>
       /// <returns>true(含有)/false(不含有)</returns>
       public static bool CheckNickName(string nickName,int? id)
        {
            return DataProvider.Users.CheckNickName(nickName, id);
        }

       /// <summary>
       /// Name检测
       /// </summary>
       /// <param name="name"></param>
       /// <param name="id">需排除的自身userid</param>
       /// <returns>true(含有)/false(不含有)</returns>
       public static bool CheckName(string name, int? id)
       {
           return DataProvider.Users.CheckName(name, id);
       }

       /// <summary>
       /// email检测
       /// </summary>
       /// <param name="email"></param>
       /// <param name="id">需排除的自身userid</param>
       /// <returns>true(含有)/false(不含有)</returns>
       public static bool CheckEmail(string email, int? id)
       {
           return DataProvider.Users.CheckEmail(email, id);
       }

       /// <summary>
       /// 修改UserInfo
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public static int UpdateUser(UserInfo user)
       {
           //加密密码
           user.Password = new PwdToken(TokenConfig.PwdTokenKey, user.Password).GetToken();
           return DataProvider.Users.UpdateUser(user);
       }

       /// <summary>
       /// 根据Name和Password获取用户信息
       /// </summary>
       /// <param name="name"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static UserInfo GetUserInfo(string name,string password)
       {
           //加密密码
           password = new PwdToken(TokenConfig.PwdTokenKey, password).GetToken();
           return DataProvider.Users.GetUserInfo(name, password);
       }
       /// <summary>
       /// 根据用户id获取用户信息
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static UserInfo GetUserInfo(int id)
       {
           return DataProvider.Users.GetUserInfo(id);
       }
       /// <summary>
       /// 根据email获取用户信息
       /// </summary>
       /// <param name="email"></param>
       /// <returns></returns>
       public static UserInfo GetUserInfo(string email)
       {
           return DataProvider.Users.GetUserInfo(email);
       }
       /// <summary>
       /// 登录成功写入用户信息
       /// </summary>
       /// <param name="userInfo"></param>
       public static void Login(UserInfo userInfo)
       {
           long stamp = DateTimeHelper.GetTimeStamp();
           IToken token = new LoginToken(userInfo.Id.ToString(CultureInfo.InvariantCulture), stamp, TokenConfig.LoginTokenKey);//密令
           DateTime exTime = DateTime.Now.AddMilliseconds(CookieConfig.LoginCookieExpiredTime);//过期时间
           //写入cookie
           EncryptCookies.SetCookies(CookieConfig.UserIdCkName, userInfo.Id.ToString(CultureInfo.InvariantCulture),exTime);//userid
           EncryptCookies.SetCookies(CookieConfig.StampCkName, stamp.ToString(CultureInfo.InvariantCulture), exTime);//时间戳
           EncryptCookies.SetCookies(CookieConfig.TokenCkName, token.GetToken(), exTime);//密令


           var vmodel = UserInfoToVModel(userInfo);
           vmodel.Password = string.Empty;//密码要保密啊
           vmodel.RName =Roles.GetRoleInfo(vmodel.RoleId).RName;//角色名

           //写入其它信息
           EncryptCookies.SetCookies(CookieConfig.UserOther, JsonConvert.SerializeObject(vmodel), exTime);//userinfo
       }
       /// <summary>
       /// 得到当前登录用户信息
       /// </summary>
       /// <returns></returns>
       public static UserInfo GetCurrentLoginUserInfo()
       {
           if (!CheckUserIsLogin())
               return null;
           string userId = EncryptCookies.GetValue(CookieConfig.UserIdCkName);
           return DataProvider.Users.GetUserInfo(int.Parse(userId));
       }
       /// <summary>
       /// 检测用户是否登陆
       /// </summary>
       /// <returns></returns>
       public static bool CheckUserIsLogin()
       {
           string stamp = EncryptCookies.GetValue(CookieConfig.StampCkName);
           string userId = EncryptCookies.GetValue(CookieConfig.UserIdCkName);
           string tokenvalue = EncryptCookies.GetValue(CookieConfig.TokenCkName);
           if (string.IsNullOrEmpty(stamp))
               return false;
           if (string.IsNullOrEmpty(userId))
               return false;
           if (string.IsNullOrEmpty(tokenvalue))
               return false;
           //计算token
           IToken token = new LoginToken(userId, Convert.ToInt64(stamp), TokenConfig.LoginTokenKey);
           //对比cookie中的和重新计算的
           if (tokenvalue.ToUpper() == token.GetToken().ToUpper())
           {
               return true;
           }
           return false;
       }
       /// <summary>
       /// 检测用户是否登陆并返回用户信息（解析cookie信息，该信息用于展示，不能用于业务逻辑，业务逻辑请使用id获取用户信息）
       /// </summary>
       /// <returns></returns>
       public static UserVModel CheckUserIsLoginAndGetUserInfo()
       {
           string stamp = EncryptCookies.GetValue(CookieConfig.StampCkName);
           string userId = EncryptCookies.GetValue(CookieConfig.UserIdCkName);
           string tokenvalue = EncryptCookies.GetValue(CookieConfig.TokenCkName);
           string userOther= EncryptCookies.GetValue(CookieConfig.UserOther);//其它信息
           if (string.IsNullOrEmpty(stamp))
               return null;
           if (string.IsNullOrEmpty(userId))
               return null;
           if (string.IsNullOrEmpty(tokenvalue))
               return null;
           //计算token
           IToken token = new LoginToken(userId, Convert.ToInt64(stamp), TokenConfig.LoginTokenKey);
           //对比cookie中的和重新计算的
           if (tokenvalue.ToUpper() == token.GetToken().ToUpper())
           {
               try
               {
                   //解析信息
                   var user = JsonConvert.DeserializeObject<UserVModel>(userOther);
                   return user;
               }
               catch (Exception)
               {
                   LogOut();
                   return null;
               }
           }
           return null;
       }
       /// <summary>
       /// 刷新cookie中用户信息
       /// </summary>
       /// <param name="userId"></param>
       public static void RefreshCookieUserInfo(int userId)
       {
           var vmodel = UserInfoToVModel(GetUserInfo(userId));
           if(vmodel!=null)
           {
               vmodel.Password = string.Empty;//密码要保密啊
               vmodel.RName = Roles.GetRoleInfo(vmodel.RoleId).RName;//角色名
               DateTime exTime = DateTime.Now.AddMilliseconds(CookieConfig.LoginCookieExpiredTime);//过期时间
               //写入其它信息
               EncryptCookies.SetCookies(CookieConfig.UserOther, JsonConvert.SerializeObject(vmodel), exTime);//userinfo
           }
       }

       /// <summary>
       /// 注销当前用户
       /// </summary>
       public static void LogOut()
       {
           DateTime exTime = DateTime.Now.AddDays(-1);//过期时间
           //写入cookie
           EncryptCookies.SetCookies(CookieConfig.UserIdCkName, "", exTime);//userid
           EncryptCookies.SetCookies(CookieConfig.StampCkName, "", exTime);//时间戳
           EncryptCookies.SetCookies(CookieConfig.TokenCkName, "", exTime);//密令
           EncryptCookies.SetCookies(CookieConfig.UserOther, "", exTime);//userinfo
       }
       /// <summary>
       /// 更新用户在线时间
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static async Task<bool> UpdateOnLineTime(int id)
       {
           return await Task.Run(()=> DataProvider.Users.UpdateOnLineTime(id) > 0);
       }
       /// <summary>
       /// 更新用户在线时间
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static async Task<bool> UpdateActionTime(int id)
       {
           return await Task.Run(()=> DataProvider.Users.UpdateActionTime(id) > 0);
       }

       /// <summary>
       /// 更新用户最后在线时间、用户最后操作时间
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static async Task<int> UpdateActionTimeAndOnLineTime(int id)
       {
           return  await Task.Run(()=> DataProvider.Users.UpdateActionTimeAndOnLineTime(id));
       }

       #region 修改密码
       /// <summary>
       /// 更新密码
       /// </summary>
       /// <param name="name"></param>
       /// <param name="newPassword"></param>
       /// <returns></returns>
       public static bool ModifyPassWord(string name,string newPassword)
       {
           //加密密码
           newPassword = new PwdToken(TokenConfig.PwdTokenKey, newPassword).GetToken();
           return DataProvider.Users.UpdatePassword(name, newPassword)>0;
       }
       /// <summary>
       /// 更新密码
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="newPassword"></param>
       /// <returns></returns>
       public static bool ModifyPassWord(int userId, string newPassword)
       {
           //加密密码
           newPassword = new PwdToken(TokenConfig.PwdTokenKey, newPassword).GetToken();
           return DataProvider.Users.UpdatePassword(userId, newPassword) > 0;
       }
       /// <summary>
       /// 更新用户密码
       /// </summary>
       /// <param name="email"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static bool ModifyPassWordByEmail(string email, string password)
       {
           //加密密码
           password = new PwdToken(TokenConfig.PwdTokenKey, password).GetToken();
           return DataProvider.Users.UpdatePasswordByEmail(email, password) > 0;
       }
       #endregion

       #region 检测密码是否正确
       /// <summary>
       /// 检测密码是否正确
       /// </summary>
       /// <param name="name"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static bool CheckPassword(string name, string password)
       {
           //加密密码
           password = new PwdToken(TokenConfig.PwdTokenKey, password).GetToken();
           return DataProvider.Users.GetCount(name, password) > 0;
       }
       /// <summary>
       /// 检测密码是否正确
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static bool CheckPassword(int userId, string password)
       {
           //加密密码
           password = new PwdToken(TokenConfig.PwdTokenKey, password).GetToken();
           return DataProvider.Users.GetCount(userId, password) > 0;
       }
       #endregion

       #region 获取userList
       /// <summary>
       /// 获取User分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>User列表</returns>
       public static List<UserInfo> GetUserPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           return DataProvider.Users.GetUserPageList(pageIndex, pageSize, beginTime, endTime, out recordCount);
       }
       /// <summary>
       /// 获取User分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="userName">用户名或昵称</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>User列表</returns>
       public static List<UserInfo> GetUserPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, string userName,out int pageCount, out int count)
       {
           return DataProvider.Users.GetUserPageList(pageIndex, pageSize, beginTime, endTime, userName, out pageCount, out count);
       }

       #endregion

       /// <summary>
       /// 添加用户
       /// </summary>
       /// <param name="model">UserInfo</param>
       /// <returns></returns>
       public static bool AddUser(UserInfo model)
       {
           if (model == null)
               return false;
           model.Password = new PwdToken(TokenConfig.PwdTokenKey, model.Password).GetToken();
           if (model.RoleId <= 0)
               model.RoleId = 1;
           return DataProvider.Users.AddUser(model) > 0;
       }

       /// <summary>
       /// 根据id删除User
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static Int32 DeleteUser(int id)
       {
           return DataProvider.Users.DeleteUser(id);
       }
       /// <summary>
       /// 根据ids删除User多条记录
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static Int32 DeleteUsers(int[] ids)
       {
           return DataProvider.Users.DeleteUsers(ids);
       }

       #region 实体转换
       /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public static UserVModel UserInfoToVModel(UserInfo user)
       {
           if (user == null)
               return new UserVModel();
           return new UserVModel
           {
               Id = user.Id,
               Name = user.Name,
               NickName = user.NickName,
               Password = user.Password,
               RoleId = user.RoleId,
               OnLineTime = user.OnLineTime,
               ActionTime = user.ActionTime,
               CreateTime = user.CreateTime,
               Avatar = user.Avatar,
               Sex = user.Sex,
               Email = user.Email,
               EmailStatus = user.EmailStatus,
               Score = user.Score,
               ArticleNum = user.ArticleNum,
               CommentNum = user.CommentNum,
               Disable = user.Disable,
               DisableReason = user.DisableReason
           };
       }
       /// <summary>
       /// DataModels 转 ViewModels
       /// </summary>
       /// <param name="userInfos"></param>
       /// <returns></returns>
       public static List<UserVModel> UserInfosToVModels(List<UserInfo> userInfos)
       {
           return userInfos.Select(UserInfoToVModel).ToList();
       }
       /// <summary>
       /// ViewModel 转 DataModel
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public static UserInfo UserVModelToInfo(UserVModel user)
       {
           if (user == null)
               return new UserInfo();
           return new UserInfo
           {
               Id = user.Id,
               Name = user.Name,
               NickName = user.NickName,
               Password = user.Password,
               RoleId = user.RoleId,
               OnLineTime = user.OnLineTime,
               ActionTime = user.ActionTime,
               CreateTime = user.CreateTime,
               Avatar = user.Avatar,
               Sex = user.Sex,
               Email = user.Email,
               EmailStatus = user.EmailStatus,
               Score = user.Score,
               ArticleNum = user.ArticleNum,
               CommentNum = user.CommentNum,
               Disable = user.Disable,
               DisableReason = user.DisableReason
           };
       }
       /// <summary>
       /// ViewModels 转 DataModels
       /// </summary>
       /// <param name="userVModels"></param>
       /// <returns></returns>
       public static List<UserInfo> UserVModelsToInfos(List<UserVModel> userVModels)
       {
           return userVModels.Select(UserVModelToInfo).ToList();
       }
       #endregion

       /// <summary>
       /// 更新用户头像
       /// </summary>
       /// <param name="id"></param>
       /// <param name="avatar"></param>
       /// <returns></returns>
       public static int UpdateAvatar(int id, string avatar)
       {
           return DataProvider.Users.UpdateAvatar(id, avatar);
       }

       #region 前端
       /// <summary>
       ///根据用户多个id获取多个用户信息
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static List<UserVModel> GetUsersByIds(int[] ids)
       {
           return DataProvider.Users.GetUsersByIds(ids);
       }

       /// <summary>
       /// 修改UserVModel
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public static bool UpdateUser(UserVModel user)
       {
           return DataProvider.Users.UpdateUser(user);
       }
       /// <summary>
       /// 更新用户评论数
       /// </summary>
       /// <param name="id"></param>
       /// <param name="num">+1/-1</param>
       /// <returns></returns>
       public static int UpdateCommentNum(int id, int num)
       {
           return DataProvider.Users.UpdateCommentNum(id, num);
       }

       /// <summary>
       /// 更新用户文章数
       /// </summary>
       /// <param name="id"></param>
       /// <param name="num">+1/-1</param>
       /// <returns></returns>
       public static int UpdateArticleNum(int id, int num)
       {
           return DataProvider.Users.UpdateArticleNum(id, num);
       }

       /// <summary>
       /// 用户近期评论榜
       /// </summary>
       /// <param name="days"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public static List<UserVModel> CommentHotTop(int days, int pageSize)
       {
           return DataProvider.Users.CommentHotTop(days, pageSize);
       }
       /// <summary>
       /// 更新用户邮箱激活状态
       /// </summary>
       /// <param name="id"></param>
       /// <param name="emailStatus"></param>
       /// <returns></returns>
       public static bool UpdateEmailStatus(int id, bool emailStatus)
       {
           return DataProvider.Users.UpdateEmailStatus(id, emailStatus);
       }
       #endregion

       /// <summary>
       /// 更新用户禁用状态
       /// </summary>
       /// <param name="id">用户id</param>
       /// <param name="disable">是否禁用</param>
       /// <param name="disableReason">原因</param>
       /// <returns></returns>
       public static bool UpdateDisable(int id, bool disable, string disableReason = null)
       {
           return DataProvider.Users.UpdateDisable(id, disable, disableReason);
       }
    }
}
