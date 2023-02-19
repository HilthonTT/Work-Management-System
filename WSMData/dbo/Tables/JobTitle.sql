CREATE TABLE [dbo].[JobTitle]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [JobName] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(255) NOT NULL, 
    [DepartmentId] INT NOT NULL, 
    [Archived] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_JobTitle_ToDepartment] FOREIGN KEY (DepartmentId) REFERENCES Department(Id) ON DELETE NO ACTION
)
