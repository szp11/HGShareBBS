-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个AdminLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_addAdminLog] 
				@UserId bigint,
				@Controllers varchar(50),
				@Action varchar(50),
				@Parameter text,
				@ActionId varchar(50),
				@Ip varchar(20),
				@Url varchar(500),
				@InTime datetime,
				@Method varchar(10),
				@IsAjax int,
				@UserAgent varchar(500),
				@ControllersDsc nvarchar(100),
				@ActionDsc nvarchar(100)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [AdminLog](
		[UserId],[Controllers],[Action],[Parameter],[ActionId],[Ip],[Url],[InTime],[Method],[IsAjax],[UserAgent],[ControllersDsc],[ActionDsc]
	)VALUES(
		@UserId,@Controllers,@Action,@Parameter,@ActionId,@Ip,@Url,@InTime,@Method,@IsAjax,@UserAgent,@ControllersDsc,@ActionDsc
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断AdminLog是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsAdminLog]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[AdminLog] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id获取AdminLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetAdminLogListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[UserId],[Controllers],[Action],[Parameter],[ActionId],[Ip],[Url],[InTime],[Method],[IsAjax],[UserAgent],[ControllersDsc],[ActionDsc]
	FROM [dbo].[AdminLog] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id删除AdminLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteAdminLogById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[AdminLog] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有AdminLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetAdminLogAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[UserId],[Controllers],[Action],[Parameter],[ActionId],[Ip],[Url],[InTime],[Method],[IsAjax],[UserAgent],[ControllersDsc],[ActionDsc]
	FROM [dbo].[AdminLog] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取AdminLog
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetAdminLogPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--用户ID
	@UserId BIGINT,
	--访问控制器
	@Controllers VARCHAR(50),
	--访问Action
	@Action VARCHAR(50),
	--请求参数
	@Parameter TEXT,
	--请求中主要ID
	@ActionId VARCHAR(50),
	--IP
	@Ip VARCHAR(20),
	--Url
	@Url VARCHAR(500),
	--记录时间
	@InTime DATETIME,
	--请求方法 get/post....
	@Method VARCHAR(10),
	--是否是Ajax访问 0默认访问 1Ajax访问
	@IsAjax INT,
	--UserAgent
	@UserAgent VARCHAR(500),
	--控制器描述
	@ControllersDsc NVARCHAR(100),
	--Action描述
	@ActionDsc NVARCHAR(100),
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
		SET @Condition= @Condition + ' AND [AdminLog].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [AdminLog].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[AdminLog] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [AdminLog].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [AdminLog].*  FROM dbo.[AdminLog]'
			+@Condition	+') [AdminLog] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

