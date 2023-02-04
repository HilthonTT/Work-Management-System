CREATE PROCEDURE [dbo].[spDepartment_Update]
	@Id int,
	@CompanyId int,
	@DepartmentName nvarchar(100),
	@Address nvarchar(255),
	@ChairPersonId nvarchar(128),
	@PhoneNumber nvarchar(50),
	@Description nvarchar(MAX)

AS
begin
	set nocount on;

	UPDATE dbo.Department set
	CompanyId = @CompanyId,
	DepartmentName = @DepartmentName,
	[Address] = @Address,
	ChairPersonId = @ChairPersonId,
	PhoneNumber = @PhoneNumber,
	[Description] = @Description

	WHERE Id = @Id
end
