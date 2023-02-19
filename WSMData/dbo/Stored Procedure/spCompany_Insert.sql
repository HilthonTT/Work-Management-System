CREATE PROCEDURE [dbo].[spCompany_Insert]
	@Id int output,
	@CompanyName nvarchar(100),
	@Address nvarchar(500),
	@PhoneNumber nvarchar(50),
	@ChairPersonId nvarchar(128),
	@Description nvarchar(255),
	@DateFounded datetime2,
	@Archived bit
AS
begin
	set nocount on;
	
	insert into dbo.Company(CompanyName, [Address] ,PhoneNumber, ChairPersonId, [Description], DateFounded, Archived)
	values (@CompanyName, @PhoneNumber, @Address , @ChairPersonId, @Description, @DateFounded, @Archived)

	select @Id = SCOPE_IDENTITY();
end