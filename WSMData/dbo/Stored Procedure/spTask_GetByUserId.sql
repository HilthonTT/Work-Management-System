CREATE PROCEDURE [dbo].[spTask_GetByUserId]
	@UserId nvarchar(128)

AS
begin
	set nocount on;

	select [Id], [UserId], [DepartmentId], [Title], [Description], [DateDue], [PercentageDone], [IsDone], [DateCreated]
	from dbo.Task
	where UserId = @UserId
end