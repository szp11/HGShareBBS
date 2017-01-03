-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个Comment
-- =============================================
CREATE PROCEDURE [dbo].[proc_addComment] 
				@AId bigint,
				@UserId int,
				@CreateTime date(3),
				@Content nvarchar(1000),
				@IP varchar(20),
				@UserAgent varchar(500),
				@State smallint,
				@RefuseReason nvarchar(400),
				@IsDelete bit,
				@LastEditUserId int,
				@LastEditTime datetime,
				@IsStick bit,
				@DianZanNum int
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [Comment](
		[AId],[UserId],[CreateTime],[Content],[IP],[UserAgent],[State],[RefuseReason],[IsDelete],[LastEditUserId],[LastEditTime],[IsStick],[DianZanNum]
	)VALUES(
		@AId,@UserId,@CreateTime,@Content,@IP,@UserAgent,@State,@RefuseReason,@IsDelete,@LastEditUserId,@LastEditTime,@IsStick,@DianZanNum
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断Comment是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsComment]
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[Comment] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过获取Comment
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetCommentListById]
@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[AId],[UserId],[CreateTime],[Content],[IP],[UserAgent],[State],[RefuseReason],[IsDelete],[LastEditUserId],[LastEditTime],[IsStick],[DianZanNum]
	FROM [dbo].[Comment] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过删除Comment
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteCommentById]
@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[Comment] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有Comment
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetCommentAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[AId],[UserId],[CreateTime],[Content],[IP],[UserAgent],[State],[RefuseReason],[IsDelete],[LastEditUserId],[LastEditTime],[IsStick],[DianZanNum]
	FROM [dbo].[Comment] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取Comment
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetCommentPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--文章Id
	@AId BIGINT,
	--评论用户Id
	@UserId INT,
	--创建时间
	@CreateTime DATE(3),
	--评论内容
	@Content NVARCHAR(1000),
	--评论者Ip
	@IP VARCHAR(20),
	--用户UA信息
	@UserAgent VARCHAR(500),
	--审核状态0待审核 1已通过 2未通过
	@State SMALLINT,
	--拒绝原因
	@RefuseReason NVARCHAR(400),
	--是否删除
	@IsDelete BIT,
	--最后修改用户 0默认
	@LastEditUserId INT,
	--最后修改时间
	@LastEditTime DATETIME,
	--是否置顶
	@IsStick BIT,
	--点赞数
	@DianZanNum INT,
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
		SET @Condition= @Condition + ' AND [Comment].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [Comment].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[Comment] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [Comment].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [Comment].*  FROM dbo.[Comment]'
			+@Condition	+') [Comment] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

