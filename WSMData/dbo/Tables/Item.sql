CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ModelName] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(256) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Price] DECIMAL NOT NULL , 
    [EAN] FLOAT NULL ,
    [Archived] BIT NOT NULL DEFAULT 0
)
