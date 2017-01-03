using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HGShare.DataProvider.DapperHelper;
using HGShare.Model;
using HGShare.VWModel;

namespace HGShare.DataProvider
{
   public static class Users
   {
       private const string Fileds = "[Id],[Name],[NickName],[Password],[RoleId],[OnLineTime],[ActionTime],[CreateTime],[Avatar],[Sex],[Email],[EmailStatus],[Score],[ArticleNum],[CommentNum],[Disable],[DisableReason]";

       /// <summary>
        /// 添加UserInfo
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int AddUser(UserInfo user)
        {
            string sql = @"INSERT INTO [User]
			([Name],[NickName],[Password],[RoleId],[Sex],[Email])
			VALUES
			(@Name,@NickName,@Password,@RoleId,@Sex,@Email) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@Name", user.Name, DbType.String);
            par.Add("@NickName", user.NickName, DbType.String);
            par.Add("@Password", user.Password, DbType.AnsiString);
            par.Add("@RoleId", user.RoleId, DbType.Int32);
            par.Add("@Sex", user.Sex, DbType.Int16);
            par.Add("@Email", user.Email, DbType.AnsiString);
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
						Sex=@Sex,
						Email=@Email
                        WHERE  Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", user.Id, DbType.Int32);
            par.Add("@Name", user.Name, DbType.String);
            par.Add("@NickName", user.NickName, DbType.String);
            par.Add("@Password", user.Password, DbType.AnsiString);
            par.Add("@RoleId", user.RoleId, DbType.Int32);
            par.Add("@Sex", user.Sex, DbType.Int16);
            par.Add("@Email", user.Email, DbType.AnsiString);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }

