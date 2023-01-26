CREATE PROCEDURE [dbo].[spCompany_Insert]
	@Id int output,
	@CompanyName nvarchar(100),
	@PhoneNumber nvarchar(50),
	@NumberOfEmployees int,
	@ChairPersonId nvarchar(128),
	@Description nvarchar(255),
	@Revenue money,
	@StockPrices money,
	@DateFounded datetime2
AS
begin
	set nocount on;
	
	insert into dbo.Company(CompanyName, PhoneNumber, NumberOfEmployees, ChairPersonId, [Description], Revenue, StockPrices, DateFounded)
	values (@CompanyName, @PhoneNumber, @NumberOfEmployees, @ChairPersonId, @Description, @Revenue, @StockPrices, @DateFounded)

	select @Id = SCOPE_IDENTITY();
end