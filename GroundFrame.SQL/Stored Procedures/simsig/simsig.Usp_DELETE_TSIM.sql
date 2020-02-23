/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.USp_GET_TSIM.sql
** Name:		simsig.USp_GET_TSIM
** Desc:		Stored procedure to delete a SimSig simulation
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

CREATE PROCEDURE [simsig].[Usp_DELETE_TSIM]
	@id SMALLINT,
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
		SET @debug_message = 'Executing [simsig].[Usp_DELETE_TSIM] started.';
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

	--Check user has editor or admin role

	IF ISNULL((SELECT [role_bitmap] FROM [app].[TUSER] WHERE [id] = @app_user_id),0) & 6 = 0
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The user isn''t in the editor or admin role. Check their permissions';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
		
		THROW 50000, 'The user does not have permission to perform this action.', 1;
	END;

	BEGIN TRAN TRAN_DELETESIM

	BEGIN TRY
		DELETE FROM [simsig].[TLOCATIONNODE]
		WHERE
			[sim_id] = @id;

		DELETE FROM [simsig].[TLOCATION]
		WHERE
			[sim_id] = @id;

		DELETE FROM [simsig].[TSIMERA]
		WHERE
			[sim_id] = @id;

		DELETE FROM [simsig].[TSIMERA]
		WHERE
			[sim_id] = @id;

		DELETE FROM [simsig].[TSIM]
		WHERE
			[id] = @id;

		COMMIT TRAN TRAN_DELETESIM;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_DELETESIM;

		IF @debug = 1
		BEGIN
			SET @debug_message = 'An error has occured trying to delete [app].[TSIM] record [id] = ' + CAST(@id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @error_message = 'An error has occurred deleting simulation [id] = ' + CAST(@id AS NVARCHAR(16)) + ':- ' + ERROR_MESSAGE();
		THROW 50000, @error_message, 1;
	END CATCH

	SET NOCOUNT OFF;
END