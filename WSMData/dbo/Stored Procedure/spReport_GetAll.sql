CREATE PROCEDURE [dbo].[spReport_GetAll]
AS
begin
	set nocount on;

	select [Id], [TaskId], [UserId], [Title], [Description], [DateCreated]
	from dbo.Report

	Order BY Title;
end