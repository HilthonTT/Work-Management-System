CREATE PROCEDURE [dbo].[spPart_Insert]
	@Id int output,
	@PartName nvarchar(100),
	@ModelName nvarchar(255),
	@MachineId int,
	@DatePurchased datetime2
AS
begin
	set nocount on;

	insert dbo.Part(PartName, ModelName, MachineId, DatePurchased)
	values (@PartName, @ModelName, @MachineId, @DatePurchased)

	select @Id = SCOPE_IDENTITY()
end