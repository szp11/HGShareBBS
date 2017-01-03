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
    /// Role 
    /// </summary>
    public class Roles
    {

		/// <summary>
		/// 添加RoleInfo
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
		public static int AddRole(RoleInfo role)
		{
			string sql = @"INSERT INTO [Role]
			([RName],[CreateTime],[IsDel],[Description],[IsSuper],[ArticleNeedVerified],[CommentNeedVerified])
			VALUES
			(@RName,@CreateTime,@IsDel,@Description,@IsSuper,@ArticleNeedVerified,@CommentNeedVerified) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@RName",role.RName , DbType.AnsiString);
			par.Add("@CreateTime",role.CreateTime , DbType.DateTime);
			par.Add("@IsDel",role.IsDel , DbType.Boolean);
			par.Add("@Description",role.Description , DbType.String);
			par.Add("@IsSuper",role.IsSuper , DbType.Boolean);
			par.Add("@ArticleNeedVerified",role.ArticleNeedVerified , DbType.Boolean);
			par.Add("@CommentNeedVerified",role.CommentNeedVerified , DbType.Boolean);
			return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改RoleInfo
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
		public static int UpdateRole(RoleInfo role)
		{
			string sql = @"UPDATE  [Role] SET 
						RName=@RName,
						CreateTime=@CreateTime,
						IsDel=@IsDel,
						Description=@Description,
						IsSuper=@IsSuper,
						ArticleNeedVerified=@ArticleNeedVerified,
						CommentNeedVerified=@CommentNeedVerified
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", role.Id, DbType.Int32);
			par.Add("@RName",role.RName , DbType.AnsiString);
			par.Add("@CreateTime",role.CreateTime , DbType.DateTime);
			par.Add("@IsDel",role.IsDel , DbType.Boolean);
			par.Add("@Description",role.Description , DbType.String);
			par.Add("@IsSuper",role.IsSuper , DbType.Boolean);
			par.Add("@ArticleNeedVerified",role.ArticleNeedVerified , DbType.Boolean);
			par.Add("@CommentNeedVerified",role.CommentNeedVerified , DbType.Boolean);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取RoleInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static RoleInfo GetRoleInfo(int id)
		{
			string sql = "select [Id],[RName],[CreateTime],[IsDel],[Description],[IsSuper],[ArticleNeedVerified],[CommentNeedVerified] FROM [Role] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerQuerySql<RoleInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除Role
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteRole(int id)
		{
			string sql="DELETE [Role] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除Role多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteRoles(int[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [Role] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取Role分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Role列表</returns>
       public static List<RoleInfo> GetRolePageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<RoleInfo>(DbConfig.ArticleManagerConnString, "proc_GetRolePageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取Role分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Role列表</returns>
       public static List<RoleInfo> GetRolePageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[RName],[CreateTime],[IsDel],[Description],[IsSuper],[ArticleNeedVerified],[CommentNeedVerified]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<RoleInfo>.GetPageList("[Role]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
