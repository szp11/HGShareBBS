using System.Collections.Generic;
using System.Data;
using Dapper;
using HGShare.DataProvider.DapperHelper;

namespace HGShare.DataProvider
{
    /// <summary>
    /// 单表分页
    /// </summary>
    public static class Paging<T>
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldKey">主键</param>
        /// <param name="fieldShow">显示字段</param>
        /// <param name="fieldOrder">排序</param>
        /// <param name="where">条件</param>
        /// <param name="pageCurrent">页码</param>
        /// <param name="pageSize">页码大小</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns></returns>
        public static IList<T> GetPageList(string tableName, string fieldKey, string fieldShow, string fieldOrder, string where, int pageCurrent, int pageSize, out int pageCount, out int count)
        {
            pageCount = 0;
            count = 0;
            var par = new DynamicParameters();
            par.Add("@tbname", tableName, DbType.String);
            par.Add("@FieldKey", fieldKey, DbType.String);
            par.Add("@PageCurrent", pageCurrent, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@FieldShow", fieldShow, DbType.String);
            par.Add("@FieldOrder", fieldOrder, DbType.String);
            par.Add("@Where", where, DbType.String);
            par.Add("@PageCount", pageCount, DbType.Int32, ParameterDirection.Output);
            par.Add("@Count", count, DbType.Int32, ParameterDirection.Output);
            var result = DapWrapper.InnerQueryProc<T>(DbConfig.ArticleManagerConnString, "sp_PageView", par);
            pageCount = par.Get<int>("@PageCount");
            count = par.Get<int>("@Count");
            return result;
        }
    }
}
