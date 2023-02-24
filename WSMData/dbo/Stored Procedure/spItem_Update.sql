CREATE PROCEDURE [dbo].[spItem_Update]
	@Id int,
	@ModelName nvarchar(128),
	@Description nvarchar(256),
	@Quantity int,
	@Price money,
	@Location nvarchar(128),
	@InternalSupplierPersonId nvarchar(128),
	@InternalSupplierCompanyId int,
	@EAN decimal,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Item set
	ModelName = @ModelName,
	[Description] = @Description,
	Quantity = @Quantity,
	Price = @Price,
	EAN = @EAN,
	[Location] = @Location,
	InternalSupplierPersonId = @InternalSupplierPersonId,
	InternalSupplierCompanyId = @InternalSupplierCompanyId,
	Archived = @Archived

	where Id = @Id;
end