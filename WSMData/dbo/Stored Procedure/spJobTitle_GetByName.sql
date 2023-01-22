CREATE PROCEDURE [dbo].[spJobTitle_GetByName]
	@JobName nvarchar(100)

AS
begin
	set nocount on;

	select [Id], [JobName], [Description], [DepartmentId]
	from dbo.JobTitle
	where JobName LIKE @JobName
end