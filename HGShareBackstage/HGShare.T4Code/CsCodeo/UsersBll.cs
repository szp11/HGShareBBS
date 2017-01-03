using System;
using System.Linq;
using System.Collections.Generic;
using HGShare.Model;
using HGShare.VWModel;
namespace HGShare.Business
{    
	/// <summary>
    /// User 
    /// </summary>
    public class Users
    {

		/// <summary>
		/// 添加UserInfo
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static int AddUser(UserInfo user)
		{
			return DataProvider.Users.AddUser(user);
		}
		/// <summary>
       /// 修改UserInfo
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public static int UpdateUser(UserInfo user)
       {
           return DataProvider.Users.UpdateUser(user);
       }
		/// <summary>
		/// 根据id获取UserInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static UserInfo GetUserInfo(int id)
		{
			return DataProvider.Users.GetUserInfo(id);
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
           return DataProvider.Users.GetUserPageList(pageIndex,pageSize,beginTime,endTime, out recordCount);
       }
	   /// <summary>
       /// 获取User分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>User列表</returns>
       public static List<UserInfo> GetUserPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           return DataProvider.Users.GetUserPageList(pageIndex,pageSize,beginTime,endTime, out pageCount, out count);
       }
	   #region 实体转换
	   /// <summary>
       /// DataModel 转 ViewModel
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public static UserVModel UserInfoToVModel(UserInfo user)
       {
           if(user==null)
               return new UserVModel();
           return new UserVModel
           {
				Id=user.Id,
			   Name=user.Name,
			   NickName=user.NickName,
			   Password=user.Password,
			   RoleId=user.RoleId,
			   OnLineTime=user.OnLineTime,
			   ActionTime=user.ActionTime,
			   CreateTime=user.CreateTime,
			   Avatar=user.Avatar,
			   Sex=user.Sex,
			   Email=user.Email,
			   EmailStatus=user.EmailStatus,
			   Score=user.Score,
			   ArticleNum=user.ArticleNum,
			   CommentNum=user.CommentNum,
			   Disable=user.Disable,
			   DisableReason=user.DisableReason,
			   QQ=user.QQ
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
              Id=user.Id,
			   Name=user.Name,
			   NickName=user.NickName,
			   Password=user.Password,
			   RoleId=user.RoleId,
			   OnLineTime=user.OnLineTime,
			   ActionTime=user.ActionTime,
			   CreateTime=user.CreateTime,
			   Avatar=user.Avatar,
			   Sex=user.Sex,
			   Email=user.Email,
			   EmailStatus=user.EmailStatus,
			   Score=user.Score,
			   ArticleNum=user.ArticleNum,
			   CommentNum=user.CommentNum,
			   Disable=user.Disable,
			   DisableReason=user.DisableReason,
			   QQ=user.QQ
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
    }
}
