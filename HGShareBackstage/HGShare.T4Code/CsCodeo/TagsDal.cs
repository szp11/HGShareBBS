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
    /// Tag 
    /// </summary>
    public class Tags
    {

		/// <summary>
		/// 添加TagInfo
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static long AddTag(TagInfo tag)
		{
			string sql = @"INSERT INTO [Tag]
			([Tag],[AId],[State],[Direction],[CreateTime],[UserId])
			VALUES
			(@Tag,@AId,@State,@Direction,@CreateTime,@UserId) 
			SELECT SCOPE_IDENTITY()
			";
			var par = new DynamicParameters();
			par.Add("@Tag",tag.Tag , DbType.String);
			par.Add("@AId",tag.AId , DbType.Int64);
			par.Add("@State",tag.State , DbType.Int16);
			par.Add("@Direction",tag.Direction , DbType.String);
			par.Add("@CreateTime",tag.CreateTime , DbType.DateTime);
			par.Add("@UserId",tag.UserId , DbType.Int32);
			return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 修改TagInfo
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static int UpdateTag(TagInfo tag)
		{
			string sql = @"UPDATE  [Tag] SET 
						Tag=@Tag,
						AId=@AId,
						State=@State,
						Direction=@Direction,
						CreateTime=@CreateTime,
						UserId=@UserId
 WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", tag.Id, DbType.Int64);
			par.Add("@Tag",tag.Tag , DbType.String);
			par.Add("@AId",tag.AId , DbType.Int64);
			par.Add("@State",tag.State , DbType.Int16);
			par.Add("@Direction",tag.Direction , DbType.String);
			par.Add("@CreateTime",tag.CreateTime , DbType.DateTime);
			par.Add("@UserId",tag.UserId , DbType.Int32);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据id获取TagInfo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static TagInfo GetTagInfo(long id)
		{
			string sql = "select [Id],[Tag],[AId],[State],[Direction],[CreateTime],[UserId] FROM [Tag] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerQuerySql<TagInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
		}
		/// <summary>
		/// 根据id删除Tag
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Int32 DeleteTag(long id)
		{
			string sql="DELETE [Tag] WHERE Id=@Id";
			var par = new DynamicParameters();
			par.Add("@Id", id, DbType.Int64);
			return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
		}
		/// <summary>
		/// 根据ids删除Tag多条记录
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public static Int32 DeleteTags(long[] ids)
		{
			if (ids.Length == 0)
                return 0;
			string sql="DELETE [Tag] WHERE Id IN ("+string.Join(",",ids)+")";
			return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
		}
		/// <summary>
       /// 获取Tag分页列表(自定义存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="recordCount">总记录数</param>
       /// <returns>Tag列表</returns>
       public static List<TagInfo> GetTagPageList(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, out int recordCount)
       {
           recordCount = 0;
           var par = new DynamicParameters();
           par.Add("@PageIndex", pageIndex, DbType.Int32);
           par.Add("@PageSize", pageSize, DbType.Int32);
           par.Add("@BeginTime", beginTime, DbType.DateTime);
           par.Add("@EndTime", !endTime.HasValue ? endTime : endTime.Value.AddDays(1).AddMilliseconds(-1), DbType.DateTime);
           par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
           var result = DapWrapper.InnerQueryProc<TagInfo>(DbConfig.ArticleManagerConnString, "proc_GetTagPageList", par);
           recordCount = par.Get<int>("@TotalCount");
           return result;
       }
	   /// <summary>
       /// 获取Tag分页列表(分页存储过程)
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="beginTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="pageCount">页数</param>
       /// <param name="count">总记录数</param>
       /// <returns>Tag列表</returns>
       public static List<TagInfo> GetTagPageList(int pageIndex, int pageSize, DateTime? beginTime,
           DateTime? endTime, out int pageCount, out int count)
       {
           const string fieldKey = "Id";
           const string fieldShow = "[Id],[Tag],[AId],[State],[Direction],[CreateTime],[UserId]";
           const string fieldOrder = "Id desc";
           const string @where = "";
          return Paging<TagInfo>.GetPageList("[Tag]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
       }
    }
}
