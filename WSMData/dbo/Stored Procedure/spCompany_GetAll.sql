CREATE PROCEDURE [dbo].[spCompany_GetAll]
AS
begin
	set nocount on;

	select [Id], [CompanyName], [PhoneNumber], [NumberOfEmployees], [ChairPersonId], [Description], [Revenue], [StockPrices], [DateFounded]
	from dbo.Company

	order by CompanyName
end