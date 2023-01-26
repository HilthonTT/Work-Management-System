CREATE TABLE [dbo].[Machine]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MachineName] NVARCHAR(100) NOT NULL, 
    [ModelName] NVARCHAR(255) NOT NULL, 
    [EuropeanArticleNumber] NVARCHAR(50) NOT NULL,
    [PurchasedPrice] MONEY NOT NULL, 
    [DatePurchased] DATETIME2 NOT NULL
)
