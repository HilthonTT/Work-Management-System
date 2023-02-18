﻿CREATE PROCEDURE [dbo].[spTask_GetById]
	@Id int
AS
begin
	select [Id], [UserId], [DepartmentId], [Title], [Description], [DateDue], [PercentageDone], [IsDone], [DateCreated]
	from dbo.Task

	where Id = @Id
end