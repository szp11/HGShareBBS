using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace HGShare.DataProvider.DapperHelper
{
    /// <summary>
    /// Dapper数据操作
    /// </summary>
    public class DapWrapper
    {
        /// <summary>
        /// 取数据，执行存储过程，带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <param name="procParams"></param>
        /// <returns></returns>
        public static List<T> InnerQueryProc<T>(String connString, String proc, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.Query<T>(proc, procParams, commandType: CommandType.StoredProcedure).ToList<T>();
            }

        }
        /// <summary>
        /// 取数据，执行sql，带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql"></param>
        /// <param name="procParams"></param>
        /// <returns></returns>
        public static List<T> InnerQuerySql<T>(String connString, String sql, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.Query<T>(sql, procParams, commandType: CommandType.Text).ToList<T>();
            }
        }
        /// <summary>
        /// 取数据，执行存储过程，无参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <returns></returns>
        public static List<T> InnerQueryProc<T>(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.Query<T>(proc, commandType: CommandType.StoredProcedure).ToList<T>();
            }
        }
        /// <summary>
        /// 取数据，执行sql，无参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> InnerQuerySql<T>(String connString, String sql)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.Query<T>(sql, commandType: CommandType.Text).ToList<T>();
            }
        }
        
        /// <summary>
        /// 取数据，返回多个结果集的读取方式
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc">存储过程名</param>
        /// <param name="procParams">参数</param>
        /// <param name="readResult">对结果集的处理函数</param>
        /// <returns></returns>
        public static T InnerQueryMultipleProc<T>(String connString, String proc, DynamicParameters procParams, Func<SqlMapper.GridReader, T> readResult)
        {
            IDbConnection conn = null;
            SqlMapper.GridReader reader = null;

            try
            {
                conn = new SqlConnection(connString);

                reader = conn.QueryMultiple(proc, procParams, commandType: CommandType.StoredProcedure);

                return readResult(reader);

            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
                if (conn != null)
                    conn.Dispose();
            }


        }

        /// <summary>
        /// 查询操作，无参数，返回第一行第一列结果
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc">读取的存储过程</param>
        /// <returns></returns>
        public static T InnerQueryScalarProc<T>(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.ExecuteScalar<T>(proc, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 查询操作，需参数，返回第一行第一列结果
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc">读取的存储过程</param>
        /// <param name="procParams">参数</param>
        /// <returns></returns>
        public static T InnerQueryScalarProc<T>(String connString, String proc, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.ExecuteScalar<T>(proc, procParams, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// 查询操作，需参数，返回第一行第一列结果
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql">读取的sql语句</param>
        /// <param name="procParams">参数</param>
        /// <returns></returns>
        public static T InnerQueryScalarSql<T>(String connString, String sql, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.ExecuteScalar<T>(sql, procParams, commandType: CommandType.Text);
            }
        }
        /// <summary>
        /// 执行更新数据库的操作，返回操作结果
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc">更新数据的存储过程</param>
        /// <returns>操作结果：1成功0失败</returns>
        public static Int32 InnerExecuteScalarProc(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.ExecuteScalar<Int32>(proc, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 执行PROC，返回影响行数
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <returns></returns>
        public static Int32 InnerExecuteProc(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.Execute(proc, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 执行SQL，返回影响行数
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数</param>
        /// <returns></returns>
        public static Int32 InnerExecuteSql(String connString, String sql, DynamicParameters sqlParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.Execute(sql, sqlParams, commandType: CommandType.Text);
            }
        }


        /// <summary>
        /// 执行PROC，返回影响行数
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <param name="procParams">参数</param>
        /// <returns></returns>
        public static Int32 InnerExecuteProc(String connString, String proc, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.Execute(proc, procParams, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// 执行SQL，返回影响行数
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static Int32 InnerExecuteText(String connString, String sql)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.Execute(sql, commandType: CommandType.Text);
            }
        }
        /// <summary>
        /// 执行存储过程，无参数,长时间 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <returns></returns>
        public static List<T> InnerQueryLongTimeProc<T>(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return conn.Query<T>(proc, commandType: CommandType.StoredProcedure, commandTimeout: 600).ToList<T>();
            }
        }


        #region 
        /// <summary>
        /// 取数据，执行存储过程，带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <param name="procParams"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<T>> InnerQueryProcAsync<T>(String connString, String proc, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.QueryAsync<T>(proc, procParams, commandType: CommandType.StoredProcedure);
            }

        }
        /// <summary>
        /// 取数据，执行sql，带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql"></param>
        /// <param name="procParams"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<T>> InnerQuerySqlAsync<T>(String connString, String sql, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.QueryAsync<T>(sql, procParams, commandType: CommandType.Text);
            }
        }
        /// <summary>
        /// 取数据，执行存储过程，无参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<T>> InnerQueryProcAsync<T>(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.QueryAsync<T>(proc, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// 取数据，执行sql，无参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<T>> InnerQuerySqlAsync<T>(String connString, String sql)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.QueryAsync<T>(sql, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取数据，返回多个结果集的读取方式
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc">存储过程名</param>
        /// <param name="procParams">参数</param>
        /// <param name="readResult">对结果集的处理函数</param>
        /// <returns></returns>
        public async static Task<T> InnerQueryMultipleProcAsync<T>(String connString, String proc, DynamicParameters procParams, Func<Task<SqlMapper.GridReader>, Task<T>> readResult)
        {
            IDbConnection conn = null;
            Task<SqlMapper.GridReader> reader = null;

            try
            {
                conn = new SqlConnection(connString);

                reader = conn.QueryMultipleAsync(proc, procParams, commandType: CommandType.StoredProcedure);

                return await readResult(reader);

            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
                if (conn != null)
                    conn.Dispose();
            }


        }

        /// <summary>
        /// 查询操作，无参数，返回第一行第一列结果
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc">读取的存储过程</param>
        /// <returns></returns>
        public async static Task<T> InnerQueryScalarProcAsync<T>(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.ExecuteScalarAsync<T>(proc, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 查询操作，需参数，返回第一行第一列结果
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc">读取的存储过程</param>
        /// <param name="procParams">参数</param>
        /// <returns></returns>
        public async static Task<T> InnerQueryScalarProcAsync<T>(String connString, String proc, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.ExecuteScalarAsync<T>(proc, procParams, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// 查询操作，需参数，返回第一行第一列结果
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql">读取的sql语句</param>
        /// <param name="procParams">参数</param>
        /// <returns></returns>
        public async static Task<T> InnerQueryScalarSqlAsync<T>(String connString, String sql, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.ExecuteScalarAsync<T>(sql, procParams, commandType: CommandType.Text);
            }
        }
        /// <summary>
        /// 执行更新数据库的操作，返回操作结果
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc">更新数据的存储过程</param>
        /// <returns>操作结果：1成功0失败</returns>
        public async static Task<Int32> InnerExecuteScalarProcAsync(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.ExecuteScalarAsync<Int32>(proc, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 执行PROC，返回影响行数
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <returns></returns>
        public async static Task<Int32> InnerExecuteProcAsync(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.ExecuteAsync(proc, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 执行SQL，返回影响行数
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数</param>
        /// <returns></returns>
        public async static Task<Int32> InnerExecuteSqlAsync(String connString, String sql, DynamicParameters sqlParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.ExecuteAsync(sql, sqlParams, commandType: CommandType.Text);
            }
        }


        /// <summary>
        /// 执行PROC，返回影响行数
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <param name="procParams">参数</param>
        /// <returns></returns>
        public async static Task<Int32> InnerExecuteProcAsync(String connString, String proc, DynamicParameters procParams)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.ExecuteAsync(proc, procParams, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// 执行SQL，返回影响行数
        /// </summary>
        /// <param name="connString">链接字符串</param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async static Task<Int32> InnerExecuteTextAsync(String connString, String sql)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.ExecuteAsync(sql, commandType: CommandType.Text);
            }
        }
        /// <summary>
        /// 执行存储过程，无参数,长时间 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString">链接字符串</param>
        /// <param name="proc"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<T>> InnerQueryLongTimeProcAsync<T>(String connString, String proc)
        {
            using (IDbConnection conn = new SqlConnection(connString))
            {
                return await conn.QueryAsync<T>(proc, commandType: CommandType.StoredProcedure, commandTimeout: 600);
            }
        }

        #endregion
    }
}
