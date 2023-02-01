CREATE TABLE [dbo].[Part]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PartName] NVARCHAR(100) NOT NULL,
    [ModelName] NVARCHAR(255) NOT NULL, 
    [MachineId] INT NULL,
    [PurchasedPrice] MONEY NOT NULL, 
    [DatePurchased] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_Part_ToMachine] FOREIGN KEY (MachineId) REFERENCES Machine(Id) ON DELETE CASCADE
)
