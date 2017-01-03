-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个UserAccessLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_addUserAccessLog] 
				@Url varchar(200),
				@Referer varchar(200),
				@UserAgent varchar(500),
				@InsertTime datetime,
				@RuanTime int,
				@Ip varchar(20),
				@UserId int
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [UserAccessLog](
		[Url],[Referer],[UserAgent],[InsertTime],[RuanTime],[Ip],[UserId]
	)VALUES(
		@Url,@Referer,@UserAgent,@InsertTime,@RuanTime,@Ip,@UserId
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断UserAccessLog是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsUserAccessLog]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[UserAccessLog] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id获取UserAccessLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserAccessLogListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[Url],[Referer],[UserAgent],[InsertTime],[RuanTime],[Ip],[UserId]
	FROM [dbo].[UserAccessLog] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id删除UserAccessLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteUserAccessLogById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[UserAccessLog] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有UserAccessLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserAccessLogAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[Url],[Referer],[UserAgent],[InsertTime],[RuanTime],[Ip],[UserId]
	FROM [dbo].[UserAccessLog] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取UserAccessLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserAccessLogPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--访问地址
	@Url VARCHAR(200),
	--来源地址
	@Referer VARCHAR(200),
	--UA
	@UserAgent VARCHAR(500),
	--创建时间
	@InsertTime DATETIME,
	--运行时间
	@RuanTime INT,
	--Ip
	@Ip VARCHAR(20),
	--用户Id
	@UserId INT,
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
		SET @Condition= @Condition + ' AND [UserAccessLog].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [UserAccessLog].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[UserAccessLog] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [UserAccessLog].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [UserAccessLog].*  FROM dbo.[UserAccessLog]'
			+@Condition	+') [UserAccessLog] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

