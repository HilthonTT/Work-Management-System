CREATE PROCEDURE [dbo].[spTask_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [UserId], [DepartmentId], [Title], [Description], [DateDue], [PercentageDone], [IsDone], [DateCreated]
	from dbo.Task

	where Id = @Id
end