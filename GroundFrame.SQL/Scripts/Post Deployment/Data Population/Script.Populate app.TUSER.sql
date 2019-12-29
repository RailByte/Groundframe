﻿/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Script\Post Deployment\Data Population\Script.Populate app.TUSER.sql
** Name:		Script.Populate app.TUSER.sql
** Desc:		Populates the internal system account and external sys.admin account
** Unit Test:	N/a
** Auth:		Tim Caceres
** Date:		2019-12-13
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-12	TC			Initial Script creation
**
*******************************/

DECLARE @today DATETIMEOFFSET = SYSDATETIMEOFFSET();

SET IDENTITY_INSERT [app].[TUSER] ON;

MERGE [app].[TUSER] AS T
USING
(
	SELECT
		[id],
		[username],
		[last_name],
		[first_name],
		[email],
		[api_key],
		[user_status_id],
		[user_type_id],
		[createdby_id],
		[createdon],
		[modifiedby_id],
		[modifiedon]
	FROM 
	(
		VALUES
		(1, N'DBA Account', N'Account', N'DBA', NULL, NULL, 1, 2, 1, @today, 1, @today),
		(2, N'Test Account', N'Account', N'Test', N'sys.admin@groundframe.co.uk', N'testuserAPIKEY', 1, 2, 1, @today, 1, @today)
	)
	AS USERS(		
		[id],
		[username],
		[last_name],
		[first_name],
		[email],
		[api_key],
		[user_status_id],
		[user_type_id],
		[createdby_id],
		[createdon],
		[modifiedby_id],
		[modifiedon]
	)
) AS S
ON T.[id] = S.[id]
WHEN NOT MATCHED BY TARGET
THEN INSERT 
(
		[id],
		[username],
		[last_name],
		[first_name],
		[email],
		[api_key],
		[user_status_id],
		[user_type_id],
		[createdby_id],
		[createdon],
		[modifiedby_id],
		[modifiedon]
)
VALUES 
(
		S.[id],
		S.[username],
		S.[last_name],
		S.[first_name],
		S.[email],
		S.[api_key],
		S.[user_status_id],
		S.[user_type_id],
		S.[createdby_id],
		S.[createdon],
		S.[modifiedby_id],
		S.[modifiedon]
);

SET IDENTITY_INSERT [app].[TUSER] OFF;
GO
