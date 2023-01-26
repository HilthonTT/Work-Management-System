CREATE TABLE [dbo].[JobTitle]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [JobName] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(255) NOT NULL, 
    [DepartmentId] INT NOT NULL, 
    CONSTRAINT [FK_JobTitle_ToDepartment] FOREIGN KEY (DepartmentId) REFERENCES Department(Id)
)
