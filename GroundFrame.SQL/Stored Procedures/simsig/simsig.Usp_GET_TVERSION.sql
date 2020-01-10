/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.USp_GET_TVERSION.sql
** Name:		simsig.USp_GET_TVERSION
** Desc:		Stored procedure to get a SimSig version (or all versions). Development versions are limited to editors
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
CREATE PROCEDURE [simsig].[USp_GET_TVERSION]
	@id SMALLINT = 0,
	@debug BIT = 0,
	@debug_session_id UNIQUEIDENTIFIER = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	--Variables
	DECLARE @debug_message NVARCHAR(2048);
	DECLARE @error_message NVARCHAR(2048);
	DECLARE @testdata_id UNIQUEIDENTIFIER = CONVERT(UNIQUEIDENTIFIER,SESSION_CONTEXT(N'testdata_id'))

	--Set the @debug_session_id if in debug mode and @debug_session_id <NULL>
	IF @debug = 1 AND @debug_session_id IS NULL
	BEGIN
		SET @debug_session_id = NEWID();
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_GET_TVERSION] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @id = ' + CONVERT(NVARCHAR(16),@id) + '.';
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

	--Set Default Parameters

	IF @id IS NULL SET @id = 0;

	--Get User Role Bit Map

	DECLARE @user_role_bitmap BIGINT = (SELECT [app].[Fn_GET_USERROLE]());

	--Paramter checks

	
	BEGIN TRY
		SELECT
			S.[id],
			S.[name],
			S.[description],
			S.[simsig_version_from],
			S.[simsig_version_to],
			S.[version_status_id]
		FROM [simsig].[VVERSION] AS S
		WHERE
			(S.id = @id OR @id = 0) --Not best practise but given the small number of records shouldn't be a problem.
			AND (S.[testdata_id] = @testdata_id OR @testdata_id IS NULL) --Used to ensure if the connection is a test only records effected by the test are returned
			AND (S.version_status_id = 1 OR (S.version_status_id = 2 AND @user_role_bitmap & 6 > 0)) --Limit development versions to editors and admins only
	END TRY
	BEGIN CATCH
		IF @debug = 1
		BEGIN
			SET @debug_message = 'An error has occured trying to retrieve [app].[TVERSION] record [id] = ' + CAST(@id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @error_message = 'An error has occurred retrieving version [id] = ' + CAST(@id AS NVARCHAR(16)) + ':- ' + ERROR_MESSAGE();
		THROW 50000, @error_message, 1;
	END CATCH

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_GET_TVERSION] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END