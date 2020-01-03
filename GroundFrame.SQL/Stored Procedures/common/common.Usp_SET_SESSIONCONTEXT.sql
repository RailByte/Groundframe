/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\common\common.Usp_SET_SESSIONCONTEXT.sql
** Name:		common.Usp_SET_SESSIONCONTEXT
** Desc:		Stored procedure to set the session context information about the user logged in
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-12
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-12	TC			Initial Script creation
**
*******************************/
CREATE PROCEDURE [common].[Usp_SET_SESSIONCONTEXT]
	@app_api_key NVARCHAR(16),
	@app_user_api_key NVARCHAR(48)
AS
BEGIN
	SET NOCOUNT ON

	--Get a application and user id's
	DECLARE @app_id INT = ISNULL((SELECT [id] FROM [app].[TAPP] WHERE [api_key] = @app_api_key AND [app_status_id] = 1),0);
	DECLARE @app_user_id INT = ISNULL((SELECT [id] FROM [app].[TUSER] WHERE [api_key] = @app_user_api_key AND [user_status_id] = 1),0);

	--Check the application and the user has access to the system
	IF @app_id = 0
	BEGIN;
		THROW 50000, 'Error executing [common].[Usp_SET_SESSIONCONTEXT]: - The application isn''t a valid application or has been disabled', 1;
	END;

	IF @app_user_id = 0
	BEGIN;
		THROW 50000, 'Error executing [common].[Usp_SET_SESSIONCONTEXT]: - The user isn''t a valid user or their account has been disabled', 1;
	END;

	EXEC sys.sp_set_session_context @key = N'application', @value = @app_id, @read_only = 0; 
	EXEC sys.sp_set_session_context @key = N'app_user', @value = @app_user_id, @read_only = 0; 
	EXEC sys.sp_set_session_context @key = N'logged_in', @value = 1, @read_only = 0; 
END