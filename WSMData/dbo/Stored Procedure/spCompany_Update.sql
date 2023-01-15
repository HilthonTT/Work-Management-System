CREATE PROCEDURE [dbo].[spCompany_Update]
	@Id int,
	@CompanyName nvarchar(100),
	@PhoneNumber nvarchar(50),
	@NumberOfEmployees int,
	@ChairPersonId nvarchar(128),
	@Description nvarchar(MAX),
	@Revenue money,
	@StockPrices money

AS
begin
	set nocount on;
	
	UPDATE dbo.Company set 
	CompanyName = @CompanyName,
	PhoneNumber = @PhoneNumber,
	NumberOfEmployees = @NumberOfEmployees,
	ChairPersonId = @ChairPersonId,
	[Description] = @Description,
	Revenue = @Revenue,
	StockPrices = @StockPrices

	WHERE Id = @Id
end
