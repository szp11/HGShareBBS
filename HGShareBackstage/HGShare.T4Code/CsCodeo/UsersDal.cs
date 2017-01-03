using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HGShare.DataProvider.DapperHelper;
using HGShare.Model;
namespace HGShare.DataProvider
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
			string sql = @"INSERT INTO [User]
			([Name],[NickName],[Password],[RoleId],[OnLineTime],[ActionTime],[CreateTime],[Avatar],[Sex],[Email],[EmailStatus],[Score],[ArticleNum],[CommentNum],[Disable],[DisableReason],[QQ])
			VALUES
			(@Name,@NickName,@Password,@RoleId,@OnLineTime,@ActionTime,@CreateTime,@Avatar,@Sex,@Email,@EmailStatus,@Score,@ArticleNum,@CommentNum,@Disable,@DisableReason,@QQ) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@Name",user.Name , DbType.String);
			par.Add("@NickName",user.NickName , DbType.String);
			par.Add("@Password",user.Password , DbType.AnsiString);
			par.Add("@RoleId",user.RoleId , DbType.Int32);
			par.Add("@OnLineTime",user.OnLineTime , DbType.DateTime);
			par.Add("@ActionTime",user.ActionTime , DbType.DateTime);
			par.Add("@CreateTime",user.CreateTime , DbType.DateTime);
			par.Add("@Avatar",user.Avatar , DbType.AnsiString);
			par.Add("@Sex",user.Sex , DbType.Int16);
			par.Add("@Email",user.Email , DbType.AnsiString);
			par.Add("@EmailStatus",user.EmailStatus , DbType.Boolean);
			par.Add("@Score",user.Score , DbType.Int64);
			par.Add("@ArticleNum",user.ArticleNum , DbType.Int32);
			par.Add("@CommentNum",user.CommentNum , DbType.Int32);
			par.Add("@Disable",user.Disable , DbType.Boolean);
			par.Add("@DisableReason",user.DisableReason , DbType.String);
			par.Add("@QQ",user.QQ , DbType.AnsiString);
			return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改UserInfo
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static int UpdateUser(UserInfo user)
		{
			string sql = @"UPDATE  [User] SET 
						Name=@Name,
						NickName=@NickName,
						Password=@Password,
						RoleId=@RoleId,
						OnLineTime=@OnLineTime,
						ActionTime=@ActionTime,
						CreateTime=@CreateTime,
						Avatar=@Avatar,
						Sex=@Sex,
						Email=@Email,
						EmailStatus=@EmailStatus,
						Score=@Score,
						ArticleNum=@ArticleNum,
						CommentNum=@CommentNum,
						Disable=@Disable,
						DisableReason=@DisableReason,
						QQ=@QQ
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", user.Id, DbType.Int32);
			par.Add("@Name",user.Name , DbType.String);
			par.Add("@NickName",user.NickName , DbType.String);
			par.Add("@Password",user.Password , DbType.AnsiString);
			par.Add("@RoleId",user.RoleId , DbType.Int32);
			par.Add("@OnLineTime",user.OnLineTime , DbType.DateTime);
			par.Add("@ActionTime",user.ActionTime , DbType.DateTime);
			par.Add("@CreateTime",user.CreateTime , DbType.DateTime);
			par.Add("@Avatar",user.Avatar , DbType.AnsiString);
			par.Add("@Sex",user.Sex , DbType.Int16);
			par.Add("@Email",user.Email , DbType.AnsiString);
			par.Add("@EmailStatus",user.EmailStatus , DbType.Boolean);
			par.Add("@Score",user.Score , DbType.Int64);
			par.Add("@ArticleNum",user.ArticleNum , DbType.Int32);
			par.Add("@CommentNum",user.CommentNum , DbType.Int32);
			par.Add("@Disable",user.Disable , DbType.Boolean);
			par.Add("@DisableReason",user.DisableReason , DbType.String);
			par.Add("@QQ",user.QQ , DbType.AnsiString);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取UserInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static UserInfo GetUserInfo(int id)
		{
			string sql = "select [Id],[Name],[NickName],[Password],[RoleId],[OnLineTime],[ActionTime],[CreateTime],[Avatar],[Sex],[Email],[EmailStatus],[Score],[ArticleNum],[CommentNum],[Disable],[DisableReason],[QQ] FROM [User] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerQuerySql<UserInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除User
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteUser(int id)
		{
			string sql="DELETE [User] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除User多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteUsers(int[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [User] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
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
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<UserInfo>(DbConfig.ArticleManagerConnString, "proc_GetUserPageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
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
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[Name],[NickName],[Password],[RoleId],[OnLineTime],[ActionTime],[CreateTime],[Avatar],[Sex],[Email],[EmailStatus],[Score],[ArticleNum],[CommentNum],[Disable],[DisableReason],[QQ]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<UserInfo>.GetPageList("[User]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
