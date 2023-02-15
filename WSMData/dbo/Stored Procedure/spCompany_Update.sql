CREATE PROCEDURE [dbo].[spCompany_Update]
	@Id int,
	@CompanyName nvarchar(100),
	@Address nvarchar(500),
	@PhoneNumber nvarchar(50),
	@ChairPersonId nvarchar(128),
	@Description nvarchar(MAX),
	@DateFounded datetime2
	

AS
begin
	set nocount on;
	
	UPDATE dbo.Company set 
	CompanyName = @CompanyName,
	[Address] = @Address,
	PhoneNumber = @PhoneNumber,
	ChairPersonId = @ChairPersonId,
	[Description] = @Description,
	DateFounded = @DateFounded

	WHERE Id = @Id
end
