CREATE PROCEDURE [dbo].[spDepartment_Update]
	@Id int,
	@CompanyId int,
	@DepartmentName nvarchar(100),
	@Address nvarchar(500),
	@ChairPersonId nvarchar(128),
	@PhoneNumber nvarchar(50),
	@Description nvarchar(MAX),
	@CreatedDate datetime2,
	@Archived bit

AS
begin
	set nocount on;

	UPDATE dbo.Department set
	CompanyId = @CompanyId,
	DepartmentName = @DepartmentName,
	[Address] = @Address,
	ChairPersonId = @ChairPersonId,
	PhoneNumber = @PhoneNumber,
	[Description] = @Description,
	CreatedDate = @CreatedDate,
	Archived = @Archived

	WHERE Id = @Id
end
