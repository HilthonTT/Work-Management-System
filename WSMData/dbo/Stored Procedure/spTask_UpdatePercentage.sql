CREATE PROCEDURE [dbo].[spTask_UpdatePercentage]
	@Id int,
	@PercentageDone int,
	@IsDone bit

AS
begin
	set nocount on;

	update dbo.Task set
	PercentageDone = @PercentageDone,
	IsDone = @IsDone

	where Id = @Id
end