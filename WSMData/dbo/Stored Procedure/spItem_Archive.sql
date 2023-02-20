CREATE PROCEDURE [dbo].[spItem_Archive]
	@Id int,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Item set
	Archived = @Archived

	where Id = @Id
end