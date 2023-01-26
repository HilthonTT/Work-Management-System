CREATE PROCEDURE [dbo].[spDepartment_Insert]
	@Id int output,
	@CompanyId int,
	@DepartmentName nvarchar(100),
	@ChairPersonId nvarchar(128),
	@Budget money,
	@Description nvarchar(255),
	@CreatedDate datetime2
AS
begin
	set nocount on;

	insert into dbo.Department(CompanyId, DepartmentName, ChairPersonId, Budget, CreatedDate)
	values (@CompanyId, @DepartmentName, @ChairPersonId, @Budget, @CreatedDate)

	select @Id = SCOPE_IDENTITY()
end
