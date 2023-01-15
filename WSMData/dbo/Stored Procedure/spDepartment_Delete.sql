CREATE PROCEDURE [dbo].[spDepartment_Delete]
	@Id int

AS
begin
	set nocount on;

	delete from dbo.Department
	WHERE Id = @Id
end