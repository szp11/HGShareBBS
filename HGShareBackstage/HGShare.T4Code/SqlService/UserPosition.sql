-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个UserPosition
-- =============================================
CREATE PROCEDURE [dbo].[proc_addUserPosition] 
				@UserId int,
				@Code int,
				@Type smallint,
				@CreateTime datetime
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [UserPosition](
		[UserId],[Code],[Type],[CreateTime]
	)VALUES(
		@UserId,@Code,@Type,@CreateTime
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断UserPosition是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsUserPosition]
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[UserPosition] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过获取UserPosition
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserPositionListById]
@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[UserId],[Code],[Type],[CreateTime]
	FROM [dbo].[UserPosition] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过删除UserPosition
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteUserPositionById]
@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[UserPosition] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有UserPosition
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserPositionAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[UserId],[Code],[Type],[CreateTime]
	FROM [dbo].[UserPosition] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取UserPosition
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserPositionPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--
	@UserId INT,
	--位置代码
	@Code INT,
	--类型 0省 1城 2区
	@Type SMALLINT,
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
		SET @Condition= @Condition + ' AND [UserPosition].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [UserPosition].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[UserPosition] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [UserPosition].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [UserPosition].*  FROM dbo.[UserPosition]'
			+@Condition	+') [UserPosition] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

