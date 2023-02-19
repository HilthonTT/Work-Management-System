﻿CREATE PROCEDURE [dbo].[spJobTitle_GetAll]
AS
begin
	set nocount on;

	select [Id], [JobName], [Description], [DepartmentId], [Archived]
	from dbo.JobTitle

	order by JobName
end