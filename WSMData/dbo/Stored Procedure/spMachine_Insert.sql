CREATE PROCEDURE [dbo].[spMachine_Insert]
	@Id int output,
	@MachineName nvarchar(100),
	@ModelName nvarchar(255),
	@EuropeanArticleNumber nvarchar(50),
	@PurchasedPrice money,
	@DatePurchased datetime2,
	@Archived bit
AS
begin
	set nocount on;

	insert dbo.Machine (MachineName, ModelName, EuropeanArticleNumber, PurchasedPrice, DatePurchased, Archived)
	values (@MachineName, @ModelName, @EuropeanArticleNumber, @PurchasedPrice, @DatePurchased, @Archived)

	select @Id = SCOPE_IDENTITY();
end