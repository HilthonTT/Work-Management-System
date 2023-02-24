CREATE PROCEDURE [dbo].[spTask_Insert]
	@Id int output,
	@UserId nvarchar(128),
	@DepartmentId int,
	@Title nvarchar(50),
	@Description nvarchar(500),
	@DateDue datetime2,
	@PercentageDone int,
	@IsDone bit,
	@DateCreated datetime2,
	@Archived bit
AS
begin
	set nocount on;

	insert into dbo.Task (UserId, DepartmentId, Title, [Description], DateDue, PercentageDone, IsDone, DateCreated, Archived)
	values (@UserId, @DepartmentId, @Title, @Description, @DateDue, @PercentageDone, @IsDone, @DateCreated, @Archived)

	select @Id = SCOPE_IDENTITY()
end