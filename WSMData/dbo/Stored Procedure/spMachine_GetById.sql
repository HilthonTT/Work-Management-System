CREATE PROCEDURE [dbo].[spMachine_GetById]
	@Id int
AS
begin
	set nocount on;

	select *
	from dbo.Machine

	where Id = @Id
end