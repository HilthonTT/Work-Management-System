CREATE PROCEDURE [dbo].[spDepartment_Archive]
	@Id int,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Department set
	Archived = @Archived

	where Id = @Id
end
