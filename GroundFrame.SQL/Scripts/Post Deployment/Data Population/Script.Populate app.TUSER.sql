/******************************
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
		[role_bitmap],
		[createdby_id],
		[createdon],
		[modifiedby_id],
		[modifiedon]
	FROM 
	(
		VALUES
		(1, N'DBA Account', N'Account', N'DBA', NULL, NULL, 1, 2, 7, 1, @today, 1, @today),
		(2, N'Test Editor Account', N'Editor Account', N'Test', N'test.editoraccount@groundframe.io', N'testuserAPIKEY', 1, 2, 3, 1, @today, 1, @today),
		(3, N'Test Standard Account', N'Standard Account', N'Test', N'test.standardaccount@groundframe.io', N'teststandarduserAPIKEY', 1, 2, 1, 1, @today, 1, @today),
		(4, N'Test Admin Account', N'Standard Admin', N'Test', N'test.adminaccount@groundframe.io', N'testadminuserAPIKEY', 1, 2, 7, 1, @today, 1, @today)
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
		[role_bitmap],
		[createdby_id],
		[createdon],
		[modifiedby_id],
		[modifiedon]
	)
) AS S
ON T.[id] = S.[id]
WHEN MATCHED
THEN UPDATE SET
		T.[username] = S.[username],
		T.[last_name] = S.[last_name],
		T.[first_name] = S.[first_name],
		T.[email] = S.[email],
		T.[api_key] = S.[api_key],
		T.[user_status_id] = S.[user_status_id],
		T.[user_type_id] = S.[user_type_id],
		T.[role_bitmap] = S.[role_bitmap],
		T.[modifiedon] = GETUTCDATE()
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
		[role_bitmap],
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
		S.[role_bitmap],
		S.[createdby_id],
		S.[createdon],
		S.[modifiedby_id],
		S.[modifiedon]
);

SET IDENTITY_INSERT [app].[TUSER] OFF;
GO
