CREATE PROCEDURE [dbo].[spMachine_GetByModelName]
	@ModelName nvarchar(255)
AS
begin
	set nocount on;

	select [Id], [MachineName], [ModelName], [EuropeanArticleNumber], [PurchasedPrice], [DatePurchased]
	from dbo.Machine

	where ModelName LIKE @ModelName
end