CREATE PROCEDURE [dbo].[spMachine_Delete]
	@Id int
AS
begin
	set nocount on;

	delete from dbo.Machine

	where Id = @Id
end
