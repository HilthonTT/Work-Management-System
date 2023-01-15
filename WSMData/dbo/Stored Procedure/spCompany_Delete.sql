CREATE PROCEDURE [dbo].[spCompany_Delete]
	@Id int

AS
begin
	set nocount on;

	delete from dbo.Company
	where Id = @Id
end