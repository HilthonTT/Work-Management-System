CREATE PROCEDURE [dbo].[spDepartment_GetAll]
AS
begin
	set nocount on;

	select [Id], [CompanyId], [DepartmentName], [ChairPersonId], [Budget], [Description], [CreatedDate]
	from dbo.Department

	order by DepartmentName
end