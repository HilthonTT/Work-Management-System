﻿CREATE TABLE [dbo].[User]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(100) NOT NULL, 
    [EmailAddress] NVARCHAR(MAX) NOT NULL, 
    [PhoneNumber] NVARCHAR(50) NOT NULL, 
    [Age] INT NOT NULL, 
    [DepartmentId] INT NOT NULL, 
    [JobTitleId] INT NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_User_ToDepartment] FOREIGN KEY (DepartmentId) REFERENCES Department(Id), 
    CONSTRAINT [FK_User_ToJobTitle] FOREIGN KEY (JobTitleId) REFERENCES JobTitle(Id)
)
