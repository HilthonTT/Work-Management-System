CREATE PROCEDURE [dbo].[spPart_Delete]
	@Id int
AS
begin
	set nocount on;

	delete from dbo.Part

	where Id = @Id
end