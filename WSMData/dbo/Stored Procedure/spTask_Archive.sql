CREATE PROCEDURE [dbo].[spTask_Archive]
	@Id int,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Task set
	Archived = @Archived

	where Id = @Id
end