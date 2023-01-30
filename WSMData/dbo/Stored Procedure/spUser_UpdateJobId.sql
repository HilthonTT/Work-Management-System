CREATE PROCEDURE [dbo].[spUser_UpdateJobId]
	@Id nvarchar(128),
	@JobTitleId int
AS
begin
	set nocount on;

	UPDATE [dbo].[User] set 
	JobTitleId = @JobTitleId

	where Id = @Id
end