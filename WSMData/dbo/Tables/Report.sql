CREATE TABLE [dbo].[Report]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TaskId] INT NOT NULL, 
    [UserId] NVARCHAR(256) NOT NULL,
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(500) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [Archived] BIT NOT NULL DEFAULT 0
)
