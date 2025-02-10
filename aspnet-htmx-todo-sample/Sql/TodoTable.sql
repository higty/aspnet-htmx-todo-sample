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


SELECT * FROM Todo



