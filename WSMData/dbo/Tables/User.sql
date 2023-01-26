CREATE TABLE [dbo].[User]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(100) NOT NULL, 
    [EmailAddress] NVARCHAR(255) NOT NULL, 
    [PhoneNumber] NVARCHAR(50) NOT NULL, 
    [DateOfBirth] DATETIME2 NOT NULL, 
    [DepartmentId] INT NULL, 
    [JobTitleId] INT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_User_ToDepartment] FOREIGN KEY (DepartmentId) REFERENCES Department(Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_User_ToJobTitle] FOREIGN KEY (JobTitleId) REFERENCES JobTitle(Id) ON DELETE CASCADE
)
