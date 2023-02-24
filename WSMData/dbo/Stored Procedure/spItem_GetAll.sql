CREATE PROCEDURE [dbo].[spItem_GetAll]
AS
begin
	set nocount on;

	select [Id], [ModelName], [Description], [Quantity], [Price], [Location], [InternalSupplierPersonId], [InternalSupplierCompanyId], [EAN], [Archived]
	from dbo.Item

	order by ModelName
end