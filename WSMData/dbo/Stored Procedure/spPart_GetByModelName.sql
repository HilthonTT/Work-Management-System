CREATE PROCEDURE [dbo].[spPart_GetByModelName]
	@ModelName nvarchar(255)
AS
begin
	set nocount on;

	select [Id], [PartName], [ModelName], [MachineId], [DatePurchased]
	from dbo.Part

	where ModelName = @ModelName
end