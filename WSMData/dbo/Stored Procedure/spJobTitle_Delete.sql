CREATE PROCEDURE [dbo].[spJobTitle_Delete]
	@Id int

AS
begin
	set nocount on;

	delete from dbo.JobTitle
	where Id = @Id
end
