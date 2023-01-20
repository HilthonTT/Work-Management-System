CREATE PROCEDURE [dbo].[spTask_Delete]
	@Id int
AS
begin
	set nocount on;

	delete from dbo.Task
	where Id = @Id
end