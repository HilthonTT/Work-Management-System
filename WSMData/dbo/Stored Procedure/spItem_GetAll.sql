CREATE PROCEDURE [dbo].[spItem_GetAll]
AS
begin
	set nocount on;

	select [Id], [ModelName], [Description], [Quantity], [Price], [EAN], [Archived]
	from dbo.Item

	order by ModelName
end