-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个Notify
-- =============================================
CREATE PROCEDURE [dbo].[proc_addNotify] 
				@FromUserId int,
				@ToUserId int,
				@CreateTime datetime,
				@IsDelete bit,
				@IsRead bit,
				@IsSystem bit
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [Notify](
		[FromUserId],[ToUserId],[CreateTime],[IsDelete],[IsRead],[IsSystem]
	)VALUES(
		@FromUserId,@ToUserId,@CreateTime,@IsDelete,@IsRead,@IsSystem
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断Notify是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsNotify]
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[Notify] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过获取Notify
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetNotifyListById]
@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[FromUserId],[ToUserId],[CreateTime],[IsDelete],[IsRead],[IsSystem]
	FROM [dbo].[Notify] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过删除Notify
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteNotifyById]
@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[Notify] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有Notify
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetNotifyAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[FromUserId],[ToUserId],[CreateTime],[IsDelete],[IsRead],[IsSystem]
	FROM [dbo].[Notify] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取Notify
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetNotifyPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--发送者
	@FromUserId INT,
	--接受者
	@ToUserId INT,
	--创建时间
	@CreateTime DATETIME,
	--是否删除
	@IsDelete BIT,
	--是否已读
	@IsRead BIT,
	--是否是系统消息
	@IsSystem BIT,
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
		SET @Condition= @Condition + ' AND [Notify].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [Notify].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[Notify] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [Notify].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [Notify].*  FROM dbo.[Notify]'
			+@Condition	+') [Notify] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

