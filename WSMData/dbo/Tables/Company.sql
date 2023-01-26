CREATE TABLE [dbo].[Company]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyName] NVARCHAR(100) NOT NULL, 
    [PhoneNumber] NVARCHAR(50) NOT NULL, 
    [NumberOfEmployees] INT NOT NULL DEFAULT 1, 
    [ChairPersonId] NVARCHAR(128) NULL, 
    [Description] NVARCHAR(255) NOT NULL, 
    [Revenue] MONEY NOT NULL, 
    [StockPrices] MONEY NOT NULL, 
    [DateFounded] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_Company_ToUser] FOREIGN KEY (ChairPersonId) REFERENCES [User](Id)
)
