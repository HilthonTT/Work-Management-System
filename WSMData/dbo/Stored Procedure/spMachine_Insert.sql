CREATE PROCEDURE [dbo].[spMachine_Insert]
	@Id int output,
	@MachineName nvarchar(100),
	@ModelName nvarchar(255),
	@EuropeanArticleNumber nvarchar(50),
	@PurchasedPrice money,
	@DatePurchased datetime2
AS
begin
	set nocount on;

	insert dbo.Machine (MachineName, ModelName, EuropeanArticleNumber, PurchasedPrice, DatePurchased)
	values (@MachineName, @ModelName, @EuropeanArticleNumber, @PurchasedPrice, @DatePurchased)

	select @Id = SCOPE_IDENTITY();
end