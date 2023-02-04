CREATE PROCEDURE [dbo].[spDepartment_GetByName]
	@DepartmentName nvarchar(100)

AS
begin
	set nocount on;

	select [Id], [CompanyId], [DepartmentName], [Address], [PhoneNumber],[ChairPersonId], [Description], [CreatedDate]
	from dbo.Department
	WHERE DepartmentName LIKE @DepartmentName
end
