﻿CREATE PROCEDURE [dbo].[spPart_GetAll]
AS
begin
	set nocount on;

	select [Id], [PartName], [ModelName], [MachineId], [DatePurchased]
	from dbo.Part

	order by PartName
end