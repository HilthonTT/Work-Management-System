CREATE PROCEDURE [dbo].[spUser_Delete]
	@Id nvarchar(128)

AS
begin
	set nocount on;

	delete from [dbo].[User]
	where Id = @Id
end