CREATE PROCEDURE [dbo].[spJobTitle_Insert]
	@Id int output,
	@JobName nvarchar(100),
	@Description nvarchar(255),
	@DepartmentId int
AS
begin
	set nocount on;

	insert into dbo.JobTitle(JobName, [Description], DepartmentId)
	values (@JobName, @Description, @DepartmentId)

	select @Id = SCOPE_IDENTITY()
end