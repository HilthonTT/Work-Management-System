CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ModelName] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(256) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Price] DECIMAL NOT NULL , 
    [Location] NVARCHAR(128) NOT NULL, 
    [InternalSupplierPersonId] NVARCHAR(128) NULL,
    [InternalSupplierCompanyId] INT NULL,
    [EAN] FLOAT NULL ,
    [Archived] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Item_ToUser] FOREIGN KEY ([InternalSupplierPersonId]) REFERENCES [User](Id) ON DELETE NO ACTION, 
    CONSTRAINT [FK_Item_ToCompany] FOREIGN KEY ([InternalSupplierCompanyId]) REFERENCES Company(Id) ON DELETE NO ACTION
)
