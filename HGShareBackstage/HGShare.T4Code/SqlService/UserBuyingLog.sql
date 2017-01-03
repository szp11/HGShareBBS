-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个UserBuyingLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_addUserBuyingLog] 
				@AId bigint,
				@Score int,
				@UserId int,
				@CreateTime datetime
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [UserBuyingLog](
		[AId],[Score],[UserId],[CreateTime]
	)VALUES(
		@AId,@Score,@UserId,@CreateTime
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断UserBuyingLog是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsUserBuyingLog]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[UserBuyingLog] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id获取UserBuyingLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserBuyingLogListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[AId],[Score],[UserId],[CreateTime]
	FROM [dbo].[UserBuyingLog] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id删除UserBuyingLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteUserBuyingLogById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[UserBuyingLog] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有UserBuyingLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserBuyingLogAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[AId],[Score],[UserId],[CreateTime]
	FROM [dbo].[UserBuyingLog] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取UserBuyingLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserBuyingLogPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--文章Id
	@AId BIGINT,
	--花费积分
	@Score INT,
	--用户Id
	@UserId INT,
	--
	@CreateTime DATETIME,
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
		SET @Condition= @Condition + ' AND [UserBuyingLog].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [UserBuyingLog].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[UserBuyingLog] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [UserBuyingLog].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [UserBuyingLog].*  FROM dbo.[UserBuyingLog]'
			+@Condition	+') [UserBuyingLog] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

