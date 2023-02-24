CREATE PROCEDURE [dbo].[spItem_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [ModelName], [Description], [Quantity], [Price], [Location], [InternalSupplierPersonId], [InternalSupplierCompanyId], [EAN], [Archived]
	from dbo.Item

	where Id = @Id
end