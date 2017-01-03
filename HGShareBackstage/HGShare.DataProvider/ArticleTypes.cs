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
    /// <summary>
    /// ArticleType 
    /// </summary>
    public class ArticleTypes
    {
        /// <summary>
        /// 添加ArticleTypeInfo
        /// </summary>
        /// <param name="articletype"></param>
        /// <returns></returns>
        public static int AddArticleType(ArticleTypeInfo articletype)
        {
            string sql = @"INSERT INTO [ArticleType]
			([Name],[PId],[Sort],[PinYin],[IsHomeMenu],[Ico],[IsShow])
			VALUES
			(@Name,@PId,@Sort,@PinYin,@IsHomeMenu,@Ico,@IsShow) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@Name", articletype.Name, DbType.AnsiString);
            par.Add("@PId", articletype.PId, DbType.Int32);
            par.Add("@Sort", articletype.Sort, DbType.Int32);
            par.Add("@PinYin", articletype.PinYin, DbType.AnsiString);
            par.Add("@IsHomeMenu", articletype.IsHomeMenu, DbType.Boolean);
            par.Add("@Ico", articletype.Ico, DbType.AnsiString);
            par.Add("@IsShow", articletype.IsShow, DbType.Boolean);
            return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 修改ArticleTypeInfo
        /// </summary>
        /// <param name="articletype"></param>
        /// <returns></returns>
        public static int UpdateArticleType(ArticleTypeInfo articletype)
        {
            string sql = @"UPDATE  [ArticleType] SET 
						Name=@Name,
						PId=@PId,
						Sort=@Sort,
						PinYin=@PinYin,
						IsHomeMenu=@IsHomeMenu,
						Ico=@Ico,
                        IsShow=@IsShow
 WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", articletype.Id, DbType.Int32);
            par.Add("@Name", articletype.Name, DbType.AnsiString);
            par.Add("@PId", articletype.PId, DbType.Int32);
            par.Add("@Sort", articletype.Sort, DbType.Int32);
            par.Add("@PinYin", articletype.PinYin, DbType.AnsiString);
            par.Add("@IsHomeMenu", articletype.IsHomeMenu, DbType.Boolean);
            par.Add("@Ico", articletype.Ico, DbType.AnsiString);
            par.Add("@IsShow", articletype.IsShow, DbType.Boolean);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据id获取ArticleTypeInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArticleTypeInfo GetArticleTypeInfo(int id)
        {
            string sql = "select [Id],[Name],[PId],[Sort],[PinYin],[IsHomeMenu],[CreateTime],[Ico],[IsShow] FROM [ArticleType] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerQuerySql<ArticleTypeInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
        }
        /// <summary>
        /// 根据id删除ArticleType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteArticleType(int id)
        {
            string sql = "DELETE [ArticleType] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据ids删除ArticleType多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteArticleTypes(int[] ids)
        {
            string sql = "DELETE [ArticleType] WHERE Id IN (" + string.Join(",", ids) + ")";
            return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 获取ArticleType分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>ArticleType列表</returns>
        public static List<ArticleTypeInfo> GetArticleTypePageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
        {
            recordCount = 0;
            var par = new DynamicParameters();
            par.Add("@PageIndex", pageIndex, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@BeginTime", beginTime, DbType.DateTime);
            par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
            par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
            var result = DapWrapper.InnerQueryProc<ArticleTypeInfo>(DbConfig.ArticleManagerConnString, "proc_GetArticleTypePageList", par);
            recordCount = par.Get<int>("@TotalCount");
            return result;
        }
        /// <summary>
        /// 获取ArticleType分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <returns>ArticleType列表</returns>
        public static List<ArticleTypeInfo> GetArticleTypePageList(int pageIndex, int pageSize, DateTime? beginTime,
            DateTime? endTime, out int pageCount, out int count)
        {
            const string fieldKey = "Id";
            const string fieldShow = "[Id],[Name],[PId],[Sort],[PinYin],dbo.GetTypeNameByTypeId(PId) PName,[IsHomeMenu],[CreateTime],[Ico],[IsShow]";
            const string fieldOrder = "Id desc";
            const string @where = "";
            return Paging<ArticleTypeInfo>.GetPageList("[ArticleType]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
        }
        /// <summary>
        /// 获取所有ArticleType
        /// </summary>
        /// <returns></returns>
        public static List<ArticleTypeInfo> GetAllArticleType()
        {
            string sql = "select [Id],[Name],[PId],[Sort],[PinYin],[IsHomeMenu],[CreateTime],[Ico],[IsShow] FROM [ArticleType]";
            return DapWrapper.InnerQuerySql<ArticleTypeInfo>(DbConfig.ArticleManagerConnString, sql);
        }

        #region 前端用
        /// <summary>
        /// 根据 IsHomeMenu 查询文章分类 按Sort 正排序，时间倒序
        /// </summary>
        /// <param name="isHomeMenu"></param>
        /// <returns></returns>
        public static List<ArticleTypeVModel> GetArticleTypesByIsHomeMenu(bool isHomeMenu)
        {
            const string sql = @"SELECT  [Id],[Name],[PId],[Sort],[PinYin],[IsHomeMenu],[CreateTime],[Ico],[IsShow]  FROM [dbo].[ArticleType]  WHERE IsHomeMenu=@IsHomeMenu ORDER BY Sort ASC,CreateTime DESC";
            var par = new DynamicParameters();
            par.Add("@IsHomeMenu", isHomeMenu, DbType.Boolean);
            var result = DapWrapper.InnerQuerySql<ArticleTypeVModel>(DbConfig.ArticleManagerConnString, sql, par);

            return result;
        }
        /// <summary>
        /// 根据 isShow 查询文章分类 按Sort 正排序，时间倒序
        /// </summary>
        /// <param name="isShow"></param>
        /// <returns></returns>
        public static List<ArticleTypeInfo> GetArticleTypesByIsShow(bool isShow)
        {
            const string sql = @"SELECT  [Id],[Name],[PId],[Sort],[PinYin],[IsHomeMenu],[CreateTime],[Ico],[IsShow]  FROM [dbo].[ArticleType]  WHERE IsShow=@IsShow ORDER BY Sort ASC,CreateTime DESC";
            var par = new DynamicParameters();
            par.Add("@IsShow", isShow, DbType.Boolean);
            var result = DapWrapper.InnerQuerySql<ArticleTypeInfo>(DbConfig.ArticleManagerConnString, sql, par);

            return result;
        }
        #endregion
    }
}
