CREATE PROCEDURE [dbo].[spDepartment_Update]
	@Id int,
	@CompanyId int,
	@DepartmentName nvarchar(100),
	@ChairPersonId nvarchar(128),
	@Budget money,
	@Description nvarchar(MAX)

AS
begin
	set nocount on;

	UPDATE dbo.Department set
	CompanyId = @CompanyId,
	DepartmentName = @DepartmentName,
	ChairPersonId = @ChairPersonId,
	Budget = @Budget,
	[Description] = @Description

	WHERE Id = @Id
end
