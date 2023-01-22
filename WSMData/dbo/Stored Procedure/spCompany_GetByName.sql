CREATE PROCEDURE [dbo].[spCompany_GetByName]
	@CompanyName nvarchar(100)


AS
begin
	set nocount on;

	select [Id], [CompanyName], [PhoneNumber], [NumberOfEmployees], [ChairPersonId], [Description], [Revenue], [StockPrices], [DateFounded] 
	from dbo.Company
	where CompanyName LIKE @CompanyName
end