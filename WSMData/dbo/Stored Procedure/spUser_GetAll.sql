CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
begin
	set nocount on;

	select [Id], [FirstName], [LastName], [EmailAddress], [PhoneNumber], [Age], [DepartmentId], [JobTitleId], [CreatedDate] 
	from [dbo].[User]
	order by FirstName
end