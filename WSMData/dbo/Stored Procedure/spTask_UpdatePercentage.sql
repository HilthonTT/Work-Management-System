CREATE PROCEDURE [dbo].[spTask_UpdatePercentage]
	@Id int,
	@PercentageDone int

AS
begin
	set nocount on;

	update dbo.Task
	set PercentageDone = @PercentageDone

	where Id = @Id
end