CREATE PROCEDURE [dbo].[spDepartment_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [CompanyId], [DepartmentName], [Address], [ChairPersonId], [PhoneNumber], [Description], [CreatedDate], [Archived]
	from dbo.Department

	where Id = @Id
end