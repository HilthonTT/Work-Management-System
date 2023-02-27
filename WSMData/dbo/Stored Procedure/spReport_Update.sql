CREATE PROCEDURE [dbo].[spReport_Update]
	@Id int,
	@Title nvarchar(50),
	@Description nvarchar(500),
	@Archived bit
AS
begin
	set nocount on;

	UPDATE dbo.Report set
	Title = @Title,
	[Description] = @Description,
	Archived = @Archived

	where Id = @Id;
end