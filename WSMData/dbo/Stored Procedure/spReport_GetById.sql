CREATE PROCEDURE [dbo].[spReport_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [TaskId], [UserId], [Title], [Description], [DateCreated] 
	from dbo.Report

	where Id = @Id
end