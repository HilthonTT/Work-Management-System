CREATE PROCEDURE [dbo].[spPart_Archive]
	@Id int,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Part set
	Archived = @Archived

	where Id = @Id
end
