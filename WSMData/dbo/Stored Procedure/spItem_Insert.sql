CREATE PROCEDURE [dbo].[spItem_Insert]
	@Id int output,
	@ModelName nvarchar(128),
	@Description nvarchar(500),
	@EAN decimal,
	@Quantity int,
	@Price money,
	@Location nvarchar(128),
	@InternalSupplierPersonId nvarchar(128),
	@InternalSupplierCompanyId int,
	@Archived bit
AS
begin
	set nocount on;

	insert into dbo.Item(ModelName, [Description], Quantity, Price, EAN, Archived, InternalSupplierPersonId, InternalSupplierCompanyId, [Location])
	values (@ModelName, @Description, @Quantity, @Price, @EAN, @Archived, @InternalSupplierPersonId, @InternalSupplierCompanyId, @Location)

	select @Id = SCOPE_IDENTITY();
end