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
    /// Area 
    /// </summary>
    public class Areas
    {

		/// <summary>
		/// 添加AreaInfo
		/// </summary>
		/// <param name="area"></param>
		/// <returns></returns>
		public static int AddArea(AreaInfo area)
		{
			string sql = @"INSERT INTO [Area]
			([Name],[Code],[PinYin],[SortPinYin],[Sort],[ParentCode])
			VALUES
			(@Name,@Code,@PinYin,@SortPinYin,@Sort,@ParentCode) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@Name",area.Name , DbType.String);
			par.Add("@Code",area.Code , DbType.Int32);
			par.Add("@PinYin",area.PinYin , DbType.AnsiString);
			par.Add("@SortPinYin",area.SortPinYin , DbType.AnsiString);
			par.Add("@Sort",area.Sort , DbType.AnsiString);
			par.Add("@ParentCode",area.ParentCode , DbType.Int32);
			return DapWrapper.InnerQueryScalarSql<int>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改AreaInfo
		/// </summary>
		/// <param name="area"></param>
		/// <returns></returns>
		public static int UpdateArea(AreaInfo area)
		{
			string sql = @"UPDATE  [Area] SET 
						Name=@Name,
						Code=@Code,
						PinYin=@PinYin,
						SortPinYin=@SortPinYin,
						Sort=@Sort,
						ParentCode=@ParentCode
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", area.Id, DbType.Int32);
			par.Add("@Name",area.Name , DbType.String);
			par.Add("@Code",area.Code , DbType.Int32);
			par.Add("@PinYin",area.PinYin , DbType.AnsiString);
			par.Add("@SortPinYin",area.SortPinYin , DbType.AnsiString);
			par.Add("@Sort",area.Sort , DbType.AnsiString);
			par.Add("@ParentCode",area.ParentCode , DbType.Int32);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取AreaInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static AreaInfo GetAreaInfo(int id)
		{
			string sql = "select [Id],[Name],[Code],[PinYin],[SortPinYin],[Sort],[ParentCode] FROM [Area] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerQuerySql<AreaInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除Area
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteArea(int id)
		{
			string sql="DELETE [Area] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int32);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除Area多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteAreas(int[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [Area] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取Area分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Area列表</returns>
       public static List<AreaInfo> GetAreaPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<AreaInfo>(DbConfig.ArticleManagerConnString, "proc_GetAreaPageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取Area分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Area列表</returns>
       public static List<AreaInfo> GetAreaPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[Name],[Code],[PinYin],[SortPinYin],[Sort],[ParentCode]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<AreaInfo>.GetPageList("[Area]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
