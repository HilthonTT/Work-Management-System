CREATE PROCEDURE [dbo].[spMachine_GetByName]
	@MachineName nvarchar(100)

AS
begin
	set nocount on;

	select [Id], [MachineName], [ModelName], [EuropeanArticleNumber], [PurchasedPrice], [DatePurchased]
	from dbo.Machine

	where MachineName LIKE @MachineName
end