CREATE PROCEDURE [dbo].[spJobTitle_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [JobName], [Description], [DepartmentId], [Archived]
	from dbo.JobTitle

	where Id = @Id
end