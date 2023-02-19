CREATE PROCEDURE [dbo].[spDepartment_Insert]
	@Id int output,
	@CompanyId int,
	@DepartmentName nvarchar(100),
	@Address nvarchar(500),
	@ChairPersonId nvarchar(128),
	@PhoneNumber nvarchar(50),
	@Description nvarchar(255),
	@CreatedDate datetime2,
	@Archived bit
AS
begin
	set nocount on;

	insert into dbo.Department(CompanyId, DepartmentName, [Address], ChairPersonId, PhoneNumber ,[Description], CreatedDate, Archived)
	values (@CompanyId, @DepartmentName, @Address, @ChairPersonId, @PhoneNumber, @Description, @CreatedDate, @Archived)

	select @Id = SCOPE_IDENTITY()
end
