CREATE PROCEDURE [dbo].[spCompany_Archive]
	@Id int,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Company set
	Archived = @Archived

	where Id = @Id
end