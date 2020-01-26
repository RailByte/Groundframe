/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.Usp_GET_USER_SETTINGS.sql
** Name:		simsig.Usp_GET_USER_SETTINGS
** Desc:		Stored procedure to get a users settings
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-23
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-23	TC			Initial Script creation
**
*******************************/
CREATE PROCEDURE [app].[Usp_GET_USER_SETTINGS]
	@debug BIT = 0,
	@debug_session_id UNIQUEIDENTIFIER = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	--Variables
	DECLARE @debug_message NVARCHAR(2048);
	DECLARE @error_message NVARCHAR(2048);

	--Set the @debug_session_id if in debug mode and @debug_session_id <NULL>
	IF @debug = 1 AND @debug_session_id IS NULL
	BEGIN
		SET @debug_session_id = NEWID();
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_GET_USER_SETTINGS] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: No parameters required';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	--Variables--
	DECLARE @logged_in BIT = ISNULL(CONVERT(BIT,SESSION_CONTEXT(N'logged_in')),0); 
	DECLARE @app_user_id INT = ISNULL(CONVERT(INT,SESSION_CONTEXT(N'app_user')),0); 

	--Check user is logged in
	IF @logged_in = 0
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The user isn''t logged in. Check that [common].[Usp_SET_SESSIONCONTEXT] has fired when the connection to the database was made';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
		
		THROW 50000, 'The user is not logged in.', 1;
	END;

	SELECT
		S.[key],
		S.[description],
		S.[data_type],
		[value] = ISNULL(NULLIF(US.[value],''), S.[default_value])
	FROM [app].[TSETTING] AS S
	LEFT JOIN [app].[TUSERSETTING] AS US ON S.[id] = US.[setting_id] AND US.[user_id] = @app_user_id
	ORDER BY 
		[key] ASC;

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_GET_USER_SETTINGS] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END
END
GO