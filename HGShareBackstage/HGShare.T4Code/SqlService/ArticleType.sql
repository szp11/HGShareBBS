-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个ArticleType
-- =============================================
CREATE PROCEDURE [dbo].[proc_addArticleType] 
				@Name varchar(100),
				@PId int,
				@Sort int,
				@PinYin varchar(200),
				@IsHomeMenu bit,
				@CreateTime datetime,
				@Ico varchar(100)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [ArticleType](
		[Name],[PId],[Sort],[PinYin],[IsHomeMenu],[CreateTime],[Ico]
	)VALUES(
		@Name,@PId,@Sort,@PinYin,@IsHomeMenu,@CreateTime,@Ico
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断ArticleType是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsArticleType]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[ArticleType] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过类型Id获取ArticleType
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetArticleTypeListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[Name],[PId],[Sort],[PinYin],[IsHomeMenu],[CreateTime],[Ico]
	FROM [dbo].[ArticleType] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过类型Id删除ArticleType
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteArticleTypeById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[ArticleType] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有ArticleType
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetArticleTypeAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[Name],[PId],[Sort],[PinYin],[IsHomeMenu],[CreateTime],[Ico]
	FROM [dbo].[ArticleType] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取ArticleType
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetArticleTypePageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--类型名
	@Name VARCHAR(100),
	--类型父级
	@PId INT,
	--排序
	@Sort INT,
	--拼音
	@PinYin VARCHAR(200),
	--是否是主页菜单
	@IsHomeMenu BIT,
	--添加时间
	@CreateTime DATETIME,
	--前台图标
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
		SET @Condition= @Condition + ' AND [ArticleType].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [ArticleType].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[ArticleType] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [ArticleType].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [ArticleType].*  FROM dbo.[ArticleType]'
			+@Condition	+') [ArticleType] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

