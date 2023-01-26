CREATE PROCEDURE [dbo].[spMachine_Update]
	@Id int,
	@MachineName nvarchar(100),
	@ModelName nvarchar(100),
	@EuropeanArticleNumber nvarchar(50),
	@PurchasedPrice money,
	@DatePurchased datetime2

AS
begin
	set nocount on;

	UPDATE dbo.Machine set
	MachineName = @MachineName,
	ModelName = @ModelName,
	EuropeanArticleNumber = @EuropeanArticleNumber,
	PurchasedPrice = @PurchasedPrice,
	DatePurchased = @DatePurchased

	where Id = @Id
end
