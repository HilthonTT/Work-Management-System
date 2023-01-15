CREATE PROCEDURE [dbo].[spUser_Insert]
	@Id nvarchar(128),
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@EmailAddress nvarchar(MAX),
	@PhoneNumber nvarchar(50),
	@Age int,
	@DepartmentId int,
	@JobTitleId int,
	@CreatedDate datetime2

AS
begin
	set nocount on;

	insert into [dbo].[User] (Id, FirstName, LastName, EmailAddress, PhoneNumber, Age, DepartmentId, JobTitleId, CreatedDate)
	values (@Id, @FirstName, @LastName, @EmailAddress, @PhoneNumber, @Age, @DepartmentId, @JobTitleId, @CreatedDate)
end