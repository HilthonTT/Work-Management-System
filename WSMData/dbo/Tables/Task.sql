CREATE TABLE [dbo].[Task]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(128) NULL, 
    [DepartmentId] INT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(255) NOT NULL, 
    [DateDue] DATETIME2 NOT NULL, 
    [PercentageDone] INT NOT NULL DEFAULT 0, 
    [IsDone] BIT NOT NULL DEFAULT 0, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_Task_ToUser] FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE NO ACTION, 
    CONSTRAINT [FK_Task_ToDepartment] FOREIGN KEY (DepartmentId) REFERENCES Department(Id) ON DELETE CASCADE
)
