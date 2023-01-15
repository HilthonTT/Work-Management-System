CREATE PROCEDURE [dbo].[spJobTitle_Insert]
	@Id int output,
	@JobName nvarchar(100),
	@Description nvarchar(MAX),
	@DepartmentId int
AS
begin
	insert into dbo.JobTitle(JobName, [Description], DepartmentId)
	values (@JobName, @Description, @DepartmentId)

	select @Id = SCOPE_IDENTITY()
end