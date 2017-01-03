-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个Area
-- =============================================
CREATE PROCEDURE [dbo].[proc_addArea] 
				@Name nvarchar(100),
				@Code int,
				@PinYin varchar(50),
				@SortPinYin varchar(10),
				@Sort varchar(10),
				@ParentCode int
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [Area](
		[Name],[Code],[PinYin],[SortPinYin],[Sort],[ParentCode]
	)VALUES(
		@Name,@Code,@PinYin,@SortPinYin,@Sort,@ParentCode
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断Area是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsArea]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[Area] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过获取Area
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetAreaListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[Name],[Code],[PinYin],[SortPinYin],[Sort],[ParentCode]
	FROM [dbo].[Area] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过删除Area
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteAreaById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[Area] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有Area
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetAreaAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[Name],[Code],[PinYin],[SortPinYin],[Sort],[ParentCode]
	FROM [dbo].[Area] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取Area
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetAreaPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--
	@Name NVARCHAR(100),
	--
	@Code INT,
	--
	@PinYin VARCHAR(50),
	--
	@SortPinYin VARCHAR(10),
	--
	@Sort VARCHAR(10),
	--
	@ParentCode INT,
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
		SET @Condition= @Condition + ' AND [Area].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [Area].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[Area] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [Area].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [Area].*  FROM dbo.[Area]'
			+@Condition	+') [Area] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

