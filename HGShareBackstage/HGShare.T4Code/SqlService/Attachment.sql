-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	添加一个Attachment
-- =============================================
CREATE PROCEDURE [dbo].[proc_addAttachment] 
				@FileName varchar(50),
				@FileTitle nvarchar(100),
				@Description nvarchar(1000),
				@Type varchar(10),
				@Width int,
				@Height int,
				@FileSize bigint,
				@IsShow bit,
				@AId bigint,
				@Score int,
				@State int,
				@UserId int,
				@InTime datetime,
				@BType int,
				@LocalPath varchar(200),
				@VirtualPath varchar(200),
				@Guid uniqueidentifier(16)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [Attachment](
		[FileName],[FileTitle],[Description],[Type],[Width],[Height],[FileSize],[IsShow],[AId],[Score],[State],[UserId],[InTime],[BType],[LocalPath],[VirtualPath],[Guid]
	)VALUES(
		@FileName,@FileTitle,@Description,@Type,@Width,@Height,@FileSize,@IsShow,@AId,@Score,@State,@UserId,@InTime,@BType,@LocalPath,@VirtualPath,@Guid
	)
	SELECT SCOPE_IDENTITY()
END


-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	判断Attachment是否存在
-- =============================================
CREATE PROCEDURE [dbo].[proc_IsExistsAttachment]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM [dbo].[Attachment] WHERE Id = @Id)
		SELECT 1
	ELSE
		SELECT 0
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id获取Attachment
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetAttachmentListById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 [Id],[FileName],[FileTitle],[Description],[Type],[Width],[Height],[FileSize],[IsShow],[AId],[Score],[State],[UserId],[InTime],[BType],[LocalPath],[VirtualPath],[Guid]
	FROM [dbo].[Attachment] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	通过Id删除Attachment
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteAttachmentById]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE [dbo].[Attachment] WHERE Id=@Id
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	获取所有Attachment
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetAttachmentAllList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [Id],[FileName],[FileTitle],[Description],[Type],[Width],[Height],[FileSize],[IsShow],[AId],[Score],[State],[UserId],[InTime],[BType],[LocalPath],[VirtualPath],[Guid]
	FROM [dbo].[Attachment] 
END
-- =============================================
-- Author:		huhangfei
-- Create date: 11/17/2016 11:27:07
-- Description:	分页获取Attachment
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetAttachmentPageList] 
	@PageIndex INT,
	@PageSize INT,
	--开始时间
	@BeginTime DATETIME,
	--结束时间
	@EndTime DATETIME,
	--文件存储名
	@FileName VARCHAR(50),
	--文件原名
	@FileTitle NVARCHAR(100),
	--描述
	@Description NVARCHAR(1000),
	--文件类型
	@Type VARCHAR(10),
	--img 宽
	@Width INT,
	--img 高
	@Height INT,
	--文件大小
	@FileSize BIGINT,
	--是否显示0 不显示 1显示
	@IsShow BIT,
	--所属文章Id
	@AId BIGINT,
	--文件来源（用于记录程序中各个上传口）
	@Score INT,
	--状态 0 初始状态 1已审核通过 2审核未通过（用于文件审核）
	@State INT,
	--用户Id
	@UserId INT,
	--添加时间
	@InTime DATETIME,
	--0附件 1图片
	@BType INT,
	--文件存储绝对路径（暂时无用）
	@LocalPath VARCHAR(200),
	--文件相对目录
	@VirtualPath VARCHAR(200),
	--上传唯一标示(上传页面初始化后产生)
	@Guid UNIQUEIDENTIFIER(16),
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
		SET @Condition= @Condition + ' AND [Attachment].CreateTime >='''+ CONVERT(varchar(20),@BeginTime,120)+''''
	END
	--时间
	IF @EndTime IS NOT NULL AND @EndTime!=''		
	BEGIN
		SET @Condition = @Condition + ' AND [Attachment].CreateTime <='''+CONVERT(varchar(20),@EndTime,120)+''''
	END

	--数据量
	SET @Sql= @Sql + 'SELECT @TotalCount=COUNT(1) FROM dbo.[Attachment] '
			+@Condition		
	
	SET @Sql = @Sql + ' SELECT [Attachment].* FROM (SELECT ROW_NUMBER() OVER(ORDER BY Id desc ) AS RowNum, [Attachment].*  FROM dbo.[Attachment]'
			+@Condition	+') [Attachment] '
	IF @PageIndex IS NOT NULL AND @PageSize IS NOT NULL
	BEGIN
		SET	@Sql+='WHERE  RowNum BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize '
	END
	
	PRINT @Sql
	PRINT @TotalCount
	EXEC sp_executesql @Sql,N'@PageIndex INT,@PageSize INT,@TotalCount INT OUTPUT',@PageIndex,@PageSize,@TotalCount OUTPUT
END

