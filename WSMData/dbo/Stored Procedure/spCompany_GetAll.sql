CREATE PROCEDURE [dbo].[spCompany_GetAll]
AS
begin
	set nocount on;

	select [Id], [CompanyName], [PhoneNumber], [ChairPersonId], [Address], [Description], [DateFounded]
	from dbo.Company

	order by CompanyName
end