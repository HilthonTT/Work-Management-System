﻿CREATE PROCEDURE [dbo].[spReport_Insert]
	@Id int output,
	@TaskId int,
	@UserId nvarchar(256),
	@Title nvarchar(50),
	@Description nvarchar(500),
	@DateCreated datetime2
AS
begin
	set nocount on;

	insert into dbo.Report(TaskId, UserId, Title, [Description], DateCreated)
	values (@TaskId, @UserId, @Title, @Description, @DateCreated)

	select @Id = SCOPE_IDENTITY();
end