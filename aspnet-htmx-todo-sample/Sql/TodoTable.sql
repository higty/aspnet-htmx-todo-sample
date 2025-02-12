CREATE TABLE Todo
([TodoId] UNIQUEIDENTIFIER NOT NULL
,[Title] NVARCHAR (128) NOT NULL
,[CreateTime] DateTimeOffset (7) NOT NULL
,[Priority] INT NOT NULL
,[DueDate] DATE 

,CONSTRAINT Todo_PrimaryKey PRIMARY KEY CLUSTERED(TodoId)
)
GO


CREATE PROCEDURE Todo_Add_20250210
(@TodoId UNIQUEIDENTIFIER 
,@Title NVARCHAR (128) 
,@CreateTime DateTimeOffset (7) 
,@Priority INT 
,@DueDate DATE 
) AS

INSERT Todo(TodoId,Title,CreateTime,Priority,DueDate)VALUES(@TodoId,@Title,@CreateTime,@Priority,@DueDate)

GO

CREATE PROCEDURE Todo_Edit_20250210
(@TodoId UNIQUEIDENTIFIER 
,@Title NVARCHAR (128) 
,@Priority INT 
,@DueDate DATE 
) AS

UPDATE Todo
SET Title = @Title
, [Priority] = @Priority
, DueDate = @DueDate
WHERE TodoId = @TodoId

GO

CREATE PROCEDURE Todo_Delete_20250210
(@TodoId UNIQUEIDENTIFIER 
) AS

DELETE Todo WHERE TodoId = @TodoId

GO


CREATE PROCEDURE Todo_Get_20250210
(@DueDateNotNull BIT
) AS

SELECT * FROM Todo WITH(nolock)
WHERE (@DueDateNotNull = 1 AND DueDate IS NOT NULL)
OR (@DueDateNotNull = 0 AND DueDate IS NULL)
OR @DueDateNotNull IS NULL
ORDER BY CreateTime DESC

GO


CREATE PROCEDURE Todo_Get_By_TodoId_20250210
(@TodoId UNIQUEIDENTIFIER
)AS

SELECT * FROM Todo WITH(nolock)
WHERE TodoId = @TodoId

GO


SELECT * FROM Todo



