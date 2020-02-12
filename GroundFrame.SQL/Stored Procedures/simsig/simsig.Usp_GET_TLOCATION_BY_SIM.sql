/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.Usp_GET_TLOCATION_BY_SIM.sql
** Name:		simsig.Usp_GET_TLOCATION_BY_SIM
** Desc:		Stored procedure to get locations for the specific simulations.
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
CREATE PROCEDURE [simsig].[Usp_GET_TLOCATION_BY_SIM]
	@sim_id SMALLINT,
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
		SET @debug_message = 'Executing [simsig].[Usp_GET_TLOCATION_BY_SIM] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @sim_id = ' + CONVERT(NVARCHAR(16),@sim_id) + '.';
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

	BEGIN TRY
		SELECT
			L.[id],
			L.[sim_id],
			L.[sim_name],
			L.[name],
			L.[tiploc],
			L.[simsig_code],
			L.[simsig_entry_point],
			L.[location_type_id],
			L.[createdby_id],
			L.[created_by_username],
			L.[createdon],
			L.[created_by_app],
			L.[modifiedby_id],
			L.[modified_by_username],
			L.[modifiedon],
			L.[modified_by_app]
		FROM [simsig].[VLOCATION] AS L
		WHERE
			L.sim_id = @sim_id 
			AND (L.[testdata_id] = @testdata_id OR @testdata_id IS NULL) --Used to ensure if the connection is a test only records effected by the test are returned
	END TRY
	BEGIN CATCH
		IF @debug = 1
		BEGIN
			SET @debug_message = 'An error has occured trying to retrieve [simsig].[TLOCATION_BY_SIM] records for simualtion [sim_id] = ' + CAST(@sim_id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @error_message = 'An error has occurred retrieving locations for simulation [id] = ' + CAST(@sim_id AS NVARCHAR(16)) + ':- ' + ERROR_MESSAGE();
		THROW 50000, @error_message, 1;
	END CATCH

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_GET_TLOCATION_BY_SIM] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END