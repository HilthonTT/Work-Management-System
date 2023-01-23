CREATE PROCEDURE [dbo].[spUser_UpdateAge]
	@Id nvarchar(128),
	@Age int
AS
begin
	set nocount on;

	update dbo.[User] set
	Age = @Age
	
	where Id = @Id
end
