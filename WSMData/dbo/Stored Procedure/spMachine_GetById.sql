CREATE PROCEDURE [dbo].[spMachine_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [MachineName], [ModelName], [EuropeanArticleNumber], [PurchasedPrice], [DatePurchased], [Archived]
	from dbo.Machine

	where Id = @Id
end