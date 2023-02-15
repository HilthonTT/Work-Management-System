CREATE PROCEDURE [dbo].[spCompany_Insert]
	@Id int output,
	@CompanyName nvarchar(100),
	@Address nvarchar(500),
	@PhoneNumber nvarchar(50),
	@ChairPersonId nvarchar(128),
	@Description nvarchar(255),
	@DateFounded datetime2
AS
begin
	set nocount on;
	
	insert into dbo.Company(CompanyName, [Address] ,PhoneNumber, ChairPersonId, [Description], DateFounded)
	values (@CompanyName, @PhoneNumber, @Address , @ChairPersonId, @Description, @DateFounded)

	select @Id = SCOPE_IDENTITY();
end