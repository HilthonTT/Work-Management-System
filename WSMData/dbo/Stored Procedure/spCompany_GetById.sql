CREATE PROCEDURE [dbo].[spCompany_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [CompanyName], [Address], [PhoneNumber], [ChairPersonId], [Description], [DateFounded]
	from dbo.Company

	where Id = @Id
end
