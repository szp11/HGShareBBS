-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个RoleModul
-- =============================================
CREATE PROCEDURE [dbo].[proc_addRoleModul] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [RoleModul](
		
	)VALUES(
		
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断RoleModul是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsRoleModul]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[RoleModul] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id获取RoleModul
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetRoleModulListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 
	FROM [dbo].[RoleModul] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id删除RoleModul
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteRoleModulById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[RoleModul] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有RoleModul
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetRoleModulAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  
	FROM [dbo].[RoleModul] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取RoleModul
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetRoleModulPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
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
		SET @Condition= @Condition + ' AND [RoleModul].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [RoleModul].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[RoleModul] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [RoleModul].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [RoleModul].*  FROM dbo.[RoleModul]'
			+@Condition	+') [RoleModul] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

