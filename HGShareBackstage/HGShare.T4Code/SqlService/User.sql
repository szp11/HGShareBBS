-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个User
-- =============================================
CREATE PROCEDURE [dbo].[proc_addUser] 
				@Name nvarchar(100),
				@NickName nvarchar(100),
				@Password varchar(50),
				@RoleId int,
				@OnLineTime datetime,
				@ActionTime datetime,
				@CreateTime datetime,
				@Avatar varchar(100),
				@Sex smallint,
				@Email varchar(50),
				@EmailStatus bit,
				@Score bigint
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [User](
		[Name],[NickName],[Password],[RoleId],[OnLineTime],[ActionTime],[CreateTime],[Avatar],[Sex],[Email],[EmailStatus],[Score]
	)VALUES(
		@Name,@NickName,@Password,@RoleId,@OnLineTime,@ActionTime,@CreateTime,@Avatar,@Sex,@Email,@EmailStatus,@Score
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断User是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsUser]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[User] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过用户ID获取User
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[Name],[NickName],[Password],[RoleId],[OnLineTime],[ActionTime],[CreateTime],[Avatar],[Sex],[Email],[EmailStatus],[Score]
	FROM [dbo].[User] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过用户ID删除User
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteUserById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[User] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有User
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[Name],[NickName],[Password],[RoleId],[OnLineTime],[ActionTime],[CreateTime],[Avatar],[Sex],[Email],[EmailStatus],[Score]
	FROM [dbo].[User] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取User
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--用户名
	@Name NVARCHAR(100),
	--用户昵称
	@NickName NVARCHAR(100),
	--密码
	@Password VARCHAR(50),
	--角色ID
	@RoleId INT,
	--最后在线时间
	@OnLineTime DATETIME,
	--最后操作时间
	@ActionTime DATETIME,
	--创建时间
	@CreateTime DATETIME,
	--头像
	@Avatar VARCHAR(100),
	--性别 0 未知 1男 2女
	@Sex SMALLINT,
	--邮箱
	@Email VARCHAR(50),
	--Email是否激活
	@EmailStatus BIT,
	--积分
	@Score BIGINT,
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
		SET @Condition= @Condition + ' AND [User].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [User].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[User] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [User].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [User].*  FROM dbo.[User]'
			+@Condition	+') [User] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

