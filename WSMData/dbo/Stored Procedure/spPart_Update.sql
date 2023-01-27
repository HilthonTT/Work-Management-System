CREATE PROCEDURE [dbo].[spPart_Update]
	@Id int,
	@PartName nvarchar(100),
	@ModelName nvarchar(255),
	@MachineId int,
	@DatePurchased datetime2
AS
begin
	set nocount on;

	update dbo.Part set
	PartName = @PartName,
	ModelName = @ModelName,
	MachineId = @MachineId,
	DatePurchased = @DatePurchased

	where Id = @Id
end