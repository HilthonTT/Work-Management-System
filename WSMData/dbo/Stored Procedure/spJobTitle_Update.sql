CREATE PROCEDURE [dbo].[spJobTitle_Update]
	@Id int,
	@JobName nvarchar(100),
	@Description nvarchar(MAX),
	@DepartmentId int

AS
begin
	set nocount on;

	UPDATE dbo.JobTitle set
	JobName = @JobName,
	[Description] = @Description,
	DepartmentId = @DepartmentId
end