        #region 检测用户是否存在
        /// <summary>
       /// NickName检测
       /// </summary>
       /// <param name="nickName"></param>
       /// <param name="id">需排除的自身userid</param>
       /// <returns>true(含有)/false(不含有)</returns>
       public static bool CheckNickName(string nickName,int? id)
        {
            string sql = "select count(Id) from [User] where NickName=@NickName";
           
            var par = new DynamicParameters();
            par.Add("@NickName", nickName, DbType.String);
           if (id.HasValue)
           {
               sql += " AND Id!=@Id";
               par.Add("@Id",id.Value,DbType.Int32);
           }
           return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par)>0;
        }
       /// <summary>
       /// Name检测
       /// </summary>
       /// <param name="name"></param>
       /// <param name="id">需排除的自身userid</param>
       /// <returns>true(含有)/false(不含有)</returns>
       public static bool CheckName(string name, int? id)
       {
           string sql = "select count(Id) from [User] where Name=@Name";

           var par = new DynamicParameters();
           par.Add("@Name", name, DbType.String);
           if (id.HasValue)
           {
               sql += " AND Id!=@Id";
               par.Add("@Id", id.Value, DbType.Int32);
           }
           return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par) > 0;
       }
       /// <summary>
       /// email检测
       /// </summary>
       /// <param name="email"></param>
       /// <param name="id">需排除的自身userid</param>
       /// <returns>true(含有)/false(不含有)</returns>
       public static bool CheckEmail(string email, int? id)
       {
           string sql = "select count(Id) from [User] where Email=@Email";

           var par = new DynamicParameters();
           par.Add("@Email", email, DbType.String);
           if (id.HasValue)
           {
               sql += " AND Id!=@Id";
               par.Add("@Id", id.Value, DbType.Int32);
           }
           return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par) > 0;
       }
        #endregion

       /// <summary>
       /// 根据Name password查找记录数
       /// </summary>
       /// <param name="name"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static int GetCount(string name, string password)
        {
            string sql = "select count(Id) from [User] where Name=@Name AND Password=@Password";
            var par = new DynamicParameters();
            par.Add("@Name", name, DbType.String);
            par.Add("@Password", password, DbType.String);
            return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
        }
       /// <summary>
       /// 根据Name password查找记录数
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static int GetCount(int userId, string password)
       {
           string sql = "select count(Id) from [User] where Id=@Id AND Password=@Password";
           var par = new DynamicParameters();
           par.Add("@Id", userId, DbType.Int32);
           par.Add("@Password", password, DbType.String);
           return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
       }
       /// <summary>
       /// 根据Id获取用户信息
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static UserInfo GetUserInfo(int id)
       {
           string sql = "select " + Fileds + " FROM [dbo].[User] WHERE Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Id", id, DbType.Int32);
           return DapWrapper.InnerQuerySql<UserInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
       }
       /// <summary>
       /// 根据email获取用户信息
       /// </summary>
       /// <param name="email"></param>
       /// <returns></returns>
       public static UserInfo GetUserInfo(string email)
       {
           string sql = "select " + Fileds + " FROM [dbo].[User] WHERE Email=@Email";
           var par = new DynamicParameters();
           par.Add("@Email", email, DbType.String);
           return DapWrapper.InnerQuerySql<UserInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
       }
       /// <summary>
       /// 根据Name和Password获取用户信息
       /// </summary>
       /// <param name="name"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static UserInfo GetUserInfo(string name,string password)
       {
           string sql = "select " + Fileds + " FROM [dbo].[User] WHERE Name=@Name AND Password=@Password ";
           var par = new DynamicParameters();
           par.Add("@Name", name, DbType.String);
           par.Add("@Password", password, DbType.String);
           return DapWrapper.InnerQuerySql<UserInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
       }
       /// <summary>
       /// 根据id删除User
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static Int32 DeleteUser(int id)
       {
           string sql = "DELETE [User] WHERE Id=@Id";
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
           string sql = "DELETE [User] WHERE Id IN (" + string.Join(",", ids) + ")";
           return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
       }
       /// <summary>
       /// 更新用户最后在线时间
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static int UpdateOnLineTime(int id)
        {
            string sql = "UPDATE [User] SET OnLineTime=GETDATE() WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
       /// <summary>
       /// 更新用户最后操作时间
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static int UpdateActionTime(int id)
       {
           string sql = "UPDATE [User] SET ActionTime=GETDATE() WHERE Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Id", id, DbType.Int32);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
       }
       /// <summary>
       /// 更新用户最后在线时间、用户最后操作时间
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static int UpdateActionTimeAndOnLineTime(int id)
       {
           string sql = "UPDATE [User] SET OnLineTime=GETDATE(),ActionTime=GETDATE() WHERE Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Id", id, DbType.Int32);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
       }

       /// <summary>
       /// 更新用户密码
       /// </summary>
       /// <param name="name"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static int UpdatePassword(string name,string password)
       {
           string sql = "UPDATE [User] SET Password=@Password WHERE Name=@Name";
           var par = new DynamicParameters();
           par.Add("@Name", name, DbType.String);
           par.Add("@Password", password, DbType.String);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
       }

       /// <summary>
       /// 更新用户密码
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static int UpdatePassword(int userId, string password)
       {
           string sql = "UPDATE [User] SET Password=@Password WHERE Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Id", userId, DbType.Int32);
           par.Add("@Password", password, DbType.String);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
       }
       /// <summary>
       /// 更新用户密码
       /// </summary>
       /// <param name="email"></param>
       /// <param name="password"></param>
       /// <returns></returns>
       public static int UpdatePasswordByEmail(string email, string password)
       {
           string sql = "UPDATE [User] SET Password=@Password WHERE Email=@Email";
           var par = new DynamicParameters();
           par.Add("@Email", email, DbType.String);
           par.Add("@Password", password, DbType.String);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
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
       /// <param name="userName">用户名或昵称</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>User列表</returns>
       public static List<UserInfo> GetUserPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime,string userName, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           string fieldShow = Fileds ;
           const string fieldOrder = "Id desc";
           string @where = string.IsNullOrEmpty(userName) ? "" : string.Format(" Name LIKE '%{0}%' OR  NickName LIKE '%{0}%'", userName);
          return Paging<UserInfo>.GetPageList("[User]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }

       /// <summary>
       /// 更新用户头像
       /// </summary>
       /// <param name="id"></param>
       /// <param name="avatar"></param>
       /// <returns></returns>
       public static int UpdateAvatar(int id, string avatar)
       {
           string sql = "UPDATE [User] SET Avatar=@Avatar WHERE Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Avatar", avatar, DbType.String);
           par.Add("@Id", id, DbType.Int32);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
       }

       #region 前端使用
       /// <summary>
       ///根据用户多个id获取多个用户信息
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public static List<UserVModel> GetUsersByIds(int[] ids)
       {
           if (ids == null || ids.Length == 0)
               return new List<UserVModel>();
           var par = new DynamicParameters();
           par.Add("@UserIds", string.Join(",", ids), DbType.String);
           return DapWrapper.InnerQueryProc<UserVModel>(DbConfig.ArticleManagerConnString, "proc_GetUsersByIds", par);
       }

       /// <summary>
       /// 修改UserVModel
       /// </summary>
       /// <param name="user"></param>
       /// <returns></returns>
       public static bool UpdateUser(UserVModel user)
       {
           string sql = @"UPDATE  [User] SET 
						NickName=@NickName,
						Sex=@Sex,
						Email=@Email,
                        EmailStatus=@EmailStatus
                        WHERE  Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Id", user.Id, DbType.Int32);
           par.Add("@NickName", user.NickName, DbType.String);
           par.Add("@Sex", user.Sex, DbType.Int16);
           par.Add("@EmailStatus", user.EmailStatus, DbType.Boolean);
           par.Add("@Email", user.Email, DbType.AnsiString);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par)>0;
       }

       /// <summary>
       /// 更新用户评论数
       /// </summary>
       /// <param name="id"></param>
       /// <param name="num">+1/-1</param>
       /// <returns></returns>
       public static int UpdateCommentNum(int id,int num)
       {
           const string sql = "UPDATE [User] SET CommentNum=CommentNum+@Num WHERE Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Id", id, DbType.Int32);
           par.Add("@Num", num, DbType.Int32);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
       }

       /// <summary>
       /// 更新用户文章数
       /// </summary>
       /// <param name="id"></param>
       /// <param name="num">+1/-1</param>
       /// <returns></returns>
       public static int UpdateArticleNum(int id, int num)
       {
           const string sql = "UPDATE [User] SET ArticleNum=ArticleNum+@Num WHERE Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Id", id, DbType.Int32);
           par.Add("@Num", num, DbType.Int32);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
       }

       /// <summary>
       /// 更新用户邮箱激活状态
       /// </summary>
       /// <param name="id"></param>
       /// <param name="emailStatus"></param>
       /// <returns></returns>
       public static bool UpdateEmailStatus(int id, bool emailStatus)
       {
           const string sql = "UPDATE [User] SET EmailStatus=@EmailStatus WHERE Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Id", id, DbType.Int32);
           par.Add("@EmailStatus", emailStatus, DbType.Boolean);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par)>0;
       }
       /// <summary>
       /// 用户近期评论榜
       /// </summary>
       /// <param name="days"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public static List<UserVModel> CommentHotTop(int days, int pageSize)
       {
                string sql = String.Format(@"SELECT TOP {0}
                [Id]
                ,[Name]
                ,[NickName]
                ,[Avatar]
                ,[Sex]
                ,comment.*
                FROM [dbo].[User] usr
                JOIN 
                (SELECT UserId,COUNT(1) CommentNum FROM [dbo].[Comment] 
                {1}
                GROUP BY UserId) comment 
                on usr.Id=comment.UserId", pageSize, days>=0?"WHERE DATEDIFF(DAY,CreateTime,GETDATE())<="+days:"");
                return DapWrapper.InnerQuerySql<UserVModel>(DbConfig.ArticleManagerConnString, sql);
       }

       #endregion

       /// <summary>
       /// 更新用户禁用状态
       /// </summary>
       /// <param name="id">用户id</param>
       /// <param name="disable">是否禁用</param>
       /// <param name="disableReason">原因</param>
       /// <returns></returns>
       public static bool UpdateDisable(int id, bool disable, string disableReason=null)
       {
           const string sql = "UPDATE [User] SET Disable=@Disable,DisableReason=@DisableReason WHERE Id=@Id";
           var par = new DynamicParameters();
           par.Add("@Id", id, DbType.Int32);
           par.Add("@Disable", disable, DbType.Boolean);
           par.Add("@DisableReason", disableReason, DbType.String);
           return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par) > 0;
       }
    }
}
