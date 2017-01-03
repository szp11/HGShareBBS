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
    /// 模块
    /// </summary>
    public class Moduls
    {
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public static List<ModulInfo> GetAllModul()
        {
            string sql = "SELECT [Id],[ModulName],[Controller],[Action],[Description],[CreateTime],[PId],[OrderId],[IsShow],[Priority],[IsDisplay],[Ico] FROM [dbo].[Modul]";
            return DapWrapper.InnerQuerySql<ModulInfo>(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 根据角色id获取所有启用的模块
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static List<ModulInfo> GetModulsByRoleId(int roleId)
        {
            string sql = @"SELECT m.[Id]
                          ,m.[ModulName]
                          ,m.[Controller]
                          ,m.[Action]
                          ,m.[Description]
                          ,m.[CreateTime]
                          ,m.[PId]
                          ,m.[OrderId]
                          ,m.[IsShow]
                          ,m.[Priority]
                          ,m.[IsDisplay]
                          ,m.[Ico]
                      FROM [dbo].[Modul] m
                      JOIN [dbo].[Role_Modul] rm on m.Id=rm.MId
                      WHERE rm.RId=@RId AND m.IsShow=1";
            var par = new DynamicParameters();
            par.Add("@RId", roleId, DbType.Int32);
            return DapWrapper.InnerQuerySql<ModulInfo>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 添加ModulInfo
        /// </summary>
        /// <param name="modul"></param>
        /// <returns></returns>
        public static int AddModul(ModulInfo modul)
        {
            string sql = @"INSERT INTO Modul
			([ModulName],[Controller],[Action],[Description],[PId],[OrderId],[IsShow],[Priority],[IsDisplay],[Ico])
			VALUES
			(@ModulName,@Controller,@Action,@Description,@PId,@OrderId,@IsShow,@Priority,@IsDisplay,@Ico) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@ModulName", modul.ModulName, DbType.String);
            par.Add("@Controller", modul.Controller, DbType.AnsiString);
            par.Add("@Action", modul.Action, DbType.AnsiString);
            par.Add("@Description", modul.Description, DbType.String);
            par.Add("@PId", modul.PId, DbType.Int32);
            par.Add("@OrderId", modul.OrderId, DbType.Int32);
            par.Add("@IsShow", modul.IsShow, DbType.Boolean);
            par.Add("@Priority", modul.Priority, DbType.Int32);
            par.Add("@IsDisplay", modul.IsDisplay, DbType.Boolean);
            par.Add("@Ico", modul.Ico, DbType.AnsiString);
            return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
        }

        /// <summary>
        /// 修改ModulInfo
        /// </summary>
        /// <param name="modul"></param>
        /// <returns></returns>
        public static int UpdateModul(ModulInfo modul)
        {
            string sql = @"UPDATE  [Modul] SET 
						ModulName=@ModulName,
						Controller=@Controller,
						Action=@Action,
						Description=@Description,
						PId=@PId,
						OrderId=@OrderId,
						IsShow=@IsShow,
						Priority=@Priority,
						IsDisplay=@IsDisplay,
						Ico=@Ico
 WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", modul.Id, DbType.Int32);
            par.Add("@ModulName", modul.ModulName, DbType.String);
            par.Add("@Controller", modul.Controller, DbType.AnsiString);
            par.Add("@Action", modul.Action, DbType.AnsiString);
            par.Add("@Description", modul.Description, DbType.String);
            par.Add("@PId", modul.PId, DbType.Int32);
            par.Add("@OrderId", modul.OrderId, DbType.Int32);
            par.Add("@IsShow", modul.IsShow, DbType.Boolean);
            par.Add("@Priority", modul.Priority, DbType.Int32);
            par.Add("@IsDisplay", modul.IsDisplay, DbType.Boolean);
            par.Add("@Ico", modul.Ico, DbType.AnsiString);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据id获取ModulInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ModulInfo GetModulInfo(int id)
        {
            string sql = "select [Id],[ModulName],[Controller],[Action],[Description],[CreateTime],[PId],[OrderId],[IsShow],[Priority],[IsDisplay],[Ico] FROM [Modul] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerQuerySql<ModulInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
        }
        /// <summary>
        /// 根据id删除Modul
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteModul(int id)
        {
            string sql = "DELETE [Modul] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据ids删除Modul多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteModuls(int[] ids)
        {
            string sql = "DELETE [Modul] WHERE Id IN (" + string.Join(",", ids) + ")";
            return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 获取Modul分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>Modul列表</returns>
        public static List<ModulInfo> GetModulPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            recordCount = 0;
            var par = new DynamicParameters();
            par.Add("@PageIndex", pageIndex, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@BeginTime", beginTime, DbType.DateTime);
            par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
            par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
            var result = DapWrapper.InnerQueryProc<ModulInfo>(DbConfig.ArticleManagerConnString, "proc_GetModulPageList", par);
            recordCount = par.Get<int>("@TotalCount");
            return result;
        }
        /// <summary>
        /// 获取Modul分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>Modul列表</returns>
        public static List<ModulInfo> GetModulPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            const string fieldKey = "Id";
            const string fieldShow = "[Id],[ModulName],[Controller],[Action],[Description],[CreateTime],[PId],[OrderId],[IsShow],[Priority],[IsDisplay],[Ico]";
            const string fieldOrder = "Id desc";
            const string @where = "";
            return Paging<ModulInfo>.GetPageList("[Modul]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
        }
        /// <summary>
        /// 根据父级生成排序值（排序值只会越来越大，越大的越靠前）
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <param name="id">编辑时可排除自身所占位置</param>
        /// <returns></returns>
        public static int GetOrderNumber(int pid,int? id)
        {
            string sql = "select ISNULL(MAX(OrderId),0) from [Modul] WHERE PId=@PId";
            var par = new DynamicParameters();
            par.Add("@PId", pid, DbType.Int32);
            if (id.HasValue)
            {
                sql = sql + " AND Id!=@Id ";
                par.Add("@Id",id.Value,DbType.Int32);
            }
            return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par)+1;
        }
    }
}
