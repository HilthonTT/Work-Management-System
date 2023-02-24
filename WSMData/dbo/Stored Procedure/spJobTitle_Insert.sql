CREATE PROCEDURE [dbo].[spJobTitle_Insert]
	@Id int output,
	@JobName nvarchar(100),
	@Description nvarchar(500),
	@DepartmentId int,
	@Archived bit
AS
begin
	set nocount on;

	insert into dbo.JobTitle(JobName, [Description], DepartmentId, Archived)
	values (@JobName, @Description, @DepartmentId, @Archived)

	select @Id = SCOPE_IDENTITY()
end