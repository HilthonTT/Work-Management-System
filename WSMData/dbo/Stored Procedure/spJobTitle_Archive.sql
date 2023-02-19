CREATE PROCEDURE [dbo].[spJobTitle_Archive]
	@Id int,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.JobTitle set
	Archived = @Archived

	where Id = @Id
end