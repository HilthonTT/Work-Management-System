CREATE PROCEDURE [dbo].[spTask_Update]
	@Id int,
	@UserId nvarchar(128), 
	@DepartmentId int, 
	@Title nvarchar(50), 
	@Description nvarchar(MAX), 
	@DateDue datetime2, 
	@PercentageDone int, 
	@IsDone bit,
	@Archived bit
AS
begin
	set nocount on;

	update dbo.Task set
	UserId = @UserId,
	DepartmentId = @DepartmentId,
	Title = @Title,
	[Description] = @Description,
	DateDue = @DateDue,
	PercentageDone = @PercentageDone,
	IsDone = @IsDone,
	Archived = @Archived

	where Id = @Id;
end