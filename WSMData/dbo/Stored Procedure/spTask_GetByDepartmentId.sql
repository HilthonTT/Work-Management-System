﻿CREATE PROCEDURE [dbo].[spTask_GetByDepartmentId]
	@DepartmentId int
AS
begin
	set nocount on;

	select [Id], [UserId], [DepartmentId], [Title], [Description], [DateDue], [PercentageDone], [IsDone], [DateCreated]
	from dbo.Task
	where DepartmentId = @DepartmentId
end