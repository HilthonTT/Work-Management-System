CREATE TABLE [dbo].[Department]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyId] INT NOT NULL, 
    [DepartmentName] NVARCHAR(100) NOT NULL,
    [Address] nvarchar(500) NOT NULL,
    [ChairPersonId] NVARCHAR(128) NULL, 
    [PhoneNumber] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(255) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_Department_ToUser] FOREIGN KEY (ChairPersonId) REFERENCES [User](Id) ON DELETE NO ACTION, 
    CONSTRAINT [FK_Department_ToCompany] FOREIGN KEY (CompanyId) REFERENCES Company(Id) ON DELETE NO ACTION
)
