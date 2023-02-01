CREATE PROCEDURE [dbo].[spPart_Insert]
	@Id int output,
	@PartName nvarchar(100),
	@ModelName nvarchar(255),
	@MachineId int,
	@PurchasedPrice money,
	@DatePurchased datetime2
AS
begin
	set nocount on;

	insert dbo.Part(PartName, ModelName, MachineId, DatePurchased, PurchasedPrice)
	values (@PartName, @ModelName, @MachineId, @DatePurchased, @PurchasedPrice)

	select @Id = SCOPE_IDENTITY()
end