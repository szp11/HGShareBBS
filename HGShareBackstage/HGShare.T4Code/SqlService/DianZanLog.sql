-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个DianZanLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_addDianZanLog] 
				@MId bigint,
				@UserId int,
				@Ip varchar(20),
				@CreateTime datetime,
				@IsCancel bit,
				@CancelTime datetime,
				@Type int
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [DianZanLog](
		[MId],[UserId],[Ip],[CreateTime],[IsCancel],[CancelTime],[Type]
	)VALUES(
		@MId,@UserId,@Ip,@CreateTime,@IsCancel,@CancelTime,@Type
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断DianZanLog是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsDianZanLog]
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[DianZanLog] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过获取DianZanLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetDianZanLogListById]
@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[MId],[UserId],[Ip],[CreateTime],[IsCancel],[CancelTime],[Type]
	FROM [dbo].[DianZanLog] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过删除DianZanLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteDianZanLogById]
@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[DianZanLog] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有DianZanLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetDianZanLogAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[MId],[UserId],[Ip],[CreateTime],[IsCancel],[CancelTime],[Type]
	FROM [dbo].[DianZanLog] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取DianZanLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetDianZanLogPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--主体Id
	@MId BIGINT,
	--用户Id
	@UserId INT,
	--Ip
	@Ip VARCHAR(20),
	--创建时间
	@CreateTime DATETIME,
	--是否取消
	@IsCancel BIT,
	--
	@CancelTime DATETIME,
	--类型 0文章 1评论
	@Type INT,
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
		SET @Condition= @Condition + ' AND [DianZanLog].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [DianZanLog].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[DianZanLog] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [DianZanLog].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [DianZanLog].*  FROM dbo.[DianZanLog]'
			+@Condition	+') [DianZanLog] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

