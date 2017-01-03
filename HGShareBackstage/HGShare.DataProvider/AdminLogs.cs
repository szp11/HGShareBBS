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
    /// AdminLog 
    /// </summary>
    public class AdminLogs
    {

        /// <summary>
        /// 添加AdminLogInfo
        /// </summary>
        /// <param name="adminlog"></param>
        /// <returns></returns>
        public static int AddAdminLog(AdminLogInfo adminlog)
        {
            string sql = @"INSERT INTO [AdminLog]
			([UserId],[Controllers],[Action],[Parameter],[ActionId],[Ip],[Url],[InTime],[Method],[IsAjax],[UserAgent],[ControllersDsc],[ActionDsc])
			VALUES
			(@UserId,@Controllers,@Action,@Parameter,@ActionId,@Ip,@Url,@InTime,@Method,@IsAjax,@UserAgent,@ControllersDsc,@ActionDsc) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@UserId", adminlog.UserId, DbType.Int64);
            par.Add("@Controllers", adminlog.Controllers, DbType.AnsiString);
            par.Add("@Action", adminlog.Action, DbType.AnsiString);
            par.Add("@Parameter", adminlog.Parameter, DbType.AnsiString);
            par.Add("@ActionId", adminlog.ActionId, DbType.AnsiString);
            par.Add("@Ip", adminlog.Ip, DbType.AnsiString);
            par.Add("@Url", adminlog.Url, DbType.AnsiString);
            par.Add("@InTime", adminlog.InTime, DbType.DateTime);
            par.Add("@Method", adminlog.Method, DbType.AnsiString);
            par.Add("@IsAjax", adminlog.IsAjax, DbType.Int32);
            par.Add("@UserAgent", adminlog.UserAgent, DbType.AnsiString);
            par.Add("@ControllersDsc", adminlog.ControllersDsc, DbType.String);
            par.Add("@ActionDsc", adminlog.ActionDsc, DbType.String);
            return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 修改AdminLogInfo
        /// </summary>
        /// <param name="adminlog"></param>
        /// <returns></returns>
        public static int UpdateAdminLog(AdminLogInfo adminlog)
        {
            string sql = @"UPDATE  [AdminLog] SET 
						UserId=@UserId,
						Controllers=@Controllers,
						Action=@Action,
						Parameter=@Parameter,
						ActionId=@ActionId,
						Ip=@Ip,
						Url=@Url,
						InTime=@InTime,
						Method=@Method,
						IsAjax=@IsAjax,
						UserAgent=@UserAgent,
						ControllersDsc=@ControllersDsc,
						ActionDsc=@ActionDsc
 WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", adminlog.Id, DbType.Int32);
            par.Add("@UserId", adminlog.UserId, DbType.Int64);
            par.Add("@Controllers", adminlog.Controllers, DbType.AnsiString);
            par.Add("@Action", adminlog.Action, DbType.AnsiString);
            par.Add("@Parameter", adminlog.Parameter, DbType.AnsiString);
            par.Add("@ActionId", adminlog.ActionId, DbType.AnsiString);
            par.Add("@Ip", adminlog.Ip, DbType.AnsiString);
            par.Add("@Url", adminlog.Url, DbType.AnsiString);
            par.Add("@InTime", adminlog.InTime, DbType.DateTime);
            par.Add("@Method", adminlog.Method, DbType.AnsiString);
            par.Add("@IsAjax", adminlog.IsAjax, DbType.Int32);
            par.Add("@UserAgent", adminlog.UserAgent, DbType.AnsiString);
            par.Add("@ControllersDsc", adminlog.ControllersDsc, DbType.String);
            par.Add("@ActionDsc", adminlog.ActionDsc, DbType.String);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据id获取AdminLogInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AdminLogInfo GetAdminLogInfo(int id)
        {
            string sql = "select [Id],[UserId],[Controllers],[Action],[Parameter],[ActionId],[Ip],[Url],[InTime],[Method],[IsAjax],[UserAgent],[ControllersDsc],[ActionDsc] FROM [AdminLog] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerQuerySql<AdminLogInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
        }
        /// <summary>
        /// 根据id删除AdminLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteAdminLog(int id)
        {
            string sql = "DELETE [AdminLog] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据ids删除AdminLog多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteAdminLogs(int[] ids)
        {
            string sql = "DELETE [AdminLog] WHERE Id IN (" + string.Join(",", ids) + ")";
            return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 获取AdminLog分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>AdminLog列表</returns>
        public static List<AdminLogInfo> GetAdminLogPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            recordCount = 0;
            var par = new DynamicParameters();
            par.Add("@PageIndex", pageIndex, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@BeginTime", beginTime, DbType.DateTime);
            par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
            par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
            var result = DapWrapper.InnerQueryProc<AdminLogInfo>(DbConfig.ArticleManagerConnString, "proc_GetAdminLogPageList", par);
            recordCount = par.Get<int>("@TotalCount");
            return result;
        }
        /// <summary>
        /// 获取AdminLog分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>AdminLog列表</returns>
        public static List<AdminLogInfo> GetAdminLogPageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            const string fieldKey = "Id";
            const string fieldShow = "[Id],[UserId],[Controllers],[Action],[Parameter],[ActionId],[Ip],[Url],[InTime],[Method],[IsAjax],[UserAgent],[ControllersDsc],[ActionDsc]";
            const string fieldOrder = "Id desc";
            const string @where = "";
            return Paging<AdminLogInfo>.GetPageList("[AdminLog]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
        }
    }
}
