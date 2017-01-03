-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个Modul
-- =============================================
CREATE PROCEDURE [dbo].[proc_addModul] 
				@ModulName nvarchar(100),
				@Controller varchar(50),
				@Action varchar(50),
				@Description nvarchar(200),
				@CreateTime datetime,
				@PId int,
				@OrderId int,
				@IsShow bit,
				@Priority int,
				@IsDisplay bit,
				@Ico varchar(100)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [Modul](
		[ModulName],[Controller],[Action],[Description],[CreateTime],[PId],[OrderId],[IsShow],[Priority],[IsDisplay],[Ico]
	)VALUES(
		@ModulName,@Controller,@Action,@Description,@CreateTime,@PId,@OrderId,@IsShow,@Priority,@IsDisplay,@Ico
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断Modul是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsModul]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[Modul] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过模块ID获取Modul
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetModulListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[ModulName],[Controller],[Action],[Description],[CreateTime],[PId],[OrderId],[IsShow],[Priority],[IsDisplay],[Ico]
	FROM [dbo].[Modul] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过模块ID删除Modul
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteModulById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[Modul] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有Modul
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetModulAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[ModulName],[Controller],[Action],[Description],[CreateTime],[PId],[OrderId],[IsShow],[Priority],[IsDisplay],[Ico]
	FROM [dbo].[Modul] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取Modul
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetModulPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--模块名称
	@ModulName NVARCHAR(100),
	--访问控制器
	@Controller VARCHAR(50),
	--访问Action
	@Action VARCHAR(50),
	--描述
	@Description NVARCHAR(200),
	--创建时间
	@CreateTime DATETIME,
	--父级Id
	@PId INT,
	--排序
	@OrderId INT,
	--是否开启该模块
	@IsShow BIT,
	--优先级
	@Priority INT,
	--是否显示
	@IsDisplay BIT,
	--菜单图标
	@Ico VARCHAR(100),
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
		SET @Condition= @Condition + ' AND [Modul].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [Modul].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[Modul] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [Modul].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [Modul].*  FROM dbo.[Modul]'
			+@Condition	+') [Modul] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

