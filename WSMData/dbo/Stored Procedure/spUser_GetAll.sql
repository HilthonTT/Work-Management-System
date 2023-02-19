CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
begin
	set nocount on;

	select [Id], [FirstName], [LastName], [EmailAddress], [PhoneNumber], [DateOfBirth], [DepartmentId], [JobTitleId], [CreatedDate]
	from [dbo].[User]
	order by FirstName
end