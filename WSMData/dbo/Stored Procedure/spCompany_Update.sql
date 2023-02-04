CREATE PROCEDURE [dbo].[spCompany_Update]
	@Id int,
	@CompanyName nvarchar(100),
	@PhoneNumber nvarchar(50),
	@NumberOfEmployees int,
	@ChairPersonId nvarchar(128),
	@Description nvarchar(MAX),
	@Address nvarchar(255)

AS
begin
	set nocount on;
	
	UPDATE dbo.Company set 
	CompanyName = @CompanyName,
	PhoneNumber = @PhoneNumber,
	ChairPersonId = @ChairPersonId,
	[Description] = @Description,
	[Address] = @Address

	WHERE Id = @Id
end
