-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个UserOther
-- =============================================
CREATE PROCEDURE [dbo].[proc_addUserOther] 
			@UserId int,
				@PersonalityIntroduce nvarchar(1000)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [UserOther](
		[UserId],[PersonalityIntroduce]
	)VALUES(
		@UserId,@PersonalityIntroduce
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断UserOther是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsUserOther]
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[UserOther] WHERE UserId = @UserId)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过用户编号获取UserOther
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserOtherListByUserId]
@UserId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [UserId],[PersonalityIntroduce]
	FROM [dbo].[UserOther] WHERE UserId=@UserId
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过用户编号删除UserOther
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteUserOtherByUserId]
@UserId int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[UserOther] WHERE UserId=@UserId
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有UserOther
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserOtherAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [UserId],[PersonalityIntroduce]
	FROM [dbo].[UserOther] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取UserOther
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserOtherPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--用户编号
	@UserId INT,
	--个性介绍
	@PersonalityIntroduce NVARCHAR(1000),
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
		SET @Condition= @Condition + ' AND [UserOther].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [UserOther].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[UserOther] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [UserOther].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY UserId desc ) AS RowNum, [UserOther].*  FROM dbo.[UserOther]'
			+@Condition	+') [UserOther] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

