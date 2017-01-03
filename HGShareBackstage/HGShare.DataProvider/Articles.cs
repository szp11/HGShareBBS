using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HGShare.DataProvider.DapperHelper;
using HGShare.Model;
using HGShare.VWModel;

namespace HGShare.DataProvider
{
    /// <summary>
    /// Article 
    /// </summary>
    public class Articles
    {

        /// <summary>
        /// 添加ArticleInfo
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static long AddArticle(ArticleInfo article)
        {
            string sql = @"INSERT INTO [Article]
			([Title],[Content],[Type],[UserId],[ImgNum],[AttachmentNum],[LastEditUserId],[Guid],[State])
			VALUES
			(@Title,@Content,@Type,@UserId,@ImgNum,@AttachmentNum,@LastEditUserId,@Guid,@State) 
			SELECT SCOPE_IDENTITY()
			";
            var par = new DynamicParameters();
            par.Add("@Title", article.Title, DbType.String);
            par.Add("@Content", article.Content, DbType.AnsiString);
            par.Add("@Type", article.Type, DbType.Int32);
            par.Add("@UserId", article.UserId, DbType.Int32);
            par.Add("@ImgNum", article.ImgNum, DbType.Int32);
            par.Add("@AttachmentNum", article.AttachmentNum, DbType.Int32);
            par.Add("@LastEditUserId", article.LastEditUserId, DbType.Int32);
            par.Add("@Guid", article.Guid, DbType.Guid);
            par.Add("@State", article.State, DbType.Int16);
            return DapWrapper.InnerQueryScalarSql<long>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 修改ArticleInfo
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static int UpdateArticle(ArticleInfo article)
        {
            string sql = @"UPDATE  [Article] SET 
						Title=@Title,
						Content=@Content,
						Type=@Type,
						CommentNum=@CommentNum,
						Dot=@Dot,
						CreateTime=@CreateTime,
						UserId=@UserId,
						ImgNum=@ImgNum,
						AttachmentNum=@AttachmentNum,
						LastEditUserId=@LastEditUserId,
                        LastEditTime=GETDATE()
 WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", article.Id, DbType.Int32);
            par.Add("@Title", article.Title, DbType.String);
            par.Add("@Content", article.Content, DbType.AnsiString);
            par.Add("@Type", article.Type, DbType.Int32);
            par.Add("@CommentNum", article.CommentNum, DbType.Int32);
            par.Add("@Dot", article.Dot, DbType.Int32);
            par.Add("@CreateTime", article.CreateTime, DbType.DateTime);
            par.Add("@UserId", article.UserId, DbType.Int32);
            par.Add("@ImgNum", article.ImgNum, DbType.Int32);
            par.Add("@AttachmentNum", article.AttachmentNum, DbType.Int32);
            par.Add("@LastEditUserId", article.LastEditUserId, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据id获取ArticleInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArticleInfo GetArticleInfo(long id)
        {
            string sql = "select [Id],[Title],[Content],[Type],[CommentNum],[Dot],[CreateTime],[UserId],[ImgNum],[AttachmentNum],[LastEditUserId],[LastEditTime],[Guid],[IsDelete],[State],[RefuseReason],[BType],[DianZanNum],[Score],[IsStick],[IsJiaJing],[IsCloseComment],[CloseCommentReason] FROM [Article] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerQuerySql<ArticleInfo>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
        }
        /// <summary>
        /// 根据id删除Article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Int32 DeleteArticle(long id)
        {
            string sql = "DELETE [Article] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 根据ids删除Article多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Int32 DeleteArticles(long[] ids)
        {
            string sql = "DELETE [Article] WHERE Id IN (" + string.Join(",", ids) + ")";
            return DapWrapper.InnerExecuteText(DbConfig.ArticleManagerConnString, sql);
        }

        /// <summary>
        /// 获取Article分页列表(分页存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">总记录数</param>
        /// <param name="where">搜索条件 默认为空</param>
        /// <returns>Article列表</returns>
        public static List<ArticleVModel> GetArticlePageList(
            int pageIndex,
            int pageSize,
            DateTime? beginTime,
            DateTime? endTime, 
            out int pageCount, 
            out int count,
            string where="")
        {
            const string fieldKey = "Id";
            const string fieldShow = @" [Id],
                                        [Title],
                                        [Content],
                                        [Type],
                                        [CommentNum],
                                        [Dot],
                                        [CreateTime],
                                        [UserId],
                                        [ImgNum],
                                        [AttachmentNum],
                                        [LastEditUserId],
                                        [LastEditTime],
                                        dbo.GetNickNameByUserId([UserId]) AS UserName,
                                        dbo.GetNickNameByUserId([LastEditUserId]) AS LastEditUserName,
                                        [Guid],dbo.GetTypeNameByTypeId([Type]) TypeName,
                                        [State],
                                        [RefuseReason],
                                        [BType],
                                        [DianZanNum],
                                        [Score],
                                        [IsStick],
                                        [IsJiaJing],
                                        [IsCloseComment],
                                        [CloseCommentReason]";
            const string fieldOrder = "Id desc";
            return Paging<ArticleVModel>.GetPageList("[Article]", fieldKey, fieldShow, fieldOrder, where, pageIndex, pageSize, out pageCount, out count).ToList();
        }

        /// <summary>
        /// 更新文章状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UpdateState(long id,int userId, bool state, string reason = null)
        {
            string sql = @"UPDATE  [Article] SET 
						State=@State,
						LastEditUserId=@LastEditUserId,
                        RefuseReason=@RefuseReason,
                        LastEditTime=GETDATE()
                        WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int64);
            par.Add("@State", state?1:2, DbType.Int16);
            par.Add("@RefuseReason", reason, DbType.String);
            par.Add("@LastEditUserId", userId, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par)>0;
        }
        /// <summary>
        /// 更新文章状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UpdateState(long [] ids, int userId, bool state, string reason = null)
        {
            if (ids.Length == 0)
                return false;
            string sql = string.Format(@"UPDATE  [Article] SET 
						State=@State,
						LastEditUserId=@LastEditUserId,
                        RefuseReason=@RefuseReason,
                        LastEditTime=GETDATE()
                        WHERE Id IN ({0})",string.Join(",",ids));
            var par = new DynamicParameters();
            par.Add("@State", state ? 1 : 2, DbType.Int16);
            par.Add("@RefuseReason", reason, DbType.String);
            par.Add("@LastEditUserId", userId, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par) > 0;
        }
        /// <summary>
        /// 更新文章是否关闭评论状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="isCloseComment"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UpdateIsCloseComment(long id, int userId, bool isCloseComment, string reason = null)
        {
            string sql = string.Format(@"UPDATE  [Article] SET 
						IsCloseComment=@IsCloseComment,
						LastEditUserId=@LastEditUserId,
                        CloseCommentReason=@CloseCommentReason,
                        LastEditTime=GETDATE()
                        WHERE Id ={0}", id);
            var par = new DynamicParameters();
            par.Add("@IsCloseComment", isCloseComment, DbType.Boolean);
            par.Add("@CloseCommentReason", reason, DbType.String);
            par.Add("@LastEditUserId", userId, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par) > 0;
        }

        /// <summary>
        /// 更新文章精华状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="isJiaJing"></param>
        /// <returns></returns>
        public static bool UpdateIsJiaJing(long id, int userId, bool isJiaJing)
        {
            string sql = @"UPDATE  [Article] SET 
						IsJiaJing=@IsJiaJing,
						LastEditUserId=@LastEditUserId,
                        LastEditTime=GETDATE()
                        WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int64);
            par.Add("@IsJiaJing", isJiaJing, DbType.Boolean);
            par.Add("@LastEditUserId", userId, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par) > 0;
        }
        /// <summary>
        /// 更新文章置顶状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="isStick"></param>
        /// <returns></returns>
        public static bool UpdateIsStick(long id, int userId, bool isStick)
        {
            string sql = @"UPDATE  [Article] SET 
						IsStick=@IsStick,
						LastEditUserId=@LastEditUserId,
                        LastEditTime=GETDATE()
                        WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int64);
            par.Add("@IsStick", isStick, DbType.Boolean);
            par.Add("@LastEditUserId", userId, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par) > 0;
        }
        #region 前端使用

        /// <summary>
        /// 获取Article分页列表(自定义存储过程)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="bType"></param>
        /// <param name="isJingHua"></param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="type"></param>
        /// <returns>Article列表</returns>
        public static List<ArticleVModel> GetArticlePageList(int pageIndex, int pageSize, int type, int bType,int isJingHua, out int recordCount)
        {
            recordCount = 0;
            var par = new DynamicParameters();
            par.Add("@PageIndex", pageIndex, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@Type", type, DbType.Int32);
            par.Add("@BType", bType, DbType.Int32);
            par.Add("@IsJiaJing", isJingHua, DbType.Int32);
            par.Add("@TotalCount", recordCount, DbType.Int32, ParameterDirection.Output);
            var result = DapWrapper.InnerQueryProc<ArticleVModel>(DbConfig.ArticleManagerConnString, "proc_GetArticlePageList", par);
            recordCount = par.Get<int>("@TotalCount");
            return result;
        }

        /// <summary>
        /// 根据用户Id获取Article分页列表(自定义存储过程)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns>Article列表</returns>
        public static async Task<IEnumerable<ArticleVModel>> SearchArticlesByUserId(int userId,int state, int pageIndex, int pageSize)
        {
            var par = new DynamicParameters();
            par.Add("@PageIndex", pageIndex, DbType.Int32);
            par.Add("@PageSize", pageSize, DbType.Int32);
            par.Add("@UserId", userId, DbType.Int32);
            par.Add("@State", state, DbType.Int32);
            par.Add("@TotalCount", 0, DbType.Int32, ParameterDirection.Output);
            var result = await DapWrapper.InnerQueryProcAsync<ArticleVModel>(DbConfig.ArticleManagerConnString, "proc_GetArticlePageListByUserId", par);
            return result;
        }

        /// <summary>
        /// 按照用户id查询记录数
        /// </summary>
        /// <param name="userId">分类</param>
        /// <param name="state">-1所有 0待审核 1 已通过 2未通过..</param>
        /// <returns></returns>
        public static async Task<int> SearchArticlesCountByUserId(int userId, int state)
        {
            var par = new DynamicParameters();
            par.Add("@UserId", userId, DbType.Int32);
            par.Add("@State", state, DbType.Int32);
            var result = await DapWrapper.InnerQueryScalarProcAsync<int>(DbConfig.ArticleManagerConnString, "proc_GetArticleCountByUserId", par);
            return result;
        }

        /// <summary>
        /// 根据id获取ArticleVModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArticleVModel GetArticleVModel(long id)
        {
            string sql = @"select [Id],
                                [Title],
                                [Content],
                                [Type],
                                [CommentNum],
                                [Dot],
                                [CreateTime],
                                [UserId],
                                [ImgNum],
                                [AttachmentNum],
                                [LastEditUserId],
                                [LastEditTime],
                                [Guid],
                                [IsStick],
                                [IsJiaJing],
                                dbo.GetTypeNameByTypeId(Type) TypeName,
                                dbo.GetNickNameByUserId(UserId) UserName,
                                dbo.GetNickNameByUserId([LastEditUserId]) AS LastEditUserName,
                                [State],
                                [RefuseReason] ,
                                [IsCloseComment],
                                [CloseCommentReason],
                                [BType],
                                [DianZanNum]
                                FROM [Article] WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", id, DbType.Int32);
            return DapWrapper.InnerQuerySql<ArticleVModel>(DbConfig.ArticleManagerConnString, sql, par).FirstOrDefault();
        }

        /// <summary>
        /// 文章评论数+1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1-1</param>
        /// <returns></returns>
        public static int UpdateCommentNum(long aId, int num)
        {
            const string sql = @"UPDATE [Article] SET CommentNum=CommentNum+@Num WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", aId, DbType.Int32);
            par.Add("@Num", num, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 文章点击量+1
        /// </summary>
        /// <param name="aId"></param>
        /// <returns></returns>
        public async static Task<int> UpdateDot(long aId)
        {
            const string sql = @"proc_UpdateArticleDot";
            var par = new DynamicParameters();
            par.Add("@AId", aId, DbType.Int32);
            return await DapWrapper.InnerExecuteProcAsync(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 点赞数+1/-1
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="num">+1/-1</param>
        /// <returns></returns>
        public static int UpdateDianZanNum(long aId, int num)
        {
            const string sql = @"UPDATE [Article] SET DianZanNum=DianZanNum+@Num WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", aId, DbType.Int32);
            par.Add("@Num", num, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 近期浏览量排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public static List<ArticleVModel> ArticlesDotHotTop(int days, int pageSize)
        {
            string sql = string.Format(@"SELECT TOP {0} [Id]
            ,[Title]
            ,[Dot]
            FROM [dbo].[Article]
            {1}
            order by Dot desc", pageSize, days >= 0 ? "WHERE DATEDIFF(DAY,CreateTime,GETDATE())<=" + days : "");
            return DapWrapper.InnerQuerySql<ArticleVModel>(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 近期评论数排行榜
        /// </summary>
        /// <param name="days">天数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public static List<ArticleVModel> ArticlesCommentNumHotTop(int days, int pageSize)
        {
            string sql = string.Format(@"SELECT TOP {0} [Id]
            ,[Title]
            ,[CommentNum]
            FROM [dbo].[Article]
            {1}
            order by CommentNum desc", pageSize, days >= 0 ? "WHERE DATEDIFF(DAY,CreateTime,GETDATE())<=" + days : "");
            return DapWrapper.InnerQuerySql<ArticleVModel>(DbConfig.ArticleManagerConnString, sql);
        }
        /// <summary>
        /// 获取用户最后发帖时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DateTime GetLastPostTime(int userId)
        {
            string sql = @"SELECT MAX(CreateTime)
                          FROM [dbo].[Article]
                          WHERE UserId=@UserId";
            var par = new DynamicParameters();
            par.Add("@UserId", userId, DbType.Int32);
            return DapWrapper.InnerQueryScalarSql<DateTime>(DbConfig.ArticleManagerConnString, sql, par);
        }
        /// <summary>
        /// 修改文章主体信息
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static int UpdateArticlePart(ArticleVModel article)
        {
            string sql = @"UPDATE  [Article] SET 
						Title=@Title,
						Content=@Content,
						Type=@Type,
						State=@State,
						ImgNum=@ImgNum,
						AttachmentNum=@AttachmentNum,
						LastEditUserId=@LastEditUserId,
                        LastEditTime=GETDATE()
                        WHERE Id=@Id";
            var par = new DynamicParameters();
            par.Add("@Id", article.Id, DbType.Int32);
            par.Add("@Title", article.Title, DbType.String);
            par.Add("@Content", article.Content, DbType.AnsiString);
            par.Add("@Type", article.Type, DbType.Int32);
            par.Add("@State", article.State, DbType.Int32);
            par.Add("@ImgNum", article.ImgNum, DbType.Int32);
            par.Add("@AttachmentNum", article.AttachmentNum, DbType.Int32);
            par.Add("@LastEditUserId", article.LastEditUserId, DbType.Int32);
            return DapWrapper.InnerExecuteSql(DbConfig.ArticleManagerConnString, sql, par);
        }
        #endregion

        #region esdata 索引器使用
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public static List<ArticleVModel> GetAllData()
        {
            string sql = @"select [Id],[Title],[Content],[Type],[CommentNum],[Dot],[CreateTime],[UserId],[ImgNum],[AttachmentNum],[LastEditUserId],[LastEditTime],[Guid],[IsDelete],[State],[RefuseReason],[BType],[DianZanNum],[Score],[IsStick],[IsJiaJing],[IsCloseComment],[CloseCommentReason],
                dbo.GetTypeNameByTypeId([Article].Type) AS TypeName,  
                dbo.GetNickNameByUserId([UserId]) AS UserName,
                dbo.GetNickNameByUserId([LastEditUserId]) AS LastEditUserName FROM [Article]";
            return DapWrapper.InnerQuerySql<ArticleVModel>(DbConfig.ArticleManagerConnString, sql);
        }
        #endregion
        /// <summary>
        /// 获取文章热字段DOt的值
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async static Task<IEnumerable<ArticleHotFieldVModel>> GetArticleHotFieldDots(long[] ids)
        {
            if(ids==null || ids.Length==0)
                return new List<ArticleHotFieldVModel>();
            string idstr = string.Join(",", ids);
            string sql = string.Format(@"SELECT  [Id]
                          ,[AId]
                          ,[Dot]
                           FROM [ArticleManager].[dbo].[Article_HotField] WHERE AId IN({0})", idstr);

            return await DapWrapper.InnerQuerySqlAsync<ArticleHotFieldVModel>(DbConfig.ArticleManagerConnString, sql);

        }
    }
}
