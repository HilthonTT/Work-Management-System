CREATE PROCEDURE [dbo].[spUser_Update]
	@Id nvarchar(128),
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@EmailAddress nvarchar(MAX),
	@PhoneNumber nvarchar(50),
	@DateOfBirth datetime2,
	@DepartmentId int,
	@JobTitleId int,
	@CreatedDate datetime2
AS
begin
	set nocount on;

	update [dbo].[User] set
	FirstName = @FirstName,
	LastName = @LastName,
	EmailAddress = @EmailAddress,
	PhoneNumber = @PhoneNumber,
	DateOfBirth = @DateOfBirth,
	DepartmentId = @DepartmentId,
	JobTitleId = @JobTitleId,
	CreatedDate = @CreatedDate

	where Id = @Id
end