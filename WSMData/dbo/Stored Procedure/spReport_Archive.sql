CREATE PROCEDURE [dbo].[spReport_Archive]
	@Id int,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Report set
	Archived = @Archived

	where Id = @Id
end