CREATE PROCEDURE [dbo].[spTask_GetAll]
AS
begin
	select [Id], [UserId], [DepartmentId], [Title], [Description], [DateDue], [PercentageDone], [IsDone], [DateCreated]
	from dbo.Task
end