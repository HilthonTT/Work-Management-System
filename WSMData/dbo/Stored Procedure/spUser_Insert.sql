﻿CREATE PROCEDURE [dbo].[spUser_Insert]
	@Id nvarchar(128),
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@EmailAddress nvarchar(255),
	@PhoneNumber nvarchar(50),
	@DateOfBirth datetime2,
	@DepartmentId int,
	@JobTitleId int,
	@CreatedDate datetime2

AS
begin
	set nocount on;

	insert into [dbo].[User] (Id, FirstName, LastName, EmailAddress, PhoneNumber, DateOfBirth, DepartmentId, JobTitleId, CreatedDate)
	values (@Id, @FirstName, @LastName, @EmailAddress, @PhoneNumber, @DateOfBirth, @DepartmentId, @JobTitleId, @CreatedDate)
end