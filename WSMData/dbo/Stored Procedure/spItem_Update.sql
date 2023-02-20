CREATE PROCEDURE [dbo].[spItem_Update]
	@Id int,
	@ModelName nvarchar(128),
	@Description nvarchar(256),
	@Quantity int,
	@Price money,
	@EAN decimal,
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Item set
	ModelName = @ModelName,
	[Description] = @Description,
	Quantity = @Quantity,
	Price = @Price,
	EAN = @EAN,
	Archived = @Archived

	where Id = @Id;
end