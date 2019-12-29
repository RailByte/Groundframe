/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Views\app\app.VUSER.sql
** Name:		app.VUSER
** Desc:		View containing user data join to relevant views.
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-12
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-19	TC			Initial Script creation
**
*******************************/

CREATE VIEW [app].[VUSER]
AS
SELECT
	U.[id],
	U.[username],
	U.[last_name],
	U.[first_name],
	U.[email],
	U.[api_key], --Auth0 API Key
	U.[user_status_id],
	[user_status_name] = US.[name],
	[user_status_description] = US.[description],
	U.[user_type_id],
	[user_type_name] = UT.[name],
	[user_type_description] = UT.[description],
	U.[datetime_last_loggedin],
	U.[createdby_id],
	[created_by_username] = CB.[username],
	U.[createdon],
	U.[modifiedby_id],
	[modifiedby_by_username] = MB.[username],
	U.[modifiedon]
FROM [app].[TUSER] AS U
INNER JOIN [app].[TUSER] AS CB ON U.[createdby_id] = CB.[id]
INNER JOIN [app].[TUSER] AS MB ON U.[modifiedby_id] = MB.[id]
INNER JOIN [app].[TUSERSTATUS] AS US ON U.[user_status_id] = US.[id]
INNER JOIN [app].[TUSERTYPE] AS UT ON U.[user_type_id] = UT.[id]