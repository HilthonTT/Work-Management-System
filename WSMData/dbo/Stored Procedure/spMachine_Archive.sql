CREATE PROCEDURE [dbo].[spMachine_Archive]
	@Id int,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Machine set
	Archived = @Archived

	where Id = @Id
end