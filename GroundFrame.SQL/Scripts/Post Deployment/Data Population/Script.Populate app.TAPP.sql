/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Script\Post Deployment\Data Population\Script.Populate app.TAPP.sql
** Name:		Script.Populate app.TAPP.sql
** Desc:		Populates the internal applications
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

--Create API Key records if they don't already exist. Not ussing the [common].[Usp_GENERATE_APIKEY] Stored Proc as we need to create a standard key these apps

IF NOT EXISTS (SELECT 1 FROM [common].[TAPIKEY] WHERE [api_key] = 'dbappAPIKEY')
BEGIN
	INSERT INTO [common].[TAPIKEY]
	(
		[api_key]
	)
	VALUES
	(
		'dbappAPIKEY'
	);
END;

IF NOT EXISTS (SELECT 1 FROM [common].[TAPIKEY] WHERE [api_key] = 'testappAPIKEY')
BEGIN
	INSERT INTO [common].[TAPIKEY]
	(
		[api_key]
	)
	VALUES
	(
		'testappAPIKEY'
	);
END;

SET IDENTITY_INSERT [app].[TAPP] ON;

MERGE [app].[TAPP] AS T
USING
(
	SELECT
		[id],
		[name],
		[description],
		[api_key],
		[app_status_id],
		[app_type_id],
		[app_url],
		[owner_id],
		[createdby_id],
		[createdon],
		[modifiedby_id],
		[modifiedon]
	FROM 
	(
		VALUES
		(1, N'Database Application', N'This is the internal database application', N'dbappAPIKEY', 1, 2, NULL, 1, 1, @today, 1, @today),
		(2, N'Test Application', N'This is the application used for internal testing', N'testappAPIKEY', 1, 2, NULL, 2, 1, @today, 1, @today)
	)
	AS APPS(		
		[id],
		[name],
		[description],
		[api_key],
		[app_status_id],
		[app_type_id],
		[app_url],
		[owner_id],
		[createdby_id],
		[createdon],
		[modifiedby_id],
		[modifiedon]
	)
) AS S
ON T.[id] = S.[id]
WHEN MATCHED
THEN UPDATE
SET
	T.[name] = S.[name],
	T.[description] = S.[description],
	T.[api_key] = S.[api_key],
	T.[app_status_id] = S.[app_status_id],
	T.[app_type_id] = S.[app_type_id],
	T.[app_url] = S.[app_url],
	T.[owner_id] = S.[owner_id],
	T.[modifiedby_id] = S.[modifiedby_id],
	T.[modifiedon] = S.[modifiedon]
WHEN NOT MATCHED BY TARGET
THEN INSERT 
(
	[id],
	[name],
	[description],
	[api_key],
	[app_status_id],
	[app_type_id],
	[app_url],
	[owner_id],
	[createdby_id],
	[createdon],
	[modifiedby_id],
	[modifiedon]
)
VALUES 
(
	S.[id],
	S.[name],
	S.[description],
	S.[api_key],
	S.[app_status_id],
	S.[app_type_id],
	S.[app_url],
	S.[owner_id],
	S.[createdby_id],
	S.[createdon],
	S.[modifiedby_id],
	S.[modifiedon]
);

SET IDENTITY_INSERT [app].[TAPP] OFF;
GO
