using System;
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
            par.Add("@MId", rolemodul.MId, DbType.Int32);
            par.Add("@RId", rolemodul.RId, DbType.Int32);
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
            par.Add("@MId", rolemodul.MId, DbType.Int32);
            par.Add("@RId", rolemodul.RId, DbType.Int32);
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
            string sql = "DELETE [Role_Modul] WHERE Id=@Id";
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
            string sql = "DELETE [Role_Modul] WHERE Id IN (" + string.Join(",", ids) + ")";
            return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 根据rid删除RoleModul多条记录
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static Int32 DeleteRoleModuls(int rid)
        {
            string sql = "DELETE [Role_Modul] WHERE RId=@RId";
            var par = new DynamicParameters();
            par.Add("@RId", rid, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据角色id获取已有模块id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetMIds(int id)
        {
            string sql = "select [MId] FROM [Role_Modul] WHERE RId=@RId";
            var par = new DynamicParameters();
            par.Add("@RId", id, DbType.Int32);
            return DapWrapper.InnerQuerySql<int>(DbConfig.ArticleManagerConnString, sql, par).ToArray();
        }
    }
}
