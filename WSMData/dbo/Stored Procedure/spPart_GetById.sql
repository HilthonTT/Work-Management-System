CREATE PROCEDURE [dbo].[spPart_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [PartName], [ModelName], [MachineId], [PurchasedPrice], [DatePurchased]
	from dbo.Part
	
	where Id = @Id
end