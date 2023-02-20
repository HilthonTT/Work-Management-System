CREATE PROCEDURE [dbo].[spItem_Insert]
	@Id int output,
	@ModelName nvarchar(128),
	@Description nvarchar(256),
	@EAN decimal,
	@Quantity int,
	@Price money,
	@Archived bit
AS
begin
	set nocount on;

	insert into dbo.Item(ModelName, [Description], Quantity, Price, EAN, Archived)
	values (@ModelName, @Description, @Quantity, @Price, @EAN, @Archived)

	select @Id = SCOPE_IDENTITY();
end