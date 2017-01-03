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
    /// RoleModul 
    /// </summary>
    public class RoleModuls
    {

		/// <summary>
		/// 添加RoleModulInfo
		/// </summary>
		/// <param name="rolemodul"></param>
		/// <returns></returns>
		public static int AddRoleModul(RoleModulInfo rolemodul)
		{
			string sql = @"INSERT INTO [Role_Modul]
			([MId],[RId])
			VALUES
			(@MId,@RId) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@MId",rolemodul.MId , DbType.Int32);
			par.Add("@RId",rolemodul.RId , DbType.Int32);
			return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改RoleModulInfo
		/// </summary>
		/// <param name="rolemodul"></param>
		/// <returns></returns>
		public static int UpdateRoleModul(RoleModulInfo rolemodul)
		{
			string sql = @"UPDATE  [Role_Modul] SET 
						MId=@MId,
						RId=@RId
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", rolemodul.Id, DbType.Int32);
			par.Add("@MId",rolemodul.MId , DbType.Int32);
			par.Add("@RId",rolemodul.RId , DbType.Int32);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取RoleModulInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static RoleModulInfo GetRoleModulInfo(int id)
		{
			string sql = "select [Id],[MId],[RId] FROM [Role_Modul] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerQuerySql<RoleModulInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除RoleModul
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteRoleModul(int id)
		{
			string sql="DELETE [Role_Modul] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除RoleModul多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteRoleModuls(int[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [Role_Modul] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取RoleModul分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>RoleModul列表</returns>
       public static List<RoleModulInfo> GetRoleModulPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<RoleModulInfo>(DbConfig.ArticleManagerConnString, "proc_GetRoleModulPageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取RoleModul分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>RoleModul列表</returns>
       public static List<RoleModulInfo> GetRoleModulPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[MId],[RId]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<RoleModulInfo>.GetPageList("[Role_Modul]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
