﻿CREATE PROCEDURE [dbo].[spMachine_GetAll]
AS
begin
	set nocount on;

	select [Id], [MachineName], [ModelName], [EuropeanArticleNumber], [PurchasedPrice], [DatePurchased] 
	from dbo.Machine

	order by MachineName
end