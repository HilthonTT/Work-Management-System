CREATE PROCEDURE [dbo].[spTask_Insert]
	@Id int output,
	@UserId nvarchar(128),
	@DepartmentId int,
	@Title nvarchar(50),
	@Description nvarchar(MAX),
	@DateDue datetime2,
	@PercentageDone int,
	@IsDone bit
AS
begin
	set nocount on;

	insert into dbo.Task (UserId, DepartmentId, Title, DateDue, PercentageDone, IsDone)
	values (@UserId, @DepartmentId, @Title, @DateDue, @PercentageDone, @IsDone)

	select @Id = SCOPE_IDENTITY()
end