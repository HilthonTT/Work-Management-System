CREATE PROCEDURE [dbo].[spUser_GetByName]
	@FirstName nvarchar(100),
	@LastName nvarchar(100)

AS
begin
	set nocount on;

	select *
	from [dbo].[User]
	where FirstName LIKE @FirstName and LastName LIKE @LastName
end