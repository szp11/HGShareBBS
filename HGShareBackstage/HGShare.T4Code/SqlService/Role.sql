-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个Role
-- =============================================
CREATE PROCEDURE [dbo].[proc_addRole] 
				@RName varchar(400),
				@CreateTime datetime,
				@IsDel bit,
				@Description nvarchar(1000),
				@IsSuper bit
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [Role](
		[RName],[CreateTime],[IsDel],[Description],[IsSuper]
	)VALUES(
		@RName,@CreateTime,@IsDel,@Description,@IsSuper
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断Role是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsRole]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[Role] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过获取Role
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetRoleListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[RName],[CreateTime],[IsDel],[Description],[IsSuper]
	FROM [dbo].[Role] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过删除Role
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteRoleById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[Role] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有Role
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetRoleAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[RName],[CreateTime],[IsDel],[Description],[IsSuper]
	FROM [dbo].[Role] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取Role
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetRolePageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--角色名
	@RName VARCHAR(400),
	--添加时间
	@CreateTime DATETIME,
	--是否禁用
	@IsDel BIT,
	--描述
	@Description NVARCHAR(1000),
	--是否是超级角色
	@IsSuper BIT,
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
		SET @Condition= @Condition + ' AND [Role].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [Role].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[Role] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [Role].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [Role].*  FROM dbo.[Role]'
			+@Condition	+') [Role] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

