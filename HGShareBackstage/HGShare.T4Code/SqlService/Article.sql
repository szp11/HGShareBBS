-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个Article
-- =============================================
CREATE PROCEDURE [dbo].[proc_addArticle] 
				@Title nvarchar(400),
				@Content text,
				@Type int,
				@CommentNum int,
				@Dot int,
				@CreateTime datetime,
				@UserId int,
				@ImgNum int,
				@AttachmentNum int,
				@LastEditUserId int,
				@LastEditTime datetime,
				@Guid uniqueidentifier(16),
				@IsDelete bit,
				@State smallint,
				@RefuseReason nvarchar(400),
				@BType smallint,
				@DianZanNum int,
				@Score int,
				@IsStick bit,
				@IsJiaJing bit,
				@IsCloseComment bit,
				@CloseCommentReason nvarchar(400)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [Article](
		[Title],[Content],[Type],[CommentNum],[Dot],[CreateTime],[UserId],[ImgNum],[AttachmentNum],[LastEditUserId],[LastEditTime],[Guid],[IsDelete],[State],[RefuseReason],[BType],[DianZanNum],[Score],[IsStick],[IsJiaJing],[IsCloseComment],[CloseCommentReason]
	)VALUES(
		@Title,@Content,@Type,@CommentNum,@Dot,@CreateTime,@UserId,@ImgNum,@AttachmentNum,@LastEditUserId,@LastEditTime,@Guid,@IsDelete,@State,@RefuseReason,@BType,@DianZanNum,@Score,@IsStick,@IsJiaJing,@IsCloseComment,@CloseCommentReason
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断Article是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsArticle]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[Article] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id获取Article
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetArticleListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[Title],[Content],[Type],[CommentNum],[Dot],[CreateTime],[UserId],[ImgNum],[AttachmentNum],[LastEditUserId],[LastEditTime],[Guid],[IsDelete],[State],[RefuseReason],[BType],[DianZanNum],[Score],[IsStick],[IsJiaJing],[IsCloseComment],[CloseCommentReason]
	FROM [dbo].[Article] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id删除Article
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteArticleById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[Article] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有Article
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetArticleAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[Title],[Content],[Type],[CommentNum],[Dot],[CreateTime],[UserId],[ImgNum],[AttachmentNum],[LastEditUserId],[LastEditTime],[Guid],[IsDelete],[State],[RefuseReason],[BType],[DianZanNum],[Score],[IsStick],[IsJiaJing],[IsCloseComment],[CloseCommentReason]
	FROM [dbo].[Article] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取Article
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetArticlePageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--标题
	@Title NVARCHAR(400),
	--内容
	@Content TEXT,
	--类型(自定义类型)
	@Type INT,
	--评论数
	@CommentNum INT,
	--浏览量
	@Dot INT,
	--添加时间
	@CreateTime DATETIME,
	--用户Id
	@UserId INT,
	--图片数量
	@ImgNum INT,
	--附件数量
	@AttachmentNum INT,
	--最后修改用户Id
	@LastEditUserId INT,
	--最后修改时间
	@LastEditTime DATETIME,
	--文章唯一标示
	@Guid UNIQUEIDENTIFIER(16),
	--是否删除
	@IsDelete BIT,
	--审核状态 0待审核 1审核通过 2审核未通过
	@State SMALLINT,
	--拒绝通过原因
	@RefuseReason NVARCHAR(400),
	--文章类型 :  1 普通文章 ,2 问答
	@BType SMALLINT,
	--点赞数
	@DianZanNum INT,
	--浏览消费积分
	@Score INT,
	--是否置顶
	@IsStick BIT,
	--是否加精
	@IsJiaJing BIT,
	--是否关闭评论
	@IsCloseComment BIT,
	--关闭评论原因
	@CloseCommentReason NVARCHAR(400),
	--总数据条数
	@TotalCount INT OUTPUT
AS
BEGIN
DECLARE 
		@Sql NVARCHAR(MAX),
		@Condition NVARCHAR(MAX)

	SET @Condition=' WHERE 1=1'	
	SET @Sql=''
	
	--时间	
	IF @BeginTime IS NOT NULL AND @BeginTime!=''
	BEGIN
		SET @Condition= @Condition + ' AND [Article].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [Article].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[Article] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [Article].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [Article].*  FROM dbo.[Article]'
			+@Condition	+') [Article] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

