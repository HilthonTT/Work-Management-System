CREATE PROCEDURE [dbo].[spDepartment_GetByName]
	@DepartmentName nvarchar(100)

AS
begin
	set nocount on;

	select *
	from dbo.Department
	WHERE DepartmentName = @DepartmentName
end
